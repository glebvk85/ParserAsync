using System;

namespace ParserAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(args);
            parser.ParsePages();
            Console.WriteLine("Press Enter to exit program...");
            Console.ReadLine();
        }


    }
}
