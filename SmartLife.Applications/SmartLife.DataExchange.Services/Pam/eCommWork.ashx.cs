using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using e0571.web.core.TypeInherited;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.Model;
using SmartLife.Share.Models.Pam;
using e0571.web.core.Utils;

namespace SmartLife.DataExchange.Services.pam
{
    /// <summary>
    /// eCommWork 的摘要说明
    /// </summary>
    public class eCommWork : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string caller = context.Request.Form["caller"];
                string callee = context.Request.Form["callee"];
                string uuid = context.Request.Form["uuid"];
                string c_name = context.Request.Form["c_name"];
                string msg = string.Format("caller:{0},callee:{1},uuid:{2},cname:{3}", caller, callee, uuid, c_name);

                //报警
                if (c_name == "dAlarm") {
                    SPParam theSPParam = new { Caller = caller, Callee = callee, UuidOfIPCC = uuid }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_DeviceAlarm", theSPParam);
                    if (theSPParam.ErrorCode != 0)
                    {
                        msg = "SP_Pam_DeviceAlarm ErrorCode:" + theSPParam.ErrorCode.ToString() + ", ErrorMessage:" + theSPParam.ErrorMessage;
                    }
                    else {
                        msg = "ReturnRet:" + TypeConverter.ChangeString(theSPParam["CallQueueRet"]);
                    }
                }
                else
                {
                    //填入到达离开
                    SPParam theSPParam = new { ExtNo = caller, Callee = callee }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pam_ResponseWorkExcute", theSPParam);
                    if (theSPParam.ErrorCode != 0)
                    {
                        msg = "SP_Pam_ResponseWorkExcute ErrorCode:" + theSPParam.ErrorCode.ToString() + " ErrorMessage:" + theSPParam.ErrorMessage;
                    }
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(msg);
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("invalid");
            }
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
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