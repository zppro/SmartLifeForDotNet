using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SmartLife.Share.Behaviors;

namespace SmartLife.CertManage.ManageServices.AppShare
{
    /// <summary>
    /// forReStart 的摘要说明
    /// </summary>
    public class forReStart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("0");
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