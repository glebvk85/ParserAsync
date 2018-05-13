using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ParserAsync
{
    class DataCollector
    {
        private readonly object locker = new object();
        private const string pathFileResult = "output.txt";

        private readonly List<ParseResultItem> Urls = new List<ParseResultItem>();

        public void Add(string url, int count, int number)
        {
            lock (locker)
            {
                Urls.Add(new ParseResultItem(url, count, number));
                Urls.OrderBy(e => e.Number);
                File.WriteAllText(pathFileResult, string.Join(Environment.NewLine, Urls), Encoding.UTF8);
            }
        }
    }
}
