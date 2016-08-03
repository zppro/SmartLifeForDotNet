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
    public class SmsTokenValidateAttribute : TokenBaseValidateAttribute
    {
        protected override bool  Validate()
        {
            //对连接服务直接PASS
            string token = WebOperationContext.Current.IncomingRequest.Headers["Token"];
            string mobileNo = WebOperationContext.Current.IncomingRequest.Headers["MobileNo"];
            return TokenProvider.GenTokenDynamic(mobileNo, GlobalManager.SALT_SMS) == token;
        }
    }
}
