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
    public class AuthClientGov : AuthClientBase
    { 
         
        #region 认证用户 Gov
        [WebInvoke(UriTemplate = "AuthenticateUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthenticateClientGovRet> AuthenticateUser(AuthenticateClientGovParam param)
        {
            InvokeResult<AuthenticateClientGovRet> result = new InvokeResult<AuthenticateClientGovRet>() { Success = true, ret = new AuthenticateClientGovRet() };
            try
            {
                AuthenticateUser<AuthenticateClientGovParam, AuthenticateClientGovRet>(param, result);
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