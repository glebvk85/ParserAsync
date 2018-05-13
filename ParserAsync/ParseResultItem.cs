using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAsync
{
    class ParseResultItem
    {
        public string Url { get; }
        public int Count { get; }
        public int Number { get; }

        public ParseResultItem(string url, int count, int number)
        {
            Url = url;
            Count = count;
            Number = number;
        }

        public override string ToString()
        {
            return $"{Url};{Count}";
        }
    }
}
