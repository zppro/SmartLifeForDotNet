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
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Models.WeiXin.Pub;

namespace SmartLife.CertManage.ManageServices.WeiXin.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WXStationAuthRequestService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<WXStationAuthRequest> List()
        {
            CollectionInvokeResult<WXStationAuthRequest> result = new CollectionInvokeResult<WXStationAuthRequest> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<WXStationAuthRequest>();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询列表 Query
        [WebGet(UriTemplate = "Query?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<WXStationAuthRequest> Query(string strParms)
        {
            CollectionInvokeResult<WXStationAuthRequest> result = new CollectionInvokeResult<WXStationAuthRequest> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<WXStationAuthRequest>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region Jqgrid数据格式的列表 ListForJqgrid
        [WebInvoke(UriTemplate = "ListForJqgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public JqgridCollectionInvokeResult<WXStationAuthRequest> ListForJqgrid(JqgridCollectionParam<WXStationAuthRequest> param)
        {
            JqgridCollectionInvokeResult<WXStationAuthRequest> result = new JqgridCollectionInvokeResult<WXStationAuthRequest> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                /**********************************************************/
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sidx))
                {
                    filters.Add("OrderByClause", param.sidx + " " + param.sord ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.JqgridDoPage<WXStationAuthRequest>(BuilderFactory.DefaultBulder().List<WXStationAuthRequest>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<WXStationAuthRequest> ListForEasyUIgrid(EasyUIgridCollectionParam<WXStationAuthRequest> param)
        {
            EasyUIgridCollectionInvokeResult<WXStationAuthRequest> result = new EasyUIgridCollectionInvokeResult<WXStationAuthRequest> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                /**********************************************************/
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<WXStationAuthRequest>(BuilderFactory.DefaultBulder().List<WXStationAuthRequest>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 Create
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXStationAuthRequestPK> Create(WXStationAuthRequest wXStationAuthRequest)
        {
            ModelInvokeResult<WXStationAuthRequestPK> result = new ModelInvokeResult<WXStationAuthRequestPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (wXStationAuthRequest.Id == -1)
                {
                    wXStationAuthRequest.Id = null;
                }
                /***********************begin 自定义代码*******************/
                wXStationAuthRequest.OperatedBy = NormalSession.UserId.ToGuid();
                wXStationAuthRequest.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = wXStationAuthRequest.GetCreateMethodName(), ParameterObject = wXStationAuthRequest.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXStationAuthRequestPK { Id = wXStationAuthRequest.Id };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改 Update
        [WebInvoke(UriTemplate = "{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXStationAuthRequestPK> Update(string strId, WXStationAuthRequest wXStationAuthRequest)
        {
            ModelInvokeResult<WXStationAuthRequestPK> result = new ModelInvokeResult<WXStationAuthRequestPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                wXStationAuthRequest.OperatedBy = NormalSession.UserId.ToGuid();
                wXStationAuthRequest.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = wXStationAuthRequest.GetUpdateMethodName(), ParameterObject = wXStationAuthRequest.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXStationAuthRequestPK { Id = int.Parse(strId) };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 删除 Delete
        [WebInvoke(UriTemplate = "{strId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WXStationAuthRequestPK> Delete(string strId)
        {
            ModelInvokeResult<WXStationAuthRequestPK> result = new ModelInvokeResult<WXStationAuthRequestPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                WXStationAuthRequestPK pk = new WXStationAuthRequestPK { Id = int.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new WXStationAuthRequest().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WXStationAuthRequestPK { Id = int.Parse(strId) };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 删除所选 DeleteSelected
        [WebInvoke(UriTemplate = "DeleteSelected/{strIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WXStationAuthRequest().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    WXStationAuthRequestPK pk = new WXStationAuthRequestPK { Id = int.Parse(strId) };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
         
    
        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, WXStationAuthRequestPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<WXStationAuthRequest> Load(string strId)
        {
            ModelInvokeResult<WXStationAuthRequest> result = new ModelInvokeResult<WXStationAuthRequest> { Success = true };
            try
            {
                var wXStationAuthRequest = BuilderFactory.DefaultBulder().Load<WXStationAuthRequest, WXStationAuthRequestPK>(new WXStationAuthRequestPK { Id = int.Parse(strId) });
                result.instance = wXStationAuthRequest;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #endregion

        #region 获取审核数据
        #region EasyUIgrid数据格式的列表 ListForEasyUIgridForAudit
        [WebInvoke(UriTemplate = "ListForEasyUIgridForAudit", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridForAudit(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.value != null)
                        {
                            filters[field.key] = field.value;
                        }
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/ 
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("WXStationAuthRequest_ListByPageForAudit", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 审核所选 AuditSelected
        [WebInvoke(UriTemplate = "AuditSelected/{doStatus},{strIds}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AuditSelected(string doStatus, string strIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WXStationAuthRequest().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    
                    /***********************begin 自定义代码*******************/
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new { Id = int.Parse(strId), DoStatus = byte.Parse(doStatus) }.ToStringObjectDictionary(), Type = SqlExecuteType.UPDATE });

                    //设置提醒标志已响应 
                    WXStationAuthRequest wxStationAuthRequest = BuilderFactory.DefaultBulder().Load<WXStationAuthRequest,WXStationAuthRequestPK>(new WXStationAuthRequestPK{ Id = int.Parse(strId)});
                    if (wxStationAuthRequest.Id != null)
                    {
                        statements.Add(new IBatisNetBatchStatement { StatementName = "ReponseReminder", ParameterObject = new { ResponseAppType = GlobalManager.DIKey_01004_Web, ObjectType = "Merchant", ObjectKey = wxStationAuthRequest.StationId, SourceTable = wxStationAuthRequest.GetMappingTableName(), SourceColumn = "Id", SourceType = "B0101", SourceKey = strId }.ToStringObjectDictionary(), Type = SqlExecuteType.UPDATE });
                    }
                }

                
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #endregion
    }
}