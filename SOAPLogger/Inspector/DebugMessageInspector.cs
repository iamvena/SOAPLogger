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
            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);

            var requestCopy = buffer.CreateMessage();

            var messageLog = new MessageInspectorLogger();

            messageLog.Log(requestCopy);

            reply = buffer.CreateMessage();

            buffer.Close();
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
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);

            var requestCopy = buffer.CreateMessage();

            var messageLog = new MessageInspectorLogger();

            messageLog.Log(requestCopy);

            request = buffer.CreateMessage();

            buffer.Close();

            return null;
        }
    }
}
