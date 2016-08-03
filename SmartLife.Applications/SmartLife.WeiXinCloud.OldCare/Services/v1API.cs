using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.IO;
using System.Text;
using System.Collections.Concurrent;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using win.core.utils;
using Newtonsoft.Json;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.WeiXin.Pub;
using SmartLife.Share.Models.Oca;
using web.core.share.models.WeiXin.Pub;
using SmartLife.Share.Models.Pub;

namespace SmartLife.WeiXinCloud.OldCare.Services
{
    [ServiceErrorOutput]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class v1API : BaseWcfService
    {
        #region 注册/注销分机
        [WebInvoke(UriTemplate = "CheckIn/{extNo}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CheckIn(string extNo)
        {
            InvokeResult result = new InvokeResult();
            if (!Global.theExtNoProcessingNormalUserCount.ContainsKey(extNo))
            {
                result.Success = Global.theExtNoProcessingNormalUserCount.TryAdd(extNo, 0);
            }
            else
            {
                result.Success = false;
                result.ErrorCode = 57001;
            }
            return result;
        }
        [WebInvoke(UriTemplate = "CheckOut/{extNo}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CheckOut(string extNo)
        {
            InvokeResult result = new InvokeResult();
            int normalUserCount;
            if (Global.theExtNoProcessingNormalUserCount.TryRemove(extNo, out normalUserCount))
            {
                if (normalUserCount > 0)
                {
                    foreach (var key in Global.theWXClientMappingExtNos.Keys)
                    {
                        //如果还有服务的微信客户端，则需要将它们从映射中清除
                        if (Global.theWXClientMappingExtNos[key] == extNo)
                        {
                            string outV;
                            result.Success = Global.theWXClientMappingExtNos.TryRemove(key, out outV);
                        }
                    }
                }
            }
            else
            {
                result.Success = false;
                result.ErrorCode = 57002;
            }
            return result;
        }
        #endregion

        #region 获取当前分机服务的微信客户
        [WebGet(UriTemplate = "GetWeiXinClient/{extNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<string> GetWeiXinClient(string extNo)
        {
            CollectionInvokeResult<string> result = new CollectionInvokeResult<string> { Success = true, rows = new List<string>() };

            foreach (string key in Global.theWXClientMappingExtNos.Keys)
            {
                ConcurrentQueue<WXReceiveMessage> queue = Global.theMessagesFromWeiXin[key];
                int messageCount = 0;
                if (queue != null)
                {
                    messageCount = queue.Count;
                }

                if (messageCount > 0)
                {
                    result.rows.Add(key);
                }
            }
            return result;
        }
        #endregion

        #region 获取微信消息

        [WebGet(UriTemplate = "RetrieveMessage/{openId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<WXReceiveMessage> RetrieveMessage(string openId)
        {
            InvokeResult<WXReceiveMessage> result = new InvokeResult<WXReceiveMessage> { Success = true };
            try
            {
                result.ret = _RetrieveMessage(openId);
            }
            catch (Exception ex)
            {
                result.ErrorCode = 500;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /**跨域调用**/
        [WebGet(UriTemplate = "RetrieveMessageAsJSONP/{openId}?jsoncallback={callback}", BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream RetrieveMessageAsJSONP(string openId, string callback)
        {
            InvokeResult<WXReceiveMessage> result = new InvokeResult<WXReceiveMessage> { Success = true };
            try
            {
                result.ret = _RetrieveMessage(openId);
            }
            catch (Exception ex)
            {
                result.ErrorCode = 500;
                result.ErrorMessage = ex.Message;
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(callback + "(" + result.ToJson() + ")");
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }

        private WXReceiveMessage _RetrieveMessage(string openId)
        {
            WXReceiveMessage result = null;
            int maxWait = 10;
            WXReceiveMessage m;
            ConcurrentQueue<WXReceiveMessage> messages;
            if (!Global.theMessagesFromWeiXin.TryGetValue(openId, out messages))
            {
                messages = new ConcurrentQueue<WXReceiveMessage>();
                Global.theMessagesFromWeiXin.TryAdd(openId, messages);
            }


            bool dequeued = messages.TryDequeue(out m);
            //模拟等待 
            int waitime = maxWait;
            while (!dequeued && waitime > 0)
            {
                dequeued = messages.TryDequeue(out m);
                System.Threading.Thread.Sleep(200);
                waitime--;
            }
            if (m != null)
            {
                result = m;
            }
            return result;
        }
        #endregion

        #region 认证

        #region 判断是否具有服务人员资格
        [WebGet(UriTemplate = "CheckOpenIdIsAuth/{openId},{stationId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CheckOpenIdIsAuth(string openId, string stationId)
        {
            InvokeResult result = new InvokeResult { Success = true };
            try
            {
                result.Success = GlobalManager.IsAuthorizedForOpenIdToStation(openId, stationId.ToGuid());
            }
            catch (Exception ex)
            {
                result.Success = false; 
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 服务人员获取通过资格认证的商家
        [WebGet(UriTemplate = "GetMerchantsAuthorized/{openId}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<ServiceStation_Light> GetMerchantsAuthorized(string openId)
        {
            CollectionInvokeResult<ServiceStation_Light> result = new CollectionInvokeResult<ServiceStation_Light> { Success = true };
            try
            {
                result.rows = BuilderFactory.DefaultBulder().List<ServiceStation_Light>("ServiceStation_Light_ListForServeManAuthorized", new { OpenId = openId }.ToStringObjectDictionary());
            }
            catch (Exception ex)
            {
                result.Success = false; 
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 添加服务人员资格认证申请
        [WebInvoke(UriTemplate = "AuthServeMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult AuthServeMan(AuthServeManParam param)
        {
            InvokeResult result = new InvokeResult();
            try
            {
                var requests = BuilderFactory.DefaultBulder().List<WXStationAuthRequest>(new WXStationAuthRequest { OpenId = param.OpenId, StationId = param.StationId, DoStatus = 1 }.ToStringObjectDictionary(false));
                bool addNew = false;
                if (requests.Count == 0)
                {
                    //新申请
                    addNew = true;
                }
                else
                {
                    if (requests.Any(item => item.DoStatus == 0 || item.DoStatus == 1))
                    {
                        //已经存在或还在等待审核，则忽略 
                    }
                    else if (requests.Any(item => item.DoStatus == 2))
                    {
                        //重新申请
                        addNew = true; 
                    }
                }

                if (addNew)
                {
                    
                    WXStationAuthRequest wxRequest = new WXStationAuthRequest { OpenId = param.OpenId, StationId = param.StationId };
                    string  sourceKey = TypeConverter.ChangeString(BuilderFactory.DefaultBulder().Create<WXStationAuthRequest>(wxRequest.ToStringObjectDictionary(false)));
                    //提醒记录

                    int reminderId = Convert.ToInt32(BuilderFactory.DefaultBulder().Create<Reminder>(new Reminder { SourceTable = wxRequest.GetMappingTableName(), SourceColumn = "Id", SourceKey = sourceKey, SourceType = "B0101", RemindTime = DateTime.Now, RemindContent = "您有一条微信服务人员资格申请等待审核" }.ToStringObjectDictionary(false)));
                    BuilderFactory.DefaultBulder().Create<ReminderObject>(new ReminderObject { ReminderId = reminderId, ObjectType = "Merchant", ObjectKey = param.StationId.ToString() }.ToStringObjectDictionary(false));
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

        #region 移除服务人员资格认证申请
        [WebInvoke(UriTemplate = "RemoveServeManForMerchant", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult RemoveServeManForMerchant(RemoveServeManForMerchantParam param)
        {
            InvokeResult result = new InvokeResult();
            try
            {
                BuilderFactory.DefaultBulder().Delete<WXStationAuthRequest>(new WXStationAuthRequest { OpenId = param.OpenId, StationId = param.StationId }.ToStringObjectDictionary(false));
            }
            catch (Exception ex)
            {
                result.Success = false; 
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region 发送商家服务人员认证审核通过微信提醒
        [WebInvoke(UriTemplate = "ServeManAuthFinishRemind", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult ServeManAuthFinishRemind(ServeManAuthFinishRemindParam param)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                if (param.Items != null && param.Items.Count > 0)
                {
                    WXMessageTemplate template;
                    if (param.PassFlag == 1)
                    {
                        template = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_ServeManAuthPass });
                    }
                    else
                    {
                        template = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_ServeManAuthDeny });
                    }
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    WXSend send = new WXSend
                    {
                        fromUserName = GlobalManager.TheServiceAccount.AccountCode,
                        BatchNum = "0",
                        SourceCatalog = "认证审核通过微信提醒",
                        SourceTable = new WXStationAuthRequest().GetMappingTableName(),
                        SendCatalog = WXRequestMessageType.text.ToString(),
                        SendContent = template.TemplateContent.ReplaceWithKeys(new
                        {
                            MerchantName = param.MerchantName
                        }.ToStringObjectDictionary(), false, "$")
                    };
                    
                    foreach (var item in param.Items)
                    {
                        send.SourceId = TypeConverter.ChangeString(item.Id);
                        send.toUserName = item.OpenId;
                        statements.Add(new IBatisNetBatchStatement { StatementName = send.GetCreateMethodName(), ParameterObject = send.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                    }
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

        #endregion




        #region 判断服务人员是否能够接单
        [WebGet(UriTemplate = "CheckOpenIdCanResponse/{openId},{workOrderId},{buttonTag}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<CheckOpenIdCanResponseRet> CheckOpenIdCanResponse(string openId, string workOrderId, string buttonTag)
        {
            InvokeResult<CheckOpenIdCanResponseRet> result = new InvokeResult<CheckOpenIdCanResponseRet> { Success = true, ret = new CheckOpenIdCanResponseRet() };
            try
            {
                //判读传入的OpenId是否合法 
                var workOrder = BuilderFactory.DefaultBulder().Load<ServiceWorkOrder, ServiceWorkOrderPK>(new ServiceWorkOrderPK { WorkOrderId = workOrderId.ToGuid() });
                if (workOrder.WorkOrderId == null)
                {
                    result.Success = false;
                    result.ErrorCode = 51002;
                }
                else
                {
                    //判断服务人员和传入的OpenId
                    if (!GlobalManager.IsAuthorizedForOpenIdToStation(openId, workOrder.ServeStationId))
                    {
                        result.Success = false;
                        result.ErrorCode = 51003;
                    }
                    else
                    {
                        /*
                        if (GlobalManager.GetServeManIdByOpenId(openId, workOrder.ServeStationId) != workOrder.ServeManId)
                        {
                            //检查OpenId对应的是否当前相应工单规定的服务人员
                            result.Success = false;
                            result.ErrorCode = 51004;
                        }
                        else
                        {
                            
                        }
                         */
                        if (buttonTag == GlobalManager.enumButtonTag.Arrive.ToString())
                        {
                            if (workOrder.ServeManArriveTime != null)
                            {
                                //工单已被响应到达
                                result.Success = false;
                                result.ErrorCode = 51005;
                            }
                        }
                        else if (buttonTag == GlobalManager.enumButtonTag.Leave.ToString())
                        {
                            if (workOrder.ServeManArriveTime == null)
                            {
                                //工单未被响应到达无法响应离开
                                result.Success = false;
                                result.ErrorCode = 51006;
                            }
                            if (workOrder.ServeManLeaveTime != null)
                            {
                                //工单已被响应离开
                                result.Success = false;
                                result.ErrorCode = 51007;
                            }
                        }
                        else if (buttonTag == GlobalManager.enumButtonTag.Feedback.ToString())
                        {
                            if (workOrder.ServeManArriveTime == null)
                            {
                                //工单未被响应到达无法响应评价
                                result.Success = false;
                                result.ErrorCode = 51008;
                            }
                            if (workOrder.ServeManLeaveTime == null)
                            {
                                //工单未被响应离开无法响应评价
                                result.Success = false;
                                result.ErrorCode = 51009;
                            }
                        }
                    }

                    result.ret.ServeManName = workOrder.ServeManName;
                    result.ret.OldManName = workOrder.OldManName;
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

        #region 服务人员响应
        [WebInvoke(UriTemplate = "ResponseWorkOrderByServeMan", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult ResponseWorkOrderByServeMan(ResponseWorkOrderByServeManParam param)
        {
            InvokeResult result = new InvokeResult() { Success = true };
            try
            {
                var spParam = param.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Oca_WorkOrderResponseByServeMan", spParam);
                if (spParam.ErrorCode != 0)
                {
                    result.ErrorCode = spParam.ErrorCode;
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

        #region 服务人员查询自身服务

        #endregion
    }

    public class AuthServeManParam
    {
        public string OpenId { get; set; }
        public Guid? StationId { get; set; }
    }

    public class RemoveServeManForMerchantParam
    {
        public string OpenId { get; set; }
        public Guid? StationId { get; set; }
    }

    public class ResponseWorkOrderByServeManParam
    {
        public string OpenId { get; set; }
        public Guid? WorkOrderId { get; set; }
        public string ButtonTag { get; set; }
        public string FeedbackToOldMan { get; set; }
    }

    public class ServeManAuthFinishRemindParam
    {
        public int PassFlag { get; set; }
        public string MerchantName { get; set; }
        public IList<ServeManAuthPass_AuthRequestItem> Items { get; set; }
    }
    public class ServeManAuthPass_AuthRequestItem
    {
        public int? Id { get; set; }
        public string OpenId { get; set; }
    }

    public class CheckOpenIdCanResponseRet
    {
        public string ServeManName { get; set; }
        public string OldManName { get; set; }
    }

    
}