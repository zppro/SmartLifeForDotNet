using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Text;
using System.Dynamic;
using System.Collections;
using System.Xml.Linq;

using e0571.web.core.Model;
using e0571.web.core.Wcf;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Models.Oca;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Behaviors;


namespace SmartLife.DataExchange.Services.P12.C03
{
    /// <summary>
    /// A0101 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.bnet.cn/v3.0/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    //[System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class A0101 : WebService
    {
        [WebMethod]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        //[System.Xml.Serialization.XmlElementAttribute("ReturnInvokeReturn", Namespace = "http://www.bnet.cn/v3.0/")]
        public string ReturnInvoke(string reqXml)
        {
            dynamic dx = new DynamicXml(reqXml);
            if (dx != null) {
                string callee = "", msgContent = "", strReturnType = "", strStat = "";

                //时间戳
                string strLocateTime = DateTime.ParseExact((string)dx.TimeStamp, "yyyyMMddHHmmssfff", new System.Globalization.CultureInfo("zh-CN", true), System.Globalization.DateTimeStyles.AllowInnerWhite).ToString("yyyy-MM-dd HH:mm:ss");

                foreach (dynamic item in dx.ParamItems.ParamItem) {
                    string strParamID = (string)item.ParamID.value;
                    string strParamValue = (string)item.ParamValue.value;

                    if (strParamID == "DestTermID")
                    {
                        callee = strParamValue;
                    }
                    else if (strParamID == "MsgContent")
                    {
                        msgContent = strParamValue;
                    }
                    else if (strParamID == "ReturnType")
                    {
                        strReturnType = strParamValue;
                    }
                    else if (strParamID == "Stat")
                    {
                        strStat = strParamValue;
                    }
                }

                try
                {
                    if (!string.IsNullOrEmpty(callee) && !string.IsNullOrEmpty(msgContent))
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

                        BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(sql);
                    }
                }
                catch (Exception ex)
                {
                    SmartLife.Share.Behaviors.GlobalManager.inputLogger.Info(ex.ToString());
                }

            }

            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<Package>");
            sb.AppendFormat("<TimeStamp>{0}</TimeStamp>", dx.TimeStamp);
            sb.AppendFormat("<TransactionID>{0}</TransactionID>", dx.TransactionID);
            sb.AppendFormat("<ResultCode>{0}</ResultCode>", "0000");
            sb.AppendFormat("<ResultMsg>{0}</ResultMsg>", "成功");
            sb.AppendFormat("<Status>{0}</Status>", 1);
            sb.Append("</Package>");

            return sb.ToString();
        }
    }

    public class DynamicXml : DynamicObject, IEnumerable {
        private readonly List<XElement> _elements;

        public DynamicXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            _elements = new List<XElement> { doc.Root };
        }

        protected DynamicXml(XElement element)
        {
            _elements = new List<XElement> { element };
        }

        protected DynamicXml(IEnumerable<XElement> elements)
        {
            _elements = new List<XElement>(elements);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (binder.Name == "Value")
                result = _elements[0].Value;
            else if (binder.Name == "Count")
                result = _elements.Count;
            else
            {
                var attr = _elements[0].Attribute(
                    XName.Get(binder.Name));
                if (attr != null)
                    result = attr;
                else
                {
                    var items = _elements.Descendants(
                        XName.Get(binder.Name));
                    if (items == null || items.Count() == 0) return false;
                    result = new DynamicXml(items);
                }
            }
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int ndx = (int)indexes[0]; result = new DynamicXml(_elements[ndx]);
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var element in _elements)
                yield return new DynamicXml(element);
        }
    }
}
