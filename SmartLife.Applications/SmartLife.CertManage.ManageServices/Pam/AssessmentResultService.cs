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
    public class AssessmentResultService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<AssessmentResult> List()
        {
            CollectionInvokeResult<AssessmentResult> result = new CollectionInvokeResult<AssessmentResult> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<AssessmentResult>();
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
        public CollectionInvokeResult<AssessmentResult> Query(string strParms)
        {
            CollectionInvokeResult<AssessmentResult> result = new CollectionInvokeResult<AssessmentResult> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<AssessmentResult>(dictionary);
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
        public JqgridCollectionInvokeResult<AssessmentResult> ListForJqgrid(JqgridCollectionParam<AssessmentResult> param)
        {
            JqgridCollectionInvokeResult<AssessmentResult> result = new JqgridCollectionInvokeResult<AssessmentResult> { Success = true };
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

                gridCollectionPager.JqgridDoPage<AssessmentResult>(BuilderFactory.DefaultBulder().List<AssessmentResult>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<AssessmentResult> ListForEasyUIgrid(EasyUIgridCollectionParam<AssessmentResult> param)
        {
            EasyUIgridCollectionInvokeResult<AssessmentResult> result = new EasyUIgridCollectionInvokeResult<AssessmentResult> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<AssessmentResult>(BuilderFactory.DefaultBulder().List<AssessmentResult>(filters), param, ref result);
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
        public ModelInvokeResult<AssessmentResultPK> Create(AssessmentResult assessmentResult)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentResult.OperatedOn = DateTime.Now;
                assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetCreateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentResultPK { Id = assessmentResult.Id };
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
        public ModelInvokeResult<AssessmentResultPK> Update(string strId, AssessmentResult assessmentResult)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (strId == null || strId == "undefined")
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                /***********************begin 自定义代码*******************/
                assessmentResult.Id = Int32.Parse(strId);
                assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentResult.OperatedOn = DateTime.Now;
                assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetUpdateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentResultPK { Id = Int32.Parse(strId) };

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
        public ModelInvokeResult<AssessmentResultPK> Delete(string strId)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AssessmentResultPK pk = new AssessmentResultPK { Id = Int32.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new AssessmentResult().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentResultPK { Id = Int32.Parse(strId) };
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
                string statementName = new AssessmentResult().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    AssessmentResultPK pk = new AssessmentResultPK { Id = Int32.Parse(strId) };
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
        public ModelInvokeResult<AssessmentResultPK> Nullify(string strId)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AssessmentResult assessmentResult = new AssessmentResult { Id = Int32.Parse(strId), Status = 0 };
                /***********************begin 自定义代码*******************/
                assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentResult.OperatedOn = DateTime.Now;
                assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetUpdateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentResultPK { Id = Int32.Parse(strId) };
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
                string statementName = new AssessmentResult().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    AssessmentResult assessmentResult = new AssessmentResult { Id = Int32.Parse(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentResult.OperatedOn = DateTime.Now;
                    assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, AssessmentResultPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<AssessmentResult> Load(string strId)
        {
            ModelInvokeResult<AssessmentResult> result = new ModelInvokeResult<AssessmentResult> { Success = true };
            try
            {
                var assessmentResult = BuilderFactory.DefaultBulder().Load<AssessmentResult, AssessmentResultPK>(new AssessmentResultPK { Id = Int32.Parse(strId) });
                result.instance = assessmentResult;
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

        #region ListForEasyUIgridByPage数据格式的列表 ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<AssessmentResult> ListForEasyUIgridByPage(EasyUIgridCollectionParam<AssessmentResult> param)
        {
            EasyUIgridCollectionInvokeResult<AssessmentResult> result = new EasyUIgridCollectionInvokeResult<AssessmentResult> { Success = true };
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
                            filters[field.key] = field.value;
                        }
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
                gridCollectionPager.EasyUIgridDoPage4Model<AssessmentResult>(filters, param, ref result);  
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改计划时间
        [WebInvoke(UriTemplate = "UpdateWorkSchedule/{strId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<AssessmentResultPK> UpdateWorkSchedule(string strId, AssessmentResult assessmentResult)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (strId == null || strId == "undefined")
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                /***********************begin 自定义代码*******************/
                assessmentResult.Id = Int32.Parse(strId);
                assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                assessmentResult.OperatedOn = DateTime.Now;
                assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetUpdateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/

                /***********************begin 修改已经制定的工作计划中的服务时间*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new AssessmentResultPK { Id = Int32.Parse(strId) };
                string sql = "update Pam_WorkPlan set PlanDate=SUBSTRING(CONVERT(varchar(100),PlanDate, 21),1,11)+'" + assessmentResult.WorkSchedule + "'"
                            + " ,OperatedBy='" + NormalSession.UserId.ToGuid() + "',OperatedOn='" + DateTime.Now + "'"
                            +" where  PlanDate>GETDATE() and WorkItem='" + assessmentResult.WorkItem + "'  and OldManId='" + assessmentResult.OldManId + "'";
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                /***********************end 修改已经制定的工作计划中的服务时间*********************/

                /***********************begin 重新生成工作执行表*********************/
                SPParam spParam = new { }.ToSPParam();
                spParam["OldManId"] = assessmentResult.OldManId;
                spParam["BatchFlag"] = 0;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                /***********************end 重新生成工作执行表*********************/
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 老人批量设置服务项目
        [WebInvoke(UriTemplate = "CreateAssessment/{strOldManId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateAssessment(string strOldManId, IList<AssessmentItem> assessmentItems)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                AssessmentResult assessmentResult = new AssessmentResult();
                foreach (AssessmentItem assessmentItem in assessmentItems)
                {
                    /***********************begin 自定义代码*******************/
                    assessmentResult.Id = null;
                    assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentResult.OperatedOn = DateTime.Now;
                    assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    assessmentResult.OldManId = strOldManId.ToGuid();
                    assessmentResult.WorkItem = assessmentItem.WorkItem;
                    assessmentResult.WorkSchedule = assessmentItem.WorkSchedule;
                    assessmentResult.Workload = assessmentItem.Workload;
                    assessmentResult.RemindFlag = assessmentItem.RemindFlag;
                    assessmentResult.RemindRepeats = assessmentItem.RemindRepeats;
                    assessmentResult.PlayRepeats = assessmentItem.PlayRepeats;
                    assessmentResult.CheckDescription = assessmentItem.Remark;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetCreateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/ 
                }
                if (statements.Count > 0)
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
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

        #region 建立勾选标志
        [WebInvoke(UriTemplate = "UpdateCheckFlag/{strIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<AssessmentResultPK> UpdateCheckFlag(string strIds, AssessmentResult assessmentResult)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrIds = strIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new AssessmentResult().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    assessmentResult.Id = Int32.Parse(strId);
                    assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentResult.OperatedOn = DateTime.Now;
                    assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE }); 
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

        #region 作废个人评估结果的同时作废工作计划 NullifyEx
        [WebInvoke(UriTemplate = "NullifyEx/{strIds},{strOldManId},{strWorkItems}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<AssessmentResultPK> NullifyEx(string strIds, string strOldManId, string strWorkItems)
        {
            ModelInvokeResult<AssessmentResultPK> result = new ModelInvokeResult<AssessmentResultPK> { Success = true };
            try
            {
                string[] arrIds = strIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] arrWorkItems = strWorkItems.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                foreach (string strId in arrIds)
                {
                    AssessmentResult assessmentResult = new AssessmentResult { Id = Int32.Parse(strId), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentResult.OperatedOn = DateTime.Now;
                    assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetUpdateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                /***********************begin 自定义代码*******************/
                string sql = "delete from  Pam_WorkPlan where OldManId='" + strOldManId + "' and WorkItem in ('" + string.Join("','", arrWorkItems) + "') and PlanDate>GETDATE()";
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                /***********************end 自定义代码*********************/
                /***********************begin 重新生成工作执行表*********************/
                SPParam spParam = new { }.ToSPParam();
                spParam["OldManId"] = strOldManId.ToGuid();
                spParam["BatchFlag"] = 0;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                /***********************end 重新生成工作执行表*********************/
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 确认老人评估结果
        [WebInvoke(UriTemplate = "ConfirmEvaluated", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ConfirmEvaluated(List<AssessmentResult> assessmentResults)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                foreach (AssessmentResult assessmentResult in assessmentResults)
                { 
                    assessmentResult.OperatedBy = NormalSession.UserId.ToGuid();
                    assessmentResult.OperatedOn = DateTime.Now;
                    assessmentResult.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                    statements.Add(new IBatisNetBatchStatement { StatementName = assessmentResult.GetUpdateMethodName(), ParameterObject = assessmentResult.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        #endregion
    }
}

