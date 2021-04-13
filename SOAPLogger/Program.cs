using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            //uses SoapExtension to extract soap request/response message
            var cdkReadDeal = new CdkHelper();

            cdkReadDeal.Execute();

            ////dani tawagon if sa atoa gikan ang soap message - xml
            //var soapRequest = new SOAPRequestSample();

            //soapRequest.Invoke("VF9qw7vTuToz", "machaikag", "3PA0003568", "782049", "Purchase");

            //mao ni sha ang another approach to extract soap request/response message
            var messageInspectorLogger = new MessageInspectorLogger();

            messageInspectorLogger.Log();
        }
    }
}
