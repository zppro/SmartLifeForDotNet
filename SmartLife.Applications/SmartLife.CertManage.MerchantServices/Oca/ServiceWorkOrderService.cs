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
    [MerchantTokenValidate()]
    [MerchantServicesValidate(ApplicationIdFrom = "CB001", ApplicationIdTo = "IP005")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceWorkOrderService : BaseWcfService
    {
        #region 业务接口

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

        #region 商家处理中的工单
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

        #region 商家处理完成的工单
        [WebGet(UriTemplate = "GetWorkOrderProcessed", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderProcessed()
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = new { StationId = stationId }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().ListStringObjectDictionary("GetWorkOrderProcessed", filters);
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
        public CollectionInvokeResult<StringObjectDictionary> GetWorkOrderFinished(InputWorkOrderParam param)
        {
            CollectionInvokeResult<StringObjectDictionary> result = new CollectionInvokeResult<StringObjectDictionary> { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                StringObjectDictionary filters = param.ToStringObjectDictionary(false);
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

        #region 响应工单 
        [WebInvoke(UriTemplate = "ResponseWorkOrder", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ResponseWorkOrder(ResponseWorkOrderParam param)
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
        public InvokeResult DispatchServeMan(DispatchServeManParam param)
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
        public InvokeResult InputWorkOrder(InputWorkOrderParam param)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                Guid stationId = Guid.Parse(GetHttpHeader("StationId"));
                var spParam = param.ToSPParam();
                spParam["StationId"] = stationId;
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

        #endregion
    }

    public class ResponseWorkOrderParam
    {
        public Guid? WorkOrderId { get; set; } 
        public Guid? OperatedBy { get; set; }
    }

    public class DispatchServeManParam
    {
        public Guid? WorkOrderId { get; set; }
        public string ServeManName { get; set; } 
    }

    public class InputWorkOrderParam
    {
        public Guid? WorkOrderId { get; set; }
        public string OtherCharges { get; set; }
        public decimal? OtherChargesFee { get; set; }
        public string ServeBeginTime { get; set; }
        public string ServeEndTime { get; set; }
        public string ServeResult { get; set; }
        public string ServeResultRemark { get; set; }
        public string FeedbackToOldMan { get; set; }
        public string FeedbackRemarkToOldMan { get; set; }

        public string OldManName { get; set; }
        public string ServeManArriveTime_Start { get; set; }
        public string ServeManArriveTime_End { get; set; }
        public string ServeManLeaveTime_Start { get; set; }
        public string ServeManLeaveTime_End { get; set; }
        public string ServiceTime { get; set; }
        public string FeedbackToServiceStation { get; set; }
    }
}