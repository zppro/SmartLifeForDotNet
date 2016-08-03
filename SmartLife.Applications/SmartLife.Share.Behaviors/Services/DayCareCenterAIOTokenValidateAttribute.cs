using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using e0571.web.core.Wcf;
using e0571.web.core.Utils;
using System.ServiceModel.Web;
using e0571.web.core.Security;

namespace SmartLife.Share.Behaviors.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DayCareCenterAIOTokenValidateAttribute : TokenBaseValidateAttribute
    {
        protected override bool  Validate()
        {
            //对连接服务直接PASS
            string applicationId = WebOperationContext.Current.IncomingRequest.Headers["applicationId"];
            string token = WebOperationContext.Current.IncomingRequest.Headers["Token"];
            string dccNo = WebOperationContext.Current.IncomingRequest.Headers["DCCNo"];
            return TokenProvider.GenTokenDynamic(dccNo + "|" + applicationId, GlobalManager.SALT_DATA_EXCHANGE) == token;
        }
    }
}
