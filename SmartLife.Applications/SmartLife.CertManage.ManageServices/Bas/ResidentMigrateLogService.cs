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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Bas
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ResidentMigrateLogService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ResidentMigrateLog> List()
        {
            CollectionInvokeResult<ResidentMigrateLog> result = new CollectionInvokeResult<ResidentMigrateLog> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentMigrateLog>();
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
        public CollectionInvokeResult<ResidentMigrateLog> Query(string strParms)
        {
            CollectionInvokeResult<ResidentMigrateLog> result = new CollectionInvokeResult<ResidentMigrateLog> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentMigrateLog>(dictionary);
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
        public JqgridCollectionInvokeResult<ResidentMigrateLog> ListForJqgrid(JqgridCollectionParam<ResidentMigrateLog> param)
        {
            JqgridCollectionInvokeResult<ResidentMigrateLog> result = new JqgridCollectionInvokeResult<ResidentMigrateLog> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ResidentMigrateLog>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentMigrateLog>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ResidentMigrateLog> ListForEasyUIgrid(EasyUIgridCollectionParam<ResidentMigrateLog> param)
        {
            EasyUIgridCollectionInvokeResult<ResidentMigrateLog> result = new EasyUIgridCollectionInvokeResult<ResidentMigrateLog> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ResidentMigrateLog>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ResidentMigrateLog>(filters), param, ref result);
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
        public ModelInvokeResult<ResidentMigrateLogPK> Create(ResidentMigrateLog residentMigrateLog)
        {
            ModelInvokeResult<ResidentMigrateLogPK> result = new ModelInvokeResult<ResidentMigrateLogPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (residentMigrateLog.ResidentDynamicMoveId == GlobalManager.GuidAsAutoGenerate)
                {
                    residentMigrateLog.ResidentDynamicMoveId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                residentMigrateLog.OperatedBy = NormalSession.UserId.ToGuid();
                residentMigrateLog.OperatedOn = DateTime.Now;
                residentMigrateLog.EventTime = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentMigrateLog.GetCreateMethodName(), ParameterObject = residentMigrateLog.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentMigrateLogPK { ResidentDynamicMoveId = residentMigrateLog.ResidentDynamicMoveId };
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
        [WebInvoke(UriTemplate = "{strResidentDynamicMoveId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentMigrateLogPK> Update(string strResidentDynamicMoveId, ResidentMigrateLog residentMigrateLog)
        {
            ModelInvokeResult<ResidentMigrateLogPK> result = new ModelInvokeResult<ResidentMigrateLogPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid();
                if (_ResidentDynamicMoveId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                residentMigrateLog.ResidentDynamicMoveId = _ResidentDynamicMoveId;
                /***********************begin 自定义代码*******************/
                residentMigrateLog.OperatedBy = NormalSession.UserId.ToGuid();
                residentMigrateLog.OperatedOn = DateTime.Now;
                residentMigrateLog.EventTime = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentMigrateLog.GetUpdateMethodName(), ParameterObject = residentMigrateLog.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentMigrateLogPK { ResidentDynamicMoveId = _ResidentDynamicMoveId };

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
        [WebInvoke(UriTemplate = "{strResidentDynamicMoveId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentMigrateLogPK> Delete(string strResidentDynamicMoveId)
        {
            ModelInvokeResult<ResidentMigrateLogPK> result = new ModelInvokeResult<ResidentMigrateLogPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid();
                if (_ResidentDynamicMoveId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ResidentMigrateLogPK pk = new ResidentMigrateLogPK { ResidentDynamicMoveId = _ResidentDynamicMoveId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ResidentMigrateLog().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentMigrateLogPK { ResidentDynamicMoveId = _ResidentDynamicMoveId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strResidentDynamicMoveIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strResidentDynamicMoveIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentDynamicMoveIds = strResidentDynamicMoveIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentDynamicMoveIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ResidentMigrateLog().GetDeleteMethodName();
                foreach (string strResidentDynamicMoveId in arrResidentDynamicMoveIds)
                {
                    ResidentMigrateLogPK pk = new ResidentMigrateLogPK { ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strResidentDynamicMoveId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ResidentMigrateLogPK> Nullify(string strResidentDynamicMoveId)
        {
            ModelInvokeResult<ResidentMigrateLogPK> result = new ModelInvokeResult<ResidentMigrateLogPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid();
                if (_ResidentDynamicMoveId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ResidentMigrateLog residentMigrateLog = new ResidentMigrateLog { ResidentDynamicMoveId = _ResidentDynamicMoveId };
                /***********************begin 自定义代码*******************/
                residentMigrateLog.OperatedBy = NormalSession.UserId.ToGuid();
                residentMigrateLog.OperatedOn = DateTime.Now;
                residentMigrateLog.EventTime = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = residentMigrateLog.GetUpdateMethodName(), ParameterObject = residentMigrateLog.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ResidentMigrateLogPK { ResidentDynamicMoveId = _ResidentDynamicMoveId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strResidentDynamicMoveIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strResidentDynamicMoveIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentDynamicMoveIds = strResidentDynamicMoveIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentDynamicMoveIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ResidentMigrateLog().GetUpdateMethodName();
                foreach (string strResidentDynamicMoveId in arrResidentDynamicMoveIds)
                {
                    ResidentMigrateLog residentMigrateLog = new ResidentMigrateLog { ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid()};
                    /***********************begin 自定义代码*******************/
                    residentMigrateLog.OperatedBy = NormalSession.UserId.ToGuid();
                    residentMigrateLog.OperatedOn = DateTime.Now;
                    residentMigrateLog.EventTime = DateTime.Now;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = residentMigrateLog.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ResidentMigrateLogPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strResidentDynamicMoveId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ResidentMigrateLog> Load(string strResidentDynamicMoveId)
        {
            ModelInvokeResult<ResidentMigrateLog> result = new ModelInvokeResult<ResidentMigrateLog> { Success = true };
            try
            {
                Guid? _ResidentDynamicMoveId = strResidentDynamicMoveId.ToGuid();
                if (_ResidentDynamicMoveId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var residentMigrateLog = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ResidentMigrateLog, ResidentMigrateLogPK>(new ResidentMigrateLogPK { ResidentDynamicMoveId = _ResidentDynamicMoveId });
                result.instance = residentMigrateLog;
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

