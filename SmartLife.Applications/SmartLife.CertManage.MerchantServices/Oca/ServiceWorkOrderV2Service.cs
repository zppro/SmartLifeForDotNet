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

namespace SmartLife.CertManage.MerchantServices.Oca
{
    //[MerchantTokenValidate()]
    [MerchantServicesValidate(ApplicationIdFrom = "BP001", ApplicationIdTo = "IP005")]
    //[ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceWorkOrderV2Service : BaseWcfService
    {
        #region 业务接口

        #region easyuiGrid所以工单
        [WebInvoke(UriTemplate = "GetWorkOrderInfoAll", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderInfoAll(InputWorkOrderV2Param param)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                List<string> whereClause = new List<string>();
                StringObjectDictionary param_filters = param.ToStringObjectDictionary(false);
                StringObjectDictionary filters = new StringObjectDictionary();
                string workOrderDoStatus = "";
                filters["StationId"] = stationId;
                if (param_filters != null)
                {
                    foreach (var field in param_filters)
                    {
                        if (field.Key == "fuzzyFields")
                        {
                            List<string> fuzzys = new List<string>();
                            foreach (var fields in param.fuzzyFields)
                            {
                                fuzzys.Add(string.Format("{0} like '%{1}%'", fields.key, fields.value));
                            }
                            if (fuzzys.Count > 0)
                            {
                                whereClause.Add("(" + string.Join(" or ", fuzzys.ToArray()) + ")");
                            }
                        }
                        if (field.Key == "workOrderDoStatus")
                        {
                            workOrderDoStatus = param.workOrderDoStatus;
                        }
                        if (field.Key == "CheckInTime")
                        {
                            filters["CheckInTime"] =Convert.ToDateTime( field.Value);
                        }
                        else
                        {
                            filters[field.Key] = field.Value;
                        }
                    }
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                List<StringObjectDictionary> _rows = new List<StringObjectDictionary>();
                String[] sq = workOrderDoStatus.Split(',');
                for (var i = 0; i < sq.Length; i++)
                {
                    if (sq[i] == "1")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderToResponse", filters));
                    }
                    if (sq[i] == "2")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcess", filters));
                    }
                    if (sq[i] == "3")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServicing", filters));
                    }
                    if (sq[i] == "4")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServiced", filters));
                    }
                    if (sq[i] == "5")
                    {
                        _rows.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderCallBackFinished", filters));
                    }
                }
                result.rows = _rows;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
        

