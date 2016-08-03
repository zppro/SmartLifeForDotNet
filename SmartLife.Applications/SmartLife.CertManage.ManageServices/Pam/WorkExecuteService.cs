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
    public class WorkExecuteService : BaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<WorkExecute> List()
        {
            CollectionInvokeResult<WorkExecute> result = new CollectionInvokeResult<WorkExecute> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<WorkExecute>();
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
        public CollectionInvokeResult<WorkExecute> Query(string strParms)
        {
            CollectionInvokeResult<WorkExecute> result = new CollectionInvokeResult<WorkExecute> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<WorkExecute>(dictionary);
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
        public JqgridCollectionInvokeResult<WorkExecute> ListForJqgrid(JqgridCollectionParam<WorkExecute> param)
        {
            JqgridCollectionInvokeResult<WorkExecute> result = new JqgridCollectionInvokeResult<WorkExecute> { Success = true };
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

                gridCollectionPager.JqgridDoPage<WorkExecute>(BuilderFactory.DefaultBulder().List<WorkExecute>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<WorkExecute> ListForEasyUIgrid(EasyUIgridCollectionParam<WorkExecute> param)
        {
            EasyUIgridCollectionInvokeResult<WorkExecute> result = new EasyUIgridCollectionInvokeResult<WorkExecute> { Success = true };
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

                gridCollectionPager.EasyUIgridDoPage<WorkExecute>(BuilderFactory.DefaultBulder().List<WorkExecute>(filters), param, ref result);
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
        public ModelInvokeResult<WorkExecutePK> Create(WorkExecute workExecute)
        {
            ModelInvokeResult<WorkExecutePK> result = new ModelInvokeResult<WorkExecutePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workExecute.GetCreateMethodName(), ParameterObject = workExecute.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkExecutePK { Id = workExecute.Id };
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
        public ModelInvokeResult<WorkExecutePK> Update(string strId, WorkExecute workExecute)
        {
            ModelInvokeResult<WorkExecutePK> result = new ModelInvokeResult<WorkExecutePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>(); 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workExecute.GetUpdateMethodName(), ParameterObject = workExecute.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkExecutePK { Id = Int32.Parse(strId) };

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
        public ModelInvokeResult<WorkExecutePK> Delete(string strId)
        {
            ModelInvokeResult<WorkExecutePK> result = new ModelInvokeResult<WorkExecutePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                WorkExecutePK pk = new WorkExecutePK { Id = Int32.Parse(strId) };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new WorkExecute().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkExecutePK { Id = Int32.Parse(strId) };
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
                string statementName = new WorkExecute().GetDeleteMethodName();
                foreach (string strId in arrIds)
                {
                    WorkExecutePK pk = new WorkExecutePK { Id = Int32.Parse(strId) };
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
        public ModelInvokeResult<WorkExecutePK> Nullify(string strId)
        {
            ModelInvokeResult<WorkExecutePK> result = new ModelInvokeResult<WorkExecutePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                WorkExecute workExecute = new WorkExecute { Id = Int32.Parse(strId)  }; 
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = workExecute.GetUpdateMethodName(), ParameterObject = workExecute.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new WorkExecutePK { Id = Int32.Parse(strId) };
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
                string statementName = new WorkExecute().GetUpdateMethodName();
                foreach (string strId in arrIds)
                {
                    WorkExecute workExecute = new WorkExecute { Id = Int32.Parse(strId) }; 
                    /***********************end 自定义代码*********************/
                    statements.Add(new IBatisNetBatchStatement { StatementName = statementName, ParameterObject = workExecute.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
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
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, WorkExecutePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<WorkExecute> Load(string strId)
        {
            ModelInvokeResult<WorkExecute> result = new ModelInvokeResult<WorkExecute> { Success = true };
            try
            {
                var workExecute = BuilderFactory.DefaultBulder().Load<WorkExecute, WorkExecutePK>(new WorkExecutePK { Id = Int32.Parse(strId) });
                result.instance = workExecute;
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

        #region EasyUIgrid数据格式的列表 ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListForEasyUIgridByPage(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                        if (field.key == "WorkItemStatus_Start")
                        {
                            if (field.value == "SS002") { whereClause.Add(" ServeManArriveTime is null and Reminded<RemindMax "); }
                            else if (field.value == "SS003") { whereClause.Add(" ServeManArriveTime is null and Reminded=RemindMax "); }
                            else if (field.value == "SS004") { whereClause.Add(" ServeManArriveTime is not null and ServeManLeaveTime is null "); }
                            else if (field.value == "SS005") { whereClause.Add(" ServeManArriveTime is not null and ServeManLeaveTime is not null "); }
                        }
                        else
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
                        fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                    }
                    if (fuzzys.Count > 0)
                    {
                        whereClause.Add("(" + string.Join((string.IsNullOrEmpty(param.fuzzyFieldOP) ? " AND " : " " + param.fuzzyFieldOP + " "), fuzzys.ToArray()) + ")");
                    }
                }
                /***********************end 模糊查询***********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("ListWorkExecuteByPage", filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取当天实时监控信息
        [WebGet(UriTemplate = "GetWorkExecuteMonitor/{stationId},{strShowType}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkExecuteMonitor(string stationId, string strShowType)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters.Add("StationId", Guid.Parse(stationId));
                if (GlobalManager.isAgencyUser(new User() { UserId = NormalSession.UserId.ToGuid() }))
                {
                    filters.Add("UserId", NormalSession.UserId.ToGuid());
                }
                //监控模式
                if (!string.IsNullOrEmpty(strShowType))
                {
                    //当前时间一个小时之前所有未完成服务项目数据，以及当前时间前后一个小时之内所有数据
                    filters.Add("WhereClause", " ((a.RemindTime<DATEADD(HH,-1,GETDATE()) and a.ServeManLeaveTime is null) " +
                    " or ( a.RemindTime between DATEADD(HH,-1,GETDATE()) and DATEADD(HH,1,GETDATE())) ) ");
                }

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("DayOfWorkExecuteMonitor", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 获取指定房间内的床位和入住老人的信息
        [WebGet(UriTemplate = "GetRoomInfo/{stationId},{roomId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetRoomInfo(string stationId, string roomId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                filters.Add("StationId", Guid.Parse(stationId));
                filters.Add("RoomId", Guid.Parse(roomId)); 
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetRoomInfoForMonitor", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 获取指定房间内的报警信息
        [WebGet(UriTemplate = "GetCautionInfoForRoom/Query?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetCautionInfoForRoom(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            { 
                var dictionary = new StringObjectDictionary().MixInJson(strParms); 
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCautionInfo", dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 服务情况统计
        //[WebGet(UriTemplate = "GetWorkItemTimes", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "GetWorkItemTimes", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkItemTimes(GetWorkItemTimesParam parms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };
            try
            {
                if (!string.IsNullOrEmpty(parms.StationId.ToString()) && !string.IsNullOrEmpty(parms.RemindTime_Start.ToString()) && !string.IsNullOrEmpty(parms.RemindTime_End.ToString()))
                {
                    SPParam spParam = new { StationId = parms.StationId, RemindTime_Start = parms.RemindTime_Start, RemindTime_End = parms.RemindTime_End }.ToSPParam();
                    if (parms.UserId!=null&&!string.IsNullOrEmpty(parms.UserId.ToString())) {spParam.Add("UserId",parms.UserId); }
                    if (parms.WorkItem!=null&&!string.IsNullOrEmpty(parms.WorkItem.ToString())) { spParam.Add("WorkItem", parms.WorkItem); }
                    result.rows = BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Pam_GetWorkItemForServeMan", spParam);
                    if (spParam.ErrorCode > 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = spParam.ErrorMessage;
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "请检查机构和查询时间是否存在";
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


        #region 异常完成的工作
        [WebInvoke(UriTemplate = "ListWorkItemUnfinished", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<StringObjectDictionary> ListWorkItemUnfinished(EasyUIgridCollectionParam<StringObjectDictionary> param)
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
                    List<string> fuzzys = new List<string>();
                    foreach (var field in param.fuzzyFields)
                    {
                        if (field.key == "UserIds" && field.value!="")
                        {
                            whereClause.Add("u.UserId in (" + field .value+ ")");
                        }
                        else if (field.key == "OldManIds" && field.value != "")
                        {
                            whereClause.Add("p.OldManId in (" + field.value + ")");
                        }
                        else if (field.key == "WorkItems" && field.value != "")
                        {
                            whereClause.Add("w.WorkItem in (" + field.value + ")");
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
                whereClause.Add(" ServeManLeaveTime is null ");
                /***********************end 模糊查询***********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4StringObjectDictionary("WorkItemUnfinished", filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 大屏幕调用机构即时服务信息
        [WebGet(UriTemplate = "LoadAgencyServicesForBigScreen/{serveStationId},{timeSpanOfMinute}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadAgencyServicesForBigScreen(string serveStationId, string timeSpanOfMinute)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServeStationId = serveStationId, timeSpanOfMinute = Convert.ToInt32(timeSpanOfMinute) }.ToStringObjectDictionary(); 
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetAgencyServeForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 大屏幕调用机构即时服务信息统计数据
        [WebGet(UriTemplate = "LoadAgencyServicesCountForBigScreen/{serveStationId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadAgencyServicesCountForBigScreen(string serveStationId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServeStationId = serveStationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetAgencyServeCountForBigScreen", filters);
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
    public class GetWorkItemTimesParam
    {
        public Guid? StationId { get; set; }
        public Guid? UserId { get;set; }
        public string RemindTime_Start { get; set; }
        public string RemindTime_End { get; set; }
        public string WorkItem { get; set; }
    }
}

