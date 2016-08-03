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
using SmartLife.Share.Models.Pam;

namespace SmartLife.CertManage.ManageServices.Pam
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AssessmentItemService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<AssessmentItem> List()
        {
            CollectionInvokeResult<AssessmentItem> result = new CollectionInvokeResult<AssessmentItem> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<AssessmentItem>();
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
        public CollectionInvokeResult<AssessmentItem> Query(string strParms)
        {
            CollectionInvokeResult<AssessmentItem> result = new CollectionInvokeResult<AssessmentItem> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<AssessmentItem>(dictionary);
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
        public JqgridCollectionInvokeResult<AssessmentItem> ListForJqgrid(JqgridCollectionParam<AssessmentItem> param)
        {
            JqgridCollectionInvokeResult<AssessmentItem> result = new JqgridCollectionInvokeResult<AssessmentItem> { Success = true };
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

                gridCollectionPager.JqgridDoPage<AssessmentItem>(BuilderFactory.DefaultBulder().List<AssessmentItem>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<AssessmentItem> ListForEasyUIgrid(EasyUIgridCollectionParam<AssessmentItem> param)
        {
            EasyUIgridCollectionInvokeResult<AssessmentItem> result = new EasyUIgridCollectionInvokeResult<AssessmentItem> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<AssessmentItem>(BuilderFactory.DefaultBulder().List<AssessmentItem>(filters), param, ref result);
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
        public ModelInvokeResult<AssessmentItemPK> Create(AssessmentItem assessmentItem)
        {
            ModelInvokeResult<AssessmentItemPK> result = new ModelInvokeResult<AssessmentItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                /***********************begin 自定义代码*******************/
                assessmentItem.Id = null;
                assessmentItem.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentItem.OperatedOn = DateTime.Now;
                assessmentItem.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentItem.GetCreateMethodName(), ParameterObject = assessmentItem.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentItemPK { Id = assessmentItem.Id };
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
        public ModelInvokeResult<AssessmentItemPK> Update(string strId, AssessmentItem assessmentItem)
        {
            ModelInvokeResult<AssessmentItemPK> result = new ModelInvokeResult<AssessmentItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                assessmentItem.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentItem.OperatedOn = DateTime.Now;
                assessmentItem.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentItem.GetUpdateMethodName(), ParameterObject = assessmentItem.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentItemPK { Id = assessmentItem.Id };

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
        public ModelInvokeResult<AssessmentItemPK> Delete(string strId)
        {
            ModelInvokeResult<AssessmentItemPK> result = new ModelInvokeResult<AssessmentItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AssessmentItemPK pk = new AssessmentItemPK { Id = Int32.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new AssessmentItem().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentItemPK { Id = Int32.Parse(strId) };
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
                string statementName = new AssessmentItem().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    AssessmentItemPK pk = new AssessmentItemPK { Id = Int32.Parse(strId) };
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
        public ModelInvokeResult<AssessmentItemPK> Nullify(string strId)
        {
            ModelInvokeResult<AssessmentItemPK> result = new ModelInvokeResult<AssessmentItemPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AssessmentItem assessmentItem = new AssessmentItem { Id = Int32.Parse(strId), Status = 0 };
                /***********************begin 自定义代码*******************/
                assessmentItem.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentItem.OperatedOn = DateTime.Now;
                assessmentItem.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentItem.GetUpdateMethodName(), ParameterObject = assessmentItem.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentItemPK { Id = Int32.Parse(strId) };
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
                string statementName = new AssessmentItem().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    AssessmentItem assessmentItem = new AssessmentItem { Id = Int32.Parse(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    assessmentItem.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentItem.OperatedOn = DateTime.Now;
                    assessmentItem.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = assessmentItem.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, AssessmentItemPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<AssessmentItem> Load(string strId)
        {
            ModelInvokeResult<AssessmentItem> result = new ModelInvokeResult<AssessmentItem> { Success = true };
            try
            {
                var assessmentItem = BuilderFactory.DefaultBulder().Load<AssessmentItem, AssessmentItemPK>(new AssessmentItemPK { Id = Int32.Parse(strId) });
                result.instance = assessmentItem;
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

        #region  自定义

        #region ListForEasyUIgridByPage数据格式的列表 ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<AssessmentItem> ListForEasyUIgridByPage(EasyUIgridCollectionParam<AssessmentItem> param)
        {
            EasyUIgridCollectionInvokeResult<AssessmentItem> result = new EasyUIgridCollectionInvokeResult<AssessmentItem> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary(); 
                List<string> whereClause = new List<string>();
                /**********************************************************/
                if (param.filterFields != null)
                {
                    foreach (var field in param.filterFields)
                    {
                        if (field.value != "")
                        {
                            if (field.key == "OldManId")
                            {
                                whereClause.Add(field.value);
                            }
                            else
                            {
                                filters[field.key] = field.value;
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
                        if (field.value != "")
                        {
                            if (field.key == "WorkItems")
                            {
                                whereClause.Add(string.Format("WorkItem in ( {0})", field.value));
                            }
                            else if (field.key.Contains("_Start"))
                            {
                                var keys = field.key.Substring(0, field.key.IndexOf("_Start"));
                                whereClause.Add(string.Format("{0} >= '{1}'", keys, field.value));
                            }
                            else if (field.key.Contains("_End"))
                            {
                                var keye = field.key.Substring(0, field.key.IndexOf("_End"));
                                whereClause.Add(string.Format("{0} <= '{1}'", keye, field.value));
                            }
                            else
                            {
                                fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                            }
                        }
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
                gridCollectionPager.EasyUIgridDoPage4Model<AssessmentItem>(filters, param, ref result); 
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
        [WebGet(UriTemplate = "GetWorkItem/{strStationId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkItem(string strStationId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };
            try
            {
                string sql = "select * from Sys_DictionaryItem where DictionaryId='13001' "
                    + "and ItemId not in (select WorkItem from Pam_AssessmentItem where StationId='" + strStationId + "' and Status=1)";
                result.rows = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql); ;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 查询所属机构服务项目列表 AdmissionServiceItems
        [WebInvoke(UriTemplate = "AdmissionServiceItems", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> AdmissionServiceItems(AdmissionServiceItemParam admissionServiceItemParam)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };
            try
            {
                string sql = "";
                if (admissionServiceItemParam.OldManId == null)
                {
                    sql = string.Format("select a.StationId,'' OldManId ,a.WorkItem,a.WorkSchedule,a.Workload,a.RemindFlag,a.RemindRepeats,a.PlayRepeats,0 CheckFlag,a.Remark CheckDescription,b.ItemName " +
                        " ,0 Preferred from Pam_AssessmentItem a inner join  Sys_DictionaryItem b on a.WorkItem=b.ItemId " +
                        " and b.DictionaryId='13001' where a.StationId='{0}' and a.Status=1 order by a.WorkSchedule", admissionServiceItemParam.StationId);
                }
                else
                {
                    sql = string.Format("select a.*,b.ItemName from ( select StationId,'{1}' OldManId," +
                          " WorkItem,WorkSchedule,Workload,RemindFlag,RemindRepeats,PlayRepeats,0 CheckFlag,Remark CheckDescription " +
                          " ,0 Preferred from Pam_AssessmentItem where Status=1 and StationId='{0}' " +
                          " and WorkItem not in(select WorkItem From Pam_AssessmentResult where Status=1  and OldManId='{1}') " +
                          " union all " +
                          " select '{0}' StationId,OldManId, WorkItem,WorkSchedule,Workload,RemindFlag,RemindRepeats,PlayRepeats, " +
                          " CheckFlag,CheckDescription,1 Preferred from  Pam_AssessmentResult  where Status=1 and OldManId='{1}' " +
                          " ) a inner join  Sys_DictionaryItem b on a.WorkItem=b.ItemId and b.DictionaryId='13001' "+
                          " order by a.WorkSchedule", admissionServiceItemParam.StationId, admissionServiceItemParam.OldManId);
                    
                }
                result.rows = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql);
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

    #region 入院服务项
    public class AdmissionServiceItemParam
    {
        public Guid? StationId { get; set; }
        public Guid? OldManId { get; set; }
    }
    #endregion
}

