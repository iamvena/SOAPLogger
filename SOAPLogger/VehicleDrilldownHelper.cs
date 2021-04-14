using SOAPLogger.ChromeDataServiceRef;
using SOAPLogger.Inspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPLogger
{
    public class VehicleDrilldownHelper
    {
        public void GetMakes()
        {
            var apiKey = "209544";

            var apiSecret = "d773ba49e6d247a5";

            var _descriptionClient = new Description7cPortTypeClient("Description7cPort");

            //Add this class to use the inspector message logger
            _descriptionClient.Endpoint.EndpointBehaviors.Add(new DebugMessageBehavior());

            //make the actual request
            var req = _descriptionClient.getDivisions(new DivisionsRequest
            {
                accountInfo = new AccountInfo
                {
                    behalfOf = "ADS",
                    secret = apiSecret,
                    country = "US",
                    language = "en",
                    number = apiKey
                },
                modelYear = 2021
            });
        }
    }
}
