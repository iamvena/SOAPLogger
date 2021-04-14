using SOAPLogger.DealInsertUpdateServiceRef;
using SOAPLogger.Extension;
using SOAPLogger.Inspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPLogger
{

    public class CdkHelper : SoapLoggerExtension
    {
        public void Execute()
        {
            var authToken = new authenticationToken
            {
                password = "VF9qw7vTuToz",
                username = "machaikag"
            };

            var dealerId = new dealerId
            {
                id = "3PA0003568"
            };

            DealInsertUpdateServiceRef.DealSaveClient soap = new DealSaveClient();

            soap.ClientCredentials.UserName.UserName = "machaikag";
            soap.ClientCredentials.UserName.Password = "VF9qw7vTuToz";

            DealInsertUpdateServiceRef.dealReadRequest read = new dealReadRequest();

            read.dealId = "782049";
            read.leasePurchase = leasePurchase.Purchase;
            read.leasePurchaseSpecified = true;


            ////Add this class to use the inspector message logger
            //soap.Endpoint.EndpointBehaviors.Add(new DebugMessageBehavior());


            soap.read(authToken, dealerId, read);
        }
    }
}
