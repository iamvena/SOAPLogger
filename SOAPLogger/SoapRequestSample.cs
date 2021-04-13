using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SOAPLogger
{
    /// <summary>
    /// If atoa nga soap message ang gamiton to request 
    /// </summary>
    public class SOAPRequestSample
    {
        public SOAPRequestSample()
        {

        }

        public void Invoke(string pass, string userName, string dealerId, string dealId, string type)
        {
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();

            //locate your created soap message - xml file
            var file = File.ReadAllText("C:\\Users\\sudar\\Desktop\\SOAP\\readRequest.xml");

            SOAPReqBody.LoadXml(file);

            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }

            try
            {
                using (WebResponse services = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(services.GetResponseStream()))
                    {
                        var serviceResult = reader.ReadToEnd();

                        Console.WriteLine(serviceResult);

                        Console.ReadLine();
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse errResponse = ex.Response;

                using (Stream resStream = errResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(resStream);

                    string text = reader.ReadToEnd();
                }
            }

        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://uat-3pa.dmotorworks.com:443/pip-deal/services/DealInsertUpdate");

            req.Headers.Add(@"SOAP:Action");

            req.ContentType = "text/xml;charset=\"utf-8\"";

            req.Accept = "text/xml";

            req.Method = "POST";

            return req;
        }
    }
}
