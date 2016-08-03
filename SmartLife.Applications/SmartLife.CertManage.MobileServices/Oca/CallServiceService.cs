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

namespace SmartLife.CertManage.MobileServices.Oca
{
    [MobileServicesValidate(ApplicationIdFrom = "MM101", ApplicationIdTo = "IP004")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CallServiceService4IPhone : CallServiceServiceInner
    {
        #region 分页获取紧急服务接口
        [WebGet(UriTemplate = "GetEmergency/{pageFrom},{pageTo}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<CallService4M> GetEmergency(string pageFrom, string pageTo)
        {
            return base.GetEmergencyInner(pageFrom, pageTo);
        }
        #endregion

        #region 获取服务日志
        [WebGet(UriTemplate = "GetServiceLog/{callServiceId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceTrackLog4M> GetServiceLog(string callServiceId)
        {
            return base.GetServiceLogInner(callServiceId);
        }
        #endregion

        #region 亲人响应
        [WebInvoke(UriTemplate = "ResponseByFamilyMember/{callServiceId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ResponseByFamilyMember(string callServiceId)
        {
            return base.ResponseByFamilyMemberInner(callServiceId);
        }
        #endregion

        #region 获取紧急服务呼叫电话
        [WebGet(UriTemplate = "GetCallByOldMan/{oldManId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Oca_OldManRelatedPhoneNo> GetCallByOldMan(string oldManId)
        {
            return base.GetCallByOldManInner(oldManId);
        }
        #endregion

        #region 记录亲人行为日志
        [WebInvoke(UriTemplate = "LogByFamilyMember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult LogByFamilyMember(ServiceTrackLog serviceTrackLog)
        {
            return base.LogByFamilyMemberInner(serviceTrackLog);
        }
        #endregion
    }
    [MobileServicesValidate(ApplicationIdFrom = "MM301", ApplicationIdTo = "IP004")]
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CallServiceService4Android : CallServiceServiceInner
    {
        #region 分页获取紧急服务接口
        [WebGet(UriTemplate = "GetEmergency/{pageFrom},{pageTo}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<CallService4M> GetEmergency(string pageFrom, string pageTo)
        {
            return base.GetEmergencyInner(pageFrom, pageTo);
        }
        #endregion

        #region 获取服务日志
        [WebGet(UriTemplate = "GetServiceLog/{callServiceId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceTrackLog4M> GetServiceLog(string callServiceId)
        {
            return base.GetServiceLogInner(callServiceId);
        }
        #endregion

        #region 亲人响应
        [WebInvoke(UriTemplate = "ResponseByFamilyMember/{callServiceId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ResponseByFamilyMember(string callServiceId)
        {
            return base.ResponseByFamilyMemberInner(callServiceId);
        }
        #endregion

        #region 获取紧急服务呼叫电话
        [WebGet(UriTemplate = "GetCallByOldMan/{oldManId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<Oca_OldManRelatedPhoneNo> GetCallByOldMan(string oldManId)
        {
            return base.GetCallByOldManInner(oldManId);
        }
        #endregion

        #region 记录亲人行为日志
        [WebInvoke(UriTemplate = "LogByFamilyMember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult LogByFamilyMember(ServiceTrackLog serviceTrackLog)
        {
            return base.LogByFamilyMemberInner(serviceTrackLog);
        }
        #endregion
    }


    public class CallServiceServiceInner : BaseWcfService
    {
        #region 分页获取紧急服务接口 
        public CollectionInvokeResult<CallService4M> GetEmergencyInner(string pageFrom, string pageTo)
        {
            CollectionInvokeResult<CallService4M> result = new CollectionInvokeResult<CallService4M> { Success = true };
            try
            {

                StringObjectDictionary filters = new { FamilyMemberId = GetHttpHeader("FamilyMemberId"), PageFrom = pageFrom, PageTo = pageTo }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().List<CallService4M>("CallServiceListByEmergency", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取服务日志实现
        public CollectionInvokeResult<ServiceTrackLog4M> GetServiceLogInner(string callServiceId)
        {
            CollectionInvokeResult<ServiceTrackLog4M> result = new CollectionInvokeResult<ServiceTrackLog4M> { Success = true };
            try
            {

                StringObjectDictionary filters = new { CallServiceId = Guid.Parse(callServiceId) }.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().List<ServiceTrackLog4M>("TrackLogListByService", filters);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 亲人响应实现
        public InvokeResult ResponseByFamilyMemberInner(string callServiceId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                var param = new ServiceFamilyResponse { FamilyMemberId = Guid.Parse(GetHttpHeader("FamilyMemberId")), CallServiceId = Guid.Parse(callServiceId) };
                ServiceFamilyResponse res = BuilderFactory.DefaultBulder().List<ServiceFamilyResponse>(param).FirstOrDefault();

                if (res == null)
                {
                    //添加
                    BuilderFactory.DefaultBulder().Create<ServiceFamilyResponse>(param);
                }
                else
                {
                    //修改
                    param.Id = res.Id;
                    param.Status = 1;
                    BuilderFactory.DefaultBulder().Update<ServiceFamilyResponse>(param);
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

        #region 获取紧急服务呼叫电话实现
        public CollectionInvokeResult<Oca_OldManRelatedPhoneNo> GetCallByOldManInner(string oldManId)
        {
            //社区电话和亲属电话
            CollectionInvokeResult<Oca_OldManRelatedPhoneNo> result = new CollectionInvokeResult<Oca_OldManRelatedPhoneNo> { Success = true };
            try
            {
                //目前社区电话预定义
                List<Oca_OldManRelatedPhoneNo> phones = new List<Oca_OldManRelatedPhoneNo>() { 
                    new Oca_OldManRelatedPhoneNo{ Text = "社区服务", PhoneNo="18072996585"},new Oca_OldManRelatedPhoneNo{ Text="社区互助", PhoneNo="18905716786"},new Oca_OldManRelatedPhoneNo{ Text="社区医生", PhoneNo="15958102129"}
                };
                StringObjectDictionary parms = new { OldManId = Guid.Parse(oldManId), FamilyMemberStatus = 1 }.ToStringObjectDictionary();
                phones.AddRange(BuilderFactory.DefaultBulder().ListStringObjectDictionary("OldManFamilyInfo_By_OldMan_List2", parms).Select(item => new Oca_OldManRelatedPhoneNo { Text = TypeConverter.ChangeString(item["RelationNameOfOldMan"]), PhoneNo = TypeConverter.ChangeString(item["FamilyMemberContactPhone"]) }));
                result.rows = phones;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 记录亲人行为日志实现
        public InvokeResult LogByFamilyMemberInner(ServiceTrackLog serviceTrackLog)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                serviceTrackLog.Id = null;
                CallCenter callCenter = BuilderFactory.DefaultBulder().List<CallCenter>().FirstOrDefault();
                if (callCenter != null)
                {
                    serviceTrackLog.StationId = callCenter.StationId;
                }
                BuilderFactory.DefaultBulder().Create<ServiceTrackLog>(serviceTrackLog);
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


    public class Oca_OldManRelatedPhoneNo
    {
        public string Text { get; set; }//显示名称
        public string PhoneNo { get; set; }//电话号码
    }
}