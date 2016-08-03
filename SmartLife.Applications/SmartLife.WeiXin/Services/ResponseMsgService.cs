using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.WeiXin.xml;
using SmartLife.WeiXin.Pub;
using SmartLife.WeiXin.Model;

namespace SmartLife.WeiXin.Services
{
    public class ResponseMsgService
    {
        #region 回复 文字请求
        public string resText(RequestXML requestXML) {
            string resxml = "";
            Reply reply = new Reply();
            if (requestXML.Content.Equals("1"))
            {
                var mediaId = new MultimediaService().UploadMultimedia(GlobalToken.AccessToken, "image", "test1.jpg");
                resxml = reply.strreplyImage(requestXML.FromUserName, requestXML.ToUserName, mediaId);
                //Util.WriteTxt(resxml);
            }
            if (requestXML.Content.Equals("2"))
            {
                resxml = reply.strreplyImage(requestXML.FromUserName, requestXML.ToUserName, "L7vNwJMTBWViMx7fQeFsHSzXv9Sgz1E9ECvNq1qnjTRSp9tF7p7Owc_60zSknHMG");
                //Util.WriteTxt(resxml);
            }
            if (requestXML.Content.Equals("3"))
            {
                var mediaId = new MultimediaService().UploadMultimedia(GlobalToken.AccessToken, "voice", "test1.amr");
                resxml = reply.strreplyVoice(requestXML.FromUserName, requestXML.ToUserName, mediaId);
                // Util.WriteTxt(resxml);
            }
            if (requestXML.Content.Equals("4"))
            {
                new PushMessageService().pushMessage();
                //Util.WriteTxt(resxml);
            }
            if (requestXML.Content.Equals("5"))
            {
                new PushMessageService().pushImage();
                //Util.WriteTxt(resxml);
            }
            if (requestXML.Content.Equals("6")) {
                resxml = new ReplyContent().replyContent(requestXML.FromUserName, requestXML.ToUserName,"测试");
            }
            return resxml;
        }
        #endregion

        #region 回复事件请求
        public string resEvent(RequestXML requestXML) {
            string resxml = "";
            //关注
            if (requestXML.Event.Equals("subscribe"))
            {
                new SubscribeService().subscribe(requestXML.FromUserName);
                resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + Util.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[欢迎加入蓝谷养老]]></Content><FuncFlag>0</FuncFlag></xml>";
            }
            //取消关注
            if (requestXML.Event.Equals("unsubscribe"))
            {
                new SubscribeService().unsubscribe(requestXML.FromUserName);
            }
            //服务项目查询
            if (requestXML.EventKey.Equals("WX101"))
            {
                //resxml = new GovernmentService().GovernmentItem(requestXML);
            }

            if (requestXML.EventKey.Equals("WX201"))
            {
                //resxml = new GovernmentService().OldManItem(requestXML);
            }
            //政府 服务记录查询  
            if (requestXML.EventKey.Equals("WX102")) 
            {
                resxml = new GovernmentService().isAuthOrBound(requestXML);
            }
            //政府 服务记录查询  
            if (requestXML.EventKey.Equals("WX202"))
            {
                resxml = new OldManService().isAuthOrBound(requestXML);
            }

            //自定义菜单 click事件    
            if (requestXML.EventKey.Equals("V1001_HELLO_WORLD"))
            {
                resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + Util.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + 10 + "</ArticleCount><Articles>";
                List<string> list = new List<string>();
                //图文消息 最多返回10条数据
                for (int i = 0; i < 10; i++)
                {
                    list.Add("1");
                }
                for (int i = 0; i < list.Count; i++)
                {
                    resxml += "<item><Title><![CDATA[浙江-杭州]]></Title><Description><![CDATA[元旦特价：￥300 市场价：￥400]]></Description><Url><![CDATA[http://www.hougelou.com]]></Url></item>";
                }
                resxml += "</Articles><FuncFlag>1</FuncFlag></xml>";
            }
            return resxml;
        }
        #endregion

        #region 回复定位请求
        public string resLocation(RequestXML requestXML) {
            //string city = GetMapInfo(requestXML.Location_X, requestXML.Location_Y);
            string resxml = "";
            string city = "";
            if (city == "0")
            {
                resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + Util.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，没有找到" + city + " 的相关产品信息]]></Content><FuncFlag>0</FuncFlag></xml>";
            }
            else
            {
                resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + Util.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，这是 " + city + " 的产品信息]]></Content><FuncFlag>0</FuncFlag></xml>";
            }
            return resxml;
        }
        #endregion

        #region 下载图片
        public void resImage(RequestXML requestXML)
        {
            string mediaId = requestXML.MediaId;
            new MultimediaService().GetMultimedia(GlobalToken.AccessToken, mediaId);
        }
        #endregion

        #region 下载语音文件
        public void resVoice(RequestXML requestXML)
        {
            string mediaId = requestXML.MediaId;
            new MultimediaService().GetMultimedia(GlobalToken.AccessToken, mediaId);
        }
        #endregion

    }
}