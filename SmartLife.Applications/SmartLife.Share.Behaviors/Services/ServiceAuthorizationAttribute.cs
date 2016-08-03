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
    public class ServiceAuthorizationAttribute : ServiceBaseAuthorizationAttribute
    {
        protected override bool needAuthorize(OperationContext context)
        {
            string runMode = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get(GlobalManager.PIKey_Sys_RunMode));
            if (runMode == "1")
            {
                //开发模式
                return false;
            }
            string requestApplicationId = WebOperationContext.Current.IncomingRequest.Headers[GlobalManager.ApplicationIdKey];
            if (!string.IsNullOrEmpty(requestApplicationId))
            {
                //检查请求应用对应的白名单
                IList<string> whiteLists = CacheManager.WhiteListCacheProvider.Get(requestApplicationId) as IList<string>;
                if (whiteLists != null)
                {
                    MessageProperties messageProperties = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    if (endpointProperty != null)
                    {
                        return !whiteLists.Any(item => item == endpointProperty.Address);
                    }
                }
            }
            return true;
        }
    }
}
