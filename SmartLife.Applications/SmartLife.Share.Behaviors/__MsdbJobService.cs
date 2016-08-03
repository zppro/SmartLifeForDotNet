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
using SmartLife.Share.Models.SQLServer;

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __MsdbJobService : AppBaseWcfService
    {
        #region 获取作业
        #region EasyUIgrid数据格式的列表 ListForEasyUIgrid
        [WebInvoke(UriTemplate = "ListForEasyUIgrid/{connectId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<MsdbJob> ListForEasyUIgrid(string connectId, EasyUIgridCollectionParam<MsdbJob> param)
        {
            EasyUIgridCollectionInvokeResult<MsdbJob> result = new EasyUIgridCollectionInvokeResult<MsdbJob> { Success = true };
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
                filters.Add("job_id", DBNull.Value);
                filters.Add("job_name", DBNull.Value);
                filters.Add("job_aspect", DBNull.Value);
                filters.Add("job_type", DBNull.Value);
                filters.Add("owner_login_name", GlobalManager.Job_Database_User);
                filters.Add("subsystem", DBNull.Value);
                filters.Add("category_name", DBNull.Value);
                filters.Add("enabled", DBNull.Value);
                filters.Add("execution_status", DBNull.Value);
                filters.Add("date_comparator", DBNull.Value);
                filters.Add("date_created", DBNull.Value);
                filters.Add("date_last_modified", DBNull.Value);
                filters.Add("description", DBNull.Value);
                var jobs = BuilderFactory.DefaultBulder(connectId).ExecuteSPForQuery<MsdbJob>("SP_Tol_GetJobs", filters);//.Where(item => item.name.StartsWith("SmartLife-UI-"))
                //var jobs = BuilderFactory.DefaultBulder(connectId).ExecuteNativeSqlForQuery("exec sp_help_job");
                gridCollectionPager.EasyUIgridDoPage<MsdbJob>(jobs, param, ref result);
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

        #region 获取作业计划
        #region 普通列表 List
        [WebGet(UriTemplate = "Schedules/{job_id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<MsdbJobSchedule> GetSchedules(string job_id)
        {
            CollectionInvokeResult<MsdbJobSchedule> result = new CollectionInvokeResult<MsdbJobSchedule> { Success = true };
            try
            {
                Guid? _job_id = job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }

                StringObjectDictionary paramObject = new StringObjectDictionary { };
                paramObject["job_id"] = _job_id;
                paramObject["job_name"] = DBNull.Value;
                paramObject["schedule_name"] = DBNull.Value;
                paramObject["schedule_id"] = DBNull.Value;
                paramObject["include_description"] = DBNull.Value;
                result.rows = BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPForQuery<MsdbJobSchedule>("SP_Tol_GetJobSchedules", paramObject);
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

        #region 更改作业基本信息
        [WebInvoke(UriTemplate = "{str_job_id}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Update(string str_job_id, MsdbJob4Update param)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                Guid? _job_id = str_job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                param.job_id = _job_id;
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();

                //仅更改作业的name和description
                var paramObjectForJob = new { job_id = param.job_id, new_name = param.new_name, description = param.description }.ToStringObjectDictionary(false);
                paramObjectForJob["job_name"] = DBNull.Value;
                paramObjectForJob["enabled"] = DBNull.Value;
                paramObjectForJob["start_step_id"] = DBNull.Value;
                paramObjectForJob["category_name"] = DBNull.Value;
                paramObjectForJob["owner_login_name"] = DBNull.Value;
                paramObjectForJob["notify_level_eventlog"] = DBNull.Value;
                paramObjectForJob["notify_level_email"] = DBNull.Value;
                paramObjectForJob["notify_level_netsend"] = DBNull.Value;
                paramObjectForJob["notify_level_page"] = DBNull.Value;
                paramObjectForJob["notify_email_operator_name"] = DBNull.Value;
                paramObjectForJob["notify_netsend_operator_name"] = DBNull.Value;
                paramObjectForJob["notify_page_operator_name"] = DBNull.Value;
                paramObjectForJob["delete_level"] = DBNull.Value;
                paramObjectForJob["automatic_post"] = DBNull.Value;
                
                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Tol_UpdateJob", ParameterObject = paramObjectForJob, Type = SqlExecuteType.UPDATE });

                var paramObjectForJobSchedule = new { job_id = param.job_id, name = param.schedule_name, freq_type = param.freq_type, freq_interval = param.freq_interval, freq_subday_type = param.freq_subday_type, freq_subday_interval = param.freq_subday_interval }.ToStringObjectDictionary(false);
                paramObjectForJobSchedule["job_name"] = DBNull.Value;
                paramObjectForJobSchedule["new_name"] = DBNull.Value;
                paramObjectForJobSchedule["enabled"] = DBNull.Value;
                paramObjectForJobSchedule["freq_relative_interval"] = DBNull.Value;
                paramObjectForJobSchedule["freq_recurrence_factor"] = DBNull.Value;
                paramObjectForJobSchedule["active_start_date"] = DBNull.Value;
                paramObjectForJobSchedule["active_end_date"] = DBNull.Value;
                paramObjectForJobSchedule["active_start_time"] = DBNull.Value;
                paramObjectForJobSchedule["active_end_time"] = DBNull.Value;
                paramObjectForJobSchedule["automatic_post"] = DBNull.Value;
                statements.Add(new IBatisNetBatchStatement { StatementName = "SP_Tol_UpdateJobSchedule", ParameterObject = paramObjectForJobSchedule, Type = SqlExecuteType.UPDATE });
                //BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPNoneQuery("SP_Tol_UpdateJob", paramObjectForJob);
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 启用作业
        [WebInvoke(UriTemplate = "Enable/{str_job_id}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Enable(string str_job_id)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                Guid? _job_id = str_job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                StringObjectDictionary  paramObject = new StringObjectDictionary();
                paramObject["job_id"] = _job_id;
                paramObject["enabled"] = 1;
                paramObject["job_name"] = DBNull.Value;
                paramObject["new_name"] = DBNull.Value;
                paramObject["description"] = DBNull.Value;
                paramObject["start_step_id"] = DBNull.Value;
                paramObject["category_name"] = DBNull.Value;
                paramObject["owner_login_name"] = DBNull.Value;
                paramObject["notify_level_eventlog"] = DBNull.Value;
                paramObject["notify_level_email"] = DBNull.Value;
                paramObject["notify_level_netsend"] = DBNull.Value;
                paramObject["notify_level_page"] = DBNull.Value;
                paramObject["notify_email_operator_name"] = DBNull.Value;
                paramObject["notify_netsend_operator_name"] = DBNull.Value;
                paramObject["notify_page_operator_name"] = DBNull.Value;
                paramObject["delete_level"] = DBNull.Value;
                paramObject["automatic_post"] = DBNull.Value;
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPNoneQuery("SP_Tol_UpdateJob", paramObject);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 禁用作业
        [WebInvoke(UriTemplate = "Disable/{str_job_id}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Disable(string str_job_id)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                Guid? _job_id = str_job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                StringObjectDictionary paramObject = new StringObjectDictionary();
                paramObject["job_id"] = _job_id;
                paramObject["enabled"] = 0;
                paramObject["job_name"] = DBNull.Value;
                paramObject["new_name"] = DBNull.Value;
                paramObject["description"] = DBNull.Value;
                paramObject["start_step_id"] = DBNull.Value;
                paramObject["category_name"] = DBNull.Value;
                paramObject["owner_login_name"] = DBNull.Value;
                paramObject["notify_level_eventlog"] = DBNull.Value;
                paramObject["notify_level_email"] = DBNull.Value;
                paramObject["notify_level_netsend"] = DBNull.Value;
                paramObject["notify_level_page"] = DBNull.Value;
                paramObject["notify_email_operator_name"] = DBNull.Value;
                paramObject["notify_netsend_operator_name"] = DBNull.Value;
                paramObject["notify_page_operator_name"] = DBNull.Value;
                paramObject["delete_level"] = DBNull.Value;
                paramObject["automatic_post"] = DBNull.Value;
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPNoneQuery("SP_Tol_UpdateJob", paramObject);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 开始作业
        [WebInvoke(UriTemplate = "Start/{str_job_id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Start(string str_job_id)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                Guid? _job_id = str_job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                StringObjectDictionary paramObject = new StringObjectDictionary();
                paramObject["job_name"] = DBNull.Value;
                paramObject["job_id"] = _job_id; 
                paramObject["error_flag"] = DBNull.Value;
                paramObject["server_name"] = DBNull.Value;
                paramObject["step_name"] = DBNull.Value;
                paramObject["output_flag"] = DBNull.Value;
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPNoneQuery("SP_Tol_StartJob", paramObject);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 停止作业
        [WebInvoke(UriTemplate = "Stop/{str_job_id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult Stop(string str_job_id)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                Guid? _job_id = str_job_id.ToGuid();
                if (_job_id == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                StringObjectDictionary paramObject = new StringObjectDictionary();
                paramObject["job_name"] = DBNull.Value;
                paramObject["job_id"] = _job_id;
                paramObject["originating_server"] = DBNull.Value;
                paramObject["server_name"] = DBNull.Value;
                BuilderFactory.DefaultBulder(GetHttpHeader(GlobalManager.ConnectIdKey)).ExecuteSPNoneQuery("SP_Tol_StopJob", paramObject);
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