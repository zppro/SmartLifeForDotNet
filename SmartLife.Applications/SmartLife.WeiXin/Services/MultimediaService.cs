using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using SmartLife.WeiXin.Pub;
using SmartLife.WeiXin.Model;

namespace SmartLife.WeiXin.Services
{
    public class MultimediaService
    {
        /// <summary>
        /// 下载保存多媒体文件,返回多媒体保存路径
        /// </summary>
        /// <param name="ACCESS_TOKEN"></param>
        /// <param name="MEDIA_ID"></param>
        /// <returns></returns>
        public void GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();   //获取响应资源中的url地址
                WebClient mywebclient = new WebClient();
                if (myResponse.ContentType.EndsWith("image/jpeg"))
                {
                    savepath = AppDomain.CurrentDomain.BaseDirectory + "\\img\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                }
                else if (myResponse.ContentType.EndsWith("audio/amr"))
                {
                    savepath = AppDomain.CurrentDomain.BaseDirectory + "\\vioce\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".amr";
                }
                try
                {
                    mywebclient.DownloadFile(strpath, savepath);

                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                    Util.WriteTxt(ex.ToString());
                }

            }
        }

        /// <summary>  
        /// 上传多媒体文件,返回 MediaId  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <param name="Type"></param>  
        /// <returns></returns>  
        public string UploadMultimedia(string ACCESS_TOKEN, string Type ,string fileName)
        {
            string result = "";
            string filepath = "";
            string wxurl = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ACCESS_TOKEN + "&type=" + Type;
            if ("image".Equals(Type))
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "\\imgTest\\" + fileName;
            }
            else if ("voice".Equals(Type))
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "\\voiceTest\\" + fileName;
            }
            Util.WriteTxt("上传路径:" + filepath);
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filepath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
                Util.WriteTxt("上传result:" + result);
                Multimedia _mode = JsonHelper.ParseFromJson<Multimedia>(result);
                result = _mode.media_id;
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.Message;
                Util.WriteTxt("多媒体上传:" + ex.ToString());
            }
            Util.WriteTxt("上传MediaId:" + result);
            return result;
        }  




    }
}