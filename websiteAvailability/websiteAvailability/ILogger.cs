using System;
using System.Collections.Generic;
using System.IO;
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
        public void Log(LogType type, string template, params object[] args)
        {
            switch (type)
            {
                case LogType.Console:
                    Console.WriteLine(template, args);
                    break;
                case LogType.TextFile:
                    LogTextFile("applog.txt", template, args);
                    break;
                default:
                    Console.WriteLine(template, args);
                    break;
            }
            Console.WriteLine("{0}: {1}", type, String.Format(template, args));
        }

        public void LogTextFile(string fileName, string template, params object[] args)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(template, args);
            }
        }
    }
}
