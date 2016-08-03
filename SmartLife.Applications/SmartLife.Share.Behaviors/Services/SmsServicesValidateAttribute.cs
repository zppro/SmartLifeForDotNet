using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using e0571.web.core.Wcf;
using e0571.web.core.Utils;
using System.ServiceModel.Web;

namespace SmartLife.Share.Behaviors.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SmsServicesValidateAttribute : AppServicesBaseValidateAttribute
    {
        protected override bool  Validate(string ApplicationIdFrom, string ApplicationIdTo)
        {
            string applicationId = WebOperationContext.Current.IncomingRequest.Headers["ApplicationId"];
            return ApplicationIdFrom == applicationId && GlobalManager.CurrentApplicationId == ApplicationIdTo;
        }
    }
}
