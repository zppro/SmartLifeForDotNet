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
    public class MerchantServicesValidateAttribute : AppServicesBaseValidateAttribute
    {
        protected override bool  Validate(string ApplicationIdFrom, string ApplicationIdTo)
        {
            //对连接服务直接PASS 
            string applicationId = WebOperationContext.Current.IncomingRequest.Headers["ApplicationId"];
            return ApplicationIdFrom == applicationId && GlobalManager.CurrentApplicationId == ApplicationIdTo;
        }
    }
}
