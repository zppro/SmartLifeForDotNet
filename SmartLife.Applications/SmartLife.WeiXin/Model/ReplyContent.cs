using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
   
    [XmlRoot("xml")]
    public class ReplyContent
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        //[XmlArray("ToUserName")]
        [XmlText]
        public string ToUserName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
       [XmlElement("FromUserName")]
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        [XmlElement("CreateTime")]
        public int CreateTime { get; set; }

        /// <summary>
        /// text
        /// </summary>
        [XmlElement("MsgType")]
        public string MsgType { get; set; }

        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        [XmlElement("Content")]
        public string Content { get; set; }

        #region 回复文本
        public string replyContent(string ToUserName, string FromUserName, string Content)
        {

            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType>" +
                                        "<Content><![CDATA[{3}]]></Content></xml>", ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), Content);
            return str;
        }
        #endregion
    }
}