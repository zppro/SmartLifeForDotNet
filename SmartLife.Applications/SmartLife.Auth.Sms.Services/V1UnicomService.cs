using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Auth;
using SmartLife.Share.Models.Auth.SmsServices;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;

namespace SmartLife.Auth.Sms.Services
{
    [SmsServicesValidate(ApplicationIdFrom = "CS001", ApplicationIdTo = "IS001")] 
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1UnicomService : BaseWcfService
    {
        #region 测试服务是否能够连接
        [WebInvoke(UriTemplate = "", Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string _Connect()
        {
            return base.__Connect();
        }
        #endregion

        #region 认证
        [WebInvoke(UriTemplate = "AuthenticateUnicomMobileNo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthSmsUnicomRet> AuthenticateUnicomMobileNo(AuthSmsUnicomParam param)
        {
            InvokeResult<AuthSmsUnicomRet> result = new InvokeResult<AuthSmsUnicomRet>() { Success = true, ret = new AuthSmsUnicomRet() };
            try
            {
                string applicationIdFrom = GetHttpHeader("ApplicationId");
                SPParam theSPParam = new { MobileNo = param.MobileNo, ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = GlobalManager.SmartLife_CertManage_SmsServices }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Auth_Sms", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    result.ret.Token = TokenProvider.GenTokenDynamic(param.MobileNo, GlobalManager.SALT_SMS);
                    result.ret.AccessPoint = theSPParam["AccessPoint"].ToString();
                }
                else
                {
                    result.Success = false;
                    result.ErrorCode = Convert.ToInt32(theSPParam["ErrorCode"]);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion
    }
}