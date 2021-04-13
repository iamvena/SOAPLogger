using SOAPLogger.Inspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SOAPLogger
{
    /// <summary>
    /// Gets the LastMessage/request soap message and log to file
    /// </summary>
    public class MessageInspectorLogger
    {
        public void Log()
        {
            var debugMessage = DebugMessageInspector.lastMessage;

            var ms = new MemoryStream();

            XmlWriter writer = XmlWriter.Create(ms);

            debugMessage.WriteMessage(writer);

            writer.Flush();

            ms.Position = 0;

            var xmlDoc = new XmlDocument();

            xmlDoc.PreserveWhitespace = true;

            xmlDoc.Load(ms);

            File.WriteAllText(@"C:/Users/sudar/source/repos/ConsoleApp5/ConsoleApp5/Models/vena.log", xmlDoc.InnerXml);
        }
    }
}
