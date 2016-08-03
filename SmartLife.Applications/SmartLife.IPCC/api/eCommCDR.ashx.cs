using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.Share.Models.PO.eComm;
using System.Collections.Concurrent;
using SmartLife.Share.Behaviors;
using e0571.web.core.TypeInherited;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.Utils;
using e0571.web.core.Model;


namespace SmartLife.IPCC.api
{
    /// <summary>
    /// eCommCDR 的摘要说明
    /// </summary>
    public class eCommCDR : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string caller = context.Request.Form["caller"];
                string called = context.Request.Form["called"];
                string begin_time = context.Request.Form["begin_time"];
                string end_time = context.Request.Form["end_time"];
                string duration = context.Request.Form["duration"];
                string record_path = context.Request.Form["record_path"];
                string call_type = context.Request.Form["call_type"];
                string trunk = context.Request.Form["trunk"];
                string uuid = context.Request.Form["uuid"];
                string hangupcause = context.Request.Form["hangupcause"];
                string msg = string.Format("caller:{0},called:{1},begin_time:{2},end_time:{3},duration:{4},record_path:{5},call_type:{6},trunk:{7},uuid:{8},hangupcause:{9}", caller, called, begin_time, end_time, duration, record_path, call_type, trunk, uuid, hangupcause);
                Global.DebugInfo(msg);

                call_type = call_type.ToLower();
                //85027666_192.168.1.18_5060
                string[] trunk_array = trunk.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                trunk = trunk_array[0];

                bool bUpdateVoiceTime = false;
                if (call_type == "in")
                {
                    //第一种需要回拨的情形
                    if (called == trunk)
                    {
                        bUpdateVoiceTime = true;    //回拨模式呼入不计入时长
                        //当产生的通话记录中的被叫号码(called)和中继号码(trunk)是一样的则表示用户主动放弃来电，需要回拨 
                        SPParam theSPParam = new { UuidOfIPCC = uuid }.ToSPParam();
                        BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_AbandonByCaller_eComm", theSPParam);
                        if (theSPParam.ErrorCode != 0)
                        {
                            msg = "SP_Api_AbandonByCaller_eComm ErrorCode:" + theSPParam.ErrorCode.ToString() + " ErrorMessage:" + theSPParam.ErrorMessage;
                        }
                    }
                }

                //计入通话时长
                if (!bUpdateVoiceTime)
                {
                    DateTime startTime = DateTimeAdapter.ConvertUnitToNormal(Int32.Parse(begin_time));
                    DateTime endTime = DateTimeAdapter.ConvertUnitToNormal(Int32.Parse(end_time));
                    int iCallSeconds = (endTime - startTime).Seconds;

                    SPParam theSPParam = new { Caller = caller, CallSeconds = iCallSeconds }.ToSPParam();
                    if (call_type == "out")//回拨模式
                    {
                        theSPParam["Caller"] = called;
                    }

                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_RecordCallSeconds_eComm", theSPParam);
                    if (theSPParam.ErrorCode != 0)
                    {
                        msg = "SP_Api_RecordCallSeconds_eComm ErrorCode:" + theSPParam.ErrorCode.ToString() + " ErrorMessage:" + theSPParam.ErrorMessage;
                    }

                    Global.DebugInfo(string.Format("caller:{0}---呼入时长:-------------->{1}", theSPParam["Caller"], iCallSeconds.ToString()));
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(msg);
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