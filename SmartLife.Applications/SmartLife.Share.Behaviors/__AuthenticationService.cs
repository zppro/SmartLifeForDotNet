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

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __AuthenticationService : AppBaseWcfService
    {
        #region 检测Session
        [WebGet(UriTemplate = "IsLoggedIn", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<IsLoggedInRet> IsLoggedIn()
        {
            InvokeResult<IsLoggedInRet> result = new InvokeResult<IsLoggedInRet>() { Success = true, ret = new IsLoggedInRet() };
            result.ret.UserId = TypeConverter.ChangeString(HttpContext.Current.Session[NormalSession.USER_ID_KEY]);
            result.ret.UserCode = TypeConverter.ChangeString(HttpContext.Current.Session[NormalSession.USER_CODE_KEY]);
            result.ret.UserName = TypeConverter.ChangeString(HttpContext.Current.Session[NormalSession.USER_NAME_KEY]);
            result.ret.LoggedIn = result.ret.UserName != "";
            return result;
        }
        #endregion

        #region 直接登录
        [WebInvoke(UriTemplate = "_Logon", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<LogonRet> _Logon(LogonParam param)
        {
            InvokeResult<LogonRet> result = new InvokeResult<LogonRet>() { Success = true, ret = new LogonRet() };
            try
            {
                CertManager.LogonUser(param.Code, param.Password,
                    new LogonSuccessDelegate<User>(delegate(LogonSuccessEventArgs<User> args)
                {
                    base.setSessionValue(NormalSession.USER_ID_KEY, args.Instance.UserId.ToString());
                    base.setSessionValue(NormalSession.USER_CODE_KEY, args.Instance.UserCode);
                    base.setSessionValue(NormalSession.USER_NAME_KEY, args.Instance.UserName);
                    base.setSessionValue(NormalSession.USER_TYPE_KEY, args.Instance.UserType);
                    base.setSessionValue(NormalSession.IP_KEY, args.IpAddress);
                    result.ret.UserName = NormalSession.UserName;

                    //var redirectToIndex = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Tag = "IndexPage" }).FirstOrDefault();
                    var redirectToIndex = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Tag = param.Tag }).FirstOrDefault();
                    if (redirectToIndex != null)
                    {
                        result.ret.RedirectUrl = redirectToIndex.Url;
                    }
                }),
                    new LogonFailureDelegate(delegate(LogonFailureEventArgs args)
                {
                    result.Success = false;
                    result.ErrorMessage = args.ErrorMessage;
                }));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
         
        #region 直接登出
        [WebInvoke(UriTemplate = "_Logout", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<LogoutRet> _Logout(LogoutParam param)
        {
            InvokeResult<LogoutRet> result = new InvokeResult<LogoutRet>() { Success = true, ret = new LogoutRet { } };
            CertManager.LogoffUser(
                new LogoffCallbackDelegate(delegate(LogoffCallbackEventArgs args)
                {
                    //var redirectToLogin = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Tag = "LoginPage" }).FirstOrDefault();
                    var redirectToLogin = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Tag = param.Tag }).FirstOrDefault();
                    if (redirectToLogin != null)
                    {
                        result.ret.RedirectUrl = redirectToLogin.Url;
                    }
                })
                );
            return result;
        }
        #endregion

        #region 认证服务登录 
        [WebGet(UriTemplate = "SignIn/{applicationIdFrom},{applicationIdTo},{token},{tag}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<AuthenticateRet> SignIn(string applicationIdFrom, string applicationIdTo, string token, string tag)
        {
            InvokeResult<AuthenticateRet> result = new InvokeResult<AuthenticateRet>() { Success = true, ret = new AuthenticateRet() };
            try
            {
                CertManager.LogonUserByToken(applicationIdFrom, applicationIdTo, token,
                    new LogonSuccessDelegate<User>(delegate(LogonSuccessEventArgs<User> args)
                    {
                        base.setSessionValue(NormalSession.USER_ID_KEY, args.Instance.UserId.ToString());
                        base.setSessionValue(NormalSession.USER_CODE_KEY, args.Instance.UserCode);
                        base.setSessionValue(NormalSession.USER_NAME_KEY, args.Instance.UserName);
                        base.setSessionValue(NormalSession.USER_TYPE_KEY, args.Instance.UserType);
                        base.setSessionValue(NormalSession.IP_KEY, args.IpAddress);
                        result.ret.SessionId = NormalSession.SessionId;
                        result.ret.UserName = NormalSession.UserName;
                        result.ret.Ip = NormalSession.Ip;
                        //var redirectToIndex = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = applicationIdTo, Tag = "IndexPage" }).FirstOrDefault();
                        var redirectToIndex = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = applicationIdTo, Tag = tag }).FirstOrDefault();
                        if (redirectToIndex != null)
                        {
                            result.ret.RedirectUrl = redirectToIndex.Url;
                        }
                    }),
                    new LogonFailureDelegate(delegate(LogonFailureEventArgs args)
                    {
                        result.Success = false;
                        result.ErrorMessage = args.ErrorMessage;
                    }));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 认证服务登出
        [WebGet(UriTemplate = "SignOut/{applicationIdFrom},{applicationIdTo},{tag}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<AuthenticateRet> SignOut(string applicationIdFrom, string applicationIdTo, string tag)
        {
            InvokeResult<AuthenticateRet> result = new InvokeResult<AuthenticateRet>() { Success = true, ret = new AuthenticateRet() };
            try
            {
                CertManager.LogoffUser(
                    new LogoffCallbackDelegate(delegate(LogoffCallbackEventArgs args)
                    {
                        var redirectToLogin = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = applicationIdFrom, ApplicationIdTo = applicationIdTo, Tag = tag }).FirstOrDefault();
                        if (redirectToLogin != null)
                        {
                            result.ret.RedirectUrl = redirectToLogin.Url;
                        }
                    }));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 单点登录
        [WebInvoke(UriTemplate = "SSO", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<SSORet> SSO(SSOParam param)
        {
            InvokeResult<SSORet> result = new InvokeResult<SSORet>() { Success = true, ret = new SSORet() };
            try
            {
                CertManager.LogonUserByToken(param.ApplicationIdFrom, param.ApplicationIdTo, param.Token,
                    new LogonSuccessDelegate<User>(delegate(LogonSuccessEventArgs<User> args)
                    {
                        base.setSessionValue(NormalSession.USER_ID_KEY, args.Instance.UserId.ToString());
                        base.setSessionValue(NormalSession.USER_CODE_KEY, args.Instance.UserCode);
                        base.setSessionValue(NormalSession.USER_NAME_KEY, args.Instance.UserName);
                        base.setSessionValue(NormalSession.USER_TYPE_KEY, args.Instance.UserType);
                        base.setSessionValue(NormalSession.IP_KEY, args.IpAddress);
                        result.ret.SessionId = NormalSession.SessionId;
                        result.ret.UserName = NormalSession.UserName;
                        result.ret.Ip = NormalSession.Ip;
                        var redirectToIndex = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<Redirect>(new Redirect { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Tag = param.Tag }).FirstOrDefault();
                        if (redirectToIndex != null)
                        {
                            result.ret.RedirectUrl = redirectToIndex.Url;
                        }
                    }),
                    new LogonFailureDelegate(delegate(LogonFailureEventArgs args)
                    {
                        result.Success = false;
                        result.ErrorMessage = args.ErrorMessage;
                    }));
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

    #region 入参类

    #region CMS管理身份
    public class LogonParam
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public string Tag { get; set; }
    }
    public class LogoutParam
    {
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public string Tag { get; set; }
    }
    #endregion

    public class AuthenticateParam
    {
        public string Token { get; set; } 
    }

    public class AddSeatExtBindingParam
    {
        public Guid? UserId { get; set; }
        public Guid? ExtId { get; set; }
    }

    public class RemoveSeatExtBindingParam
    {
        public Guid? UserId { get; set; } 
    }

    public class SSOParam
    {
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public string Token { get; set; }
        public string Tag { get; set; }
    }
    #endregion


    #region 返回结果类

    #region CMS管理身份
    public class IsLoggedInRet
    {
        public bool LoggedIn;
        public string UserId;
        public string UserCode;
        public string UserName;
    }

    public class LogonRet
    { 
        public string UserName { get; set; } 
        public string RedirectUrl { get; set; }
    }

    public class LogoutRet
    {
        public string RedirectUrl { get; set; }
    }
    #endregion

    public class AuthenticateRet
    {
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class SSORet
    {
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public string RedirectUrl { get; set; }
    }

    #endregion



}