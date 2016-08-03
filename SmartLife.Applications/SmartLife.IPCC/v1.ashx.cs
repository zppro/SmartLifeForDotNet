using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;

namespace SmartLife.IPCC
{
    /// <summary>
    /// v1 的摘要说明
    /// </summary>
    public class v1 : IHttpHandler
    {
        public static string TC_IPCC_V1_RESULT_TEMPLATE = "<?xml version=\"1.0\" encoding=\"utf-8\"?><response><version>{0}</version><method>{1}</method><result>{2}</result></response>";
        public const string VERSION = "1.0";

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string result = "";
            string method = context.Request.QueryString["method"];
            string caller = context.Request.QueryString["caller"];
            string callee = context.Request.QueryString["callee"];
            string ip = context.Request.UserHostAddress;
            try
            {

                if (method.ToLower() == "callin")
                {
                    SPParam theSPParam = new { Version = VERSION, Caller = caller, Callee = callee, IP = ip }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_CallIn", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        result = TypeConverter.ChangeString(theSPParam["CallQueueRet"]);
                    }
                    else
                    {
                        result = theSPParam.ErrorMessage;
                    }
                }
                else if (method.ToLower() == "onduty")
                {
                    //值班队列按照一个IPCC服务器来配置一组即可
                    string sessionid = TypeConverter.ChangeString(context.Request.QueryString["sessionid"]);
                    SPParam theSPParam = new { Version = VERSION, Caller = caller, Callee = callee, IP = ip, SessionId = sessionid }.ToSPParam();
                    BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Api_OnDuty", theSPParam);
                    if (theSPParam.ErrorCode == 0)
                    {
                        result = TypeConverter.ChangeString(theSPParam["CallQueueRet"]);
                    }
                    else
                    {
                        result = theSPParam.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            context.Response.Write(string.Format(TC_IPCC_V1_RESULT_TEMPLATE, VERSION, method, result));
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