using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Model;
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Behaviors;
using Newtonsoft.Json;
using SmartLife.Share.Models.Pub;
using e0571.web.core.DataAccess;

namespace SmartLife.CertManage.ManageServices.Pub
{
    /// <summary>
    /// _ArticleUpdateContent 的摘要说明
    /// </summary>
    //[ValidateInput(false)]
    public class _ArticleUpdateContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Article article = new Article();
            context.Response.ContentType = "text/html";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            string strContent = context.Request.Form["Content"].ToString();
            strContent = strContent.Replace("☞", "<");
            strContent = strContent.Replace("☜", ">");

            string _ArticleId=context.Request.Form["ArticleId"].ToString();
            string _Content = strContent;
            string connectId = context.Request.Form["ConnectId"].ToString();

            article.ArticleId = _ArticleId.ToGuid();
            article.Content =_Content;
            List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
            statements.Add(new IBatisNetBatchStatement { StatementName = article.GetUpdateMethodName(), ParameterObject = article.ToStringObjectDictionary(false), Type = SqlExecuteType.UPDATE });

            BuilderFactory.DefaultBulder(connectId).ExecuteNativeSqlNoneQuery(statements);
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