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

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SetTableService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Table> List()
        {
            CollectionInvokeResult<Table> result = new CollectionInvokeResult<Table> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<Table>();
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
        public CollectionInvokeResult<Table> Query(string strParms)
        {
            CollectionInvokeResult<Table> result = new CollectionInvokeResult<Table> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Table>(dictionary);
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
        public JqgridCollectionInvokeResult<Table> ListForJqgrid(JqgridCollectionParam<Table> param)
        {
            JqgridCollectionInvokeResult<Table> result = new JqgridCollectionInvokeResult<Table> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Table>(BuilderFactory.DefaultBulder().List<Table>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Table> ListForEasyUIgrid(EasyUIgridCollectionParam<Table> param)
        {
            EasyUIgridCollectionInvokeResult<Table> result = new EasyUIgridCollectionInvokeResult<Table> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<Table>(BuilderFactory.DefaultBulder().List<Table>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建 Create(在公共列新增时通过存储过程来新增,未在页面实现新增)
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TablePK> Create(Table TableInfo)
        {
            ModelInvokeResult<TablePK> result = new ModelInvokeResult<TablePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (string.IsNullOrEmpty(TableInfo.TableName))//主键
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                TableInfo.OperatedBy = NormalSession.UserId.ToGuid();
                TableInfo.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = TableInfo.GetCreateMethodName(), ParameterObject = TableInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TablePK { TableName = TableInfo .TableName};
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
        [WebInvoke(UriTemplate = "{strTableId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TablePK> Update(string strTableId, Table TableInfo)
        {
            ModelInvokeResult<TablePK> result = new ModelInvokeResult<TablePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (strTableId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                TableInfo.OperatedBy = NormalSession.UserId.ToGuid();
                TableInfo.OperatedOn = DateTime.Now;
                TableInfo.TableName = strTableId;
                statements.Add(new IBatisNetBatchStatement { StatementName = TableInfo.GetUpdateMethodName(), ParameterObject = TableInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TablePK { TableName = strTableId };
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
        [WebInvoke(UriTemplate = "{strTableId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TablePK> Delete(string strTableId)
        {
            ModelInvokeResult<TablePK> result = new ModelInvokeResult<TablePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _Id = strTableId;
                if (_Id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                TablePK pk = new TablePK { TableName = _Id };
                //DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Table().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = pk;
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strSetIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strSetIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = strSetIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Table().GetDeleteMethodName();
                foreach (string strTableId in arrResidentIds)
                {
                    TablePK pk = new TablePK { TableName = strTableId };
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

        #region 作废 Nullify(页面公用Nullify是调用批作废的，想单独使用调用nullifyTo(url, success, options, headers)
        [WebInvoke(UriTemplate = "Nullify/{strTableId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<TablePK> Nullify(string strTableId)
        {
            ModelInvokeResult<TablePK> result = new ModelInvokeResult<TablePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _Id = strTableId;
                if (_Id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                Table TableInfo = new Table { TableName = _Id, Status = 0 };
                /***********************begin 自定义代码*******************/
                TableInfo.OperatedBy = NormalSession.UserId.ToGuid();
                TableInfo.OperatedOn = DateTime.Now;
                //TableInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = TableInfo.GetUpdateMethodName(), ParameterObject = TableInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new TablePK { TableName = _Id };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strSetIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strSetIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrResidentIds = strSetIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrResidentIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Table().GetUpdateMethodName();
                foreach (string strTableId in arrResidentIds)
                {
                    Table TableInfo = new Table { TableName = strTableId, Status = 0 };
                    /***********************begin 自定义代码*******************/
                    TablePK pk = new TablePK { TableName = strTableId };
                    //DeleteCascade(statements, pk, "update");//级联操作，暂未实现
                    TableInfo.OperatedBy = NormalSession.UserId.ToGuid();
                    TableInfo.OperatedOn = DateTime.Now;
                    //TableInfo.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = TableInfo.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, TablePK pk, string type)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strTableId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Table> Load(string strTableId)
        {
            ModelInvokeResult<Table> result = new ModelInvokeResult<Table> { Success = true };
            try
            {
                string _Id = strTableId;
                if (_Id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var TableInfo = BuilderFactory.DefaultBulder().Load<Table, TablePK>(new TablePK { TableName = _Id });
                result.instance = TableInfo;
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