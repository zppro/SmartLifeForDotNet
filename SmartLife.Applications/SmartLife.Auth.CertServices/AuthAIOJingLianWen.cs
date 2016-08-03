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
    public class AuthAIOJingLianWen : AuthAIOBase
    { 
         
        #region 认证日照中心一体机 AIO
        [WebInvoke(UriTemplate = "Authenticate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthenticateAIOJingLianWenRet> Authenticate(AuthenticateAIOJingLianWenParam param)
        {
            InvokeResult<AuthenticateAIOJingLianWenRet> result = new InvokeResult<AuthenticateAIOJingLianWenRet>() { Success = true, ret = new AuthenticateAIOJingLianWenRet() };
            try
            {
                Authenticate<AuthenticateAIOJingLianWenParam, AuthenticateAIOJingLianWenRet>(param, result);
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