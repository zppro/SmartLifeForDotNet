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

namespace SmartLife.City.ManageServices.Pub
{ 
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SmsSendService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<SmsSend> List()
        {
            CollectionInvokeResult<SmsSend> result = new CollectionInvokeResult<SmsSend> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<SmsSend>();
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
        public CollectionInvokeResult<SmsSend> Query(string strParms)
        {
            CollectionInvokeResult<SmsSend> result = new CollectionInvokeResult<SmsSend> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<SmsSend>(dictionary);
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
        public JqgridCollectionInvokeResult<SmsSend> ListForJqgrid(JqgridCollectionParam<SmsSend> param)
        {
            JqgridCollectionInvokeResult<SmsSend> result = new JqgridCollectionInvokeResult<SmsSend> { Success = true };
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

                gridCollectionPager.JqgridDoPage<SmsSend>(BuilderFactory.DefaultBulder().List<SmsSend>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgrid(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<StringObjectDictionary>(BuilderFactory.DefaultBulder().ListStringObjectDictionary("SmsSend_List2", filters), param, ref result);
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
        public ModelInvokeResult<SmsSendPK> Create(SmsSend smsSend)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (smsSend.Id == -1)
                {
                    smsSend.Id = null;
                }
                statements.Add(new IBatisNetBatchStatement { StatementName = smsSend.GetCreateMethodName(), ParameterObject = smsSend.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = smsSend.Id };
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
        public ModelInvokeResult<SmsSendPK> Update(string strId, SmsSend smsSend)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                statements.Add(new IBatisNetBatchStatement { StatementName = smsSend.GetUpdateMethodName(), ParameterObject = smsSend.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = _Id };

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
        public ModelInvokeResult<SmsSendPK> Delete(string strId)
        {
            ModelInvokeResult<SmsSendPK> result = new ModelInvokeResult<SmsSendPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                int _Id = Convert.ToInt32(strId);
                SmsSendPK pk = new SmsSendPK { Id = _Id };
                statements.Add(new IBatisNetBatchStatement { StatementName = new SmsSend().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new SmsSendPK { Id = _Id };
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
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<SmsSend> Load(string strId)
        {
            ModelInvokeResult<SmsSend> result = new ModelInvokeResult<SmsSend> { Success = true };
            try
            {
                var smsSendInfo = BuilderFactory.DefaultBulder().Load<SmsSend, SmsSendPK>(new SmsSendPK { Id = Convert.ToInt32(strId) });
                result.instance = smsSendInfo;
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

        #region CMS扩展接口

        #region 批量插入
        [WebInvoke(UriTemplate = "BatchCreate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult BatchCreate(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            string sql = "insert into Pub_SmsSend( ScheduleTime,Mobile,BatchNum,SendContent, " +
                       " SourceCatalog,SourceTable,SourceId,SendCatalog ) " +
                       " select null ScheduleTime,Mobile,'{0}' BatchNum,null SendContent, " +
                       " null SourceCatalog,'Oca_OldManConfigInfo' SourceTable,SourceId,'0' SendCatalog " +
                       " from ( select row_number()over(order by tmpcolumn desc) as Rowid,*  from ( " +
                       " select top {1} tmpcolumn=0,a.OldManId SourceId,b.CallNo Mobile";
            int total = 0;
            int toprecord = 0;
            if (param.page > 0 && param.rows > 0)
            {
                total = param.page * param.rows;
                toprecord = (param.rows - 1) * param.page;
            }
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                /**********************************************************/
                List<string> filtersClause = new List<string>();
                if (param.filterFields != null)
                {
                    //获取默认发送短信类的属性字段
                    var className = typeof(SmsSend).GetProperties();
                    foreach (var field in param.filterFields)
                    {
                        if (className.Count(s => s.Name == field.key) > 0) {
                            sql = sql.Replace((" null " + field.key).Trim(), string.Format(" '{0}' {1}", field.value, field.key));
                            continue;
                        }
                        filtersClause.Add(string.Format(" a.{0} = '{1}'", field.key.Replace("OldManStatus", "Status"), field.value));
                    }
                }
                sql += " from Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b on a.OldManId = b.OldManId ";
                if (filtersClause.Count > 0)
                {
                    sql += " where " + string.Join(" AND ", filtersClause.ToArray());
                }
                /***********************begin 模糊查询*********************/
                List<string> whereClause = new List<string>();
                if (param.fuzzyFields != null)
                {
                    foreach (var field in param.fuzzyFields)
                    {
                        whereClause.Add(string.Format("{0} like '%{1}%' ", field.key, field.value));
                    }
                }
                /***********************end 模糊查询***********************/
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    sql += (filtersClause.Count > 0 ? " AND " : " where ") + string.Join(" AND ", whereClause.ToArray());
                }
                /**********************************************************/
                /***********************begin 排序*************************/
                /**********************************************************/
                if (!string.IsNullOrEmpty(param.sort))
                {
                    sql += " order by " + param.sort + " " + param.order ?? "ASC";
                }
                sql += " )t )tt where Rowid >{2} and  Mobile is not null and Mobile<>'' ";
                sql = string.Format(sql, (new Random(Guid.NewGuid().GetHashCode())).Next(), total, toprecord);
                /***********************end 排序***************************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
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