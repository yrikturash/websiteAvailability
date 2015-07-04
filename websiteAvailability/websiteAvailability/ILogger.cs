using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
        private static int pingNum = 0;
        public void Log(LogType type, string template, params object[] args)
        {
            pingNum++;
            switch (type)
            {
                case LogType.Console:
                    Console.WriteLine(template, args);
                    break;
                case LogType.TextFile:
                    LogTextFile("applog", template, args);
                    break;
                case LogType.XML:
                    LogXml("applog", template, args);
                    break;
                default:
                    Console.WriteLine(template, args);
                    break;
            }
            Console.WriteLine("{0}: {1}", type, String.Format(template, args));
        }

        public void LogTextFile(string fileName, string template, params object[] args)
        {
            fileName += ".txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(template, args);
            }
        }

        public void LogXml(string fileName, string template, params object[] args)
        {
            fileName += ".xml";
            if (File.Exists(fileName))
            {
                try
                {
                    XDocument doc = XDocument.Load(fileName);
                    XElement log = doc.Element("Log");
                    log.Add(new XElement("PingTry" + pingNum, string.Format(template, args)));
                    doc.Save(fileName);

                }
                catch (NullReferenceException ex)
                {
                    File.Delete(fileName);
                }
            }
            else
            {
                using (XmlWriter writer = XmlWriter.Create(fileName))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Log");

                    writer.WriteElementString("PingTry" + pingNum, string.Format(template, args));

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            
        }
    }
}
