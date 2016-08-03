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

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class FlowDefineService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<FlowDefine> List()
        {
            CollectionInvokeResult<FlowDefine> result = new CollectionInvokeResult<FlowDefine> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>();
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
        public CollectionInvokeResult<FlowDefine> Query(string strParms)
        {
            CollectionInvokeResult<FlowDefine> result = new CollectionInvokeResult<FlowDefine> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>(dictionary);
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
        public JqgridCollectionInvokeResult<FlowDefine> ListForJqgrid(JqgridCollectionParam<FlowDefine> param)
        {
            JqgridCollectionInvokeResult<FlowDefine> result = new JqgridCollectionInvokeResult<FlowDefine> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if( param.instance != null ){
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

                gridCollectionPager.JqgridDoPage<FlowDefine>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<FlowDefine> ListForEasyUIgrid(EasyUIgridCollectionParam<FlowDefine> param)
        {
            EasyUIgridCollectionInvokeResult<FlowDefine> result = new EasyUIgridCollectionInvokeResult<FlowDefine> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if( param.instance != null ){
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
                    filters.Add("OrderByClause", param.sort+ " " + param.order?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<FlowDefine>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>(filters), param, ref result);
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
        public ModelInvokeResult<FlowDefinePK> Create(FlowDefine flowDefine)
        {
            ModelInvokeResult<FlowDefinePK> result = new ModelInvokeResult<FlowDefinePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (flowDefine.FlowDefineId == GlobalManager.GuidAsAutoGenerate)
                {
                    flowDefine.FlowDefineId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                flowDefine.OperatedBy = NormalSession.UserId.ToGuid();
                flowDefine.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = flowDefine.GetCreateMethodName(), ParameterObject = flowDefine.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FlowDefinePK{ FlowDefineId = flowDefine.FlowDefineId };
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
        [WebInvoke(UriTemplate = "{strFlowDefineId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FlowDefinePK> Update(string strFlowDefineId, FlowDefine flowDefine)
        {
            ModelInvokeResult<FlowDefinePK> result = new ModelInvokeResult<FlowDefinePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FlowDefineId = strFlowDefineId.ToGuid();
                if (_FlowDefineId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                flowDefine.FlowDefineId = _FlowDefineId;
                /***********************begin 自定义代码*******************/
                flowDefine.OperatedBy = NormalSession.UserId.ToGuid();
                flowDefine.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = flowDefine.GetUpdateMethodName(), ParameterObject = flowDefine.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FlowDefinePK{ FlowDefineId = _FlowDefineId };

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
        [WebInvoke(UriTemplate = "{strFlowDefineId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FlowDefinePK> Delete(string strFlowDefineId)
        {
            ModelInvokeResult<FlowDefinePK> result = new ModelInvokeResult<FlowDefinePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FlowDefineId = strFlowDefineId.ToGuid();
                if (_FlowDefineId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                FlowDefinePK pk = new FlowDefinePK { FlowDefineId = _FlowDefineId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new FlowDefine().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FlowDefinePK{ FlowDefineId = _FlowDefineId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strFlowDefineIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strFlowDefineIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrFlowDefineIds = strFlowDefineIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrFlowDefineIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new FlowDefine().GetDeleteMethodName();
                foreach (string strFlowDefineId in arrFlowDefineIds)
                {
                    FlowDefinePK pk = new FlowDefinePK { FlowDefineId = strFlowDefineId.ToGuid() };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 作废 Nullify
        [WebInvoke(UriTemplate = "Nullify/{strFlowDefineId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<FlowDefinePK> Nullify(string strFlowDefineId)
        {
            ModelInvokeResult<FlowDefinePK> result = new ModelInvokeResult<FlowDefinePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _FlowDefineId = strFlowDefineId.ToGuid();
                if (_FlowDefineId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                FlowDefine flowDefine = new FlowDefine { FlowDefineId = _FlowDefineId, Status = 0 };
                /***********************begin 自定义代码*******************/
                flowDefine.OperatedBy = NormalSession.UserId.ToGuid();
                flowDefine.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = flowDefine.GetUpdateMethodName(), ParameterObject = flowDefine.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new FlowDefinePK{ FlowDefineId = _FlowDefineId };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
        
        #region 作废所选 NullifySelected
        [WebInvoke(UriTemplate = "NullifySelected/{strFlowDefineIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strFlowDefineIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrFlowDefineIds = strFlowDefineIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrFlowDefineIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new FlowDefine().GetUpdateMethodName();
                foreach (string strFlowDefineId in arrFlowDefineIds)
                {
                    FlowDefine flowDefine = new FlowDefine { FlowDefineId = strFlowDefineId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    flowDefine.OperatedBy = NormalSession.UserId.ToGuid();
                    flowDefine.OperatedOn = DateTime.Now;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = flowDefine.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements,FlowDefinePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strFlowDefineId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<FlowDefine> Load(string strFlowDefineId)
        {
            ModelInvokeResult<FlowDefine> result = new ModelInvokeResult<FlowDefine> { Success = true };
            try
            {
                Guid? _FlowDefineId = strFlowDefineId.ToGuid();
                if (_FlowDefineId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var flowDefine = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<FlowDefine, FlowDefinePK>(new FlowDefinePK { FlowDefineId = _FlowDefineId });
                result.instance = flowDefine; 
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

        #region 自定义

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgridPage/{strConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<FlowDefine> ListForEasyUIgridPage(string strConnectId, EasyUIgridCollectionParam<FlowDefine> param)
        {
            EasyUIgridCollectionInvokeResult<FlowDefine> result = new EasyUIgridCollectionInvokeResult<FlowDefine> { Success = true };
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
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
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
                gridCollectionPager.EasyUIgridDoPage4Model<FlowDefine>(filters, param, ref result, strConnectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 普通列表 List
        [WebGet(UriTemplate = "GetFlowNameItem", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetFlowNameItem()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                string sql = "select distinct FlowName as ItemId,FlowName as ItemCode,FlowCName as ItemName from Pub_FlowDefine where Status=1 order by FlowName asc";
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlForQuery(sql);
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

