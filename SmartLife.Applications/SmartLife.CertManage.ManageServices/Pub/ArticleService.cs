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
    public class ArticleService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Article> List()
        {
            CollectionInvokeResult<Article> result = new CollectionInvokeResult<Article> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Article>();
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
        public CollectionInvokeResult<Article> Query(string strParms)
        {
            CollectionInvokeResult<Article> result = new CollectionInvokeResult<Article> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Article>(dictionary);
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
        public JqgridCollectionInvokeResult<Article> ListForJqgrid(JqgridCollectionParam<Article> param)
        {
            JqgridCollectionInvokeResult<Article> result = new JqgridCollectionInvokeResult<Article> { Success = true };
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

                gridCollectionPager.JqgridDoPage<Article>(BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Article>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<Article> ListForEasyUIgrid(string connectId,EasyUIgridCollectionParam<Article> param)
        {
            EasyUIgridCollectionInvokeResult<Article> result = new EasyUIgridCollectionInvokeResult<Article> { Success = true };
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
                //gridCollectionPager.EasyUIgridDoPage4Model<Article>(filters, param, ref result,connectId);

                gridCollectionPager.EasyUIgridDoPage<Article>(BuilderFactory.DefaultBulder(connectId).List<Article>(filters), param, ref result);
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
        public ModelInvokeResult<ArticlePK> Create(Article article)
        {
            ModelInvokeResult<ArticlePK> result = new ModelInvokeResult<ArticlePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (article.ArticleId == GlobalManager.GuidAsAutoGenerate)
                {
                    article.ArticleId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                article.OperatedBy = NormalSession.UserId.ToGuid();
                article.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = article.GetCreateMethodName(), ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticlePK { ArticleId = article.ArticleId };
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
        [WebInvoke(UriTemplate = "{strArticleId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticlePK> Update(string strArticleId, Article article)
        {
            ModelInvokeResult<ArticlePK> result = new ModelInvokeResult<ArticlePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ArticleId = strArticleId.ToGuid();
                if (_ArticleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                article.ArticleId = _ArticleId;
                /***********************begin 自定义代码*******************/
                article.OperatedBy = NormalSession.UserId.ToGuid();
                article.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = article.GetUpdateMethodName(), ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticlePK { ArticleId = _ArticleId };

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
        [WebInvoke(UriTemplate = "{strArticleId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticlePK> Delete(string strArticleId)
        {
            ModelInvokeResult<ArticlePK> result = new ModelInvokeResult<ArticlePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ArticleId = strArticleId.ToGuid();
                if (_ArticleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                ArticlePK pk = new ArticlePK { ArticleId = _ArticleId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new Article().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticlePK { ArticleId = _ArticleId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strArticleIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strArticleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrArticleIds = strArticleIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrArticleIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Article().GetDeleteMethodName();
                foreach (string strArticleId in arrArticleIds)
                {
                    ArticlePK pk = new ArticlePK { ArticleId = strArticleId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strArticleId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<ArticlePK> Nullify(string strArticleId)
        {
            ModelInvokeResult<ArticlePK> result = new ModelInvokeResult<ArticlePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _ArticleId = strArticleId.ToGuid();
                if (_ArticleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                Article article = new Article { ArticleId = _ArticleId, Status = 0 };
                /***********************begin 自定义代码*******************/
                article.OperatedBy = NormalSession.UserId.ToGuid();
                article.OperatedOn = DateTime.Now;
                
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = article.GetUpdateMethodName(), ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticlePK { ArticleId = _ArticleId };
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
        [WebInvoke(UriTemplate = "NullifySelected/{strArticleIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strArticleIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrArticleIds = strArticleIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrArticleIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Article().GetUpdateMethodName();
                foreach (string strArticleId in arrArticleIds)
                {
                    Article article = new Article { ArticleId = strArticleId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    article.OperatedBy = NormalSession.UserId.ToGuid();
                    article.OperatedOn = DateTime.Now;
                    
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, ArticlePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strArticleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Article> Load(string strArticleId)
        {
            ModelInvokeResult<Article> result = new ModelInvokeResult<Article> { Success = true };
            try
            {
                Guid? _ArticleId = strArticleId.ToGuid();
                if (_ArticleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var article = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<Article, ArticlePK>(new ArticlePK { ArticleId = _ArticleId });
                result.instance = article;
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

        #region 业务接口

        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid2
        [WebInvoke(UriTemplate = "ListForEasyUIgrid2/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Article> ListForEasyUIgrid2(string connectId, EasyUIgridCollectionParam<Article> param)
        {
            EasyUIgridCollectionInvokeResult<Article> result = new EasyUIgridCollectionInvokeResult<Article> { Success = true };
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
                        if (field.key == "ColumnId")
                        {
                            filters.Add("WhereClause", "ArticleId in (select ArticleId from Pub_ArticleColumn_Relation where Status=1 and ColumnId='"+field.value+"')");
                        }
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
                User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
                
                if (!currentUser.isSuperAdmin())
                {
                    IList<GroupMember> groupMemberlist = BuilderFactory.DefaultBulder().List<GroupMember>(new { UserId = NormalSession.UserId }.ToStringObjectDictionary());
                    List<string> groupIds = new List<string>();
                    foreach (GroupMember groupMember in groupMemberlist) {
                        groupIds.Add(groupMember.GroupId.ToString());
                    }
                    whereClause.Add("Pub_Article.ArticleId in(select a.ArticleId from Pub_ArticleColumn_Relation a inner join Pub_ArticleColumnPermit b on a.ColumnId=b.ColumnId where b.OBJ_ID in('" + string.Join(",", groupIds.ToArray()) + "'))");
                }
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
                //gridCollectionPager.EasyUIgridDoPage4Model<Article>(filters, param, ref result,connectId);

                gridCollectionPager.EasyUIgridDoPage<Article>(BuilderFactory.DefaultBulder(connectId).List<Article>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region EasyUIgrid数据格式的列表 ListForEasyUIgridForFlow
        [WebInvoke(UriTemplate = "ListForEasyUIgridForFlow", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridForFlow( EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                //if (param.instance != null)
                //{
                //    filters = param.instance.ToStringObjectDictionary(false);
                //}
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key != "FlowTo_Start")
                        {
                            filters[field.key] = field.value;
                        }
                        else
                        {
                            if (field.value.Length == 1)
                            {
                                if (filters.ContainsKey("Status"))
                                {
                                    filters.Remove("Status");
                                }
                                whereClause.Add("Status ='1'");
                            }
                            else if (field.value.Length == 2)
                            {
                                filters["FlowTo"] = field.value;
                            }
                            else if (field.value.Length == 3)
                            {
                                int flowTo = Int32.Parse(field.value.Substring(1));
                                if (flowTo == 10)
                                {
                                    whereClause2.Add("FlowTo <> 10");
                                }
                                else
                                {
                                    whereClause2.Add("FlowFrom between " + (flowTo + 1).ToString() + " and " + (flowTo + 9).ToString());
                                }
                            }
                        }
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key == "Code" && field.value != "")
                        {
                            whereClause.Add(string.Format("ArticleId in (select ArticleId from Pub_ArticleColumn_Relation where Status=1 and ColumnId in( select ColumnId from Pub_ArticleColumn where Status=1 and Menu_Flag=1 and {0} like '{1}%' ))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("Code") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };

                if (!currentUser.isSuperAdmin())
                {
                    IList<GroupMember> groupMemberlist = BuilderFactory.DefaultBulder().List<GroupMember>(new { UserId = NormalSession.UserId }.ToStringObjectDictionary());
                    List<string> groupIds = new List<string>();
                    foreach (GroupMember groupMember in groupMemberlist)
                    {
                        groupIds.Add(groupMember.GroupId.ToString());
                    }
                    whereClause.Add("ArticleId in(select a.ArticleId from Pub_ArticleColumn_Relation a inner join Pub_ArticleColumnPermit b on a.ColumnId=b.ColumnId where b.OBJ_ID in('" + string.Join(",", groupIds.ToArray()) + "'))");
                }
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (whereClause2.Count > 0)
                {
                    filters.Add("WhereClause2", string.Join(" AND ", whereClause2.ToArray()));
                }
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ArticleInfo_ListByPage", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridForFlow 有ConnectId
        [WebInvoke(UriTemplate = "ListForEasyUIgridForFlow_ConnectId/{ConnectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridForFlow_ConnectId(string connectId, EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                //if (param.instance != null)
                //{
                //    filters = param.instance.ToStringObjectDictionary(false);
                //}
                List<string> whereClause = new List<string>();
                List<string> whereClause2 = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.key != "FlowTo_Start")
                        {
                            filters[field.key] = field.value;
                        }
                        else
                        {
                            if (field.value.Length == 1)
                            {
                                if (filters.ContainsKey("Status"))
                                {
                                    filters.Remove("Status");
                                }
                                whereClause.Add("Status ='1'");
                            }
                            else if (field.value.Length == 2)
                            {
                                filters["FlowTo"] = field.value;
                            }
                            else if (field.value.Length == 3)
                            {
                                int flowTo = Int32.Parse(field.value.Substring(1));
                                if (flowTo == 10)
                                {
                                    whereClause2.Add("FlowTo <> 10");
                                }
                                else
                                {
                                    whereClause2.Add("FlowFrom between " + (flowTo + 1).ToString() + " and " + (flowTo + 9).ToString());
                                }
                            }
                        }
                    }
                }
                /***********************begin 模糊查询*********************/
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key == "Code" && field.value != "")
                        {
                            whereClause.Add(string.Format("ArticleId in (select ArticleId from Pub_ArticleColumn_Relation where Status=1 and ColumnId in( select ColumnId from Pub_ArticleColumn where Status=1 and Menu_Flag=1 and {0} like '{1}%' ))", field.key, field.value));
                        }
                        else if (field.key.IndexOf("Code") > -1 && field.value == "")
                        {

                        }
                        else
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };

                if (!currentUser.isSuperAdmin())
                {
                    IList<GroupMember> groupMemberlist = BuilderFactory.DefaultBulder().List<GroupMember>(new { UserId = NormalSession.UserId }.ToStringObjectDictionary());
                    List<string> groupIds = new List<string>();
                    foreach (GroupMember groupMember in groupMemberlist)
                    {
                        groupIds.Add(groupMember.GroupId.ToString());
                    }
                    whereClause.Add("ArticleId in(select a.ArticleId from Pub_ArticleColumn_Relation a inner join Pub_ArticleColumnPermit b on a.ColumnId=b.ColumnId where b.OBJ_ID in('" + string.Join(",", groupIds.ToArray()) + "'))");
                }
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (whereClause2.Count > 0)
                {
                    filters.Add("WhereClause2", string.Join(" AND ", whereClause2.ToArray()));
                }
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ArticleInfo_ListByPage", filters, param, ref result, connectId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询列表 QueryGroupMember
        [WebGet(UriTemplate = "QueryGroupMember?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<GroupMember> QueryGroupMember(string strParms)
        {
            CollectionInvokeResult<GroupMember> result = new CollectionInvokeResult<GroupMember> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<GroupMember>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region UpdateArticles 批量修改状态 (用于审核后更新状态)
        [WebInvoke(UriTemplate = "UpdateArticleStatus/{_status},{ids}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateArticleStatus(string _status, string ids)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrArticleIds = ids.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrArticleIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new Article().GetUpdateMethodName();
                foreach (string strArticleId in arrArticleIds)
                {
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = new { ArticleId = strArticleId.ToGuid(), Status = _status, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now }, Type = SqlExecuteType.UPDATE });
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

        #region UpdateBaseResidents 全部批量修改状态 (用于审批后更新符合条件的所以状态)
        [WebInvoke(UriTemplate = "UpdateArticleStatusAll", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateBaseResidentsAll(FlowAction flowAction)
        {
            InvokeResult result = new InvokeResult { Success = true };
            //流转状态  0-正流，1-逆流  默认正流，正流（当前状态加上当前操作）；逆流（当前状态）
            int iAction = 0;
            if (flowAction.FlowType == 0)
            {
                //默认正向流转
                iAction += flowAction.ProcessAction;
            }

            try
            {
                FlowDefineMapping flowDefineMapping = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefineMapping>(new { MappingType = flowAction.MappingType, MappingColumn = flowAction.MappingColumn, MappingId = flowAction.MappingId }.ToStringObjectDictionary()).First();
                if (flowDefineMapping != null && !string.IsNullOrEmpty(flowDefineMapping.FlowName))
                {
                    flowAction.FlowName = flowDefineMapping.FlowName;
                }

                //找出当前流程的最高去处和默认去处
                IList<FlowDefine> flowDefineList = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<FlowDefine>(new { FlowName = flowAction.FlowName, TableName = flowAction.TableName, TableColumn = flowAction.TableColumn, Status = 1, OrderByClause = " FlowTo ,CurrentState " }.ToStringObjectDictionary());

                //如果是流程到了最顶级的话或者未进入流程或者切换流程则跳回当前流程默认的最低级别,以达到重新开始流转效果
                string sql = "update {1} set Status=1 where {0} in (";

                sql += "select x.{0} From  (" +
                    "select a.{0},MAX(b.Id) as FId From {1} a left join Pub_Flow b  on a.{0}=b.BIZ_ID " +
                    "where a.{4}= '{5}'  ";

                if (!string.IsNullOrEmpty(flowAction.WhereClause))
                {
                    sql += " and " + flowAction.WhereClause;
                }

                sql += "group by a.{0} ) x left join Pub_Flow y on x.FId=y.Id  left join Pub_FlowDefine z on  y.FlowDefineId=z.FlowDefineId " +
                    "where z.FlowName='{2}' and z.TableName='{1}' and z.TableColumn='{0}' and y.FlowTo = {3}";

                sql += ")";

                string statements = string.Format(sql, flowAction.TableColumn, flowAction.TableName, flowAction.FlowName,
                    flowDefineList.Last().FlowTo, flowAction.MappingColumn, flowAction.MappingId);

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

        #region 修改文章的能容
        [WebInvoke(UriTemplate = "_ArticleUpdateContent", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
         public ModelInvokeResult<ArticlePK> UpdateContent( Article article)
        {
            ModelInvokeResult<ArticlePK> result = new ModelInvokeResult<ArticlePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                if (article.ArticleId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                } 
                /***********************begin 自定义代码*******************/
                article.OperatedBy = NormalSession.UserId.ToGuid();
                article.OperatedOn = DateTime.Now;

                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = article.GetUpdateMethodName(), ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ExecuteNativeSqlNoneQuery(statements);
                result.instance = new ArticlePK { ArticleId = article.ArticleId };

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

