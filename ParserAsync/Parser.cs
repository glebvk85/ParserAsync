using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NLog;
using System.Threading;

namespace ParserAsync
{
    class Parser
    {
        private readonly DataCollector dataCollector = new DataCollector();
        private readonly Logger logger = LogManager.GetLogger("default");
        private readonly string[] urls;
        private int countTaskComplete = 0;

        public Parser(string[] urls)
        {
            this.urls = urls;
        }

        private async Task CollectCountUrlsFromPage(string url, int number)
        {
            Interlocked.Increment(ref countTaskComplete);
            logger.Info($"Complete {countTaskComplete} of {urls.Length}");
            int count = await Parse(url);
            dataCollector.Add(url, count, number);
        }

        public async Task<int> Parse(string url)
        {
            logger.Info($"Start parse {url}");
            int count = 0;
            return await Task.Run(() =>
            {
                var hw = new HtmlWeb();
                HtmlDocument doc;
                try
                {
                    doc = hw.Load(url);
                }
                catch (Exception x)
                {
                    logger.Error($"{url}: {x.Message}");
                    return 0;
                }
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    count++;
                }
                logger.Info($"Finish parse {url}");
                return count;
            });
        }

        public async void ParsePages()
        {
            var tasks = new Task[urls.Length];
            for (int i = 0; i < urls.Length; i++)
            {
                tasks[i] = CollectCountUrlsFromPage(urls[i], i);
            }
            await Task.WhenAll(tasks);
            logger.Info("Complete!");
            Console.WriteLine("Complete!");
        }
    }
}
