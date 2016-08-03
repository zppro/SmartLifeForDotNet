using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.WeiXin.Model;
using System.Net;
using System.Text;
using SmartLife.WeiXin.Pub;
using System.IO;

namespace SmartLife.WeiXin.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PushMessage : AppBaseWcfService
    {
         #region 客服 发送 文本消息
        [WebGet(UriTemplate = "PushText?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string PushText(string strParms)
        {
            string res = "";
            string openid = "oWygrt3IX8a6phvaXyv0lJivEvYo";
            string content = "客服消息测试";
            string Access_Token = GlobalToken.AccessToken;

            var dictionary = new StringObjectDictionary().MixInJson(strParms);
            foreach (var item in dictionary)
            {
                if (item.Key.Equals("openid") && item.Value.ToString() != "")
                {
                    openid = item.Value.ToString();
                }
                if (item.Key.Equals("content") && item.Value.ToString() != "")
                {
                    content = item.Value.ToString();
                }
            }

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"}}";
            res = GetPage(posturl, postData);
            return "OK";
        }
        #endregion

        #region 客服 发送 图片
         [WebGet(UriTemplate = "pushImage", ResponseFormat = WebMessageFormat.Json)]
        public void pushImage()
        {
            string res = "";
            string openid = "oWygrt3IX8a6phvaXyv0lJivEvYo";
            string Access_Token = GlobalToken.AccessToken;

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + openid + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + "L7vNwJMTBWViMx7fQeFsHSzXv9Sgz1E9ECvNq1qnjTRSp9tF7p7Owc_60zSknHMG" + "\"}}";
            res = GetPage(posturl, postData);
        }
        #endregion

        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                // 设置参数  
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //Response.Write(err);
                return string.Empty;
            }
        }
    }
}