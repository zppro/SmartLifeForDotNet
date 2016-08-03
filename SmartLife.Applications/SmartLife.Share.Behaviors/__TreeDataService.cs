using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Net;

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
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __TreeDataService :BaseWcfService
    {
        //异步加载使用
        [WebGet(UriTemplate = "getTreeData/{strTreeCode},{strPId},{strOrderByClause},{strTreeParams}", ResponseFormat = WebMessageFormat.Json)]
        public IList<TreeNode> getTreeData(string strTreeCode, string strPId, string strOrderByClause, string strTreeParams)
        {
            try
            {
                fetchTreeDataParam param = new fetchTreeDataParam { TreeCode = strTreeCode };
                if (strPId != "null")
                {
                    param.PId = strPId;
                }
                if (strOrderByClause != "null")
                {
                    param.OrderByClause = strOrderByClause;
                }
                if (strTreeParams != "null")
                {
                    param.TreeParams = strTreeParams;
                }
                return _getTreeData<TreeNode>(param.ToStringObjectDictionary(false));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [WebGet(UriTemplate = "getTreeData2/{strTreeCode},{strOrderByClause},{strTreeParams}", ResponseFormat = WebMessageFormat.Json)]
        public IList<TreeNodeEasyUI> getTreeData2(string strTreeCode, string strOrderByClause, string strTreeParams)
        {
            try
            {
                string strPId = GetRequestQuery("id", "_");
                fetchTreeDataParam param = new fetchTreeDataParam { TreeCode = strTreeCode };
                if (strPId != "null")
                {
                    param.PId = strPId;
                }
                if (strOrderByClause != "null")
                {
                    param.OrderByClause = strOrderByClause;
                }
                if (strTreeParams != "null")
                {
                    param.TreeParams = strTreeParams;
                }
                var nodes = _getTreeData<TreeNode>(param.ToStringObjectDictionary(false));
                
                return nodes.Select(item => item.toEasyUINode()).ToList();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        //全部加载使用
        [WebInvoke(UriTemplate = "fetchTreeData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public IList<TreeNode> fetchTreeData(fetchTreeDataParam param)
        {
            try
            {
                return _getTreeData<TreeNode>(param.ToStringObjectDictionary(false));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private IList<T> _getTreeData<T>(StringObjectDictionary param) where T : TreeNode, new()
        {

            if (param.ContainsKey("OrderByClause"))
            {
                param["OrderByClause"] = string.Join(",", param["OrderByClause"].ToString().Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }

            param.Add("ErrorCode", null);
            param.Add("ErrorMessage", null);

            var results = BuilderFactory.DefaultBulder(GlobalManager.getConnectString(GetHttpHeader(GlobalManager.ConnectIdKey))).ExecuteSPForQuery<StringObjectDictionary>("SP_Sys_GetTreeData", param).Select(item => { T node = new T(); node.MixDictionary(item); node.pId = node.pId.Trim(); return node; }).ToList();
            return results;

        }

        public IList<T> __getTreeData<T>(string strTreeCode, string strPId, string strOrderByClause) where T : TreeNode, new()
        {
            StringObjectDictionary param = new
            {
                TreeCode = strTreeCode,
            }.ToStringObjectDictionary();

            if (strPId != "null")
            {
                param.Add("PId", strPId);
            }

            if (strOrderByClause != "null")
            {
                string[] arrOrderbyClause = strOrderByClause.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                param.Add("OrderByClause", string.Join(",", strOrderByClause.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)));
            }

            param.Add("ErrorCode", null);
            param.Add("ErrorMessage", null);

            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteSPForQuery<StringObjectDictionary>("SP_Sys_GetTreeData", param).Select(item => { T node = new T(); node.MixDictionary(item); node.pId = node.pId.Trim(); return node; }).ToList();
        }
    }

    public class TreeNode
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        //public string parentId { get; set; }
        public string pId { get; set; }
        public int levels { get; set; }
        public bool isParent { get; set; }
        public bool @checked { get; set; }

        [NonSerialized]
        public string attributes;

        public void MixDictionary(StringObjectDictionary sod)
        {
            this.MixInWithDictionary(sod);
            sod.Remove("Id");
            sod.Remove("Name");
            sod.Remove("IsParent");
            
            attributes = sod.ToJson();
        }

        public TreeNodeEasyUI toEasyUINode()
        {
            return new TreeNodeEasyUI { id = this.id, text = this.name, state = this.isParent ? "closed" : "open", attributes = attributes };
        }
    }

    public class TreeNodeEasyUI  
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string attributes { get; set; } 
    }

    public class fetchTreeDataParam
    {
        public string TreeCode { get; set; }
        public string PId { get; set; }
        public string OrderByClause { get; set; }
        public string TreeParams { get; set; }
    }
}
