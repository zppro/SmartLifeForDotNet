using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
    [XmlRoot("xml")]
    public class ReplyMusic
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        private string ToUserName{ get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        private string FromUserName{ get; set; }
        /// <summary>
        ///  消息创建时间 （整型）
        /// </summary>
	    private int CreateTime{ get; set; }
        /// <summary>
        /// music 类型
        /// </summary>
	 	private string MsgType{ get; set; }
        /// <summary>
        /// 音乐标题
        /// </summary>
	 	private string Title{ get; set; }
        /// <summary>
        /// 音乐描述
        /// </summary>
	 	private string Description{ get; set; } 
        /// <summary>
        /// 音乐链接
        /// </summary>
	 	private string MusicURL{ get; set; } 
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
	 	private string HQMusicUrl{ get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        private string ThumbMediaId { get; set; }


        #region 回复音乐消息
        public string replyMusic(string ToUserName, string FromUserName, string Title, string Description, string MusicURL, string HQMusicUrl, string ThumbMediaId) {
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[music]]></MsgType><Music>" +
                                       "<Title><![CDATA[{3}]]></Title><Description><![CDATA[{4}]]></Description><MusicUrl><![CDATA[{5}]]></MusicUrl><HQMusicUrl><![CDATA[{6}]]></HQMusicUrl>" +
                                       "<ThumbMediaId><![CDATA[{7}]]></ThumbMediaId></Music></xml>",
                                       ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), Title, Description, MusicURL, HQMusicUrl, ThumbMediaId);
            return str;
        }
        #endregion

    }
}