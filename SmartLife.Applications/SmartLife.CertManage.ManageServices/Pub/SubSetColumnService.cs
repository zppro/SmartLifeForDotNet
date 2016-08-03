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
    public class SubSetColumnService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SubSetColumn> List()
        {
            CollectionInvokeResult<SubSetColumn> result = new CollectionInvokeResult<SubSetColumn> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<SubSetColumn>();
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
        public CollectionInvokeResult<SubSetColumn> Query(string strParms)
        {
            CollectionInvokeResult<SubSetColumn> result = new CollectionInvokeResult<SubSetColumn> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SubSetColumn>(dictionary);
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
        public JqgridCollectionInvokeResult<SubSetColumn> ListForJqgrid(JqgridCollectionParam<SubSetColumn> param)
        {
            JqgridCollectionInvokeResult<SubSetColumn> result = new JqgridCollectionInvokeResult<SubSetColumn> { Success = true };
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

                gridCollectionPager.JqgridDoPage<SubSetColumn>(BuilderFactory.DefaultBulder().List<SubSetColumn>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<SubSetColumn> ListForEasyUIgrid(EasyUIgridCollectionParam<SubSetColumn> param)
        {
            EasyUIgridCollectionInvokeResult<SubSetColumn> result = new EasyUIgridCollectionInvokeResult<SubSetColumn> { Success = true };
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

                IList<SubSetColumn> subSetColumn = BuilderFactory.DefaultBulder().List<SubSetColumn>(filters);
                if (param.page == 0 && param.rows == 0)//不分页显示
                    result.rows = subSetColumn;
                else
                    gridCollectionPager.EasyUIgridDoPage<SubSetColumn>(subSetColumn, param, ref result);
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
        public ModelInvokeResult<SubSetColumnPK> Create(SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (subSetColumn.ColumnNameNew == "自动生成")
                {
                    subSetColumn.ColumnNameNew = GlobalManager.getPK(subSetColumn.GetMappingTableName(), "ColumnNameNew");
                }
                /***********************begin 自定义代码*******************/
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetCreateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = subSetColumn.SubSetId, ColumnNameNew = subSetColumn.ColumnNameNew };
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
        [WebInvoke(UriTemplate = "{strSubSetId},{strColumnNameNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> Update(string strSubSetId, string strColumnNameNew, SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                subSetColumn.SubSetId = _SubSetId;

                string _ColumnNameNew = subSetColumn.ColumnNameNew;//双主键中的一个可更新时
                StringObjectDictionary dictionary = subSetColumn.ToStringObjectDictionary(false);
                dictionary.Add("Old_ColumnNameNew", strColumnNameNew);
                
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                statements.Add(new IBatisNetBatchStatement { StatementName = "SubSetColumn_Update2", ParameterObject = dictionary, Type = SqlExecuteType.UPDATE });
                
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };

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
        [WebInvoke(UriTemplate = "{strSubSetId},{strColumnNameNew}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> Delete(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                SubSetColumnPK pk = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new SubSetColumn().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strSubSetId},{strColumnNameNews}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strSubSetId, string strColumnNameNews)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnNameNews = strColumnNameNews.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnNameNews.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new SubSetColumn().GetDeleteMethodName();
                foreach (string strColumnNameNew in arrColumnNameNews)
                {
                    SubSetColumnPK pk = new SubSetColumnPK { SubSetId = strSubSetId, ColumnNameNew = strColumnNameNew };
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

        #region 删除所选 DeleteSelected2
        [WebInvoke(UriTemplate = "DeleteSelected2/{strSubSetIdColumnNameNews}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected2(string strSubSetIdColumnNameNews)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrSubSetIdColumnNameNews = strSubSetIdColumnNameNews.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrSubSetIdColumnNameNews.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new SubSetColumn().GetDeleteMethodName();
                foreach (string strSubSetIdColumnNameNew in arrSubSetIdColumnNameNews)
                {
                    string[] arrSubSetIdColumnNameNew = strSubSetIdColumnNameNew.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    SubSetColumnPK pk = new SubSetColumnPK { SubSetId = arrSubSetIdColumnNameNew[0], ColumnNameNew = arrSubSetIdColumnNameNew[1] };
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

        #region 作废 Nullify
        [WebInvoke(UriTemplate = "Nullify/{strSubSetId},{strColumnNameNew}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> Nullify(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                SubSetColumn subSetColumn = new SubSetColumn { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew, Status = 0 };
                /***********************begin 自定义代码*******************/
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetUpdateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strSubSetId},{strColumnNameNews}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strSubSetId, string strColumnNameNews)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnNameNews = strColumnNameNews.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnNameNews.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new SubSetColumn().GetUpdateMethodName();
                string _SubSetId = strSubSetId;
                foreach (string strColumnNameNew in arrColumnNameNews)
                {
                    string _ColumnNameNew = strColumnNameNew;
                    SubSetColumn subSetColumn = new SubSetColumn { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew, Status = 0 };
                    /***********************begin 自定义代码*******************/
                    subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                    subSetColumn.OperatedOn = DateTime.Now;

                    //SubSetColumnPK pk = new SubSetColumnPK { SubSetId = strSubSetId, ColumnNameNew = strColumnNameNews };
                    //DeleteCascade(statements, pk);

                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, SubSetColumnPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strSubSetId},{strColumnNameNew}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<SubSetColumn> Load(string strSubSetId, string strColumnNameNew)
        {
            ModelInvokeResult<SubSetColumn> result = new ModelInvokeResult<SubSetColumn> { Success = true };
            try
            {
                string _SubSetId = strSubSetId;
                string _ColumnNameNew = strColumnNameNew;
                var subSetColumn = BuilderFactory.DefaultBulder().Load<SubSetColumn, SubSetColumnPK>(new SubSetColumnPK { SubSetId = _SubSetId, ColumnNameNew = _ColumnNameNew });
                result.instance = subSetColumn;
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

        #region 修改新列名
        [WebInvoke(UriTemplate = "UpdateColumnNameNew/{Id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumn> UpdateColumnNameNew(string Id, SubSetColumn subSetColumn)
        {
            ModelInvokeResult<SubSetColumn> result = new ModelInvokeResult<SubSetColumn> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                subSetColumn.Id = int.Parse(Id);
                subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                subSetColumn.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "SubSetColumn_UpdateColumnNameNew", ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SubSetColumn { Id=int.Parse(Id) };

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 创建多个 Create2(strParam格式:TableName.ColumnName.ColumnNameNew.WidgetId.SubSetId.ColumnCNameNew...)
        [WebInvoke(UriTemplate = "Create2/{strParam}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<SubSetColumnPK> Create2(string strParam)
        {
            ModelInvokeResult<SubSetColumnPK> result = new ModelInvokeResult<SubSetColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                SubSetColumn subSetColumn = new SubSetColumn();
                if (strParam != "")
                {
                    string[] param = strParam.Trim().Split(new char[] { ',' });
                    for (int i = 0; i < param.Length; i++) {
                        subSetColumn = new SubSetColumn();
                        subSetColumn.OperatedBy = NormalSession.UserId.ToGuid();
                        subSetColumn.OperatedOn = DateTime.Now;
                        subSetColumn.OrderNo = 0;
                        subSetColumn.ComputeColumnFlag = 0;

                        string[] param1 = param[i].Split(new char[] { '.' });

                        subSetColumn.TableName = param1[0];
                        subSetColumn.ColumnName = param1[1];
                        subSetColumn.ColumnNameNew = param1[2];
                        subSetColumn.WidgetId = param1[3];
                        subSetColumn.SubSetId = param1[4];
                        subSetColumn.ColumnCNameNew = param1[5];
                        statements.Add(new IBatisNetBatchStatement { StatementName = subSetColumn.GetCreateMethodName(), ParameterObject = subSetColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    result.instance = new SubSetColumnPK { SubSetId = subSetColumn.SubSetId, ColumnNameNew = subSetColumn.ColumnNameNew };
                }
                else {
                    result.Success = false;
                    result.ErrorMessage = "无参数传递";
                }
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

