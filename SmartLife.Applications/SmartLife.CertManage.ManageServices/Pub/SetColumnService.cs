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
    public class SetColumnService : AppBaseWcfService
    {
        #region 查询列表 Query
        [WebGet(UriTemplate = "Query?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Column> Query(string strParms)
        {
            CollectionInvokeResult<Column> result = new CollectionInvokeResult<Column> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<Column>(dictionary);
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
        public ModelInvokeResult<ColumnPK> Create(Column columnOjb)
        {
            ModelInvokeResult<ColumnPK> result = new ModelInvokeResult<ColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                statements.Add(new IBatisNetBatchStatement { StatementName = columnOjb.GetCreateMethodName(), ParameterObject = columnOjb.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ColumnPK { TableName = columnOjb.TableName, ColumnName = columnOjb.ColumnName };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建_自定义(strParam格式:TableName.ColumnName,TableName.ColumnName......)
        [WebInvoke(UriTemplate = "Create2/{strParam}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Create2(string strParam)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                StringObjectDictionary param = new StringObjectDictionary();
                param.Add("ColumnString", strParam.Trim());
                param.Add("OperatedBy", NormalSession.UserId.ToGuid());
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pub_InsertColumn", param);
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
        [WebInvoke(UriTemplate = "{strTableName},{strColumnName}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ColumnPK> Update(string strTableName, string strColumnName, Column columnObj)
        {
            ModelInvokeResult<ColumnPK> result = new ModelInvokeResult<ColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if ((string.IsNullOrEmpty(strTableName) || strTableName == "undefined") || (string.IsNullOrEmpty(strColumnName) || strColumnName == "undefined"))
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                columnObj.TableName = strTableName;
                columnObj.ColumnName = strColumnName;
                columnObj.OperatedBy = NormalSession.UserId.ToGuid();
                columnObj.OperatedOn = DateTime.Now;

                statements.Add(new IBatisNetBatchStatement { StatementName = columnObj.GetUpdateMethodName(), ParameterObject = columnObj.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ColumnPK { TableName = strTableName, ColumnName = strColumnName };
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
        [WebInvoke(UriTemplate = "{strColumnName},{strTableName}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ColumnPK> Delete(string strColumnName, string strTableName)
        {
            ModelInvokeResult<ColumnPK> result = new ModelInvokeResult<ColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _ColumnName = strColumnName;
                string _TableName = strTableName;
                ColumnPK pk = new ColumnPK { ColumnName = _ColumnName, TableName = _TableName };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Column().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                //BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ColumnPK { ColumnName = _ColumnName, TableName = _TableName };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strTableName},{strColumnNames}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strTableName, string strColumnNames)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnNames = strColumnNames.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnNames.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Column().GetDeleteMethodName();
                foreach (string strColumnName in arrColumnNames)
                {
                    ColumnPK pk = new ColumnPK { ColumnName = strColumnName, TableName = strTableName };
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                    DeleteCascade(statements, pk);
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

        #region 删除所选 DeleteSelected2
        [WebInvoke(UriTemplate = "DeleteSelected2/{strColumnNameTableNames}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected2(string strColumnNameTableNames)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnNameTableNames = strColumnNameTableNames.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnNameTableNames.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Column().GetDeleteMethodName();
                foreach (string strColumnNameTableName in arrColumnNameTableNames)
                {
                    string[] arrColumnNameTableName = strColumnNameTableName.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    ColumnPK pk = new ColumnPK { ColumnName = arrColumnNameTableName[0], TableName = arrColumnNameTableName[1] };
                    DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk, Type = SqlExecuteType.DELETE });
                }
                //BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 批量作废
        [WebInvoke(UriTemplate = "NullifySelected/{strTableName},{strColumnNames}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strTableName, string strColumnNames)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnNames = strColumnNames.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnNames.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                foreach (string strColumnName in arrColumnNames)
                {
                    Column newColumn = new Column
                    {
                        TableName = strTableName,
                        ColumnName = strColumnName,
                        Status = 0,
                        OperatedBy = NormalSession.UserId.ToGuid(),
                        OperatedOn = DateTime.Now
                    };
                    //DeleteCascade(statements, pk);
                    statements.Add(new IBatisNetBatchStatement
                    {
                        StatementName = newColumn.GetUpdateMethodName(),
                        ParameterObject = newColumn.ToStringObjectDictionary(false),
                        Type = SqlExecuteType.UPDATE
                    });

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

        #region 载入 Load
        [WebGet(UriTemplate = "{tableName},{colName}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Column> Load(string colName, string tableName)
        {
            ModelInvokeResult<Column> result = new ModelInvokeResult<Column> { Success = true };
            try
            {
                var obj_column = BuilderFactory.DefaultBulder().Load<Column, ColumnPK>(new ColumnPK { ColumnName = colName, TableName = tableName });
                result.instance = obj_column;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 DBColumnListForEasyUIgrid
        [WebInvoke(UriTemplate = "DBColumnListForEasyUIgrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Column> List_DBColumnForEasyUIgrid(EasyUIgridCollectionParam<Column> param)
        {
            EasyUIgridCollectionInvokeResult<Column> result = new EasyUIgridCollectionInvokeResult<Column> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                //if (param.instance != null)
                //{
                //    filters = param.instance.ToStringObjectDictionary(false);
                //}
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        filters[field.key] = field.value;
                    }
                }
                /**********************************************************/
                IList<Column> columns = BuilderFactory.DefaultBulder().ExecuteSPForQuery<Column>("SP_Pub_GetDBColumn", filters);
                if (param.page == 0 && param.rows == 0)//不分页显示
                    result.rows = columns;
                else
                    gridCollectionPager.EasyUIgridDoPage<Column>(columns, param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Column> ListForEasyUIgrid(EasyUIgridCollectionParam<Column> param)
        {
            EasyUIgridCollectionInvokeResult<Column> result = new EasyUIgridCollectionInvokeResult<Column> { Success = true };
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
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                IList<Column> columns = BuilderFactory.DefaultBulder().List<Column>(filters);
                if (param.page == 0 && param.rows == 0)//不分页显示
                    result.rows = columns;
                else
                    gridCollectionPager.EasyUIgridDoPage<Column>(columns, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region --扩展服务--
        [WebGet(UriTemplate = "DBType", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DictItem> List_DBType()
        {
            CollectionInvokeResult<DictItem> result = new CollectionInvokeResult<DictItem> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<DictItem>("GetDBType", null);
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ColumnPK pk)
        {
            //SqlAdaper sqlAdaper = BuilderFactory.DefaultBulder().GetAdaper();
            //ISqlMapper mapper = sqlAdaper.BeginTransaction();
            //var obj = new object { };
            //List<IBatisNetBatchStatement> mapStatement = new List<IBatisNetBatchStatement>();
            try
            {
                //设置需要操作的参数
                string strParms = "{'TableName':'" + pk.TableName + "'}";
                //取得当前表非当前Id的数据
                string result=DeleteSelectedTable(strParms, statements.Count);
                if (result!=null) {
                    DeleteSelectedTableJoin(strParms);
                }
            }
            catch (Exception ex)
            {
                //sqlAdaper.RollBackTransaction(mapper);
                throw new Exception("级联删除出错",ex);
            }
        }
        #endregion

        #region 删除所选公共列对应的公共表
        public string DeleteSelectedTable(string strParms,int count)
        {
            CollectionInvokeResult<Column> strColumnResult = new CollectionInvokeResult<Column> { Success = true };
            ModelInvokeResult<TablePK> result = new ModelInvokeResult<TablePK> { Success = true };
            string str = null;
            try
            {
                //查询表内数据是否等于1，是则删除操作
                strColumnResult = Query(strParms);
                if (strColumnResult.rows.Count == count)
                {
                    string strTableId = strColumnResult.rows[0].TableName;
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    string _TableName = strTableId;
                    TablePK pk = new TablePK { TableName = _TableName };
                    statements.Add(new IBatisNetBatchStatement { StatementName = new Table().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    str = _TableName;
                }
                return str;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                throw new Exception("删除公共表出错");
            }
        }
        #endregion

        #region 删除所选公共列的表对应的表关系
        public void DeleteSelectedTableJoin(string strParms)
        {
            CollectionInvokeResult<TableJoin> tableJoinResult = new CollectionInvokeResult<TableJoin> { Success = true };
            List<IBatisNetBatchStatement> tableJoinStatements = new List<IBatisNetBatchStatement>();
            try
            {
                //InvokeResult invokeResultResult = new InvokeResult { Success = true };
                //查询表关系Id进行删除操作
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                tableJoinResult.rows = BuilderFactory.DefaultBulder().List<TableJoin>(dictionary);
                //得到id
                for (int i = 0; i < tableJoinResult.rows.Count; i++)
                {
                    string statementName = new TableJoin().GetDeleteMethodName();
                    TableJoinPK pk2 = new TableJoinPK { Id = tableJoinResult.rows[i].Id };
                    tableJoinStatements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = pk2, Type = SqlExecuteType.DELETE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(tableJoinStatements);
            }
            catch (Exception ex)
            {
                tableJoinResult.Success = false;
                tableJoinResult.ErrorMessage = ex.Message;
                throw new Exception("删除表关系出错");
            }
        }
        #endregion
    }
    

    //#region 批量操作
    //public class BatchSmsSend {
    //    public int Rows { get; set; }
    //    public int PageSize { get; set; }
    //    public string Mobiles { get; set; }
    //    public string SendContent { get; set; }
    //    public string SourceCatalog { get; set; }
    //}
    //#endregion
}