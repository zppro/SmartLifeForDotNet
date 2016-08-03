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
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Auth;
using SmartLife.Share.Models.Auth.SmsServices;
using e0571.web.core.Security;
using SmartLife.Share.Behaviors;

namespace SmartLife.Auth.Sms.Services
{
    [SmsServicesValidate(ApplicationIdFrom = "CS001", ApplicationIdTo = "IS001")]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class V1ShareService : BaseWcfService
    {
        #region 取当前城市的部署节点
        [WebInvoke(UriTemplate = "GetCityDeployNodes", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<AuthSmsCityDeployNodeRet> GetCityDeployNodes()
        {
            CollectionInvokeResult<AuthSmsCityDeployNodeRet> result = new CollectionInvokeResult<AuthSmsCityDeployNodeRet>() { Success = true };
            try
            {
                string applicationIdFrom = GetHttpHeader("ApplicationId");
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCityDeployNodesForSms", new { ApplicationIdFrom = applicationIdFrom }).Select(item => new AuthSmsCityDeployNodeRet
                {
                    Token = TokenProvider.GenTokenDynamic(item["NodeId"].ToString(), GlobalManager.SALT_SMS),
                    AccessPoint = item["AccessPoint"].ToString(),
                    NodeId = item["NodeId"].ToString()
                }).ToList();
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