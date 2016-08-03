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
    public class ArticleColumn_RelationService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ArticleColumn_Relation> List()
        {
            CollectionInvokeResult<ArticleColumn_Relation> result = new CollectionInvokeResult<ArticleColumn_Relation> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn_Relation>();
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
        public CollectionInvokeResult<ArticleColumn_Relation> Query(string strParms)
        {
            CollectionInvokeResult<ArticleColumn_Relation> result = new CollectionInvokeResult<ArticleColumn_Relation> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn_Relation>(dictionary);
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
        public JqgridCollectionInvokeResult<ArticleColumn_Relation> ListForJqgrid(JqgridCollectionParam<ArticleColumn_Relation> param)
        {
            JqgridCollectionInvokeResult<ArticleColumn_Relation> result = new JqgridCollectionInvokeResult<ArticleColumn_Relation> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ArticleColumn_Relation>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn_Relation>(filters), param, ref result);
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
        [WebInvoke(UriTemplate = "ListForEasyUIgrid/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<ArticleColumn_Relation> ListForEasyUIgrid(string connectId,EasyUIgridCollectionParam<ArticleColumn_Relation> param)
        {
            EasyUIgridCollectionInvokeResult<ArticleColumn_Relation> result = new EasyUIgridCollectionInvokeResult<ArticleColumn_Relation> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ArticleColumn_Relation>(BuilderFactory.DefaultBulder(connectId).List<ArticleColumn_Relation>(filters), param, ref result);
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
        public ModelInvokeResult<ArticleColumn_RelationPK> Create(ArticleColumn_Relation articleColumn)
        {
            ModelInvokeResult<ArticleColumn_RelationPK> result = new ModelInvokeResult<ArticleColumn_RelationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetCreateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumn_RelationPK { Id = articleColumn.Id };
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
        public ModelInvokeResult<ArticleColumn_RelationPK> Update(string strId, ArticleColumn_Relation articleColumn)
        {
            ModelInvokeResult<ArticleColumn_RelationPK> result = new ModelInvokeResult<ArticleColumn_RelationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                int _Id = int.Parse(strId);
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetUpdateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumn_RelationPK { Id = _Id };

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
        public ModelInvokeResult<ArticleColumn_RelationPK> Delete(string strId)
        {
            ModelInvokeResult<ArticleColumn_RelationPK> result = new ModelInvokeResult<ArticleColumn_RelationPK> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                ArticleColumn_RelationPK pk = new ArticleColumn_RelationPK { Id = _Id };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ArticleColumn_Relation().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumn_RelationPK { Id = _Id };
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
                string statementName = new ArticleColumn_Relation().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    ArticleColumn_RelationPK pk = new ArticleColumn_RelationPK { Id = int.Parse(strId) };
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
        [WebInvoke(UriTemplate = "Nullify/{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticleColumn_RelationPK> Nullify(string strId)
        {
            ModelInvokeResult<ArticleColumn_RelationPK> result = new ModelInvokeResult<ArticleColumn_RelationPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = int.Parse(strId);
                ArticleColumn_Relation articleColumn = new ArticleColumn_Relation { Id = _Id, Status = 0 };
                /***********************begin 自定义代码*******************/
                articleColumn.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn.GetUpdateMethodName(), ParameterObject = articleColumn.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumn_RelationPK { Id = _Id };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strIds)
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
                string statementName = new ArticleColumn_Relation().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    ArticleColumn_Relation articleColumn = new ArticleColumn_Relation { Id = int.Parse(strId), Status = 0 };
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ArticleColumn_RelationPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ArticleColumn_Relation> Load(string strId)
        {
            ModelInvokeResult<ArticleColumn_Relation> result = new ModelInvokeResult<ArticleColumn_Relation> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                var articleColumn = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ArticleColumn_Relation, ArticleColumn_RelationPK>(new ArticleColumn_RelationPK { Id = _Id });
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

        #region 批量创建 Create2
        [WebInvoke(UriTemplate = "Create2/{ArticleId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Create2(string articleId, IList<string> columnIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                ArticleColumn_Relation articleColumn_relation = new ArticleColumn_Relation();
                
                /***********************begin 自定义代码*******************/
                articleColumn_relation.ArticleId = articleId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn_relation.GetDeleteMethodName(), ParameterObject = articleColumn_relation.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                articleColumn_relation.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumn_relation.OperatedOn = DateTime.Now;
                foreach (var item in columnIds)
                {
                    articleColumn_relation.ColumnId = item.ToGuid(); ;
                    articleColumn_relation.Status = 1;
                    articleColumn_relation.OperatedBy = NormalSession.UserId.ToGuid();
                    articleColumn_relation.OperatedOn = DateTime.Now;

                    statements.Add(new IBatisNetBatchStatement { StatementName = articleColumn_relation.GetCreateMethodName(), ParameterObject = articleColumn_relation.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                /***********************end 自定义代码*********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                //result.instance = new ArticleColumn_RelationPK { Id = articleColumn.Id };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
    }
}

