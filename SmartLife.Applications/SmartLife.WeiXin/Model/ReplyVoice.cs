using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
    public class ReplyVoice
    {   
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间戳 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 语音，voice
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 通过上传多媒体文件，得到的id
        /// </summary>
        public string MediaId { get; set; }

        #region 回复语音消息
        public string replyVoice(string ToUserName, string FromUserName, string MediaId) {
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[voice]]></MsgType>" +
                                       " <Voice><MediaId><![CDATA[{3}]]></MediaId></Voice></xml>", ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), MediaId);
            return str;

        }
    #endregion
    }
}