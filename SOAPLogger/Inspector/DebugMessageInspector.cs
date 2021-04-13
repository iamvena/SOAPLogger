using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace SOAPLogger.Inspector
{
    public class DebugMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Enables inspection or modification of a message after a reply message is 
        /// received but prior to passing it back to the client application
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //var result = Intercept(reply);

            //MessageBuffer buffer = message.CreateBufferedCopy(Int32.MaxValue);

            //lastMessage = buffer.CreateMessage();

            //XmlDictionaryReader rv = buffer.CreateMessage().GetReaderAtBodyContents();

            //buffer.Close();
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is
        /// sent to a service
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var result = Intercept(request);

            return result;
        }

        public static Message newMessage = null;

        public static Message lastMessage = null;

        private Message Intercept(Message message)
        {
            MessageBuffer buffer = message.CreateBufferedCopy(Int32.MaxValue);

            Message requestCopy = buffer.CreateMessage();

            lastMessage = requestCopy;

            //call logger here
            var messageLog = new MessageInspectorLogger();

            messageLog.Log();

            var m = buffer.CreateMessage();

            buffer.Close();

            //read the message into an XmlDocument as then you can work with the contents
            //message is a forward reading class only.
            //var ms = new MemoryStream();

            //XmlWriter writer = XmlWriter.Create(ms);

            //copiedMessage.WriteMessage(writer);

            //writer.Flush();

            //ms.Position = 0;

            //var xmlDoc = new XmlDocument();

            //xmlDoc.PreserveWhitespace = true;

            //xmlDoc.Load(ms);

            //File.WriteAllText(@"C:/Users/sudar/source/repos/ConsoleApp5/ConsoleApp5/Models/vena.log", xmlDoc.InnerXml);

            ////read the contents of the message here and update as required;
            //// as the message is forward reading then we need to recreate it before moving on
            //ms = new MemoryStream();

            //xmlDoc.Save(ms);

            //ms.Position = 0;

            ////XmlReader reader = XmlReader.Create(ms);

            ////var newMessage = Message.CreateMessage(reader, int.MaxValue, message.Version);

            ////newMessage.Properties.CopyProperties(message.Properties);

            ////message = newMessage;

            //writer.Flush();
            //writer.Close();

            //ms.Flush();
            //ms.Close();

            return m;
        }
    }
}
