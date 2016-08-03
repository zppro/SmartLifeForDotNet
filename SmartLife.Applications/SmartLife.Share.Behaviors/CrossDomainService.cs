using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Security;
using System.Text;
using System.IO;
using System.Dynamic;

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

using SmartLife.Share.Models.Pam;

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CrossDomainService : BaseWcfService
    {
        #region 跨域调用
        /**跨域调用**/
        [WebGet(UriTemplate = "GetRemoteDataAsJSONP/{remoteUrl}?jsoncallback={callback}", ResponseFormat = WebMessageFormat.Json)]
        public Stream GetRemoteDataAsJSONP(string remoteUrl, string callback)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            HttpAdapter.getSyncTo(remoteUrl, (ret, res) =>
            { 
                result.ret = JsonConvert.DeserializeObject(ret).ToString();
            });
            byte[] resultBytes = Encoding.UTF8.GetBytes(callback + "(" + result.ToJson() + ")");
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }

        [WebInvoke(UriTemplate = "GetRemoteData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Stream GetRemoteData(Remote remote)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            if (remote != null && !string.IsNullOrEmpty(remote.RemoteUrl))
            {
                HttpAdapter.getSyncTo(remote.RemoteUrl, (ret, res) =>
                {
                    if (remote.RetType == "json")
                    {
                        result.ret = JsonConvert.DeserializeObject(ret).ToString();
                    }
                    else {
                        result.ret = ret.ToString();
                    }
                });
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(result.ToJson());
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }

        [WebInvoke(UriTemplate = "GetConferenceInfo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Stream GetConferenceInfo(ConferenceInfo conferenceInfo)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            if (!string.IsNullOrEmpty(conferenceInfo.ConferenceUrl))
            {
                HttpAdapter.getSyncTo(conferenceInfo.ConferenceUrl, (ret, res) =>
                {
                    if (string.IsNullOrEmpty(conferenceInfo.ConferenceMemberUrl))
                    {
                        result.ret = JsonConvert.DeserializeObject(ret).ToString(); //未获得获取会议室成员的URL，直接返回会议室列表
                    }
                    else
                    {
                        List<StringObjectDictionary> conferencelist = new List<StringObjectDictionary>();
                        var conf_data = JsonConvert.DeserializeObject(ret);

                        if ((bool)conf_data.success  && conf_data.data != null)
                        {
                            StringObjectDictionary conference;
                            foreach (var item in conf_data.data)
                            {
                                string confname = (string)item.conf_name;
                                if (confname.IndexOf(conferenceInfo.ExtCode) > -1)   //过滤出属于当前区域的会议室
                                {
                                    conference = new StringObjectDictionary();
                                    conference.Add("conf_name", confname);
                                    conference.Add("uuid", item.uuid);
                                    conference.Add("members", item.members);
                                    conference.Add("runtimes", item.runtimes);

                                    string url = conferenceInfo.ConferenceMemberUrl + "&confname=" + confname;
                                    HttpAdapter.getSyncTo(url, (ret2, res2) =>
                                    {
                                        var conf_data2 = JsonConvert.DeserializeObject(ret2);
                                        if (conf_data2.success == "true" && conf_data2.data != null)
                                        {
                                            List<StringObjectDictionary> sodlist = new List<StringObjectDictionary>();
                                            StringObjectDictionary conferenceMember;
                                            foreach (var o in conf_data2.data)
                                            {
                                                conferenceMember = new StringObjectDictionary();
                                                conferenceMember.Add("id", o.id);
                                                conferenceMember.Add("uuid", o.uuid);
                                                conferenceMember.Add("caller_name", o.caller_name);
                                                conferenceMember.Add("caller_number", o.caller_number);
                                                conferenceMember.Add("join_time", o.join_time);
                                                conferenceMember.Add("last_talking", o.last_talking);
                                                sodlist.Add(conferenceMember);
                                            }
                                            conference.Add("member_data", sodlist.ToArray());
                                        }
                                    });

                                    conferencelist.Add(conference);
                                }
                            }

                            if (conferencelist.Count > 0)
                            {
                                result.ret = JsonConvert.SerializeObject(new { data = conferencelist }.ToStringObjectDictionary());
                            }
                            else
                            {
                                result.ret = "";
                            }
                        }
                    }
                });
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(result.ToJson());
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }

        #region 获取指定队里下分机状态
        [WebInvoke(UriTemplate = "QueueAgentList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Stream QueueAgentList(IpccQueue ipccQueue)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            if (!string.IsNullOrEmpty(ipccQueue.RemoteUrl))
            {
                IList<StringObjectDictionary> queueAgents = new List<StringObjectDictionary>();

                if (ipccQueue.QueueNames != null && ipccQueue.QueueNames.Count > 0)
                {
                    try {
                        string agent_no = "";
                        foreach (string item in ipccQueue.QueueNames)
                        {
                            HttpAdapter.getSyncTo(ipccQueue.RemoteUrl + item, (ret, res) =>
                            {
                                dynamic agent = JsonConvert.DeserializeObject(ret);
                                if (agent.success == "true" && agent.data !=null)
                                {
                                    StringObjectDictionary sod;
                                    foreach (var key in agent.data)
                                    {
                                        agent_no = (string)key.agent_no;

                                        sod = queueAgents.FirstOrDefault(s => s["agent_no"].Equals(agent_no));
                                        if (sod == null)
                                        {
                                            sod = new StringObjectDictionary();
                                            sod.Add("agent_no", agent_no);
                                            sod.Add("uuid", key.uuid);
                                            sod.Add("status", key.status);
                                            sod.Add("cause", key.cause);
                                            sod.Add("state", key.state);
                                            sod.Add("last_offered_call", key.last_offered_call);
                                            sod.Add("last_status_change", key.last_status_change);
                                            sod.Add("no_answer_count", key.no_answer_count);
                                            sod.Add("answer_count", key.answer_count);
                                            sod.Add("ext", (item.IndexOf("999") > -1 ? 1 : 0));
                                            sod.Add("queueName", new List<string>() { item });
                                            queueAgents.Add(sod);
                                        }
                                        else
                                        {
                                            List<string> queueNames = (List<string>)sod.First(s => s.Key == "queueName").Value;
                                            if (queueNames != null)
                                            {
                                                queueNames.Add(item);
                                            }
                                        }
                                    }
                                }
                            });
                        }

                        if (queueAgents.Count > 0)
                        {
                            result.ret = JsonConvert.SerializeObject(queueAgents);
                        }
                        else {
                            result.ret = "";
                        }
                    }
                    catch(Exception ex){
                        result.Success = false;
                        result.ErrorMessage = ex.ToString();
                    }
                }
            }

            byte[] resultBytes = Encoding.UTF8.GetBytes(result.ToJson());
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }
        #endregion

        #region 获取指定队里的排队列表
        [WebInvoke(UriTemplate = "QueueMemberList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Stream QueueMemberList(IpccQueue ipccQueue)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            if (!string.IsNullOrEmpty(ipccQueue.RemoteUrl))
            {
                IList<StringObjectDictionary> queueMembers = new List<StringObjectDictionary>();

                if (ipccQueue.QueueNames != null && ipccQueue.QueueNames.Count > 0)
                {
                    try
                    {
                        foreach (string item in ipccQueue.QueueNames)
                        {
                            HttpAdapter.getSyncTo(ipccQueue.RemoteUrl + item, (ret, res) =>
                            {
                                dynamic agent = JsonConvert.DeserializeObject(ret);
                                if (agent.success == "true" && agent.data != null)
                                {
                                    foreach (var key in agent.data)
                                    {
                                        queueMembers.Add(new { queueName = item, uuid = key.uuid, cid_number = key.cid_number, cid_name = key.cid_name, join_time = key.join_time }.ToStringObjectDictionary());
                                    }
                                }
                            });
                        }

                        if (queueMembers.Count > 0)
                        {
                            result.ret = JsonConvert.SerializeObject(queueMembers);
                        }
                        else
                        {
                            result.ret = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.ErrorMessage = ex.ToString();
                    }
                }
            }

            byte[] resultBytes = Encoding.UTF8.GetBytes(result.ToJson());
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }
        #endregion


        #region 发送自定义语音
        #region 获取IPCC预定义语音
        [WebInvoke(UriTemplate = "IpccSpeechTemplateList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Stream IpccSpeechTemplateList(SpeechTemplate speechtemplate)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            if (!string.IsNullOrEmpty(speechtemplate.RemoteUrl))
            {
                try {
                    HttpAdapter.postSyncAsJSON(speechtemplate.RemoteUrl, new { orgNo = speechtemplate.OrgCode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if (speechtemplate.RetType == "json")
                        {
                            result.ret = JsonConvert.DeserializeObject(ret).ToString();
                        }
                        else
                        {
                            result.ret = ret.ToString();
                        }
                    });
                }
                catch(Exception ex){
                    result.Success = false;
                    result.ErrorMessage = ex.ToString();
                }
            }

            byte[] resultBytes = Encoding.UTF8.GetBytes(result.ToJson());
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }
        #endregion

        #region IPCC录音操作
        [WebInvoke(UriTemplate = "IpccRECManage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult IpccRECManage(RECData recdata)
        {
            InvokeResult result = new InvokeResult { Success = true };
            if (!string.IsNullOrEmpty(recdata.RemoteUrl))
            {
                try
                {
                    HttpAdapter.postSyncAsJSON(recdata.RemoteUrl, recdata.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if (!string.IsNullOrEmpty((string)ret))
                        {
                            result.Success = false;
                            result.ErrorMessage = (string)ret;
                        }
                    });
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.ErrorMessage = ex.ToString();
                }
            }

            return result;
        }
        #endregion
        #endregion

        #region 桔豆盒子

        #region 桔豆盒子课程推送
        [WebInvoke(UriTemplate = "PushJuDouCourse", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult PushJuDouCourse(JuDouCourse judouCourse)
        {
            InvokeResult result = new InvokeResult { Success = true };
            if (judouCourse != null && !string.IsNullOrEmpty(judouCourse.RemoteUrl))
            {
                try
                {
                    //点播
                    string strSql = string.Format("select a.BeginTime,DATEADD(S,a.CourseDuration,a.BeginTime) EndTime,a.CourseFlag,"+
                                   " a.CourseInfo,c.DeviceCode From Pam_CourseSchedule a inner join Pam_Device b on a.DeviceId=b.DeviceId and "+
                                   " a.StationId=b.StationId and b.Status=1 inner join Pub_Device c on b.DeviceId=c.DeviceId and c.Status=1 "+
                                   " where a.Status=1 and a.StationId='{0}' and a.DeviceId in('{1}') and a.BeginTime>= '{2}' and BeginTime<='{3}'"+
                                   " and CourseFlag=3 order by a.DeviceId,a.BeginTime asc", judouCourse.StationId,
                                   string.Join("','",judouCourse.DeviceIds.Split(',')),
                                   judouCourse.CourseBeginTime,judouCourse.CourseEndTime);
                    IList<StringObjectDictionary> dibbleCourseList = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);
                    //直播
                    strSql=string.Format("select a.BeginTime,DATEADD(S,a.CourseDuration,a.BeginTime) EndTime,a.CourseFlag,a.CourseInfo,c.DeviceCode"+
                                   " From (select MAX(Id) Id,BeginTime From Pam_CourseSchedule "+
                                   " where Status=1 and StationId='{0}' and CourseFlag=2 and BeginTime>= '{1}' and BeginTime<='{2}' "+
                                   " group by BeginTime) x inner join Pam_CourseSchedule a on x.Id=a.Id inner join Pam_Device b on a.DeviceId=b.DeviceId and "+
                                   " a.StationId=b.StationId and b.Status=1 inner join Pub_Device c on b.DeviceId=c.DeviceId and c.Status=1 "+
                                   " order by a.BeginTime asc",judouCourse.StationId,judouCourse.CourseBeginTime,judouCourse.CourseEndTime);
                    IList<StringObjectDictionary> liveCourseList = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(strSql);

                    IList<object> pushCourse = new List<object>();
                    string[] stationDevices,courseInfo;
                    string beginTime,endTime;
                    //点播
                    foreach(var item in dibbleCourseList){
                        stationDevices = ((string)item["DeviceCode"]??"").Split('-');
                        courseInfo = ((string)(item["CourseInfo"]??"")).Split('|');
                        beginTime = ((DateTime)item["BeginTime"]).ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "+08:00";
                        endTime = ((DateTime)item["EndTime"]).ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "+08:00";
                        pushCourse.Add(new { begin= beginTime , end=endTime, bindId = stationDevices[0] , uuid=new string[]{ stationDevices[2] },vod_url= new string[]{ courseInfo[2] },live_type= 3 }.ToStringObjectDictionary());
                    }
                    //直播
                    foreach(var item in liveCourseList){
                        stationDevices = ((string)item["DeviceCode"]??"").Split('-');
                        courseInfo = ((string)(item["CourseInfo"]??"")).Split('|');
                        beginTime = ((DateTime)item["BeginTime"]).ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "+08:00";
                        endTime = ((DateTime)item["EndTime"]).ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "+08:00";
                        pushCourse.Add(new { begin= beginTime , end=endTime, bindId = stationDevices[0] , uuid=new string[]{ "all" },vod_url= new string[]{ courseInfo[2]} ,live_type= 2 }.ToStringObjectDictionary());
                    }


                    HttpAdapter.postSyncStr(judouCourse.RemoteUrl, pushCourse.ToJson(), (ret, res) =>
                    {
                        if (string.IsNullOrEmpty((string)ret))
                        {
                            result.Success = false;
                            result.ErrorMessage = (string)ret;
                        }
                    }, (he) =>
                    {
                        result.ErrorCode = he.ErrorCode;
                        result.ErrorMessage = he.Message;
                    });
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.ErrorMessage = ex.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 桔豆盒子信息推送
        [WebInvoke(UriTemplate = "PushJuDouMsg", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult PushJuDouMsg(JudouDevice judouDevice)
        {
            InvokeResult result = new InvokeResult { Success = true };
            if (!string.IsNullOrEmpty(judouDevice.RemoteUrl))
            {
                try
                {
                    HttpAdapter.postSyncAsJSON(judouDevice.RemoteUrl, judouDevice.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if (string.IsNullOrEmpty((string)ret))
                        {
                            result.Success = false;
                            result.ErrorMessage = (string)ret;
                        }
                    });
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.ErrorMessage = ex.ToString();
                }
            }

            return result;
        }
        #endregion
        #endregion

        #endregion

    }

    public class Remote
    {
        public string RemoteUrl { get; set; }
        public string RetType { get; set; }
    }

    public class ConferenceInfo {
        public string ConferenceUrl { get; set; }
        public string ConferenceMemberUrl { get; set; }
        public string ExtCode { get; set; }
    }

    public class IpccQueue
    {
        public string RemoteUrl { get; set; }
        public IList<string> QueueNames { get; set; }
    }

    public class SpeechTemplate : Remote {
        public string OrgCode { get; set; }
    }

    public class RECData : Remote
    {
        public string ExtNo { get; set; }
        public string FileName { get; set; }
        public IList<string> TelNo { get; set; }
    }

    //设备通知
    public class JudouDevice : Remote {
        public string exp_date { get; set; }
        public string position { get; set; }
        public string msg_type { get; set; }
        public string msg_content { get; set; }
        public string deviceid { get; set; }
    }

    //课程信息
    public class JuDouCourse : Remote{
        public string StationId { get; set; }
        public string DeviceIds { get; set; }
        public DateTime CourseBeginTime { get; set; }
        public DateTime CourseEndTime { get; set; }
    }
}
