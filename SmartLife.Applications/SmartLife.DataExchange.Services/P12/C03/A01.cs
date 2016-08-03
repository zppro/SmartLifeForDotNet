using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Xml.Linq;

using Newtonsoft.Json;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Behaviors;


namespace SmartLife.DataExchange.Services.P12.C03
{
    [ServiceLogging]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class A01 : BaseWcfService
    {
        [WebInvoke(UriTemplate = "ReceiveSMSMessage", Method = "POST", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream ReceiveSMSMessage(Stream s)
        {
            string result = "";
            XmlDocument xmlDoc = new XmlDocument();

            //返回的xml
            string retXml = "<Package><TimeStamp></TimeStamp> <TransactionID></TransactionID><ResultCode>0000</ResultCode><ResultMsg>成功</ResultMsg ><Status> 1</Status></Package>";
            XElement el = XElement.Parse(retXml);
            
            using (StreamReader sr = new StreamReader(s))
            {
                result = sr.ReadToEnd();
                SmartLife.Share.Behaviors.GlobalManager.inputLogger.Info(result);
            }
            if (!string.IsNullOrEmpty(result))
            {
                string callee = "", msgContent = "", strReturnType = "", strStat = "";
                string strLocateTime = null;

                //载入xml
                xmlDoc.LoadXml(result);
                XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  

                //获取定位时间
                XmlNodeList cNodes = rootElem.GetElementsByTagName("TimeStamp");
                if (cNodes.Count > 0)
                {
                    el.Element("TimeStamp").Value = cNodes[0].InnerText;
                    strLocateTime = DateTime.ParseExact(cNodes[0].InnerText, "yyyyMMddHHmmssfff", new System.Globalization.CultureInfo("zh-CN", true), System.Globalization.DateTimeStyles.AllowInnerWhite).ToString("yyyy-MM-dd HH:mm:ss"); 
                }

                //
                cNodes = rootElem.GetElementsByTagName("TransactionID");
                if (cNodes.Count > 0)
                {
                    el.Element("TransactionID").Value = cNodes[0].InnerText;
                }

                //获取定位消息
                cNodes = rootElem.GetElementsByTagName("ParamItem");
                string getNodeType = "", getNodeValue = "";
                foreach (XmlNode node in cNodes)
                {
                    getNodeType = node["ParamID"].InnerText;
                    getNodeValue = node["ParamValue"].InnerText;
                    if (getNodeType == "DestTermID")
                    {
                        callee = getNodeValue;
                    }
                    else if (getNodeType == "MsgContent")
                    {
                        msgContent = getNodeValue;
                    }
                    else if (getNodeType == "ReturnType")
                    {
                        strReturnType = getNodeValue;
                    }
                    else if (getNodeType == "Stat")
                    {
                        strStat= getNodeValue;
                    }
                }

                try
                {
                    if (!string.IsNullOrEmpty(callee) && string.IsNullOrEmpty(msgContent))
                    {
                        string sql = "";
                        if (strReturnType == "Deliver")
                        {
                            if (msgContent == "105")
                            {
                                sql = "insert Pub_Reminder(SourceTable,SourceColumn,SourceKey,SourceType,RemindTime,RemindContent)" +
                                      "select 'Oca_OldManConfigInfo','OldManId',OldManId,'105','" + strLocateTime + "','电压低于20%报警' from Oca_OldManConfigInfo a.CallNo='" + callee + "' or a.CallNo2='" + callee + "' or a.CallNo3='" + callee + "'";
                            }
                            else
                            {
                                string[] strContent = msgContent.Split('/');
                                sql = "insert Oca_OldManLocateInfo(OldManId,HomeLongitudeS,HomeLatitudeS,LocateTime,LocateLongitudeS,LocateLatitudeS) " +
                                      "select a.OldManId,'" + strContent[2] + "','" + strContent[3] + "','" + strLocateTime + "',b.LongitudeS,b.LatitudeS from Oca_OldManConfigInfo a inner join Oca_OldManBaseInfo b " +
                                      " on a.OldManId=b.OldManId where a.CallNo='" + callee + "' or a.CallNo2='" + callee + "' or a.CallNo3='" + callee + "'";
                            }
                        }
                        else if (strReturnType == "Receipt" && strStat != "UNDELIV")
                        {
                            sql = "update Pub_SmsSend set SendResult=1,SendTime='" + strLocateTime + "' where  Id=(select MAX(Id) From Pub_SmsSend where Mobile='" + callee + "' and SendTime is null group by Mobile)";
                        }
                        if (!string.IsNullOrEmpty(sql))
                        {
                            BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }


            Stream retStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(el.ToString()));
            return retStream;
        }
    }
}