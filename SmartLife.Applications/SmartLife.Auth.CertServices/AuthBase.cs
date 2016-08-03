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
using e0571.web.core.Security;
using e0571.web.core.Service;
using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Cer;


namespace SmartLife.Auth.CertServices
{
    public class AuthBase : AppBaseWcfService
    {
        #region 用户认证实现
        protected void AuthenticateUser<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateUserParam
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
                        SPParam theSPParam = new ObjectToken { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, ObjectType = GlobalManager.DIKey_00011_User, ObjectId = TypeConverter.ChangeString(args.Instance.UserId) }.ToSPParam();
                        BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Cer_SignIn", theSPParam);
                        if (theSPParam.ErrorCode == 0)
                        {
                            result.ret.Token = TypeConverter.ChangeString(theSPParam["Token"]);

                            Redirect r = Global.redirects.FirstOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.ObjectId == param.ObjectId && item.Tag == (param.Tag ?? "SignIn"));
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

        #region 用户注销实现
        protected void LogOffUser<T, V>(T param, InvokeResult<V> result)
            where T : LogOffUserParam
            where V : LogOffUserRet
        {
            if (param is LogOffUserParam)
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

                SPParam theSPParam = new ObjectToken { ApplicationIdFrom = param.ApplicationIdFrom, ApplicationIdTo = param.ApplicationIdTo, Token = param.Token }.ToSPParam();
                BuilderFactory.DefaultBulder(connectstring_Or_Dbname).ExecuteSPNoneQuery("SP_Cer_SignOut", theSPParam);
                if (theSPParam.ErrorCode == 0)
                {
                    //浏览器类
                    Redirect r = Global.redirects.FirstOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.ObjectId == param.ObjectId && item.Tag == (param.Tag ?? "SignOut"));
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
        public string ObjectId { get; set; }//Area
        public byte RunMode { get; set; }
        public string Tag { get; set; }
    }

    public class AuthenticateUserParam : AuthenticateParam {
        
        public string Code { get; set; }
        public string Password { get; set; } 
        
    }
      
    public class LogOffParam
    {
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public Guid? Token { get; set; }
        public string ObjectId { get; set; }
        public string Tag { get; set; }
        public byte RunMode { get; set; }
    }

    public class LogOffUserParam : LogOffParam { } 


   

    #endregion


    #region 返回结果类

    public class AuthenticateRet
    {
        public string Token { get; set; }
    }

    public class AuthenticateUserRet : AuthenticateRet
    {
        public string RedirectUrl { get; set; }
    }
     
    public class LogOffRet { }

    public class LogOffUserRet : LogOffRet
    {
        public string RedirectUrl { get; set; }
    } 

    
    #endregion
}