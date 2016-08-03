using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net;
using SmartLife.WeiXin.xml;
using SmartLife.WeiXin.Pub;
using SmartLife.CertManage.ManageServices.WeiXin;
using SmartLife.WeiXin.Services;

namespace SmartLife.WeiXin
{
    /// <summary>
    /// v1 的摘要说明
    /// </summary>
    public class v1 : IHttpHandler
    {
      
        public void ProcessRequest(HttpContext context)
        {
            //var a = Reply.replyContent("13215321", "9999", "8888");
            //context.Response.Write(a);

            //var a = Reply.strreplyContent();
            //context.Response.Write(a);
            //return;

            //string menustr = BottomMenu.createMenuStrAsXml();
            //// context.Response.Write(menustr);
            //Util.WriteTxt("菜单：" + menustr);
            //BottomMenu.createMenu();
            //return;
            string postStr = "";
           
            if (context.Request.HttpMethod == "POST")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    //封装请求类
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;

                    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                    RequestXML requestXML = new RequestXML();
                    requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                    requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                    requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                    requestXML.MsgType = MsgType.InnerText;

                    if (requestXML.MsgType.Equals("text"))
                    {
                        requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                    }
                    else if (requestXML.MsgType.Equals("location"))
                    {
                        requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                        requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                    }
                    else if (requestXML.MsgType.Equals("image"))
                    {
                        requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        requestXML.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                    }
                    else if (requestXML.MsgType.Equals("event"))
                    {
                        requestXML.Event = rootElement.SelectSingleNode("Event").InnerText;
                        requestXML.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                    }
                    else if (requestXML.MsgType.Equals("voice")) 
                    {
                        requestXML.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                    }
                    //回复消息
                    ResponseMsg(requestXML, context);
                }
            }
            else
            {
                Valid(context);
            }

        }

        private void Valid(HttpContext context)
        {
            string echoStr = context.Request.QueryString["echoStr"];
            if (CheckSignature(context))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    context.Response.Write(echoStr);
                    context.Response.End();
                }
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(HttpContext context)
        {
            string signature = context.Request.QueryString["signature"];
            string timestamp = context.Request.QueryString["timestamp"];
            string nonce = context.Request.QueryString["nonce"];
            string[] ArrTmp = { Global.TheServiceAccount.Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 回复消息(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private void ResponseMsg(RequestXML requestXML, HttpContext context)
        {
            ResponseMsgService responseMsg = new ResponseMsgService();
            try
            {
                string resxml = "";
                if (requestXML.MsgType.Equals("text"))
                {
                    resxml = responseMsg.resText(requestXML);
                }
                if (requestXML.MsgType.Equals("event"))
                {
                    resxml = responseMsg.resEvent(requestXML);
                }
                else if (requestXML.MsgType.Equals("location"))
                {
                    resxml = responseMsg.resLocation(requestXML);
                }
                else if (requestXML.MsgType == "image")
                {
                    responseMsg.resImage(requestXML);
                }
                else if (requestXML.MsgType == "voice")
                {
                    responseMsg.resVoice(requestXML);
                }
                context.Response.Write(resxml);
            }
            catch (Exception ex)
            {
                Util.WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}