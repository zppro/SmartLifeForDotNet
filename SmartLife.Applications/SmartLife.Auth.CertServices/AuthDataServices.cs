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
using SmartLife.Share.Models.Auth;
using System.Security.Cryptography;

namespace SmartLife.Auth.CertServices
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuthDataServices : AppBaseWcfService
    {
        #region 获取对象模型Cer_Deploye中的ObjectId
        [WebGet(UriTemplate = "GetDeployNodeObjects", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DeployNodeObject> GetDeployNodeObjects()
        {
            CollectionInvokeResult<DeployNodeObject> result = new CollectionInvokeResult<DeployNodeObject> { Success = true };
            result.rows = BuilderFactory.DefaultBulder(Global.oldConnectString).List<DictionaryItem>(new { DictionaryId = "00005", WhereClause = " ItemCode in (select distinct ObjectId from Cer_DeployNode) " }.ToStringObjectDictionary()).Select(item => new DeployNodeObject { ObjectId = item.ItemCode, ObjectItemId = item.ItemId, ObjectItemName = item.ItemName, ObjectParentId = item.ParentId, ObjectLevels = item.Levels, ObjectOrderNo = item.OrderNo }).ToList();
            return result;
        }
        #endregion

        #region 获取对象模型Cer_Deploye中的Node
        [WebGet(UriTemplate = "GetDeployNode/{objectId},{runMode},{applicationIdTo}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<DeployNode> GetDeployNode(string objectId, string runMode, string applicationIdTo)
        {
            InvokeResult<DeployNode> result = new InvokeResult<DeployNode> { Success = true };
            var deployedNode = BuilderFactory.DefaultBulder().List<DeployNode>(new DeployNode { ObjectId = objectId, RunMode = Byte.Parse( runMode), ApplicationIdFrom = GetHttpHeader("ApplicationId"), ApplicationIdTo = applicationIdTo }).FirstOrDefault();
            if (deployedNode == null)
            {
                result.ErrorCode = 51406;
                result.ErrorMessage = "无法找到指定的节点";
            }
            else
            {
                result.ret = deployedNode;
            }
            return result;
        }
        #endregion
    }
     



}