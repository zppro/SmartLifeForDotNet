using System;
using System.Collections.Generic;
using System.Web;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using e0571.web.core.Model;
using e0571.web.core.DataAccess;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using System.Web.SessionState;
using SmartLife.Share.Models.Pub;

namespace SmartLife.City.ManageServices.Pub
{
    /// <summary>
    /// _AreaBaseDataUploadify 的摘要说明
    /// </summary>
    public class _AreaBaseDataUploadify : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile file = context.Request.Files["Filedata"];


            if (file != null)
            {
                string parentId = context.Request.Form["AreaId"];
                //Guid areaId = new Guid(areaIdStr);
                IList<StringObjectDictionary> datas = NPOIManager.GetSheetData(file.InputStream, 0, true);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                Area area = new Area() { ParentId = parentId, Status = 1, CheckInTime = DateTime.Now };
                foreach (var data in datas)
                {
                    StringObjectDictionary sod = area.ToStringObjectDictionary(false);
                    IDictionary<string, object> dataItem = sod.MixInObject(data, false, e0571.web.core.Other.CaseSensitive.NORMAL);

                    statements.Add(new IBatisNetBatchStatement { StatementName = area.GetCreateMethodName(), ParameterObject = dataItem, Type = 

SqlExecuteType.INSERT });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
               
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