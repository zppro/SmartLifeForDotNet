using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Services
{
    public class PushMessageService
    {
        public void pushMessage(string openid, HttpContext context)
        {

            string res = "";
            string Access_Token = GlobalToken.AccessToken;

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + "客服消息测试" + "\"}}";
            res = GetPage(posturl, postData);
            //context.Response.Write(res); 
        }

        public void pushMessage()
        {

            string res = "";
            string openid = "oWygrt3IX8a6phvaXyv0lJivEvYo";
            string Access_Token = GlobalToken.AccessToken;

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + "客服消息测试" + "\"}}";
            res = GetPage(posturl, postData);

            //context.Response.Write(res); 
        }

        public void pushImage() { 
            string res = "";
            string openid = "oWygrt3IX8a6phvaXyv0lJivEvYo";
            string Access_Token = GlobalToken.AccessToken;

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + openid + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + "L7vNwJMTBWViMx7fQeFsHSzXv9Sgz1E9ECvNq1qnjTRSp9tF7p7Owc_60zSknHMG" + "\"}}";
            res = GetPage(posturl, postData);
        }

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