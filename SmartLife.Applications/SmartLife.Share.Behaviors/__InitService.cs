using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
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

namespace SmartLife.Share.Behaviors
{
    /// <summary>
    /// 此类做初始化请求
    /// </summary>
    /// [ServiceLogging]
    //[ServiceAuthorization(Salt = "[leblue]", ValidateKey = "smartlife")]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __InitService
    {
        #region 获取参数
        [WebInvoke(UriTemplate = "Initilize", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<InitRet> Initilize(InitParam param)
        { 
            if (GlobalManager._Initilized)
            {
                string objectId = "*";
                if (GlobalManager.CurrentDeployNode != null)
                {
                    objectId = GlobalManager.CurrentDeployNode.ObjectId;
                }
                return new InvokeResult<InitRet>() { Success = true, ret = new InitRet { ObjectId = objectId, Message = "服务已初始化" } };
            }
            InvokeResult<InitRet> result = new InvokeResult<InitRet>() { Success = true, ret = new InitRet() };
            string hostToCompare = param.Host.ToLower();
            if (!hostToCompare.StartsWith("http://"))
            {
                hostToCompare = "http://" + hostToCompare;
            }
            GlobalManager.CurrentDeployNode = GlobalManager.DeployNodes.SingleOrDefault(item => item.ApplicationIdFrom == param.ApplicationIdFrom && item.ApplicationIdTo == param.ApplicationIdTo && item.RunMode == param.RunMode && item.AccessPoint.ToLower().StartsWith(hostToCompare));
            if (GlobalManager.CurrentDeployNode != null)
            {
                result.ret.ObjectId = GlobalManager.CurrentDeployNode.ObjectId;
            }
            else
            {
                result.ret.ObjectId = "*";
            }

            GlobalManager.InitByWcfService();
            return result;
        }
        #endregion
    }

    #region 入参类 

    public class InitParam
    {
        public string Host { get; set; }
        public string ApplicationIdFrom { get; set; }
        public string ApplicationIdTo { get; set; }
        public byte RunMode { get; set; }
    }


    #endregion


    #region 返回结果类
    public class InitRet
    {
        public string ObjectId { get; set; }
        public string Message { get; set; }
    }
    #endregion
}
