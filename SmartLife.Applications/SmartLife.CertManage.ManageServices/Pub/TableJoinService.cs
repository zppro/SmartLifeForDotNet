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
using SmartLife.Share.Models.Bas;

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class TableJoinService : AppBaseWcfService
    {

        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<TableJoin> List()
        {
            CollectionInvokeResult<TableJoin> result = new CollectionInvokeResult<TableJoin> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<TableJoin>();
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
        public CollectionInvokeResult<TableJoin> Query(string strParms)
        {
            CollectionInvokeResult<TableJoin> result = new CollectionInvokeResult<TableJoin> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<TableJoin>(dictionary);
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
        public JqgridCollectionInvokeResult<TableJoin> ListForJqgrid(JqgridCollectionParam<TableJoin> param)
        {
            JqgridCollectionInvokeResult<TableJoin> result = new JqgridCollectionInvokeResult<TableJoin> { Success = true };
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

                gridCollectionPager.JqgridDoPage<TableJoin>(BuilderFactory.DefaultBulder().List<TableJoin>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<TableJoin> ListForEasyUIgrid(EasyUIgridCollectionParam<TableJoin> param)
        {
            EasyUIgridCollectionInvokeResult<TableJoin> result = new EasyUIgridCollectionInvokeResult<TableJoin> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
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

                gridCollectionPager.EasyUIgridDoPage<TableJoin>(BuilderFactory.DefaultBulder().List<TableJoin>(filters), param, ref result);
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
        public ModelInvokeResult<TableJoinPK> Create(TableJoin tableJoinInfo)
        {
            ModelInvokeResult<TableJoinPK> result = new ModelInvokeResult<TableJoinPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (tableJoinInfo.Id == -1)
                {
                    tableJoinInfo.Id = null;
                }
                /***********************begin 自定义代码*******************/
                tableJoinInfo.OperatedBy = NormalSession.UserId.ToGuid();
                tableJoinInfo.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = tableJoinInfo.GetCreateMethodName(), ParameterObject = tableJoinInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TableJoinPK { };
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
        [WebInvoke(UriTemplate = "{strtableJoinId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TableJoinPK> Update(string strtableJoinId, TableJoin tableJoinInfo)
        {
            ModelInvokeResult<TableJoinPK> result = new ModelInvokeResult<TableJoinPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                tableJoinInfo.OperatedBy = NormalSession.UserId.ToGuid();
                tableJoinInfo.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = tableJoinInfo.GetUpdateMethodName(), ParameterObject = tableJoinInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TableJoinPK { };

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
        [WebInvoke(UriTemplate = "{strtableJoinId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TableJoinPK> Delete(string strtableJoinId)
        {
            ModelInvokeResult<TableJoinPK> result = new ModelInvokeResult<TableJoinPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int? _strtableJoinId = int.Parse(strtableJoinId);
                if (_strtableJoinId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                TableJoinPK pk = new TableJoinPK { Id = _strtableJoinId };
                //DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new TableJoin().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TableJoinPK { };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strtableJoinIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strtableJoinIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrTableJoinIds = strtableJoinIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrTableJoinIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new TableJoin().GetDeleteMethodName();
                foreach (string strtableJoinId in arrTableJoinIds)
                {
                    TableJoinPK pk = new TableJoinPK { Id = int.Parse(strtableJoinId) };
                    //DeleteCascade(statements, pk);
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


        #region 作废 Nullify
        [WebInvoke(UriTemplate = "Nullify/{strtableJoinId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TableJoinPK> Nullify(string strtableJoinId)
        {
            ModelInvokeResult<TableJoinPK> result = new ModelInvokeResult<TableJoinPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int? _strtableJoinId = int.Parse(strtableJoinId);
                if (_strtableJoinId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                TableJoin tableJoinInfo = new TableJoin { Id = _strtableJoinId, Status = 0 };
                /***********************begin 自定义代码*******************/
                tableJoinInfo.OperatedBy = NormalSession.UserId.ToGuid();
                tableJoinInfo.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = tableJoinInfo.GetUpdateMethodName(), ParameterObject = tableJoinInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TableJoinPK { };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strtableJoinIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strtableJoinIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrTableJoinIds = strtableJoinIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrTableJoinIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new TableJoin().GetUpdateMethodName();
                foreach (string strtableJoinId in arrTableJoinIds)
                {
                    TableJoin tableJoinInfo = new TableJoin { Id = int.Parse(strtableJoinId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    TableJoinPK pk = new TableJoinPK { Id = int.Parse(strtableJoinId) };
                    DeleteCascade(statements, pk, "update");
                    tableJoinInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    tableJoinInfo.OperatedOn = DateTime.Now;
                    
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = tableJoinInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, TableJoinPK pk, string type)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strtableJoinId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<TableJoin> Load(string strtableJoinId)
        {
            ModelInvokeResult<TableJoin> result = new ModelInvokeResult<TableJoin> { Success = true };
            try
            {
                int? _strtableJoinId = int.Parse(strtableJoinId);
                if (_strtableJoinId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var tableJoinInfo = BuilderFactory.DefaultBulder().Load<TableJoin, TableJoinPK>(new TableJoinPK { Id = _strtableJoinId });
                result.instance = tableJoinInfo;
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

        #region 查询是否已经存在表1表2 QueryDuplicates
        [WebGet(UriTemplate = "QueryDuplicates?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<TableJoin> QueryDuplicates(string strParms)
        {
            CollectionInvokeResult<TableJoin> result = new CollectionInvokeResult<TableJoin> { Success = true };
            StringObjectDictionary filters = new StringObjectDictionary();
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                filters.Add("WhereClause", "((TableName1='" + dictionary["TableName1"] + "' and TableName2='" + dictionary["TableName2"] + "') or (TableName2='" + dictionary["TableName1"] + "' and TableName1='" + dictionary["TableName2"] + "')) and Status=1 and Id != " + dictionary["Id"]);
                result.rows = BuilderFactory.DefaultBulder().List<TableJoin>(filters);
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