using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using e0571.web.core.Utils;

namespace SmartLife.Auth.ManageServices.AppShare
{
    /// <summary>
    /// Uploadify 
    /// </summary>
    public class uploadify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            

            if (file != null)
            {
                string saveName = @context.Request["saveName"];
                string fileType = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]);
                FileAdapter.EnsurePath(uploadPath);
                if (string.IsNullOrEmpty(saveName))
                {
                    file.SaveAs(uploadPath + file.FileName);
                }
                else
                {
                    file.SaveAs(uploadPath + saveName + fileType);
                }
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
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