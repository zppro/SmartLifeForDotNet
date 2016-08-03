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
 
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.PO.eComm;

using System.Collections.Concurrent;
using SmartLife.Share.Behaviors;

namespace SmartLife.IPCC
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class eCommService : BaseWcfService
    {
        public string VERSION_1 = "1.0";

        [WebInvoke(UriTemplate = "OnCall/{method},{caller},{callee},{uuid}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string OnCall(string method, string caller, string callee,string uuid)
        {
            List<string> results = new List<string>();
            try
            {
                if (method.ToLower() == "callin")
                {
                    Global.DebugInfo(string.Format("----------------------- OnCall method:{0},caller:{1},callee:{2},uuid:{3}", method, caller, callee, uuid));
                    string ip = GetHttpHeader("X-Forwarded-For");
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = GetHttpHeader("X-Real-IP");
                    }
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = GetClientIp();
                    }
                    SPParam theSPParam = new { Version = VERSION_1, Caller = caller, Callee = callee, IP = ip, UuidOfIPCC = uuid }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_CallIn_eComm", theSPParam);
                    if (theSPParam.ErrorCode != 0)
                    {
                        results.Add(false.ToString());
                        results.Add(theSPParam.ErrorCode.ToString());
                        results.Add(theSPParam.ErrorMessage);
                    }
                    else
                    {
                        results.Add(true.ToString());
                        results.Add("0");
                        results.Add("");
                    }
                    results.Add(TypeConverter.ChangeString(theSPParam["CallQueueRet"]));
                }
            }
            catch (Exception ex)
            {
                results.Add(false.ToString());
                results.Add("-1");
                results.Add(ex.Message);
                results.Add("-1");
            }
            return string.Join("\n\r", results.ToArray());
        }

        [WebInvoke(UriTemplate = "OnRolledOut/{caller},{callee},{uuid}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string OnRolledOut(string caller, string callee, string uuid)
        {
            List<string> results = new List<string>();
            try
            {
                Global.DebugInfo(string.Format("----------------------- OnRolledOut caller:{0},callee:{1},uuid:{2}", caller, callee, uuid));

                SPParam theSPParam = new {  Caller = caller, Callee = callee, UuidOfIPCC = uuid }.ToSPParam();
                BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_CallInRolledOut_eComm", theSPParam);
                if (theSPParam.ErrorCode != 0)
                {
                    results.Add(false.ToString());
                    results.Add(theSPParam.ErrorCode.ToString());
                    results.Add(theSPParam.ErrorMessage);
                }
                else
                {
                    results.Add(true.ToString());
                    results.Add("0");
                    results.Add("");
                }
            }
            catch (Exception ex)
            {
                results.Add(false.ToString());
                results.Add("-1");
                results.Add(ex.Message);
                results.Add("-1");
            }
            return string.Join("\n\r", results.ToArray());
        }

        [WebInvoke(UriTemplate = "ClearCTI/{extNo}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult ClearCTI(string extNo)
        {
            InvokeResult result = new InvokeResult { Success = true };
            if (Global.ctiCalls.ContainsKey(extNo))
            {
                Global.ctiCalls[extNo] = new ConcurrentQueue<CTI_Call>();
            }
            string conferenceName = extNo + "_";
            //if (Global.ctiConferences.ContainsKey(conferenceName))
            //{
            //    Global.ctiConferences[conferenceName] = new ConcurrentQueue<CTI_Conference>();
            //}
            //清除当前分机下所有的多方通话事件
            ConcurrentQueue<CTI_Conference> currentCTI_Conference;
            foreach (var item in Global.ctiConferences.Keys) {
                if (item.IndexOf(conferenceName) > -1) {
                    Global.ctiConferences.TryRemove(item, out currentCTI_Conference);
                    currentCTI_Conference = null;
                }
            }
            return result;
        }

        [WebInvoke(UriTemplate = "CTI_Call/{extNo}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<CTI_Call> GetCTI_Call(string extNo)
        {
            InvokeResult<CTI_Call> result = new InvokeResult<CTI_Call> { Success = true };
            try
            {
                int maxWait = 10;
                CTI_Call o;
                ConcurrentQueue<CTI_Call> queue;
                if (!Global.ctiCalls.TryGetValue(extNo, out queue))
                {
                    queue = new ConcurrentQueue<CTI_Call>();
                    Global.ctiCalls.TryAdd(extNo, queue);
                }
                bool dequeued = queue.TryDequeue(out o);
                //模拟等待
                //Random r = new Random();
                //int waitime = r.Next(maxWait + 1);
                int waitime = maxWait;
                while (!dequeued && waitime > 0)
                {
                    dequeued = queue.TryDequeue(out o);
                    System.Threading.Thread.Sleep(200);
                    waitime--;
                }
                //o = new CTI_Object { ExtNo = extNo, CallTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CallType = "in", PeerNumber = "18668001381", Trunk = "85027666" };
                if (o == null)
                {
                    result.ret = null;
                }
                else
                {
                    result.ret = o;
                }
            }
            catch (Exception ex)
            {
                result.ErrorCode = 500;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [WebInvoke(UriTemplate = "CTI_Conference/{extNo},{callServiceId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public InvokeResult<CTI_Conference> GetCTI_Conference(string extNo, string callServiceId)
        {
            InvokeResult<CTI_Conference> result = new InvokeResult<CTI_Conference> { Success = true };
            try
            {
                string conferenceName = string.IsNullOrEmpty(callServiceId) ? extNo : (extNo + "_" + callServiceId);
                int maxWait = 10;
                CTI_Conference o;
                ConcurrentQueue<CTI_Conference> queue;
                if (!Global.ctiConferences.TryGetValue(conferenceName, out queue))
                {
                    queue = new ConcurrentQueue<CTI_Conference>();
                    Global.ctiConferences.TryAdd(conferenceName, queue);
                }
                bool dequeued = queue.TryDequeue(out o);
                //模拟等待
                //Random r = new Random();
                //int waitime = r.Next(maxWait + 1);
                int waitime = maxWait;
                while (!dequeued && waitime > 0)
                {
                    dequeued = queue.TryDequeue(out o);
                    System.Threading.Thread.Sleep(200);
                    waitime--;
                }
                //Global.DebugInfo(string.Format("conferenceName:{0}----------------->{1}", conferenceName, Global.ctiConferences.Count.ToString()));
                //o = new CTI_Object { ExtNo = extNo, CallTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CallType = "in", PeerNumber = "18668001381", Trunk = "85027666" };
                if (o == null)
                {
                    result.ret = null;
                }
                else
                {
                    result.ret = o;
                }
            }
            catch (Exception ex)
            {
                result.ErrorCode = 500;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}