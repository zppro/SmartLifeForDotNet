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
    public class AuthAIOBase : AppBaseWcfService
    {
        #region 用户认证实现
        protected void Authenticate<T, V>(T param, InvokeResult<V> result)
            where T : AuthenticateAIOParam
            where V : AuthenticateAIORet
        {
            string applicationId = GetHttpHeader(GlobalManager.ApplicationIdKey);

            try
            {
                string connectstring_Or_Dbname = Global.oldConnectString;
                string areaCode = param.DCCNo.Substring(0, 6);
                 
                //GlobalManager.outputLogger.Info(connectstring_Or_Dbname);
                
                if (applicationId == GlobalManager.SmartLife_DayCareCenter_AIO)
                {
                    #region 日照中心一体机 
                    var accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = applicationId, ApplicationIdTo = GlobalManager.SmartLife_DataExchange_Services, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                    if (accessNode != null)
                    {
                        result.ret.AccessPoint = accessNode.AccessPoint;
                    }
                    else
                    {
                        accessNode = BuilderFactory.DefaultBulder(connectstring_Or_Dbname).List<DeployNode>(new DeployNode { ObjectId = areaCode, ApplicationIdFrom = GlobalManager.SmartLife_CertManage_ManageCMS, ApplicationIdTo = GlobalManager.SmartLife_CertManage_ManageServices, RunMode = param.RunMode }.ToStringObjectDictionary(false)).FirstOrDefault();
                        if (accessNode != null)
                        {
                            result.ret.AccessPoint = accessNode.AccessPoint;
                        }
                    }
                    result.ret.Token = TokenProvider.GenTokenDynamic(param.DCCNo + "|" + applicationId, GlobalManager.SALT_DATA_EXCHANGE);
                    #endregion
                }

                if (result.ret.AccessPoint == null)
                {
                    result.Success = false;
                    result.ErrorCode = 52101;
                }
            }
            catch (Exception ex)
            {
                GlobalManager.outputLogger.Info(ex.ToString());
            }
        }
        #endregion 
          
    }



    #region 入参类

    public class AuthenticateAIOParam
    {
        public byte RunMode { get; set; } 
        public string DCCNo { get; set; }
    }

    public class AuthenticateAIOJingLianWenParam : AuthenticateAIOParam { }
    #endregion


    #region 返回结果类

    public class AuthenticateAIORet{ 
        public string AccessPoint { get; set; }
        public string Token { get; set; }
    }
    public class AuthenticateAIOJingLianWenRet : AuthenticateAIORet { }
    
    #endregion
}