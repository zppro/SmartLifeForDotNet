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
using SmartLife.Share.Models.Auth; 

namespace SmartLife.Auth.CertServices
{
    public class AuthScheduleBase : AppBaseWcfService
    {
        #region 用户认证实现
        protected void AuthenticateSchedule<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateSchduleParam
            where V : AuthenticateSchduleRet
        {
            string applicationId=GetHttpHeader(GlobalManager.ApplicationIdKey);
            if (applicationId == GlobalManager.SmartLife_WeiXinCloud_Schedule)
            { 
                try
                {
                    string connectstring_Or_Dbname = Global.oldConnectString;

                    var nodes = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode {   ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_WeiXinCloud_OldCare, RunMode = param.RunMode }.ToStringObjectDictionary(false));
                      
                    if (nodes.Count > 0)
                    {
                        result.ret.AccessPoints = nodes.Select(item => new AccessPointPart { ObjectId = item.ObjectId, Url = item.AccessPoint }).ToList();
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "无法找到AccessPoint";
                    }
                }
                catch (Exception ex)
                {
                    GlobalManager.outputLogger.Info(ex.ToString());
                }
            }
        }
        #endregion 



        #region 机构认证实现
        protected void AuthenticatePamSchedule<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateSchduleParam
            where V : AuthenticateSchduleRet
        {
            string applicationId = GetHttpHeader(GlobalManager.ApplicationIdKey);
            if (applicationId == GlobalManager.SmartLife_Client_Schedule)
            {
                try
                {
                    string connectstring_Or_Dbname = Global.oldConnectString;

                    var nodes = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_DataExchange_Services, RunMode = param.RunMode }.ToStringObjectDictionary(false));

                    if (nodes.Count > 0)
                    {
                        result.ret.AccessPoints = nodes.Select(item => new AccessPointPart { ObjectId = item.ObjectId, Url = item.AccessPoint }).ToList();
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "无法找到AccessPoint";
                    }
                }
                catch (Exception ex)
                {
                    GlobalManager.outputLogger.Info(ex.ToString());
                }
            }
        }
        #endregion 
          
    }



    #region 入参类

    public class AuthenticateSchduleParam
    {
        public byte RunMode { get; set; }
    }
    public class AuthenticateWeiXinSchduleParam : AuthenticateSchduleParam { }
    public class AuthenticatePamSchduleParam : AuthenticateSchduleParam { }
    #endregion


    #region 返回结果类

    public class AuthenticateSchduleRet
    {
        public IList<AccessPointPart> AccessPoints { get; set; }
    }
     
     
    #endregion
}