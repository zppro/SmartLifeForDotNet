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
    public class ArticleColumnPermitService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ArticleColumnPermit> List()
        {
            CollectionInvokeResult<ArticleColumnPermit> result = new CollectionInvokeResult<ArticleColumnPermit> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ArticleColumnPermit>();
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
        public CollectionInvokeResult<ArticleColumnPermit> Query(string strParms)
        {
            CollectionInvokeResult<ArticleColumnPermit> result = new CollectionInvokeResult<ArticleColumnPermit> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<ArticleColumnPermit>(dictionary);
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
        public JqgridCollectionInvokeResult<ArticleColumnPermit> ListForJqgrid(JqgridCollectionParam<ArticleColumnPermit> param)
        {
            JqgridCollectionInvokeResult<ArticleColumnPermit> result = new JqgridCollectionInvokeResult<ArticleColumnPermit> { Success = true };
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

                gridCollectionPager.JqgridDoPage<ArticleColumnPermit>(BuilderFactory.DefaultBulder().List<ArticleColumnPermit>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<ArticleColumnPermit> ListForEasyUIgrid(EasyUIgridCollectionParam<ArticleColumnPermit> param)
        {
            EasyUIgridCollectionInvokeResult<ArticleColumnPermit> result = new EasyUIgridCollectionInvokeResult<ArticleColumnPermit> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<ArticleColumnPermit>(BuilderFactory.DefaultBulder().List<ArticleColumnPermit>(filters), param, ref result);
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
        public ModelInvokeResult<ArticleColumnPermitPK> Create(ArticleColumnPermit articleColumnPermit)
        {
            ModelInvokeResult<ArticleColumnPermitPK> result = new ModelInvokeResult<ArticleColumnPermitPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (articleColumnPermit.Id == 0)
                {
                    articleColumnPermit.Id = int.Parse(GlobalManager.getPK(articleColumnPermit.GetMappingTableName(), "Id"));
                }
                /***********************begin 自定义代码*******************/
                articleColumnPermit.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumnPermit.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumnPermit.GetCreateMethodName(), ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPermitPK { Id = articleColumnPermit.Id };
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
        public ModelInvokeResult<ArticleColumnPermitPK> Update(string strId, ArticleColumnPermit articleColumnPermit)
        {
            ModelInvokeResult<ArticleColumnPermitPK> result = new ModelInvokeResult<ArticleColumnPermitPK> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                articleColumnPermit.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumnPermit.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumnPermit.GetUpdateMethodName(), ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPermitPK { Id = _Id };

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
        public ModelInvokeResult<ArticleColumnPermitPK> Delete(string strId)
        {
            ModelInvokeResult<ArticleColumnPermitPK> result = new ModelInvokeResult<ArticleColumnPermitPK> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                ArticleColumnPermitPK pk = new ArticleColumnPermitPK { Id = _Id };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new ArticleColumnPermit().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPermitPK { Id = _Id };
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
                string statementName = new ArticleColumnPermit().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    ArticleColumnPermitPK pk = new ArticleColumnPermitPK { Id = int.Parse(strId) };
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
        [WebInvoke(UriTemplate = "Nullify/{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticleColumnPermitPK> Nullify(string strId)
        {
            ModelInvokeResult<ArticleColumnPermitPK> result = new ModelInvokeResult<ArticleColumnPermitPK> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                ArticleColumnPermit articleColumnPermit = new ArticleColumnPermit { Id = _Id, Status = 0 };
                /***********************begin 自定义代码*******************/
                articleColumnPermit.OperatedBy = NormalSession.UserId.ToGuid();
                articleColumnPermit.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumnPermit.GetUpdateMethodName(), ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticleColumnPermitPK { Id = _Id };
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
                string statementName = new ArticleColumnPermit().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    ArticleColumnPermit articleColumnPermit = new ArticleColumnPermit { Id = int.Parse(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    articleColumnPermit.OperatedBy = NormalSession.UserId.ToGuid();
                    articleColumnPermit.OperatedOn = DateTime.Now;
                    
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ArticleColumnPermitPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ArticleColumnPermit> Load(string strId)
        {
            ModelInvokeResult<ArticleColumnPermit> result = new ModelInvokeResult<ArticleColumnPermit> { Success = true };
            try
            {
                int _Id = int.Parse(strId);
                var articleColumnPermit = BuilderFactory.DefaultBulder().Load<ArticleColumnPermit, ArticleColumnPermitPK>(new ArticleColumnPermitPK { Id = _Id });
                result.instance = articleColumnPermit;
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

        #region 自定义方法

        #region 创建 SetUpColumnPermit 多条插入
        [WebInvoke(UriTemplate = "SetUpColumnPermit/{groupId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult SetUpColumnPermit2(string groupId, IList<string> permitIds)
        {
            InvokeResult result = new InvokeResult { Success = true };

            List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
            try
            {
                ArticleColumnPermit articleColumnPermit = new ArticleColumnPermit();
                articleColumnPermit.OBJ_ID = groupId.ToGuid();
                statements.Add(new IBatisNetBatchStatement { StatementName = articleColumnPermit.GetDeleteMethodName(), ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.DELETE });

                foreach (var item in permitIds)
                {
                    string[] stra = item.Split('_');
                    articleColumnPermit.ColumnId = stra[0].ToGuid();
                    articleColumnPermit.PermitType = byte.Parse(stra[1]);
                    articleColumnPermit.Category = 0;
                    articleColumnPermit.OperatedBy = NormalSession.UserId.ToGuid();
                    articleColumnPermit.OperatedOn = DateTime.Now;

                    statements.Add(new IBatisNetBatchStatement { StatementName = articleColumnPermit.GetCreateMethodName(), ParameterObject = articleColumnPermit.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
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

        #endregion
    }
}

