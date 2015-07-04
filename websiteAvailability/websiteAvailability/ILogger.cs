using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace websiteAvailability
{
    public enum LogType
    {
        Console,
        TextFile,
        XML,
        Json
    };

    public interface ILogger
    {
        void Log(LogType type, string template, params object[] args);
    }

    public class DefaultLogger : ILogger
    {
        public void Log(LogType type, string template, params object[] args)
        {
            Console.WriteLine(template, args);
        }
    }

    public class ExtendedLogger : ILogger
    {
        // TODO add writing to file

        public void Log(LogType type, string template, params object[] args)
        {
            Console.WriteLine("{0}: {1}", type, String.Format(template, args));
        }
    }
}
