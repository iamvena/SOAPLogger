using SOAPLogger.Inspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
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
        public void Log(Message lastMessage)
        {
            var ms = new MemoryStream();

            XmlWriter writer = XmlWriter.Create(ms);

            lastMessage.WriteMessage(writer);

            writer.Flush();

            ms.Position = 0;

            var xmlDoc = new XmlDocument();

            xmlDoc.PreserveWhitespace = true;

            xmlDoc.Load(ms);

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xmlDoc.log");

            File.WriteAllText(filePath, xmlDoc.InnerXml);
        }
    }
}
