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
    public class WorkPlanService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<WorkPlan> List()
        {
            CollectionInvokeResult<WorkPlan> result = new CollectionInvokeResult<WorkPlan> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<WorkPlan>();
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
        public CollectionInvokeResult<WorkPlan> Query(string strParms)
        {
            CollectionInvokeResult<WorkPlan> result = new CollectionInvokeResult<WorkPlan> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<WorkPlan>(dictionary);
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
        public JqgridCollectionInvokeResult<WorkPlan> ListForJqgrid(JqgridCollectionParam<WorkPlan> param)
        {
            JqgridCollectionInvokeResult<WorkPlan> result = new JqgridCollectionInvokeResult<WorkPlan> { Success = true };
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

                gridCollectionPager.JqgridDoPage<WorkPlan>(BuilderFactory.DefaultBulder().List<WorkPlan>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<WorkPlan> ListForEasyUIgrid(EasyUIgridCollectionParam<WorkPlan> param)
        {
            EasyUIgridCollectionInvokeResult<WorkPlan> result = new EasyUIgridCollectionInvokeResult<WorkPlan> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<WorkPlan>(BuilderFactory.DefaultBulder().List<WorkPlan>(filters), param, ref result);
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
        public ModelInvokeResult<WorkPlanPK> Create(WorkPlan workPlan)
        {
            ModelInvokeResult<WorkPlanPK> result = new ModelInvokeResult<WorkPlanPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (workPlan.PlanId == GlobalManager.GuidAsAutoGenerate)
                {
                    workPlan.PlanId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetCreateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkPlanPK { PlanId = workPlan.PlanId };
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
        [WebInvoke(UriTemplate = "{strPlanId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WorkPlanPK> Update(string strPlanId, WorkPlan workPlan)
        {
            ModelInvokeResult<WorkPlanPK> result = new ModelInvokeResult<WorkPlanPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PlanId = strPlanId.ToGuid();
                if (_PlanId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                workPlan.PlanId = _PlanId;
                /***********************begin 自定义代码*******************/
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetUpdateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkPlanPK { PlanId = _PlanId };

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
        [WebInvoke(UriTemplate = "{strPlanId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WorkPlanPK> Delete(string strPlanId)
        {
            ModelInvokeResult<WorkPlanPK> result = new ModelInvokeResult<WorkPlanPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PlanId = strPlanId.ToGuid();
                if (_PlanId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                WorkPlanPK pk = new WorkPlanPK { PlanId = _PlanId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new WorkPlan().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkPlanPK { PlanId = _PlanId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strPlanIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strPlanIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrPlanIds = strPlanIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrPlanIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WorkPlan().GetDeleteMethodName();
                foreach (string strPlanId in arrPlanIds)
                {
                    WorkPlanPK pk = new WorkPlanPK { PlanId = strPlanId.ToGuid() };
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
        [WebInvoke(UriTemplate = "Nullify/{strPlanId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WorkPlanPK> Nullify(string strPlanId)
        {
            ModelInvokeResult<WorkPlanPK> result = new ModelInvokeResult<WorkPlanPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _PlanId = strPlanId.ToGuid();
                if (_PlanId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                WorkPlan workPlan = new WorkPlan { PlanId = _PlanId, Status = 0 };
                /***********************begin 自定义代码*******************/
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetUpdateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkPlanPK { PlanId = _PlanId };
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
        [WebInvoke(UriTemplate = "NullifySelected", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(IList<WorkPlan> workPlans)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (workPlans.Count == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new WorkPlan().GetUpdateMethodName();
                string planIds = "";
                foreach (var workPlan in workPlans)
                {
                    planIds += "," + workPlan.PlanId; 
                    /***********************begin 自定义代码*******************/
                    workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                    workPlan.OperatedOn = DateTime.Now; 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                planIds = planIds.Substring(1);
                planIds = "'" + planIds + "'";
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery("delete from Pam_WorkExecute where DATEDIFF(D,RemindTime,GETDATE())=0 and OldManId in (select OldManId from Pam_WorkPlan where PlanId in (" + planIds + "))");
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, WorkPlanPK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strPlanId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<WorkPlan> Load(string strPlanId)
        {
            ModelInvokeResult<WorkPlan> result = new ModelInvokeResult<WorkPlan> { Success = true };
            try
            {
                Guid? _PlanId = strPlanId.ToGuid();
                if (_PlanId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var workPlan = BuilderFactory.DefaultBulder().Load<WorkPlan, WorkPlanPK>(new WorkPlanPK { PlanId = _PlanId });
                result.instance = workPlan;
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

        #region 获取老人每天未设置的服务项目
        [WebGet(UriTemplate = "GetWorkItemList/{oldManId},{planDate}", ResponseFormat = WebMessageFormat.Json)]
        public IList<StringObjectDictionary> GetWorkItemList(string oldManId, string planDate)
        { 
            StringObjectDictionary filters = new { OldManId = oldManId, PlanDate = planDate }.ToStringObjectDictionary();
            return BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkItemListForEveryDay", filters);
        }
        #endregion

        #region 创建 CreateAll
        [WebInvoke(UriTemplate = "CreateAll", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CreateAll(IList<WorkPlan> workPlans)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string oldManId = "";
                string roomId = "";
                List<string> arroldManIds=new List<string>();
                foreach (WorkPlan workPlan in workPlans)
                {
                    if (workPlan.PlanId == GlobalManager.GuidAsAutoGenerate)
                    {
                        workPlan.PlanId = Guid.NewGuid();
                    }
                    /***********************begin 自定义代码*******************/
                    workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                    workPlan.OperatedOn = DateTime.Now;
                    if (oldManId != workPlan.OldManId.ToString())
                    {
                        string sql = "select RoomId from Pam_RoomOldMan where OldManId='" + workPlan.OldManId.ToString() + "' and EndDate>GETDATE()";
                        var ret = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql) ;
                        if (ret.Count == 1)
                        {
                            roomId = ret[0]["RoomId"].ToString();
                            oldManId = workPlan.OldManId.ToString();
                            arroldManIds.Add(workPlan.OldManId.ToString());
                        }
                        else {
                            result.Success = false;
                            result.ErrorMessage = "老人所在的房间出现异常";
                            return result;
                        }
                    }
                    workPlan.RoomId = roomId.ToGuid();
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetCreateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    /***********************begin 自定义代码*******************/
                    /***********************此处添加自定义代码*****************/
                    /***********************end 自定义代码*********************/
                }
                if (statements.Count > 0)
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);

                    foreach (string arroldManId in arroldManIds)
                    {
                        SPParam spParam = new { }.ToSPParam();
                        spParam["OldManId"] = arroldManId.ToGuid();
                        spParam["BatchFlag"] = 0;
                        BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                    }
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
         
        #region 修改 UpdateAll
        [WebInvoke(UriTemplate = "UpdateAll", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult UpdateAll(IList<WorkPlan> workPlans)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                string oldManId = "";
                List<string> arroldManIds = new List<string>();
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                foreach (WorkPlan workPlan in workPlans)
                {
                    Guid? _PlanId = workPlan.PlanId;
                    if (_PlanId == null)
                    {
                        result.Success = false;
                        result.ErrorCode = 59996;
                        return result;
                    }
                    workPlan.PlanId = _PlanId;
                    /***********************begin 自定义代码*******************/
                    workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                    workPlan.OperatedOn = DateTime.Now;
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetUpdateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

                    if (oldManId != workPlan.OldManId.ToString())
                    {
                        arroldManIds.Add(workPlan.OldManId.ToString());                    
                    }
                }
                if (statements.Count > 0)
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                    foreach (string arroldManId in arroldManIds)
                    {
                        SPParam spParam = new { }.ToSPParam();
                        spParam["OldManId"] = arroldManId.ToGuid();
                        spParam["BatchFlag"] = 0;
                        BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                    }
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
        
        #region 重新生成工作执行表
        [WebInvoke(UriTemplate = "GenDailyWorkExcute/{oldManId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string GenDailyWorkExcute(string oldManId)
        {
            string result = "";
            try
            {
                SPParam spParam = new { }.ToSPParam();
                spParam["OldManId"] = oldManId.ToGuid();
                spParam["BatchFlag"] = 0;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 复制工作计划
        [WebInvoke(UriTemplate = "CopyWorkPlan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult CopyWorkPlan(CopyWorkPlan copyWorkPlan)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (copyWorkPlan.OldManIds == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string[] arrOldManIds = copyWorkPlan.OldManIds.Split('|');
                copyWorkPlan.OldManIds = "'" + string.Join("','",arrOldManIds) + "'";

                int days = (DateTime.Parse(copyWorkPlan.CopyTo_Start) - DateTime.Parse(copyWorkPlan.CopyFrom_Start)).Days;
                copyWorkPlan.Days = days;

                copyWorkPlan.OperatedBy = NormalSession.UserId.ToGuid();
                StringObjectDictionary filters = copyWorkPlan.ToStringObjectDictionary(false);
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "CopyWorkPlan_Week", ParameterObject = copyWorkPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
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
    public class CopyWorkPlan
    {
        public string OldManIds { get; set; }
        public string CopyFrom_Start { get; set; }
        public string CopyFrom_End { get; set; }
        public string CopyTo_Start { get; set; }
        public string CopyTo_End { get; set; }
        public int Days { get; set; }
        public Guid? OperatedBy { get; set; }
    }
}

