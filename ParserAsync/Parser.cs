using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NLog;

namespace ParserAsync
{
    class Parser
    {
        private readonly DataCollector dataCollector = new DataCollector();
        private readonly Logger logger = LogManager.GetLogger("default");

        private async Task CollectCountUrlsFromPage(string url, int number)
        {
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
                    logger.Error(x.Message);
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

        public async void ParsePages(string[] urls)
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
