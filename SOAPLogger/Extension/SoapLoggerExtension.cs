using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace SOAPLogger.Extension
{
    public class SoapLoggerExtension : SoapExtension
    {
        Stream oldStream;

        Stream newStream;

        string fileName = "vena";

        /// <summary>
        /// Save the Stream representing the SOAP request or response to a local memory buffer
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;

            newStream = new MemoryStream();

            return newStream;
        }

        /// <summary>
        /// When the SOAP extension is accessed for the first time, the XML Web 
        /// service method it is applied to is accessed to store the file
        /// name passed in, using the corresponding SoapExtensionAttribute.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return ((SoapLoggerExtensionAttribute)attribute).FileName;
        }

        /// <summary>
        /// The SOAP extension was configured to run using a configuration file instead
        /// of an attribute applied to a specific XML Web service method.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public override object GetInitializer(Type serviceType)
        {

            //Return a file name to log the trace information to, based on the type.
            var test = "C:/Users/sudar/source/repos/ConsoleApp5/ConsoleApp5/Models/vena.log";

            return test;
        }

        /// <summary>
        /// Received the file name stored by GetInitializer and store it in a
        /// member variable for this specific instance.
        /// </summary>
        /// <param name="initializer"></param>
        public override void Initialize(object initializer)
        {
            fileName = (string)initializer;
        }

        /// <summary>
        /// Process the SOAP message REQUEST and RESPONSE , write to a log file
        /// </summary>
        /// <param name="message"></param>
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    WriteOutput(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    WriteInput(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
                default:
                    throw new Exception("Invalid Stage");
            }

            throw new NotImplementedException();
        }

        //public void WriteOutput(SoapClientMessage messsage)
        //{
        //}

        /// <summary>
        /// Write the contents of the outgoing SOAP messsage to a file
        /// </summary>
        /// <param name="messsage"></param>
        public void WriteOutput(SoapMessage message)
        {
            newStream.Position = 0;

            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            string soapString = (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";

            sw.WriteLine($"--- {soapString} at {DateTime.Now} ");

            //Log the request header field for SoapAction header.
            sw.WriteLine("The SoapAction Http request header field is : ");
            sw.WriteLine("\t" + message.Action);

            //Log the URL of the site that provides implementation of the method.
            sw.WriteLine("The URL of the XML Web service method that has been requested is : ");
            sw.WriteLine("\t" + message.Url);
            sw.WriteLine("The contents of the SOAP envelope are :");
            sw.Flush();

            newStream.Position = 0;

            Copy(newStream, fs);

            sw.Close();

            newStream.Position = 0;
        }

        public void WriteInput(SoapMessage message)
        {
            Copy(oldStream, newStream);

            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            string soapString = (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";

            sw.WriteLine($"--- {soapString} at {DateTime.Now} ");

            //Log the request header field for SoapAction header.
            sw.WriteLine("The SoapAction Http request header field is : ");
            sw.WriteLine("\t" + message.Action);

            //Log the URL of the site that provides implementation of the method.
            sw.WriteLine("The URL of the XML Web service method that has been requested is : ");
            sw.WriteLine("\t" + message.Url);
            sw.WriteLine("The contents of the SOAP envelope are :");
            sw.Flush();

            newStream.Position = 0;

            Copy(newStream, fs);

            sw.Close();

            newStream.Position = 0;
        }

        void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);

            TextWriter writer = new StreamWriter(to);

            writer.WriteLine(reader.ReadToEnd());

            writer.Flush();
        }
    }

    /// <summary>
    /// Create a SoapExtensionAttribute for the SOAP Extension that can be
    /// applied to an XML Web service method
    /// Supports applying the attribute to an XML Web service method or a method in
    /// an XML Web servie client proxy class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SoapLoggerExtensionAttribute : SoapExtensionAttribute
    {
        private string fileName;

        private int priority;

        //Set the name of the log file were SOAP message will be stored.
        public SoapLoggerExtensionAttribute() : base()
        {

            fileName = "‪C:\\Users\\sudar\\Desktop\\SOAP\\logClient.txt";
        }

        //Return the type of 'SoapLoggerExtension' class
        public override Type ExtensionType
        {
            get
            {
                return typeof(SoapLoggerExtension);
            }
        }

        //User can set priority of the 'SoapExtension'
        public override int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
    }
}
