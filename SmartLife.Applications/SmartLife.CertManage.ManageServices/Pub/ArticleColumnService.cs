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
    public class ArticleColumnService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ArticleColumn> List()
        {
            CollectionInvokeResult<ArticleColumn> result = new CollectionInvokeResult<ArticleColumn> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>();
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
        public CollectionInvokeResult<ArticleColumn> Query(string strParms)
        {
            CollectionInvokeResult<ArticleColumn> result = new CollectionInvokeResult<ArticleColumn> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>(dictionary);
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
        public JqgridCollectionInvokeResult<ArticleColumn> ListForJqgrid(JqgridCollectionParam<ArticleColumn> param)
        {
            JqgridCollectionInvokeResult<ArticleColumn> result = new JqgridCollectionInvokeResult<ArticleColumn> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ArticleColumn>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ArticleColumn> ListForEasyUIgrid( EasyUIgridCollectionParam<ArticleColumn> param)
        {
            EasyUIgridCollectionInvokeResult<ArticleColumn> result = new EasyUIgridCollectionInvokeResult<ArticleColumn> { Success = true };
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
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<ArticleColumn>(BuilderFactory.DefaultBulder().List<ArticleColumn>(filters), param, ref result);
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
        public ModelInvokeResult<ArticleColumnPK> Create(ArticleColumn articleColumn)
        {
            ModelInvokeResult<ArticleColumnPK> result = new ModelInvokeResult<ArticleColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (articleColumn.ColumnId == GlobalManager.GuidAsAutoGenerate)
                {
                    articleColumn.ColumnId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetCreateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPK { ColumnId = articleColumn.ColumnId };
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
        [WebInvoke(UriTemplate = "{strColumnId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticleColumnPK> Update(string strColumnId, ArticleColumn articleColumn)
        {
            ModelInvokeResult<ArticleColumnPK> result = new ModelInvokeResult<ArticleColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ColumnId = strColumnId.ToGuid();
                if (_ColumnId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                articleColumn.ColumnId = _ColumnId;
                /***********************begin 自定义代码*******************/
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetUpdateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPK { ColumnId = _ColumnId };

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
        [WebInvoke(UriTemplate = "{strColumnId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticleColumnPK> Delete(string strColumnId)
        {
            ModelInvokeResult<ArticleColumnPK> result = new ModelInvokeResult<ArticleColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ColumnId = strColumnId.ToGuid();
                if (_ColumnId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ArticleColumnPK pk = new ArticleColumnPK { ColumnId = _ColumnId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ArticleColumn().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPK { ColumnId = _ColumnId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strColumnIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strColumnIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnIds = strColumnIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ArticleColumn().GetDeleteMethodName();
                foreach (string strColumnId in arrColumnIds)
                {
                    ArticleColumnPK pk = new ArticleColumnPK { ColumnId = strColumnId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strColumnId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticleColumnPK> Nullify(string strColumnId)
        {
            ModelInvokeResult<ArticleColumnPK> result = new ModelInvokeResult<ArticleColumnPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ColumnId = strColumnId.ToGuid();
                if (_ColumnId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ArticleColumn articleColumn = new ArticleColumn { ColumnId = _ColumnId, Status = 0 };
                /***********************begin 自定义代码*******************/
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetUpdateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPK { ColumnId = _ColumnId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strColumnIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strColumnIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrColumnIds = strColumnIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrColumnIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new ArticleColumn().GetUpdateMethodName();
                foreach (string strColumnId in arrColumnIds)
                {
                    ArticleColumn articleColumn = new ArticleColumn { ColumnId = strColumnId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                    articleColumn.OperatedOn = DateTime.Now;
                    
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ArticleColumnPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strColumnId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ArticleColumn> Load(string strColumnId)
        {
            ModelInvokeResult<ArticleColumn> result = new ModelInvokeResult<ArticleColumn> { Success = true };
            try
            {
                Guid? _ColumnId = strColumnId.ToGuid();
                if (_ColumnId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var articleColumn = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ArticleColumn, ArticleColumnPK>(new ArticleColumnPK { ColumnId = _ColumnId });
                result.instance = articleColumn;
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

        #region 自定义业务接口

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid 有ConnectId
        [WebInvoke(UriTemplate = "ListForEasyUIgrid_ConnectId/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ArticleColumn> ListForEasyUIgrid_ConnectId(string connectId, EasyUIgridCollectionParam<ArticleColumn> param)
        {
            EasyUIgridCollectionInvokeResult<ArticleColumn> result = new EasyUIgridCollectionInvokeResult<ArticleColumn> { Success = true };
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
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                /***********************end 排序***************************/

                gridCollectionPager.EasyUIgridDoPage<ArticleColumn>(BuilderFactory.DefaultBulder(connectId).List<ArticleColumn>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 权限查询列表 Query2
        [WebGet(UriTemplate = "Query2?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ArticleColumn> Query2(string strParms)
        {
            CollectionInvokeResult<ArticleColumn> result = new CollectionInvokeResult<ArticleColumn> { Success = true };
            try
            {
                User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                if (!currentUser.isSuperAdmin())
                {
                    var dictionary = new StringObjectDictionary().MixInJson(strParms);
                    string getColumnId;
                    string getGroupId = "select  a.GroupId from Pub_Group a,Pub_GroupMember b,Pub_User c where a.GroupId=b.GroupId and b.UserId=c.UserId and c.UserId='" + NormalSession.UserId.ToGuid() + "'";
                    IList<StringObjectDictionary> groupIds = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(getGroupId);
                    if (groupIds.Count > 0) {
                        IList<ArticleColumn> articleColumnList = new List<ArticleColumn>();
                        foreach (var groupId in groupIds) {
                            getColumnId = "select distinct a.ColumnId,b.COL_Name,b.OrderNo from Pub_ArticleColumnPermit a,Pub_ArticleColumn b where a.ColumnId=b.ColumnId and a.PermitType=" + dictionary["PermitType"] + " and a.Status=" + dictionary["Status"] + " and b.AreaId='" + dictionary["AreaId"] + "' and OBJ_ID='" + groupId["GroupId"] + "' order by OrderNo asc";
                            IList<StringObjectDictionary> columnIds = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlForQuery(getColumnId);
                            if (columnIds.Count > 0)
                            {
                                foreach (var columnId in columnIds)
                                {
                                    if (articleColumnList.Count(s => s.ColumnId == columnId["ColumnId"].ToString().ToGuid()) > 0)
                                    {
                                        continue;
                                    }
                                    articleColumnList.Add(new ArticleColumn { ColumnId = columnId["ColumnId"].ToString().ToGuid(), COL_Name = columnId["COL_Name"].ToString() });
                                }
                               
                            }
                        }
                        result.rows = articleColumnList;
                    }
                }
                else
                {
                    var dictionary = new StringObjectDictionary().MixInJson(strParms);
                    dictionary.Remove("PermitType");
                    result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>(dictionary);
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