        #region 等待商家响应的工单
        [WebGet(UriTemplate = "GetWorkOrderToResponse", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderToResponse()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderToResponse", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 商家已响应的工单
        [WebGet(UriTemplate = "GetWorkOrderProcessing", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderDispatched()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessing", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 商家已响应的工单(包括接单失败)
        [WebGet(UriTemplate = "GetWorkOrderProcess", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderDispatch()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcess", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 商家处理完成服务未完成的工单
        [WebGet(UriTemplate = "GetWorkOrderProcessedServicing", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderProcessedServicing()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServicing", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 商家处理完成服务结束的工单
        [WebGet(UriTemplate = "GetWorkOrderProcessedServiced", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderProcessedServiced()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();

                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessedServiced", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 已完成的工单
        [WebInvoke(UriTemplate = "GetWorkOrderFinished", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderFinished(InputWorkOrderV2Param param)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                List<string> whereClause = new List<string>();
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary param_filters = param.ToStringObjectDictionary(false);
                StringObjectDictionary filters = new StringObjectDictionary();
                if (param_filters != null)
                {
                    foreach (var field in param_filters)
                    {
                        if (field.Key == "fuzzyFields" )
                        {
                            List<string> fuzzys = new List<string>();
                            foreach (var fields in param.fuzzyFields)
                            {
                                fuzzys.Add(string.Format("{0} like '%{1}%'", fields.key, fields.value));
                            }
                            if (fuzzys.Count > 0)
                            {
                                whereClause.Add("(" + string.Join(" or ", fuzzys.ToArray()) + ")");
                            }
                        }
                        else
                        {
                            filters[field.Key] = field.Value;
                        }
                    }
                }
                if (whereClause.Count > 0)
                {
                    filters.Add("WhereClause", string.Join(" AND ", whereClause.ToArray()));
                }
                filters["StationId"] = stationId;
                //StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderFinished", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取整张工单上的信息
        [WebGet(UriTemplate = "GetWorkOrderInfo/{workOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<StringObjectDictionary> GetWorkOrderInfo(string workOrderId)
        {
            InvokeResult<StringObjectDictionary> result = new InvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                result.ret = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderInfo", new { WorkOrderId = workOrderId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 检查是否可以响应
        [WebGet(UriTemplate = "CheckCanResponse/{workOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CheckCanResponse(string workOrderId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));

                result.Success = BuilderFactory.DefaultBulder().List<ServiceReceive>(new ServiceReceive { ReceiveStatus = 0, StationId = stationId, WorkOrderId = Guid.Parse(workOrderId) }).Count == 1;
                if (!result.Success)
                {
                    result.ErrorCode = 50002;
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

        #region 检查工单是否已确定商家
        [WebGet(UriTemplate = "CheckWorkOrderResponsed/{workOrderId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CheckWorkOrderResponsed(string workOrderId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                string param = "select * from Oca_ServiceReceive where ReceiveStatus=3 and WorkOrderId='" + workOrderId + "'";
                var ret = BuilderFactory.DefaultBulder().ExecuteNativeSqlForCount(param);
                if (ret > 0) {
                    result.Success = false;
                }
                if (!result.Success)
                {
                    result.ErrorMessage = "此工单已经确定商家";
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

        #region 响应工单
        [WebInvoke(UriTemplate = "ResponseWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ResponseWorkOrder(ResponseWorkOrderV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                var spParam = param.ToSPParam();
                spParam["StationId"] = stationId;
                spParam["ReceiveType"] = GlobalManager.DIKey_01004_Client;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_ResponseServiceWorkOrder", spParam);
                if (spParam.ErrorCode != 0)
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

        #region 派遣服务人员
        [WebInvoke(UriTemplate = "DispatchServeMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult DispatchServeMan(DispatchServeManV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                var spParam = param.ToSPParam();
                spParam["StationId"] = stationId;
                spParam["ResponseFrom"] = GlobalManager.DIKey_01004_Client;
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_WorkOrderDispatchServeMan", spParam);
                if (spParam.ErrorCode != 0)
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

        #region 填写服务处理与反馈信息
        [WebInvoke(UriTemplate = "InputWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult InputWorkOrder(InputWorkOrderV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                var spParam = param.ToSPParam();
                spParam["StationId"] = stationId;
                spParam["OperatedBy"] = NormalSession.UserId.ToGuid();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_InputWorkOrderByMerchant", spParam);
                if (spParam.ErrorCode != 0)
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


        #region 派单人员响应工单业务处理
        [WebInvoke(UriTemplate = "ResponseWorkOrderByDispatchMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ResponseWorkOrderByDispatchMan(ResponseByDispatchManV2Param param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                spParam["OperatedBy"] = NormalSession.UserId.ToGuid();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_WorkOrderResponseByDispatchMan", spParam);
                if (spParam.ErrorCode != 0)
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

        #endregion
    }

    public class ResponseWorkOrderV2Param
    {
        public Guid? WorkOrderId { get; set; }
        public Guid? OperatedBy { get; set; }
    }

    public class DispatchServeManV2Param
    {
        public Guid? WorkOrderId { get; set; }
        public Guid? OperatedBy { get; set; }
        public string ServeManName { get; set; }
        public Guid? ServeManId { get; set; }
        public DateTime? ServeBeginTime { get; set; }
        public DateTime? ServeEndTime { get; set; }
    }

    public class InputWorkOrderV2Param
    {
        public string ServeManArriveTime_Start { get; set; }
        public string ServeManArriveTime_End { get; set; }
        public string ServeManLeaveTime_Start { get; set; }
        public string ServeManLeaveTime_End { get; set; }
        public string ServiceTime { get; set; }
        /*************************************/
        public string Address { get; set; }
        public string AreaId { get; set; }
        public string AreaId2 { get; set; }
        public string AreaId3 { get; set; }
        public Guid? CallServiceId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public string DataSource { get; set; }
        public byte? DoStatus { get; set; }
        public Guid? FeedbackBy { get; set; }
        public string FeedbackRemarkToOldMan { get; set; }
        public string FeedbackRemarkToServiceStation { get; set; }
        public DateTime? FeedbackTimeToOldMan { get; set; }
        public DateTime? FeedbackTimeToServiceStation { get; set; }
        public string FeedbackToOldMan { get; set; }
        public string FeedbackToServiceStation { get; set; }
        public string Gender { get; set; }
        public int? Id { get; set; }
        public DateTime? InputtedTimeByServiceStation { get; set; }
        public string LatitudeS { get; set; }
        public string LongitudeS { get; set; }
        public string Mobile { get; set; }
        public Guid? OldManId { get; set; }
        public string OldManName { get; set; }
        public Guid? OperatedBy { get; set; }
        public DateTime? OperatedOn { get; set; }
        public string OtherCharges { get; set; }
        public decimal? OtherChargesFee { get; set; }
        public string RemarkRequired { get; set; }
        public DateTime? ServeBeginTime { get; set; }
        public DateTime? ServeEndTime { get; set; }
        public decimal? ServeFee { get; set; }
        public decimal? ServeFeeByGov { get; set; }
        public decimal? ServeFeeBySelf { get; set; }
        public string ServeFinalResult { get; set; }
        public string ServeFinalResultRemark { get; set; }
        public string ServeItemA { get; set; }
        public string ServeItemB { get; set; }
        public string ServeKeyword { get; set; }
        public DateTime? ServeManArriveTime { get; set; }
        public DateTime? ServeManDispatchedTime { get; set; }
        public DateTime? ServeManLeaveTime { get; set; }
        public string ServeManName { get; set; }
        public Guid? ServeManId { get; set; }
        public string ServeMode { get; set; }
        public string ServeRadius { get; set; }
        public string ServeResult { get; set; }
        public string ServeResultRemark { get; set; }
        public Guid? ServeStationId { get; set; }
        public string ServeStationName { get; set; }
        public string ServeType { get; set; }
        public DateTime? ServiceTimeRequired { get; set; }
        public byte? SpecialFlag { get; set; }
        public byte? Status { get; set; }
        public Guid? SupervisedBy { get; set; }
        public DateTime? SuperviseTime { get; set; }
        public string Tel { get; set; }
        public string WorkOrderContent { get; set; }
        public Guid? WorkOrderId { get; set; }
        public string WorkOrderNo { get; set; }


        public IList<gridFuzzyField> fuzzyFields { get; set; }
        public string workOrderDoStatus { get; set; }
        //附加标志（是否一键处理）
        public int? FastOverFlag { get; set; }
    }

    public class ResponseByDispatchManV2Param {
        public Guid? WorkOrderId { get; set; }
        public string ButtonTag { get; set; }
        public string FeedbackToOldMan { get; set; }
        public Guid? OperatedBy { get; set; }
    }
}