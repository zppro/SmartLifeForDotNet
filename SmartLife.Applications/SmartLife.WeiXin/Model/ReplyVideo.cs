using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
    public class ReplyVideo
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
	 	/// 消息创建时间 （整型）
	 	/// </summary>
        private int CreateTime{ get; set; }
        /// <summary>
        ///  video类型
        /// </summary>
	 	private string MsgType{ get; set; } 
        /// <summary>
        /// 通过上传多媒体文件，得到的id
        /// </summary>
	    private	string MediaId{ get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
	 	private string Title{ get; set; } 
        /// <summary>
        ///  视频消息的描述
        /// </summary>
	    private string Description{ get; set; }

        #region
        public string replyVideo(string ToUserName, string FromUserName, string MediaId, string Title, string Description) {
            string str = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[video]]></MsgType><Video>" +
                                       "<MediaId><![CDATA[{3}]]></MediaId><Title><![CDATA[{4}]]></Title><Description><![CDATA[{5}]]></Description></Video></xml>",
                                       ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), MediaId, Title, Description);
            return str;
        }
        #endregion

    }
}