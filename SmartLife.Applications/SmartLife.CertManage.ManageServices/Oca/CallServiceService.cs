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

namespace SmartLife.CertManage.ManageServices.Oca
{
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CallServiceService : AppBaseWcfService
    {
        #region CMS标准接口

        #region 普通列表 List
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<CallService> List()
        {
            CollectionInvokeResult<CallService> result = new CollectionInvokeResult<CallService> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<CallService>();
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
        public CollectionInvokeResult<CallService> Query(string strParms)
        {
            CollectionInvokeResult<CallService> result = new CollectionInvokeResult<CallService> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().List<CallService>(dictionary);
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
        public JqgridCollectionInvokeResult<CallService> ListForJqgrid(JqgridCollectionParam<CallService> param)
        {
            JqgridCollectionInvokeResult<CallService> result = new JqgridCollectionInvokeResult<CallService> { Success = true };
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

                gridCollectionPager.JqgridDoPage<CallService>(BuilderFactory.DefaultBulder().List<CallService>(filters), param, ref result);
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
        public EasyUIgridCollectionInvokeResult<CallService> ListForEasyUIgrid(EasyUIgridCollectionParam<CallService> param)
        {
            EasyUIgridCollectionInvokeResult<CallService> result = new EasyUIgridCollectionInvokeResult<CallService> { Success = true };
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
                        if (field.key == "GroupType")
                        {
                            whereClause.Add("ServiceQueueId in (select distinct GroupId from Pub_Group where GroupType='" + field.value + "' and Status=1)");
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

                gridCollectionPager.EasyUIgridDoPage<CallService>(BuilderFactory.DefaultBulder().List<CallService>(filters), param, ref result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表4Page ListForEasyUIgridByPage
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<CallService> ListForEasyUIgridByPage(EasyUIgridCollectionParam<CallService> param)
        {
            EasyUIgridCollectionInvokeResult<CallService> result = new EasyUIgridCollectionInvokeResult<CallService> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    if (param.instance.ServiceArchive == "00000") {
                        param.instance.ServiceArchive = null;
                    }
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                //判断是否是座席
                bool isCCSeat = GlobalManager.isCCSeat(new User() { UserCode = NormalSession.UserCode, UserName = NormalSession.UserName, UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType });
                //判断是否班长
                bool IsMonitor = GlobalManager.IsMonitor(new User() { UserCode = NormalSession.UserCode, UserName = NormalSession.UserName, UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType });
                if (isCCSeat && !IsMonitor)
                {
                    whereClause.Add(string.Format("(OperatedBy is null or OperatedBy='{0}')", NormalSession.UserId.ToGuid()));
                }
                /**********************************************************/
                if (param.filterFields != null)
                {
                    DateTime parseTime = new DateTime();
                    foreach (var field in param.filterFields)
                    {
                        if (field.key.IndexOf('_') > -1 && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format(" {0} >= '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                        else if (field.key.Equals("DoStatus_Start"))
                        {
                            if (field.value == "00009") continue;
                            string strDoStatus = "";
                            if (field.value == "00003")
                            {
                                strDoStatus = " DoStatus =3 ";
                            }
                            else
                            {
                                strDoStatus = " DoStatus <3 And ServiceCatalog='" + field.value + "' ";
                            }
                            whereClause.Add(strDoStatus);
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
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                
                /**********************************************************/
                
                gridCollectionPager.EasyUIgridDoPage4Model<CallService>(filters, param, ref result); 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region EasyUIgrid数据格式的列表4Page ListForEasyUIgridByPage2
        [WebInvoke(UriTemplate = "ListForEasyUIgridByPage2", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<CallService> ListForEasyUIgridByPage2(EasyUIgridCollectionParam<CallService> param)
        {
            EasyUIgridCollectionInvokeResult<CallService> result = new EasyUIgridCollectionInvokeResult<CallService> { Success = true };
            try
            {
                /***********************begin 复杂查询添加代码*********************/
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param.instance != null)
                {
                    if (param.instance.ServiceArchive == "00000")
                    {
                        param.instance.ServiceArchive = null;
                    }
                    filters = param.instance.ToStringObjectDictionary(false);
                }
                List<string> whereClause = new List<string>();
                //判断是否是座席
                bool isCCSeat = GlobalManager.isCCSeat(new User() { UserCode = NormalSession.UserCode, UserName = NormalSession.UserName, UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType });
                if (isCCSeat) {
                    whereClause.Add(string.Format("(OperatedBy is null or OperatedBy='{0}')", NormalSession.UserId.ToGuid()));
                }
                /**********************************************************/
                if (param.filterFields != null)
                {
                    DateTime parseTime = new DateTime();
                    foreach (var field in param.filterFields)
                    {
                        if (field.key == "GroupType")
                        {
                            whereClause.Add("ServiceQueueId in (select distinct GroupId from Pub_Group where GroupType='" + field.value + "' and Status=1)");
                        }
                        else if (DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format(" {0} >= '{1}' ", field.key.Substring(0, field.key.IndexOf('_')), parseTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                        else if (field.key.Equals("DoStatus_Start"))
                        {
                            if (field.value == "00009") continue;
                            string strDoStatus = "";
                            if (field.value == "00003")
                            {
                                strDoStatus = " DoStatus =3 ";
                            }
                            else
                            {
                                strDoStatus = " DoStatus <3 And ServiceCatalog='" + field.value + "' ";
                            }
                            whereClause.Add(strDoStatus);
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
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义查询代码*************/
                /***********************end 自定义代码*********************/
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                /**********************************************************/

                gridCollectionPager.EasyUIgridDoPage4Model<CallService>(filters, param, ref result);
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
        public ModelInvokeResult<CallServicePK> Create(CallService callService)
        {
            ModelInvokeResult<CallServicePK> result = new ModelInvokeResult<CallServicePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                if (callService.CallServiceId == GlobalManager.GuidAsAutoGenerate)
                {
                    callService.CallServiceId = Guid.NewGuid();
                }
                /***********************begin 自定义代码*******************/
                callService.OperatedBy = NormalSession.UserId.ToGuid();
                callService.OperatedOn = DateTime.Now;
                callService.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = callService.GetCreateMethodName(), ParameterObject = callService.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CallServicePK { CallServiceId = callService.CallServiceId };
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
        [WebInvoke(UriTemplate = "{strCallServiceId}", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<CallServicePK> Update(string strCallServiceId, CallService callService)
        {
            ModelInvokeResult<CallServicePK> result = new ModelInvokeResult<CallServicePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _CallServiceId = strCallServiceId.ToGuid();
                if (_CallServiceId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                callService.CallServiceId = _CallServiceId;
                /***********************begin 自定义代码*******************/
                callService.OperatedBy = NormalSession.UserId.ToGuid();
                callService.OperatedOn = DateTime.Now;
                callService.DataSource = GlobalManager.DIKey_00012_ManualEdit;
                //座席对老人的评价
                if (!string.IsNullOrEmpty(callService.EvaluateToOldMan)) {
                    callService.EvaluateTimeToOldMan = DateTime.Now;
                    callService.EvaluatedBy = NormalSession.UserId.ToGuid();
                }
                /***********************end 自定义代码*********************/
                statements.Add(new IBatisNetBatchStatement { StatementName = callService.GetUpdateMethodName(), ParameterObject = callService.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CallServicePK { CallServiceId = _CallServiceId };

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
        [WebInvoke(UriTemplate = "{strCallServiceId}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<CallServicePK> Delete(string strCallServiceId)
        {
            ModelInvokeResult<CallServicePK> result = new ModelInvokeResult<CallServicePK> { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Guid? _CallServiceId = strCallServiceId.ToGuid();
                if (_CallServiceId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                CallServicePK pk = new CallServicePK { CallServiceId = _CallServiceId };
                DeleteCascade(statements, pk);
                statements.Add(new IBatisNetBatchStatement { StatementName = new CallService().GetDeleteMethodName(), ParameterObject = pk, Type = SqlExecuteType.DELETE });
                /***********************begin 自定义代码*******************/
                /***********************此处添加自定义代码*****************/
                /***********************end 自定义代码*********************/
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
                result.instance = new CallServicePK { CallServiceId = _CallServiceId };
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
        [WebInvoke(UriTemplate = "DeleteSelected/{strCallServiceIds}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DeleteSelected(string strCallServiceIds)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                string[] arrCallServiceIds = strCallServiceIds.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrCallServiceIds.Length == 0)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                string statementName = new CallService().GetDeleteMethodName();
                foreach (string strCallServiceId in arrCallServiceIds)
                {
                    CallServicePK pk = new CallServicePK { CallServiceId = strCallServiceId.ToGuid() };
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
         
       

        #region 级联删除扩展接口 DeleteCascade
        private void DeleteCascade(List<IBatisNetBatchStatement> statements, CallServicePK pk)
        {
            //此处增加级联删除代码
        }
        #endregion

        #region 载入 Load
        [WebGet(UriTemplate = "{strCallServiceId}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<CallService> Load(string strCallServiceId)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };
            try
            {
                Guid? _CallServiceId = strCallServiceId.ToGuid();
                if (_CallServiceId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 59996;
                    return result;
                }
                var callService = BuilderFactory.DefaultBulder().Load<CallService, CallServicePK>(new CallServicePK { CallServiceId = _CallServiceId });
                result.instance = callService;
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

        #region 查询服务
        [WebGet(UriTemplate = "FindByClient/{strCaller},{strCallee},{strQueue}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<CallService> FindByClient(string strCaller, string strCallee,string strQueue)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                if (strQueue.Substring(strQueue.Length - 3) == "999")
                {
                }
                else
                {
                    filters = new { DoStatus = 0, ServiceQueueNo = strQueue }.ToStringObjectDictionary();
                }
                List<string> whereClause = new List<string>();
                whereClause.Add("OldManId in (select OldManId from  Oca_OldManConfigInfo where CallNo = '" + strCaller + "' or CallNo2='" + strCaller + "' or CallNo3='" + strCaller + "')");
                filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                filters.Add("OrderByClause", "CheckInTime desc");
                result.instance = BuilderFactory.DefaultBulder().List<CallService>(filters).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 查询服务
        [WebGet(UriTemplate = "FindByClient2/{strCaller},{strCallee},{strQueue}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<CallService> FindByClient2(string strCaller, string strCallee, string strQueue)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };
            try
            {
                StringObjectDictionary filters = new StringObjectDictionary();
                if (strQueue.Substring(strQueue.Length - 3) == "999"){
                    filters = new { DoStatus = 0, Caller = strCaller }.ToStringObjectDictionary();
                }
                else
                {
                    filters = new { DoStatus = 0, ServiceQueueNo = strQueue, Caller = strCaller, Callee = strCallee }.ToStringObjectDictionary();
                }
                List<string> whereClause = new List<string>();
                filters.Add("OrderByClause", "CheckInTime desc");
                result.instance = BuilderFactory.DefaultBulder().List<CallService>(filters).FirstOrDefault();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 根据IPCC通话记录uuid获取服务记录
        [WebGet(UriTemplate = "FindByUuidOfIPCC/{uuid}", ResponseFormat = WebMessageFormat.Json)]
        public ModelInvokeResult<CallService> FindByUuidOfeComm(string uuid)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };
            try
            { 
                result.instance = BuilderFactory.DefaultBulder().List<CallService>(new { UuidOfIPCC = uuid }.ToStringObjectDictionary()).FirstOrDefault();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 接手服务
        [WebInvoke(UriTemplate = "TakeOverCallServiceOfCallBack", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<CallService> TakeOverCallServiceOfCallBack(TakeOverCallServiceOfCallBackParam param)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };

            try
            {
                var spParam = param.ToSPParam(); 
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_TakeOverCallServiceOfCallBack", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
                }
                else
                {
                    result.instance = BuilderFactory.DefaultBulder().Load<CallService, CallServicePK>(new CallServicePK { CallServiceId = param.CallServiceId });
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

        #region 回拨
        [WebInvoke(UriTemplate = "DialBackForCallService", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ModelInvokeResult<CallService> DialBackForCallService(DialBackForCallServiceParam param)
        {
            ModelInvokeResult<CallService> result = new ModelInvokeResult<CallService> { Success = true };

            try
            {
                var spParam = param.ToSPParam();
                //string[] batchCallServiceIds = param.BatchCallServiceIds.Split(',');
                //if (batchCallServiceIds.Length > 1) {
                //    spParam["BatchCallServiceIds"] = "'" + string.Join("','", batchCallServiceIds.Where(s => !string.IsNullOrEmpty(s)).ToArray()) + "'";
                //}
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_DialBack_eComm", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
                }
                else
                {
                    result.instance = BuilderFactory.DefaultBulder().Load<CallService, CallServicePK>(new CallServicePK { CallServiceId = param.CallServiceId });
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

        #region 更新放弃来电标识
        [WebInvoke(UriTemplate = "AbandonByCaller", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult AbandonByCaller(AbandonParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_AbandonByCaller", spParam);
                if (spParam.ErrorCode != 0 && spParam.ErrorCode != 51000)
                {
                    result.ErrorCode = spParam.ErrorCode;
                    result.ErrorMessage = spParam.ErrorMessage;
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

        #region 居家养老首页调用
        [WebGet(UriTemplate = "LoadServices/{groupType},{doStatus}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<CallService> LoadServices(string groupType, string doStatus)
        {
            CollectionInvokeResult<CallService> result = new CollectionInvokeResult<CallService> { Success = true };
            try
            {
                StringObjectDictionary filters = new { DoStatus = int.Parse(doStatus), Status = 1 }.ToStringObjectDictionary();
                List<string> whereClause = new List<string>();
                if (int.Parse(doStatus) > 0)
                {
                    whereClause.Add("(OperatedBy='" + NormalSession.UserId + "' or OperatedBy is null )");
                }
                whereClause.Add("ServiceQueueId in (select GroupId from Pub_Group where Status=1 and GroupType= '" + groupType + "')");
                filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                filters.Add("OrderByClause", "CheckInTime desc");
                result.rows = BuilderFactory.DefaultBulder().List<CallService>(filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "LoadServiceWorkOrders/{groupType},{doStatus},{workorderStatuses}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServiceWorkOrders(string groupType, string doStatus, string workorderStatuses)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { WorkorderStatuses = workorderStatuses.Replace("-", ","), GroupType = groupType }.ToStringObjectDictionary();
                List<string> whereClause = new List<string>();
                if (int.Parse(doStatus) > 0)
                {
                    whereClause.Add("(a.OperatedBy='" + NormalSession.UserId + "' or a.OperatedBy is null )");
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                filters.Add("OrderByClause", "a.CheckInTime desc");
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetServiceWorkOrderInIndex", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 即时滚动页调用
        [WebGet(UriTemplate = "LoadServicesForScroll/{serviceCatalog},{scrollDate},{id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesForScroll(string serviceCatalog, string scrollDate, string id)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {

                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                List<string> whereClause = new List<string>();
                //即时滚动播报不用按坐席过滤服务
                //whereClause.Add("(OperatedBy='" + NormalSession.UserId + "' or OperatedBy is null)");
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceForScroll", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "StatServicesForScroll/{serviceCatalog},{scrollDate}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StatServicesForScroll(string serviceCatalog, string scrollDate)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {

                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();
                string sql = BuilderFactory.DefaultBulder().GetSql("StatCallServiceForScroll", filters);
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("StatCallServiceForScroll", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #endregion

        #region 大屏幕调用(辖区)
        [WebGet(UriTemplate = "LoadServicesForBigScreen/{serviceCatalog},{scrollDate},{id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesForBigScreen(string serviceCatalog, string scrollDate, string id)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                string[] nodesTimeSpan = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get("Sys_NodesTimeSpan")).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate, TimeSpanOfMinute = Convert.ToInt32(nodesTimeSpan[0]) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "StatServicesOfVouchForBigScreen/{serviceCatalog},{scrollDate}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StatServicesOfVouchForBigScreen(string serviceCatalog, string scrollDate)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("StatCallServiceOfVouchForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "LoadServicesOfVouchForBigScreen/{scrollDate},{id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesOfVouchForBigScreen( string scrollDate, string id)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                string[] nodesTimeSpan = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get("Sys_NodesTimeSpan")).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                StringObjectDictionary filters = new { ScrollDate = scrollDate, TimeSpanOfMinute = Convert.ToInt32(nodesTimeSpan[0]) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceOfVouchForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "StatServicesForBigScreen/{serviceCatalog},{scrollDate}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StatServicesForBigScreen(string serviceCatalog, string scrollDate)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("StatCallServiceForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "GetStatDataOfAllCountForBigScreen/{statDate}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetStatDataOfAllCountForBigScreen( string statDate)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StatDate = statDate }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetStatDataOfAllCountForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "GetStatDataOfVouchDurationForBigScreen", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetStatDataOfVouchDurationForBigScreen()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            { 
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetStatDataOfVouchDurationForBigScreen");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 大屏幕调用(街道)
        [WebGet(UriTemplate = "LoadServicesForBigScreenStreet/{serviceCatalog},{scrollDate},{id},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesForBigScreenStreet(string serviceCatalog, string scrollDate, string id, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                string[] nodesTimeSpan = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get("Sys_NodesTimeSpan")).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate, TimeSpanOfMinute = Convert.ToInt32(nodesTimeSpan[2]) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                string sql = PermissionsCategory();
                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( a.AreaId2 like '" + areaId + "%' or a.AreaId3  like '" + areaId + "%')");
                }
                else
                {
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        filters.Add("WhereClause", sql);
                    }
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "LoadServicesOfVouchForBigScreenStreet/{scrollDate},{id},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadServicesOfVouchForBigScreenStreet(string scrollDate, string id, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                string[] nodesTimeSpan = TypeConverter.ChangeString(CacheManager.ParmeterCacheProvider.Get("Sys_NodesTimeSpan")).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                StringObjectDictionary filters = new { ScrollDate = scrollDate, TimeSpanOfMinute = Convert.ToInt32(nodesTimeSpan[2]) }.ToStringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                string sql = PermissionsCategory();
                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( a.AreaId2 like '" + areaId + "%' or a.AreaId3  like '" + areaId + "%')");
                }
                else
                {
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        filters.Add("WhereClause", sql);
                    }
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceOfVouchForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "StatServicesForBigScreenStreet/{serviceCatalog},{scrollDate},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StatServicesForBigScreenStreet(string serviceCatalog, string scrollDate, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();

                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( a.AreaId2 like '" + areaId + "%' or a.AreaId3  like '" + areaId + "%')");
                }
                else
                {
                    string sql = PermissionsCategory();
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        filters.Add("WhereClause", sql);
                    }
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("StatCallServiceForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "StatServicesOfVouchForBigScreenStreet/{serviceCatalog},{scrollDate},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> StatServicesOfVouchForBigScreenStreet(string serviceCatalog, string scrollDate, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { ServiceCatalog = serviceCatalog, ScrollDate = scrollDate }.ToStringObjectDictionary();

                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( a.AreaId2 like '" + areaId + "%' or a.AreaId3  like '" + areaId + "%')");
                }
                else
                {
                    string sql = PermissionsCategory();
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        filters.Add("WhereClause", sql);
                    }
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("StatCallServiceOfVouchForBigScreen", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebGet(UriTemplate = "GetStatDataOfAllCountForBigScreenStreet/{statDate},{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetStatDataOfAllCountForBigScreenStreet(string statDate, string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                StringObjectDictionary filters = new { StatDate = statDate }.ToStringObjectDictionary();
                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( a.AreaId2 like '" + areaId + "%' or a.AreaId3  like '" + areaId + "%')");
                }
                else
                {
                    string sql = PermissionsCategory();
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        filters.Add("WhereClause", sql);
                    }
                }
                /////////////////////
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetStatDataOfAllCountForBigScreenStreet", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        
        [WebGet(UriTemplate = "GetStatDataOfVouchDurationForBigScreenStreet/{areaId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetStatDataOfVouchDurationForBigScreenStreet(string areaId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            { 
                StringObjectDictionary filters = new {  }.ToStringObjectDictionary();
                if (areaId != "AAA")
                {
                    filters.Add("WhereClause", " ( x.AreaId like '" + areaId + "%' or x.AreaId  like '" + areaId + "%')");
                }
                else
                {
                    string sql = PermissionsCategory();
                    if (sql == "user")
                    {
                        filters.Add("WhereClause", "'0'='1'");
                    }
                    else if (sql != "admin")
                    {
                        if (sql.Contains("a.AreaId2"))
                        {
                            sql = sql.Replace("a.AreaId2", "x.AreaId");
                        }
                        if (sql.Contains("a.AreaId3"))
                        {
                            sql = sql.Replace("a.AreaId3", "x.AreaId");
                        }
                        filters.Add("WhereClause", sql);
                    }
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetStatDataOfVouchDurationForBigScreenStreet", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        #region 权限查看不同内容
        public string PermissionsCategory()
        {
            User currentUser = new User { UserId = NormalSession.UserId.ToGuid(), UserType = NormalSession.UserType };
            string sql = "user";
            if (!currentUser.isSuperAdmin())
            {
                IList<UserArea> userAreas = BuilderFactory.DefaultBulder().List<UserArea>(new UserArea { UserId = NormalSession.UserId.ToGuid() });
                if (userAreas.Count > 0)
                {
                    var areaIdsOfStreet = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) == "0000").Select(item => item.AreaId.ToString()));
                    var areaIdsOfCommunity = string.Join("','", userAreas.Where(item => item.AreaId.ToString().Substring(14, 4) != "0000").Select(item => item.AreaId.ToString()));

                    if (areaIdsOfStreet == "")
                    {
                        sql = string.Format("( a.AreaId3 in ('{0}') or  (ISNULL(a.AreaId2,'')='' AND ISNULL(a.AreaId3,'')=''))", areaIdsOfCommunity);
                    }
                    else if (areaIdsOfCommunity == "")
                    {
                        sql = string.Format("(a.AreaId2 in ('{0}') or  (ISNULL(a.AreaId2,'')='' AND ISNULL(a.AreaId3,'')=''))", areaIdsOfStreet);
                    }
                    else
                    {
                        sql = string.Format("(a.AreaId2 in ('{0}') or a.AreaId3 in ('{1}') or (ISNULL(a.AreaId2,'')='' AND ISNULL(a.AreaId3,'')=''))", areaIdsOfStreet, areaIdsOfCommunity);
                    }
                }
            }
            else
            {
                sql = "admin";
            }
            return sql;
        }
        #endregion

        #endregion

        #region 获取服务提醒
        [WebGet(UriTemplate = "LoadRemindersForScroll/{id}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadRemindersForScroll(string id)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {

                StringObjectDictionary filters = new StringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetRemindersForScroll", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取匹配结果
        [WebGet(UriTemplate = "GetResultForMatchServiceStation/{oldManId},{serviceItemA},{serviceItemB},{serviceMode},{serviceRadius}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetResultForMatchServiceStation(string oldManId, string serviceItemA, string serviceItemB, string serviceMode, string serviceRadius)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary>() { Success = true };
            
            try
            {
                SPParam param = new { OldManId = Guid.Parse(oldManId), ServiceItemA = serviceItemA, ServiceItemB = serviceItemB, ServiceMode = serviceMode, ServiceRadius = int.Parse(serviceRadius) }.ToSPParam();
                 
                result.rows = BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Oca_GetResultForMatchServiceStation", param).ToList();
                if (param.ErrorCode > 0)
                {
                    result.Success = false; 
                    result.ErrorMessage = param.ErrorMessage;
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

        #region 派单
        [WebInvoke(UriTemplate = "DispatchWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<WorkOrderRet> DispatchWorkOrder(WorkOrderParam param)
        {
            InvokeResult<WorkOrderRet> result = new InvokeResult<WorkOrderRet> { Success = true , ret = new WorkOrderRet() };
            
            try
            {
                
                var spParam = param.ToSPParam();
                if (!spParam.ContainsKey("SpecialFlag") || spParam["SpecialFlag"] == null)
                {
                    spParam["SpecialFlag"] = 0;
                }
                spParam["StationId"] = GlobalManager.CurrentIPCC_CallCenterId;
                spParam["OperatedBy"] = Guid.Parse(NormalSession.UserId);
                spParam["DataSource"] = GlobalManager.DIKey_00012_ManualEdit;
                if (TypeConverter.ChangeString(spParam["ServeManName"]) == "")
                {
                    spParam["ServeManName"] = DBNull.Value;
                }
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_GenServiceWorkOrder", spParam);

                result.ret.OnlyOneStationFlag = byte.Parse(spParam["OnlyOneStationFlag"].ToString());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取服务对应的工单
        [WebGet(UriTemplate = "GetServiceWorkOrderInProcess/{callServiceId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrders(string callServiceId)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetServiceWorkOrderInProcess", new { CallServiceId = Guid.Parse(callServiceId) });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取语音
        [WebGet(UriTemplate = "GetServiceVoice/{callServiceId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceVoiceAddress> GetServiceVoice(string callServiceId)
        {
            CollectionInvokeResult<ServiceVoiceAddress> result = new CollectionInvokeResult<ServiceVoiceAddress> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceVoiceAddress>("GetServiceVoice", new { CallServiceId = Guid.Parse(callServiceId) }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取服务对象的电话号码
        [WebGet(UriTemplate = "GetOldManPhoneNos/{callserviceId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<string> GetOldManPhoneNos(string callserviceId)
        {
            CollectionInvokeResult<string> result = new CollectionInvokeResult<string> { Success = true };
            try
            {
                var temp = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetOldManPhoneNos", new { CallServiceId = callserviceId }.ToStringObjectDictionary()).FirstOrDefault();
                if (temp != null)
                {
                    result.rows = temp.Values.Select(item => TypeConverter.ChangeString(item)).Where(item => item != string.Empty).Distinct().ToList();
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

        #region 获取当天需要回拨电话
        [WebGet(UriTemplate = "GetCallForDialBackAtToday/{id},{ServiceType}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetCallForDialBackAtToday(string id, string serviceType)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {

                StringObjectDictionary filters = new StringObjectDictionary();
                int Id;
                if (int.TryParse(id, out Id))
                {
                    filters.Add("Id", Id);
                }
                else
                {
                    filters.Add("Id", 0);
                }
                string[] serviceTypes = serviceType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (serviceTypes.Length > 0) {
                    filters.Add("ServiceType", string.Join(",", serviceTypes.ToArray()));
                }
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallForDialBackAtToday", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 查询最近3次处理完成的记录 Query
        [WebGet(UriTemplate = "LoadLatestCallService?parms={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> LoadLatestCallService(string strParms)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson(strParms);
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetCallServiceArchive", dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region 政府监管平台
        [WebInvoke(UriTemplate = "Oca_CallService_ForMonitor_V_ListByPage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public EasyUIgridCollectionInvokeResult<CallService_ForMonitor_V> Oca_CallService_ForMonitor_V_ListByPage(EasyUIgridCollectionParam<CallService_ForMonitor_V> param)
        {
            EasyUIgridCollectionInvokeResult<CallService_ForMonitor_V> result = new EasyUIgridCollectionInvokeResult<CallService_ForMonitor_V> { Success = true };
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
                        DateTime parseTime = new DateTime();
                        if (field.key.Equals("OperatedOn_Start") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn >= '{0}' ", field.value));
                        }
                        else if (field.key.Equals("OperatedOn_End") && DateTime.TryParse(field.value, out parseTime))
                        {
                            whereClause.Add(string.Format("OperatedOn <= '{0}' ", field.value));
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
                        if (field.key.Contains("Caller") || field.key.Contains("Callee") || field.key.Contains("CallNo") || field.key.Contains("OldManName"))
                        {
                            fuzzys.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
                        }
                        else if (field.key.Contains("CheckInTime"))
                        {
                            whereClause.Add(string.Format("CheckInTime >= '{0}'", field.value));
                        }
                        else
                        {
                            whereClause.Add(string.Format("{0} like '%{1}%'", field.key, field.value));
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
                /***********************begin 排序*************************/

                gridCollectionPager.EasyUIgridDoPage4Model<CallService_ForMonitor_V>(filters, param, ref result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取老人亲人通话使用时长(分)
        [WebGet(UriTemplate = "GetOldManFamliyCallsTime/{strOldManId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<int> GetOldManFamliyCallsTime(string strOldManId)
        {
            InvokeResult<int> result = new InvokeResult<int> { Success = true };
            try
            {
                var tmp = BuilderFactory.DefaultBulder().ExecuteScalar("GetOldManFamliyCallsTime", new { OldManId = strOldManId }.ToStringObjectDictionary());
                if (tmp != null) {
                    result.ret = (int)tmp;
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

        #endregion
    }

    #region 输入输出
    public class TakeOverCallServiceOfCallBackParam
    {
        public Guid? CallServiceId { get; set; }
        public string ServiceExtNo { get; set; }
    }
    public class DialBackForCallServiceParam
    {
        public Guid? CallServiceId { get; set; }
        public string ServiceExtNo { get; set; }
        public string BatchCallServiceIds { get; set; }
    }
    public class WorkOrderParam
    {
        public string WorkOrderContent { get; set; }
        public string ServeItemA { get; set; }
        public string ServeItemB { get; set; }
        public string ServeMode { get; set; }
        public string ServeRadius { get; set; }
        public string ServeKeyword { get; set; }
        public string ServeType { get; set; }
        public string ServiceTimeRequired { get; set; }
        public string RemarkRequired { get; set; }
        public string SuperviseTime { get; set; }
        public byte? SpecialFlag { get; set; }
        public string ServeManName { get; set; }
        public Guid? CallServiceId { get; set; }
        public string DispatchedStationIds { get; set; } //逗号分隔 
        
    }

    public class WorkOrderRet
    {
        public byte OnlyOneStationFlag { get; set; }
    }

    public class AbandonParam
    {
        public string ServiceType { get; set; }
        public string CallNo { get; set; }
        public string AbandonDate { get; set; }
    }

    
    #endregion

}