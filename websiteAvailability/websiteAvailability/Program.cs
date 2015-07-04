using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace websiteAvailability
{
    class Program
    {
        static void Main(string[] args)
        {

            SimpleWebChecker webChecker = new SimpleWebChecker();
            var checker = Ioc.Get<IWebChecker>();
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    checker.PingHost("google.com");
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
