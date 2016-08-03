using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using SmartLife.WeiXin.Model;
using SmartLife.WeiXin.Pub;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace SmartLife.WeiXin.Services
{
    public class Reply 
    {
        #region
        public string replyContent(string ToUserName, string FromUserName, string Content)
        {
            
            ReplyContent replycontext = new ReplyContent();
            replycontext.ToUserName = "<![CDATA[" + ToUserName + "]]>";
            replycontext.FromUserName = "<![CDATA[" + FromUserName + "]]>";
            replycontext.CreateTime = Util.ConvertDateTimeInt(DateTime.Now);
            replycontext.MsgType = "<![CDATA[" + FromUserName + "]]>";
            replycontext.Content = "<![CDATA[" + Content + "]]>";
           //序列化这个对象
            string str="";
            XmlSerializer xz = new XmlSerializer(replycontext.GetType());
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = false;
                XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                xns.Add("", "");
                xz.Serialize(sw, replycontext, xns);
                //xz.Serialize(sw, replycontext);
                str = sw.ToString();
                
            }
            //str = str.Replace("","");
            str = str.Substring(str.IndexOf("<xml>"));
            str = str.Replace("\r\n", "");
            return str;
        }
        #endregion

        public string strreplyContent(string ToUserName, string FromUserName, string Content)
        {
             ReplyContent replycontext = new ReplyContent();
             replycontext.ToUserName = ToUserName;
             replycontext.FromUserName = FromUserName;
             replycontext.CreateTime = Util.ConvertDateTimeInt(DateTime.Now);
             replycontext.MsgType = "text";
             replycontext.Content = Content;
             string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[{3}]]></MsgType><Content><![CDATA[{4}]]</Content></xml>", replycontext.ToUserName, replycontext.FromUserName, replycontext.CreateTime, replycontext.MsgType, replycontext.Content);
             return str;
         }

        public string strreplyImage(string ToUserName, string FromUserName, string MediaId)
        {
            ReplyImage replyImage = new ReplyImage();
            replyImage.ToUserName = ToUserName;
            replyImage.FromUserName = FromUserName;
            replyImage.CreateTime = Util.ConvertDateTimeInt(DateTime.Now);
            replyImage.MsgType = "image";
            replyImage.MediaId = MediaId;
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType>" +
            "<![CDATA[{3}]]></MsgType><Image><MediaId><![CDATA[{4}]]></MediaId></Image></xml>", replyImage.ToUserName, replyImage.FromUserName, replyImage.CreateTime, replyImage.MsgType, replyImage.MediaId);
            return str;
        }

        public string strreplyVoice(string ToUserName, string FromUserName, string MediaId) {
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[{3}]]></MsgType>"+
            "<Voice><MediaId><![CDATA[{4}]]></MediaId></Voice></xml>", ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), "voice", MediaId);
            return str;
        }

        public string strreplyMusic(string ToUserName, string FromUserName, string Title, string Description, string MusicURL, string HQMusicUrl, string ThumbMediaId) 
        {
            ReplyMusic replyMusic = new ReplyMusic();

            return "";

        }
     
    }
       
}