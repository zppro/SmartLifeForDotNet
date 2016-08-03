using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using e0571.web.core.Model;
using e0571.web.core.DataAccess;
using SmartLife.Share.Models.Oca;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using System.Web.SessionState;

namespace SmartLife.CertManage.ManageServices.Oca
{
    /// <summary>
    /// _MutualAidPersonUploadify 的摘要说明
    /// </summary>
    public class _MutualAidPersonUploadify : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile file = context.Request.Files["Filedata"];


            if (file != null)
            {
                string AreaId = context.Request.Form["AreaId"];
                IList<StringObjectDictionary> datas = NPOIManager.GetSheetData(file.InputStream, 0, true);
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                MutualAidPerson mutualAidPerson = new MutualAidPerson() { DataSource = GlobalManager.DIKey_00012_UploadFile, OperatedBy = NormalSession.UserId.ToGuid(), OperatedOn = DateTime.Now, AreaId = AreaId };
                foreach (var data in datas)
                {
                    mutualAidPerson.MutualAidPersonId = Guid.NewGuid(); 
                    StringObjectDictionary sod = mutualAidPerson.ToStringObjectDictionary(false);
                    IDictionary<string, object> dataItem = sod.MixInObject(data, false, e0571.web.core.Other.CaseSensitive.NORMAL);

                    statements.Add(new IBatisNetBatchStatement { StatementName = mutualAidPerson.GetCreateMethodName(), ParameterObject = dataItem, Type = SqlExecuteType.INSERT });
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