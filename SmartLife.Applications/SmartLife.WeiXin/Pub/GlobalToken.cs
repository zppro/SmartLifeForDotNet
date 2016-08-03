using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using SmartLife.WeiXin.Model;

namespace SmartLife.WeiXin.Pub
{
    public class GlobalToken
    {
        private static DateTime GetAccessToken_Time;
        /// <summary>
        /// 过期时间为7200秒
        /// </summary>
        private static int Expires_Period = 7200;
        /// <summary>
        /// 
        /// </summary>
        private static string mAccessToken;

        private static string AppID = Global.TheServiceAccount.AppId;
        private static string AppSecret = Global.TheServiceAccount.AppSecret;

        public static string AccessToken
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(mAccessToken) || HasExpired())
                {
                    //获取
                    mAccessToken = GetAccessToken(AppID, AppSecret);
                    //Util.WriteTxt("重新获取票据，获取时间：" + DateTime.Now);
                }
                //Util.WriteTxt("返回票据：" + mAccessToken);
                return mAccessToken;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        private static string GetAccessToken(string appId, string appSecret)
        {
            WebClient webclient = new WebClient();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            byte[] bytes = webclient.DownloadData(url);//在指定的path上下载
            string result = Encoding.GetEncoding("utf-8").GetString(bytes);//转string
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                AccessToken amodel = js.Deserialize<AccessToken>(result);//此处为定义的类，用以将json转成model

                GetAccessToken_Time = DateTime.Now;
                Expires_Period = amodel.expires_in;
                string mAccessToken = amodel.access_token;
                //Util.WriteTxt("获取票据有效时间：" + Expires_Period);
                return mAccessToken;
            }
            catch (Exception ex){
                GetAccessToken_Time = DateTime.MinValue;
                Util.WriteTxt("获取accessToken：" + ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired()
        {
            if (GetAccessToken_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > GetAccessToken_Time.AddSeconds(Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }


        ///// <summary>
        ///// 获得access_token
        ///// </summary>
        ///// <param name="appid"></param>
        ///// <param name="appsecret"></param>
        ///// <returns></returns>
        //public static string Get_Access_token()
        //{
        //    WebClient webclient = new WebClient();
        //    var appid = Global.TheServiceAccount.AppId;
        //    var secret = Global.TheServiceAccount.AppSecret;
        //    string url = @"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
        //    byte[] bytes = webclient.DownloadData(url);//在指定的path上下载
        //    string result = Encoding.GetEncoding("utf-8").GetString(bytes);//转string
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    AccessToken amodel = js.Deserialize<AccessToken>(result);//此处为定义的类，用以将json转成model
        //    string a_token = amodel.access_token;  
        //    return a_token;
        //}
        ///// <summary>
        ///// 定时器 每隔2秒运行一次
        ///// </summary>
        ///// <returns></returns>
        //public static string timmer()
        //{
        //    TimeSpan timespan;
        //    if (first)
        //    {
        //        d1 = DateTime.Now;/*第一次获取系统时间*/
        //        accesstoken = Get_Access_token();
        //        first = false;
        //    }
        //    return accesstoken;
        //}

    }
}