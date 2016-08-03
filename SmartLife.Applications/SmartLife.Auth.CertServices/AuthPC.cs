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

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Cer;
using System.Security.Cryptography;

namespace SmartLife.Auth.CertServices
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthPC : AuthBase
    { 
         
        #region 认证用户 因为只供是内网PC调用，可以保证安全
        [WebInvoke(UriTemplate = "AuthenticateUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthenticateUserRet> AuthenticateUser(AuthenticateUserParam param)
        {
            InvokeResult<AuthenticateUserRet> result = new InvokeResult<AuthenticateUserRet>() { Success = true, ret = new AuthenticateUserRet() };
            try
            {
                AuthenticateUser<AuthenticateUserParam, AuthenticateUserRet>(param, result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region 注销用户 因为只供是内网PC调用，可以保证安全
        [WebInvoke(UriTemplate = "LogOffUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<LogOffUserRet> LogOffUser(LogOffUserParam param)
        {
            InvokeResult<LogOffUserRet> result = new InvokeResult<LogOffUserRet>() { Success = true, ret = new LogOffUserRet() };
            try
            {
                LogOffUser<LogOffUserParam, LogOffUserRet>(param, result);
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