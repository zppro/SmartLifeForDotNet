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


namespace SmartLife.CertManage.CertServices
{
    //[ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthenticationService : AppBaseWcfService
    { 
         
        #region 认证
        [WebInvoke(UriTemplate = "AuthenticateUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<AuthenticateUserRet> AuthenticateUser(AuthenticateUserParam param)
        {
            InvokeResult<AuthenticateUserRet> result = new InvokeResult<AuthenticateUserRet>() { Success = true, ret = new AuthenticateUserRet() };
            try
            {
                Authenticate<AuthenticateUserParam, AuthenticateUserRet>(param, result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private void Authenticate<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateParam
            where V : AuthenticateUserRet
        {
            if (param is AuthenticateUserParam)
            {
                string connectstring_Or_Dbname = null;
                if (param.ObjectId != "*")
                {
                    var deployNode = GlobalManager.DeployNodes.SingleOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.ObjectId == param.ObjectId
                        && item.RunMode.Value == param.RunMode);
                    if (deployNode != null)
                    {
                        connectstring_Or_Dbname = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(deployNode.ConnectString));
                    }
                }
                else
                {
                    connectstring_Or_Dbname = Global.oldConnectString;
                }
                var a = param.ToStringObjectDictionary();
                               
                CertManager.LogonUser(param.Code, param.Password,
                    new LogonSuccessDelegate<User>(delegate(LogonSuccessEventArgs<User> args)
                    {
                        //查询
                        (result as InvokeResult<AuthenticateUserRet>).ret.isCCSeat = args.Instance.isCCSeat();
                        SPParam theSPParam = new ObjectToken { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, ObjectType = GlobalManager.DIKey_00011_User, ObjectId = TypeConverter.ChangeString(args.Instance.UserId) }.ToSPParam();
                        BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Cer_SignIn", theSPParam);
                        if (theSPParam.ErrorCode == 0)
                        {
                            result.ret.Token = TypeConverter.ChangeString(theSPParam["Token"]).ToGuid();
                             
                            Redirect r = Global.redirects.FirstOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.Tag == "SignIn");
                            if (r != null)
                            {
                                result.ret.RedirectUrl = r.Url.ReplaceWithKeys(theSPParam);
                                
                            }
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = theSPParam.ErrorMessage;
                        }
                    }),
                    new LogonFailureDelegate(delegate(LogonFailureEventArgs args)
                    {
                        result.Success = false;
                        result.ErrorMessage = args.ErrorMessage;
                    }), connectstring_Or_Dbname);
            }
        }
        #endregion

        #region 注销
        [WebInvoke(UriTemplate = "LogOffUser", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<LogOffRet> LogOffUser(LogOffUserParam param)
        {
            InvokeResult<LogOffRet> result = new InvokeResult<LogOffRet>() { Success = true, ret = new LogOffRet() };
            try
            {
                LogOff<LogOffUserParam>(param, result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        private void LogOff<T>(T param, InvokeResult<LogOffRet> result)
            where T : LogOffParam
        {
            if (param is LogOffUserParam)
            {
                SPParam theSPParam = new ObjectToken { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Token = param.Token }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Cer_SignOut", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    //浏览器类
                    Redirect r = Global.redirects.FirstOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.Tag == "SignOut");
                    if (r != null)
                    {
                        theSPParam.MixInWithDictionary(param.ToStringObjectDictionary());
                        result.ret.RedirectUrl = r.Url.ReplaceWithKeys(theSPParam);
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = theSPParam.ErrorMessage;
                }
            }
        }
        #endregion
    }

    #region 入参类

    public class AuthenticateParam
    {
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ObjectId { get; set; }//Area
        public byte RunMode { get; set; }
    }

    public class AuthenticateUserParam : AuthenticateParam { }

    public class LogOffParam
    {
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public Guid? Token { get; set; }
    }

    public class LogOffUserParam : LogOffParam { }

    #endregion


    #region 返回结果类

    public class AuthenticateRet
    {
        public Guid? Token { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class AuthenticateUserRet : AuthenticateRet
    {
        public bool isCCSeat { get; set; }//是否坐席
    }

    public class LogOffRet
    {
        public string RedirectUrl { get; set; }
    }
    #endregion



}