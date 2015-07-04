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
            webChecker.PingHost("112.152.156.254");
            Console.ReadKey();
        }
    }
}
