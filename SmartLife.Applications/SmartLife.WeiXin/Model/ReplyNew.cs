using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLife.WeiXin.Pub;

namespace SmartLife.WeiXin.Model
{
    public class ReplyNew
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        private string ToUserName{ get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
	 	private string  FromUserName{ get; set; }
	 	/// <summary>
	 	/// 消息创建时间 （整型）
	 	/// </summary>
        private int CreateTime{ get; set; }
	    /// <summary>
	    /// news
	    /// </summary>
        private string MsgType{ get; set; }
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        private string ArticleCount{ get; set; }
	 	/// <summary>
	 	/// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
	 	/// </summary>
	    private string Articles{ get; set; }	 
	    /// <summary>
	    /// 图文消息标题
	    /// </summary>
        private string Title{ get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        private string Description{ get; set; }
	    /// <summary>
	    /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
	    /// </summary>
	    private string PicUrl{ get; set; }
	    /// <summary>
	    /// 点击图文消息跳转链接
	    /// </summary>
	    private string Url{ get; set; }

        public ReplyNew() { }

        public ReplyNew(string Title, string Description, string PicUrl, string Url)
        {
            this.Title = Title;
            this.Description = Description;
            this.PicUrl = PicUrl;
            this.Url = Url;
        }

        #region 回复图文消息
        public string replyNews(string ToUserName,string  FromUserName, string ArticleCount, List<ReplyNew> replyNews){
            string str=string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[news]]></MsgType>"+
                                     "<ArticleCount>{3}</ArticleCount><Articles>", ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), ArticleCount);
            foreach(ReplyNew replyNew in replyNews){
                str += string.Format("<item><Title><![CDATA[{0}]]></Title> <Description><![CDATA[{1}]]></Description><PicUrl><![CDATA[{2}]]></PicUrl><Url><![CDATA[{3}]]></Url></item>",
                    replyNew.Title, replyNew.Description, replyNew.PicUrl, replyNew.Url); 
            }
           str = str + "</Articles></xml>";
           return str;
        }
        #endregion

        #region 回复一条图文消息
        public string replyNew(string ToUserName,string  FromUserName,string title,string description,string picurl,string url) { 
            string res = string.Format(@"<xml>  
                                                <ToUserName><![CDATA[{0}]]></ToUserName>  
                                                <FromUserName><![CDATA[{1}]]></FromUserName>  
                                                <CreateTime>{2}</CreateTime>  
                                                <MsgType><![CDATA[news]]></MsgType>  
                                                <ArticleCount>1</ArticleCount>  
                                                <Articles>  
                                                <item>  
                                                <Title><![CDATA[{3}]]></Title>   
                                                <Description><![CDATA[{4}]]></Description>  
                                                <PicUrl><![CDATA[{5}]]></PicUrl>  
                                                <Url><![CDATA[{6}]]></Url>  
                                                </item>  
                                                </Articles>  
                                       </xml> ",
               ToUserName, FromUserName, Util.ConvertDateTimeInt(DateTime.Now), title, description, picurl, url);  
                return res;
        }
        #endregion
    }
}