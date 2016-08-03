using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using e0571.web.core.Wcf;
using e0571.web.core.Utils;
using e0571.web.core.Model;

namespace SmartLife.Share.Behaviors.Services
{
    public class ServiceValidateSessionAttribute : ServiceValidateAttribute
    {
        protected override object Validate()
        {
            string deviceType = TypeConverter.ChangeString(WebOperationContext.Current.IncomingRequest.Headers["device-type"]);
            if (deviceType.ToLower() == "mobile")
            {
                return null;
            }
            bool validated = NormalSession.Count > 0;
            if (!validated)
            {
                throw new WebFaultException<string>("Session needed", System.Net.HttpStatusCode.Conflict);
            }

            return null;
        }
    }
}
