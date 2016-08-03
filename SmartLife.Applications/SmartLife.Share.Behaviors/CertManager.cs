using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using e0571.web.core.Security;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Cer;

namespace SmartLife.Share.Behaviors
{
     
    public class LogonSuccessEventArgs<T> : EventArgs
    {
        public string IpAddress { get; private set; }
        public T Instance { get; private set; }

        public LogonSuccessEventArgs(T instance, string ipAddress)
        {
            Instance = instance;
            IpAddress = ipAddress;
        }
    }

    public class LogonFailureEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }

        public LogonFailureEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class LogoffCallbackEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        public LogoffCallbackEventArgs(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
    public delegate void LogonSuccessDelegate<T>(LogonSuccessEventArgs<T> args);
    public delegate void LogonFailureDelegate(LogonFailureEventArgs args);
    public delegate void LogoffCallbackDelegate(LogoffCallbackEventArgs args);
    public static class CertManager
    {


        public static void LogonUser(string userCode, string password, LogonSuccessDelegate<User> onSuccess, LogonFailureDelegate onFailure)
        {
            LogonUser(userCode, password, onSuccess, onFailure, null);
        }

        public static void LogonUser(string userCode, string password, LogonSuccessDelegate<User> onSuccess, LogonFailureDelegate onFailure, string connectstring_Or_Dbname)
        {
            IList<User> users = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<User>(new User { UserCode = TypeConverter.ChangeString(userCode) });

            if (users.Count > 0)
            {
                string passHash = password ?? "";
                if (passHash.Length != 32)
                {
                    passHash = MD5Provider.Generate(password);
                }

                var query = users.Where(item => TypeConverter.ChangeString(item.PasswordHash) == passHash);
                if (query.Count() > 0)
                {
                    User user = query.First();

                    if (user.StopFlag == 1)
                    {
                        if (onFailure != null)
                            onFailure(new LogonFailureEventArgs("此帐号已停用，请联系管理员"));
                    }
                    else
                    {
                        if (onSuccess != null)
                            onSuccess(new LogonSuccessEventArgs<User>(user, HttpContext.Current.Request.UserHostAddress));
                    }
                }
                else
                {
                    if (onFailure != null)
                        onFailure(new LogonFailureEventArgs("您输入的密码有误，请重新输入"));
                }

            }
            else
            {
                if (onFailure != null)
                    onFailure(new LogonFailureEventArgs(string.Format("此帐号【{0}】不存在", userCode)));

            }
        }

        public static void LogoffUser()
        {
            LogoffUser(null);
        }
        public static void LogoffUser(LogoffCallbackDelegate callback)
        {
            NormalSession.Abandon();

            if (callback != null)
                callback(new LogoffCallbackEventArgs(true, ""));
        }

        public static void LogonUserByToken(string applicationIdFrom,string applicationIdTo,string token, LogonSuccessDelegate<User> onSuccess, LogonFailureDelegate onFailure)
        {
            LogonUserByToken(applicationIdFrom, applicationIdTo, token, onSuccess, onFailure, null);
        }
        public static void LogonUserByToken(string applicationIdFrom, string applicationIdTo, string token, LogonSuccessDelegate<User> onSuccess, LogonFailureDelegate onFailure, string connectstring_Or_Dbname)
        {
            ObjectToken theOne = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Load<ObjectToken, ObjectTokenPK>(new ObjectTokenPK { Token = token.ToGuid() });
            if (theOne.Token != null && theOne.ApplicationIdFrom == applicationIdFrom && theOne.ApplicationIdTo == applicationIdTo)
            {
                if (theOne.ObjectType == "00001")
                {
                    //用户
                    if ((theOne.ExpireOn.Value - DateTime.Now).TotalSeconds > 0)
                    {
                        User user = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).Load<User, UserPK>(new UserPK { UserId = theOne.ObjectId.ToGuid() });
                        if (user.UserId != null)
                        {
                            if (user.StopFlag == 1)
                            {
                                if (onFailure != null)
                                    onFailure(new LogonFailureEventArgs("此帐号已停用，请联系管理员"));
                            }
                            else
                            {
                                if (onSuccess != null)
                                    onSuccess(new LogonSuccessEventArgs<User>(user, HttpContext.Current.Request.UserHostAddress));
                            }
                        }
                        else
                        {
                            if (onFailure != null)
                                onFailure(new LogonFailureEventArgs("用户不存在"));
                        }
                    }
                    else
                    {
                        if (onFailure != null)
                            onFailure(new LogonFailureEventArgs(string.Format("token【{0}】已过期", token)));
                    }
                }
                else
                {
                    if (onFailure != null)
                        onFailure(new LogonFailureEventArgs(string.Format("非法的token【{0}】", token)));
                }
            }
            else
            {
                if (onFailure != null)
                    onFailure(new LogonFailureEventArgs(string.Format("无效的token【{0}】", token)));
            }
        }
    }
}
