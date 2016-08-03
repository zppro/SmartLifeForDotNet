using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
    [XmlRoot("xml")]
    public class ReplyImage
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        [XmlElement("ToUserName")]
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
        /// image
        /// </summary>
        [XmlElement("MsgType")]
        public string MsgType { get; set; }

        /// <summary>
        /// 通过上传多媒体文件，得到的id
        /// </summary>
        [XmlArrayItem("Image")]
        [XmlElement("MediaId")]
        public string MediaId { get; set; }

        #region 回复图片
        public string replyImage(string ToUserName, string FromUserName, string MediaId)
        {
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType>" +
            "<![CDATA[image]]></MsgType><Image><MediaId><![CDATA[{3}]]></MediaId></Image></xml>", ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), MediaId);
            return str;
        }
        #endregion
    }
}