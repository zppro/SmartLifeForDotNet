using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.Share.Models.PO.eComm;
using System.Collections.Concurrent;
using SmartLife.Share.Behaviors;

namespace SmartLife.IPCC.api
{
    /// <summary>
    /// eCommCTI 的摘要说明
    /// </summary>
    public class eCommCTI : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string c_name = context.Request.Form["c_name"];
                if (string.IsNullOrEmpty(c_name))
                {
                    //CTI-Call
                    string call_state = context.Request.Form["call_state"];
                    string ext_no = context.Request.Form["ext_no"];
                    string peer_number = context.Request.Form["peer_number"];
                    string call_type = context.Request.Form["call_type"];
                    string call_time = context.Request.Form["call_time"];
                    string trunk = context.Request.Form["trunk"];
                    string uuid = context.Request.Form["uuid"];

                    Global.DebugInfo(string.Format("##### CTI-Call ext_no:{0},peer_number:{1},call_state:{2},call_type:{3},call_time:{4},trunk:{5},uuid:{6}", ext_no, peer_number, call_state, call_type, call_time, trunk, uuid));

                    call_type = call_type.ToLower();

                    if (peer_number.StartsWith("*"))
                    {
                        //功能按钮过滤
                    }
                    else
                    {
                        if (call_type != "lo")
                        {

                            CTI_Call ctiCall = new CTI_Call { ExtNo = ext_no, PeerNumber = peer_number, CallState = call_state, CallType = call_type, CallTime = call_time, Trunk = trunk, Uuid = uuid };
                            //DebugInfo(string.Format("ctiObjects4Now contain key:{0}################{1}", ext_no, Global.ctiObjects4Now.ContainsKey(ext_no)));

                            ConcurrentQueue<CTI_Call> queueOfCalls;
                            if (!Global.ctiCalls.TryGetValue(ext_no, out queueOfCalls))
                            {
                                queueOfCalls = new ConcurrentQueue<CTI_Call>();
                                Global.ctiCalls.TryAdd(ext_no, queueOfCalls);
                            }

                            /*
                            if (queue.Count > 2)
                            {
                                CTI_Object o;
                                queue.TryDequeue(out o);
                            }
                            */

                            queueOfCalls.Enqueue(ctiCall);

                            //DebugInfo(string.Format("ext_no:{0}----------------->{1}", ext_no, Global.ctiObjects4Now[ext_no].Count.ToString()));
                        }
                    }
                }
                else
                {
                    //CTI-Conference
                    string action = context.Request.Form["action"];  
                    string update_time = context.Request.Form["update_time"];
                    string member_no = context.Request.Form["member_no"];
                    string member_id = context.Request.Form["member_id"];
                    Global.DebugInfo(string.Format("##### CTI-Conference c_name:{0},action:{1},update_time:{2},member_no:{3},member_id:{4}", c_name, action, update_time, member_no, member_id));

                    CTI_Conference ctiConference = new CTI_Conference { ConferenceName = c_name, Action = action, UpdateTime = update_time, MemberPhoneNo = member_no, MemberId = member_id };
                    ConcurrentQueue<CTI_Conference> queueOfConferences;
                    if (!Global.ctiConferences.TryGetValue(c_name, out queueOfConferences))
                    {
                        queueOfConferences = new ConcurrentQueue<CTI_Conference>();
                        Global.ctiConferences.TryAdd(c_name, queueOfConferences);
                    }

                    queueOfConferences.Enqueue(ctiConference);

                    Global.DebugInfo(string.Format("c_name:{0}----------------->{1}", c_name, Global.ctiConferences.Count.ToString()));
                }
                
                context.Response.ContentType = "text/plain";
                context.Response.Write("ok");
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("invalid");
            }
            
        } 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}