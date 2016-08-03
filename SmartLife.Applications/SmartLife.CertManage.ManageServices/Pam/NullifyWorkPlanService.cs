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
    public class NullifyWorkPlanService : BaseWcfService
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
                if (result.Success)
                {
                    SPParam spParam = new { }.ToSPParam();
                    spParam["OldManId"] = workPlan.OldManId;
                    spParam["BatchFlag"] = 0;
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);                
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
                if (result.Success)
                {
                    SPParam spParam = new { }.ToSPParam();
                    spParam["OldManId"] = workPlan.OldManId;
                    spParam["BatchFlag"] = 0;
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
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
                if (result.Success)
                {
                    OldManMappingServeManRemove(strPlanId); 
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

        #region 作废所选 NullifySelected
        [WebInvoke(UriTemplate = "NullifySelected/{strPlanIds}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult NullifySelected(string strPlanIds)
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
                string statementName = new WorkPlan().GetUpdateMethodName();
                foreach (string strPlanId in arrPlanIds)
                {
                    WorkPlan workPlan = new WorkPlan { PlanId = strPlanId.ToGuid(), Status = 0 };
                    /***********************begin 自定义代码*******************/
                    workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                    workPlan.OperatedOn = DateTime.Now; 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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

        #region 自定义

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridByPage(EasyUIgridCollectionParam<StringObjectDictionary> param)
        {
            EasyUIgridCollectionInvokeResult<StringObjectDictionary> result = new EasyUIgridCollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                List<string> whereClause = new List<string>();
                /**********************************************************/
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
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.value != "")
                        {
                            if (field.key.Contains("_Start"))
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
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ListForEasyUIgridDictionaryItemByPage", filters, param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 根据OldManID或者UserID作废工作计划
        [WebInvoke(UriTemplate = "NullifyWorkPlan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WorkPlanPK> NullifyWorkPlan(WorkPlan workPlan)
        {
            ModelInvokeResult<WorkPlanPK> result = new ModelInvokeResult<WorkPlanPK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                /***********************begin 自定义代码*******************/
                workPlan.Status = 0;
                workPlan.OperatedBy = NormalSession.UserId.ToGuid();
                workPlan.OperatedOn = DateTime.Now;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = "NullifyWorkPlan_Update", ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                if (workPlan.UserId == null && workPlan.OldManId == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "操作对象不能为空,工作计划作废失败";
                }
                else
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                }
                /***********************end 自定义代码*********************/
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 新增工作计划项
        [WebInvoke(UriTemplate = "CreateWorkPlanS/{oldManIds}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<WorkPlanPK> CreateWorkPlanS(string oldManIds, WorkPlan workPlan)
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
                var arroldManIds = oldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arroldManIds.Length < 1)
                {
                    result.Success = false;
                    result.ErrorMessage = "请选择老人";
                    return result;
                }

                /***********************检查是否已经存在此项工作计划*********************/
                foreach (string arroldManId in arroldManIds)
                {
                    string sql = "";
                    //string sql = "select * from Pam_WorkPlan where OldManId='" + arroldManId + "' and UserId='" + workPlan.UserId + "' and WorkItem='" + workPlan.WorkItem + "' and PlanSchedule='" + workPlan.PlanSchedule + "'";
                    int i = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                    if (i > 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = "此项工作计划已经存在";
                        return result;
                    }
                }
                /***********************检查是否已经存在此项工作计划*********************/

                foreach (string arroldManId in arroldManIds)
                {
                    workPlan.OldManId = arroldManId.ToGuid();
                    workPlan.PlanId = Guid.NewGuid();
                    statements.Add(new IBatisNetBatchStatement { StatementName = workPlan.GetCreateMethodName(), ParameterObject = workPlan.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkPlanPK { PlanId = workPlan.PlanId };
                if (result.Success)
                {
                    /***********************如果老人和服务人员没有建立关系，这里建立关系*********************/
                    OldManMappingServeManAdd(workPlan.UserId.ToString(), oldManIds);
                    /***********************如果老人和服务人员没有建立关系，这里建立关系*********************/
                    /***********************重新生成工作执行表*********************/
                    foreach (string arroldManId in arroldManIds)
                    {
                        SPParam spParam = new { }.ToSPParam();
                        spParam["OldManId"] = arroldManId.ToGuid();
                        spParam["BatchFlag"] = 0;
                        BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
                    }
                }
                /***********************重新生成工作执行表*********************/
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

        #region 检查老人和服务人员没有建立关系，如果没有就建立关系
        private void OldManMappingServeManAdd(string userId, string oldManIds)
        {
            OldManMappingServeMan oldManMappingServeManAdd = new OldManMappingServeMan();
            List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
            var arroldManIds = oldManIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (arroldManIds.Length > 0)
            {
                foreach (string arroldManId in arroldManIds)
                {
                    string sql = "select * from Pam_OldManMappingServeMan where EndTime>GETDATE() and OldManId='" + arroldManId + "' and UserId='" + userId + "'";
                    int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                    if (count < 1)
                    {
                        oldManMappingServeManAdd.OldManId = arroldManId.ToGuid();
                        oldManMappingServeManAdd.OperatedBy = NormalSession.UserId.ToGuid();
                        oldManMappingServeManAdd.OperatedOn = DateTime.Now;
                        oldManMappingServeManAdd.BeginTime = DateTime.Now;
                        oldManMappingServeManAdd.EndTime = Convert.ToDateTime("2999-12-31 23:59:59");
                        oldManMappingServeManAdd.UserId = userId.ToGuid();
                        statements.Add(new IBatisNetBatchStatement { StatementName = oldManMappingServeManAdd.GetCreateMethodName(), ParameterObject = oldManMappingServeManAdd.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
                }
                if (statements.Count > 0)
                {
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                }
            }
        }
        #endregion

        #region 检查老人和服务人员没有建立关系，如果没有就建立关系
        public void OldManMappingServeManRemove(string planId)
        {
            OldManMappingServeMan oldManMappingServeManRemove = new OldManMappingServeMan();
            List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
            string sql = "select UserId,OldManId from Pam_WorkPlan where  PlanId='" + planId + "'";
            var row = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sql);
            if (row.Count > 0)
            {
                string oldManId = row[0]["OldManId"].ToString();
                string userId = row[0]["UserId"].ToString();
                sql = "select * from Pam_WorkPlan where Status=1 and OldManId='" + oldManId + "' and UserId='" + userId + "' ";
                int count = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(sql);
                if (count < 1)
                {
                    oldManMappingServeManRemove.EndTime = DateTime.Now;
                    oldManMappingServeManRemove.OperatedBy = NormalSession.UserId.ToGuid();
                    oldManMappingServeManRemove.OperatedOn = DateTime.Now;
                    oldManMappingServeManRemove.OldManId = oldManId.ToGuid();
                    oldManMappingServeManRemove.UserId = userId.ToGuid();
                    statements.Add(new IBatisNetBatchStatement { StatementName = "OldManMappingServeMan_Remove", ParameterObject = oldManMappingServeManRemove.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                    BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                }
                SPParam spParam = new { }.ToSPParam();
                spParam["OldManId"] = oldManId.ToGuid();
                spParam["BatchFlag"] = 0;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_GenDailyWorkExcute", spParam);
            }
        }
        #endregion

        #endregion
    }
}

