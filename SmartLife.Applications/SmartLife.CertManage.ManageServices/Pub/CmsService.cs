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
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CmsService : BaseWcfService
    {
        #region CMS标准接口

        #region 首页栏目和文章
        [WebInvoke(UriTemplate = "HomeColumnsWithArticles", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<ColumnWithArticle> HomeColumnsWithArticles(ArticleColumn param)
        {
            CollectionInvokeResult<ColumnWithArticle> result = new CollectionInvokeResult<ColumnWithArticle> { Success = true };
            int rownum=6;
            try
            {
                IList<ColumnWithArticle> list_columnWithArticle=null;
                IList<ArticleColumn> columns = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>("Cms_Index_LoadMoreColumns", param.ToStringObjectDictionary(false));
                if (columns.Count > 0)
                {
                    list_columnWithArticle = new List<ColumnWithArticle>(); 
                    foreach (var column in columns)
                    {
                        if (column.COL_Path == null)
                        {
                            column.COL_Path = column.COL_Name;
                        }
                        IList<Col_Article> articles = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Col_Article>("Cms_Article_ByColumn", new { ROWNUM = rownum, ColumnId = column.ColumnId }.ToStringObjectDictionary(false));
                        list_columnWithArticle.Add(new ColumnWithArticle { Column = column, Articles = articles });
                    }
                }  
                result.rows = list_columnWithArticle;
            }
            catch (Exception ex)
            { 
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 首页图片文章
        [WebInvoke(UriTemplate = "HomeColumnsWithPicArticles", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<ColumnWithArticle> HomePicArticles(ArticleColumn param)
        {
            CollectionInvokeResult<ColumnWithArticle> result = new CollectionInvokeResult<ColumnWithArticle> { Success = true };
            int rownum = 4;
            try
            {
                IList<ColumnWithArticle> list_columnWithArticle = null;
                IList<ArticleColumn> columns = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>(param.ToStringObjectDictionary(false));
                if (columns.Count > 0)
                {
                    list_columnWithArticle = new List<ColumnWithArticle>();
                    foreach (var column in columns)
                    {
                        if (column.COL_Path == null)
                        {
                            column.COL_Path = column.COL_Name;
                        }
                        IList<Col_Article> articles = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Col_Article>("Cms_Article_ByColumn", new { ROWNUM = rownum, ColumnId = column.ColumnId }.ToStringObjectDictionary(false));
                        foreach (var article in articles)
                        {
                            IList<Attachment> attachments = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Attachment>("Cms_Attachment_ByArticle", new { ROWNUM = 1, SourceId = article.ArticleId, SourceTable = "Pub_Article", AttachmentTag = "ArticlePic" }.ToStringObjectDictionary(false));
                            article.Attachments = attachments;   
                        }
                        list_columnWithArticle.Add(new ColumnWithArticle { Column = column, Articles = articles });
                    }
                }
                result.rows = list_columnWithArticle;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 首页栏目
        [WebInvoke(UriTemplate = "HomeColumns", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<ArticleColumn> HomeColumns(ArticleColumn param)
        {
            CollectionInvokeResult<ArticleColumn> result = new CollectionInvokeResult<ArticleColumn> { Success = true };
            try
            {
                IList<ArticleColumn> columns = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ArticleColumn>("Cms_Index_LoadMoreColumns", param.ToStringObjectDictionary(false));

                result.rows = columns;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 首页视频
        [WebInvoke(UriTemplate = "VideoArticlesForGrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<Col_Article> VideoArticlesForGrid(EasyUIgridCollectionParam<ArticleColumn> param)
        {
           EasyUIgridCollectionInvokeResult<Col_Article> result = new EasyUIgridCollectionInvokeResult<Col_Article> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                filters.Add("PageNo", param.page);
                filters.Add("PageSize", param.rows);
                if (param.page == 1)//只第一次取
                {
                    result.total = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).GetRecordCount("Count_VideoArticleListByPage", filters);
                }
                IList<Col_Article>videoArticles = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Col_Article>("VideoArticleListByPage", filters);
                result.rows = videoArticles;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 文章载入 Load
        [WebGet(UriTemplate = "LoadArticle/{strArticleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Article> LoadArticle(string strArticleId)
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

        #region 视频文章载入 Load
        [WebGet(UriTemplate = "LoadVideoArticle/{strArticleId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<Col_Article> LoadVideoArticle(string strArticleId)
        {
            ModelInvokeResult<Col_Article> result = new ModelInvokeResult<Col_Article> { Success = true };
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
                Col_Article videoArticle = article.MixIn<Col_Article>(article.ToStringObjectDictionary(false));
                IList<Attachment> attachments = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<Attachment>("Cms_Attachment_ByArticle", new { ROWNUM = 1, SourceId = article.ArticleId, SourceTable = "Pub_Article", AttachmentTag = "ArticleVideo" }.ToStringObjectDictionary(false));
                videoArticle.Attachments = attachments;

                result.instance = videoArticle;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 服务机构载入 Load
        [WebGet(UriTemplate = "LoadServiceStation/{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<ServiceStation> LoadServiceStation(string strStationId)
        {
            ModelInvokeResult<ServiceStation> result = new ModelInvokeResult<ServiceStation> { Success = true };
            try
            {
                Guid? _StationId = strStationId.ToGuid();
                if (_StationId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var station = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).Load<ServiceStation, ServiceStationPK>(new ServiceStationPK { StationId = _StationId });
                result.instance = station;
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

        #region EasyUI数据格式文章列表
        [WebInvoke(UriTemplate = "ArtListForGrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ArtListForGrid(EasyUIgridCollectionParam<ArticleColumn> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
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
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                filters.Add("PageNo", param.page);
                filters.Add("PageSize", param.rows);
                if (param.page == 1)//只第一次取
                {
                    result.total = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).GetRecordCount("Count_ArticleListByColByPage", filters);
                }
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("ArticleListByColByPage", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 服务机构查询列表 Query
        [WebGet(UriTemplate = "ServiceStationQuery?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation> ServiceStationQuery(string strParms)
        {
            CollectionInvokeResult<ServiceStation> result = new CollectionInvokeResult<ServiceStation> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<ServiceStation>("ServiceStation_CMSList", dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUI数据格式服务机构列表
        [WebInvoke(UriTemplate = "ServiceStationListForGrid", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ServiceStationListForGrid(EasyUIgridCollectionParam<ServiceStation> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                if (param.fuzzyFields != null)
                {
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (string.IsNullOrEmpty(field.value)) continue;
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                if (!string.IsNullOrEmpty(param.sort))
                {
                    filters.Add("OrderByClause", param.sort + " " + param.order ?? "ASC");
                }
                filters.Add("PageNo", param.page);
                filters.Add("PageSize", param.rows);
                if (param.page == 1)//只第一次取
                {
                    result.total = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).GetRecordCount("Count_ServiceStation_CMSListByPage", filters);
                }
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).ListStringObjectDictionary("ServiceStation_CMSListByPage", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取某区下的[街道+社区]字典项目
        [WebGet(UriTemplate = "GetStreetAndCommunityInArea/{parentId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<DictItemEx> GetStreetAndCommunityInArea(string parentId)
        {
            IList<DictItemEx> results = null;
            try
            {
                results = BuilderFactory.DefaultBulder(GetHttpHeader("ConnectId")).List<DictItemEx>("CTE_StreetAndCommunitiy_CMSList",
                    new
                    {
                        ParentId = parentId
                    }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return results;
        }
        #endregion

        #region 获取参数
        [WebGet(UriTemplate = "GetParameterValue/{parameterId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringValueRet> GetParameterValue(string parameterId)
        {
            InvokeResult<StringValueRet> result = new InvokeResult<StringValueRet>() { Success = true, ret = new StringValueRet() };
            result.ret.Value = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get(parameterId));
            return result;
        }
        #endregion

        #endregion

        #region 自定义业务接口

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


        #region 获取商家对应的服务项目和项目价格
        [WebGet(UriTemplate = "GetServeItems/{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public IList<MerchantServeItem> GetServeItems(string stationId)
        {
            return BuilderFactory.DefaultBulder().List<MerchantServeItem>(new MerchantServeItem { StationId = stationId.ToGuid() });
        }
        #endregion
         

        #region 查询日照中心服务信息
        [WebGet(UriTemplate = "GetInstitutionsCareCenterInfo?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<InstitutionsCareCenter> GetInstitutionsCareCenterInfo(string strParms)
        {
            CollectionInvokeResult<InstitutionsCareCenter> result = new CollectionInvokeResult<InstitutionsCareCenter> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<InstitutionsCareCenter>(dictionary);
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

    public class ColumnWithArticle
    {
        public ArticleColumn Column { get; set; }
        public IList<Col_Article> Articles { get; set; }
    }
}

