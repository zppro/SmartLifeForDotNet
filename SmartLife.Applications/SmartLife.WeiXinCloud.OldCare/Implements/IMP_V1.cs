using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;
using win.core.utils;
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Concurrent; 
using SmartLife.Share.Behaviors;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.WeiXin.Pub;
using SmartLife.Share.Models.Oca;
using web.core.share.models.WeiXin.Pub;

namespace SmartLife.WeiXinCloud.OldCare.Implements
{
    public class IMP_V1 : IWeiXinDebugger, IWeiXinResponseMessage, IWeiXinRequestMessage, IWeiXinDispatchMessage
    {
        #region 实现 IWeiXinDebugger

        #region 打印消息
        public string Print(string printType)
        {
            StringBuilder sb = new StringBuilder();
            if (printType == "监控信息")
            {
                #region 监控信息
                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　座席登录分机处理的微信客户端数量");
                sb.AppendLine("|");
                foreach (string key in Global.theExtNoProcessingNormalUserCount.Keys)
                {
                    sb.AppendLine("|　　　分机号：" + key + "　　微信客户端数量:" + Global.theExtNoProcessingNormalUserCount[key].ToString());
                }
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");



                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　微信客户端用户映射处理分机");
                sb.AppendLine("|");
                foreach (string key in Global.theWXClientMappingExtNos.Keys)
                {
                    ConcurrentQueue<WXReceiveMessage> queue = Global.theMessagesFromWeiXin[key];
                    int messageCount = 0;
                    if (queue != null)
                    {
                        messageCount = queue.Count;
                    }
                    sb.AppendLine("|　　　客户端微信OpenId：" + key + "　　分机号:" + Global.theWXClientMappingExtNos[key] + "　　消息数:" + messageCount.ToString());
                }
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");
                #endregion
            }
            else if (printType == "ResponseEvent_WX10101")
            {
                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　ResponseEvent_WX10101");
                sb.AppendLine("|");
                sb.AppendLine("|　　　结果：" + ResponseEvent_WX10101(new WXReceiveEventMessage { FromUserName = "oWygrt4kjeXLwCdPKFSxzgnLZwrs", ToUserName = "zj-leblue" }));
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");
            }
            else if (printType == "ResponseEvent_WX20101")
            {
                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　ResponseEvent_WX20101");
                sb.AppendLine("|");
                sb.AppendLine("|　　　结果：" + ResponseEvent_WX20101(new WXReceiveEventMessage { FromUserName = "oWygrt3V6WeAjfrJf3l0At9L-rK4", ToUserName = "zj-leblue" }));
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");
            }
            else if (printType == "ResponseEvent_WX20102")
            {
                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　ResponseEvent_WX20102");
                sb.AppendLine("|");
                sb.AppendLine("|　　　结果：" + ResponseEvent_WX20102(new WXReceiveEventMessage { FromUserName = "oWygrt3V6WeAjfrJf3l0At9L-rK4", ToUserName = "zj-leblue" }));
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");
            }
            else if (printType == "ResponseEvent_WX20103")
            {
                sb.AppendLine("----------------------------------------------------------------------------------------");
                sb.AppendLine("|");
                sb.AppendLine("|　　　　ResponseEvent_WX20103");
                sb.AppendLine("|");
                sb.AppendLine("|　　　结果：" + ResponseEvent_WX20103(new WXReceiveEventMessage { FromUserName = "oWygrt3V6WeAjfrJf3l0At9L-rK4", ToUserName = "zj-leblue" }));
                sb.AppendLine("|");
                sb.AppendLine("----------------------------------------------------------------------------------------");
            }
            return sb.ToString();
        }
        #endregion

        #endregion

        #region 实现 IWeiXinResponseMessage
        #region 输出消息
        public string FormatOutputText(string toUserName, string fromUserName, string content)
        {
            return XmlSerializeAdapter.Serialize<WXResponseTextMessage>(new WXResponseTextMessage { ToUserName = toUserName, FromUserName = fromUserName, CreateTime = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now), MsgType = "text", Content = content.Replace(@"\\", @"\") }, true);
        }
        public string FormatOutputNews(string toUserName, string fromUserName, List<WXResponseNewsItem> newsItems)
        {
            return XmlSerializeAdapter.Serialize<WXResponseNewsMessage>(new WXResponseNewsMessage { ToUserName = toUserName, FromUserName = fromUserName, CreateTime = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now), MsgType = "news", ArticleCount = newsItems.Count, Articles = newsItems }, true);
        }
        #endregion

        #region 响应消息

        #region 文本消息
        public string ResponseText(WXReceiveTextMessage receiveMessage)
        {
            string result = "";
            if (receiveMessage.Content.StartsWith("103/002/"))
            {
                string[] arrFields = receiveMessage.Content.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrFields.Length == 12)
                {
                    string lat = arrFields[3];//当前位置纬度
                    string log = arrFields[2];//当前位置经度
                    string mapAPIKey = "00001";
                    if (!string.IsNullOrEmpty(GlobalManager.TheServiceAccount.MapAPI))
                    {
                        if (GlobalManager.TheMapAPIOfInverseAdressParse.ContainsKey(GlobalManager.TheServiceAccount.MapAPI))
                        {
                            mapAPIKey = GlobalManager.TheServiceAccount.MapAPI;
                        }
                    }
                    string antiGeocodingUrl = GlobalManager.TheMapAPIOfInverseAdressParse[mapAPIKey].ReplaceWithKeys(new { lat = lat, log = log }.ToStringObjectDictionary());


                    HttpAdapter.apiJ(antiGeocodingUrl, (ret, res) =>
                    {
                        if (ret.status == 0)
                        {
                            string detailAddress = ret.result.address;
                            if (mapAPIKey == "00002")
                            {
                                detailAddress = string.IsNullOrEmpty(ret.result.street_number) ? ret.result.formatted_address + "附近" : ret.result.formatted_address;
                            }
                            result = XmlSerializeAdapter.Serialize<WXResponseTextMessage>(new WXResponseTextMessage { ToUserName = receiveMessage.FromUserName, FromUserName = receiveMessage.ToUserName, CreateTime = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now), MsgType = "text", Content = detailAddress }, true);
                            //result = string.Format("<xml><ToUserName>{0}</ToUserName><FromUserName>{1}</FromUserName><CreateTime>{2}</CreateTime><MsgType>text</MsgType><Content>{3}</Content></xml>", requestMessage.FromUserName, requestMessage.ToUserName, DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now), detailAddress);
                        }
                    });
                }
            }
            //else
            //{
            //    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, "/:coffee");
            //}
            return result;
        }
        #endregion
         
        #region 事件推送消息
        public string ResponseEvent(WXReceiveEventMessage receiveMessage)
        {
            string result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, "响应:" + receiveMessage.Event);
            if (receiveMessage.Event.ToLower() == "click")
            {
                if (receiveMessage.EventKey == "WX10101")
                {
                    result = ResponseEvent_WX10101(receiveMessage);
                }
                else if (receiveMessage.EventKey == "WX20101")
                {
                    result = ResponseEvent_WX20101(receiveMessage);
                }
                else if (receiveMessage.EventKey == "WX20102")
                {
                    result = ResponseEvent_WX20102(receiveMessage);  
                }
                else if (receiveMessage.EventKey == "WX20103")
                {
                    result = ResponseEvent_WX20103(receiveMessage);
                }
            }

            return result;
        }

        #region ResponseEvent_WX10101 签到
        private string ResponseEvent_WX10101(WXReceiveEventMessage receiveMessage)
        {
            string result = "";
            var stationIds = BuilderFactory.DefaultBulder().List<WXStationAuthRequest>(new WXStationAuthRequest { OpenId = receiveMessage.FromUserName, DoStatus = 1 }.ToStringObjectDictionary(false)).Select(item => item.StationId.ToString()).Distinct();
            if (stationIds.Count() > 0)
            {
                result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "签到成功");
                //加入到签到表
                BuilderFactory.DefaultBulder().Create<WXCheckIn>(new WXCheckIn { OpenId = receiveMessage.FromUserName });
            }
            else
            {
                WXMessageTemplate template = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_Not_As_ServeMan });
                if (template != null)
                {
                    string s =  template.TemplateContent.ReplaceEmoji();
                    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, s);
                }
            }
            return result;
        }
        #endregion

        #region ResponseEvent_WX20101 预约查询
        private string ResponseEvent_WX20101(WXReceiveEventMessage receiveMessage)
        {
            string result = "";
            int pageSize = 5;
            StringObjectDictionary filter = new { OpenId = receiveMessage.FromUserName }.ToStringObjectDictionary();
            try
            {
                var datas = BuilderFactory.DefaultBulder().ListStringObjectDictionaryByPage("ScheduleByServeMan_ListByPage", filter, new ListPager { OrderByClause = "ReserveDate desc", PageNo = 1, PageSize = pageSize });
                if (datas.Count == 0)
                {
                    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, "没有任何预约信息");
                }
                else
                {
                    WXMessageTemplate template_MyReserveInfoTitle = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyReserveInfoTitle });

                    List<WXResponseNewsItem> items = datas.Select(item =>
                        new WXResponseNewsItem
                        {
                            Title = template_MyReserveInfoTitle.TemplateContent.ReplaceWithKeys(new
                            {
                                ReserveDate = DateTime.Parse(TypeConverter.ChangeString(item["ReserveDate"])).ToString("yyyy-MM-dd"),
                                ReserveFrom = TypeConverter.ChangeString(item["ReserveFrom"]),
                                ReserveTo = TypeConverter.ChangeString(item["ReserveTo"]),
                                OldManName = TypeConverter.ChangeString(item["OldManName"]),
                                ServeItemBName = TypeConverter.ChangeString(item["ServeItemBName"])
                            }.ToStringObjectDictionary(), false, "$")
                        }).ToList();

                    if (BuilderFactory.DefaultBulder().GetRecordCount("Count_ScheduleByServeMan_ListByPage", filter) > pageSize)
                    {
                        WXMessageTemplate template_MyReserveInfoMoreUrl = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyReserveInfoMoreUrl });
                        items.Add(new WXResponseNewsItem
                        {
                            Title = "更多...",
                            Url = template_MyReserveInfoMoreUrl.TemplateContent.ReplaceWithKeys(new
                            {
                                DeployAreaCode = GlobalManager.GetDeployArea().code,
                                OpenId = receiveMessage.FromUserName,
                                TimeStamp = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now).ToString()
                            }.ToStringObjectDictionary(), false, "$")
                        });
                    }
                    items.Insert(0, new WXResponseNewsItem { Title = "预约查询结果：" });

                    result = FormatOutputNews(receiveMessage.FromUserName, receiveMessage.ToUserName, items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion

        #region ResponseEvent_WX20102 服务记录查询
        private string ResponseEvent_WX20102(WXReceiveEventMessage receiveMessage)
        {
            string result = "";
            int pageSize = 5;
            StringObjectDictionary filter = new { OpenId = receiveMessage.FromUserName }.ToStringObjectDictionary();
            try
            {
                var datas = BuilderFactory.DefaultBulder().ListStringObjectDictionaryByPage("ServeRecordByServeMan_ListByPage", filter, new ListPager { OrderByClause = "ServeManArriveTime desc", PageNo = 1, PageSize = pageSize });
                if (datas.Count == 0)
                {
                    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, "没有任何服务记录");
                }
                else
                {
                    WXMessageTemplate template_MyServeRecordTitle = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyServeRecordTitle });
                     
                    List<WXResponseNewsItem> items = datas.Select(item =>
                        new WXResponseNewsItem
                        {
                            Title = template_MyServeRecordTitle.TemplateContent.ReplaceWithKeys(new
                            {
                                ServeDate = DateTime.Parse(TypeConverter.ChangeString(item["ServeDate"])).ToString("yyyy-MM-dd"),
                                ArriveTime = DateTime.Parse(TypeConverter.ChangeString(item["ArriveTime"])).ToString("HH:mm:ss"),
                                LeaveTime =  DateTime.Parse(TypeConverter.ChangeString(item["LeaveTime"])).ToString("HH:mm:ss"),
                                OldManName = TypeConverter.ChangeString(item["OldManName"]),
                                ServeItemBName = TypeConverter.ChangeString(item["ServeItemBName"]),
                                ServeStationName = TypeConverter.ChangeString(item["ServeStationName"])
                            }.ToStringObjectDictionary(), false, "$").ReplaceEmoji()
                        }).ToList();

                    if (BuilderFactory.DefaultBulder().GetRecordCount("Count_ScheduleByServeMan_ListByPage", filter) > pageSize)
                    {
                        WXMessageTemplate template_MyServeRecordMoreUrl = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyServeRecordMoreUrl });
                        items.Add(new WXResponseNewsItem
                        {
                            Title = "更多...",
                            Url = template_MyServeRecordMoreUrl.TemplateContent.ReplaceWithKeys(new
                            {
                                DeployAreaCode = GlobalManager.GetDeployArea().code,
                                OpenId = receiveMessage.FromUserName,
                                TimeStamp = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now).ToString()
                            }.ToStringObjectDictionary(), false, "$")
                        });
                    }
                    items.Insert(0, new WXResponseNewsItem { Title = "服务记录查询结果：" });

                    result = FormatOutputNews(receiveMessage.FromUserName, receiveMessage.ToUserName, items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion

        #region ResponseEvent_WX20103 服务时长统计
        private string ResponseEvent_WX20103(WXReceiveEventMessage receiveMessage)
        {
            string result = "";
            int pageSize = 5;
            StringObjectDictionary filter = new { OpenId = receiveMessage.FromUserName }.ToStringObjectDictionary();
            try
            {
                var datas = BuilderFactory.DefaultBulder().ListStringObjectDictionary("ServeDurationByServeMan_List", filter);
                if (datas.Count == 0)
                {
                    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, "没有任何服务记录,无法统计");
                }
                else
                {
                    WXMessageTemplate template_MyServeDurationStatTitle = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyServeDurationStatTitle });

                    List<WXResponseNewsItem> items = datas.Select(item =>
                        new WXResponseNewsItem
                        {
                            Title = template_MyServeDurationStatTitle.TemplateContent.ReplaceWithKeys(new
                            { 
                                ServeStationName = TypeConverter.ChangeString(item["ServeStationName"]),
                                ServeItemBName = TypeConverter.ChangeString(item["ServeItemBName"]),
                                ByDay = DateTimeAdapter.GetTimeSpanString(DateTimeAdapter.DateInterval.HR, long.Parse(TypeConverter.ChangeString(item["ByDay"]))),
                                ByWeek = DateTimeAdapter.GetTimeSpanString(DateTimeAdapter.DateInterval.HR, long.Parse(TypeConverter.ChangeString(item["ByWeek"]))),
                                ByMonth = DateTimeAdapter.GetTimeSpanString(DateTimeAdapter.DateInterval.HR, long.Parse(TypeConverter.ChangeString(item["ByMonth"])))
                            }.ToStringObjectDictionary(), false, "$")
                        }).ToList();

                    if (datas.Count > pageSize)
                    {
                        WXMessageTemplate template_MyServeDurationStatMoreUrl = BuilderFactory.DefaultBulder().Load<WXMessageTemplate, WXMessageTemplatePK>(new WXMessageTemplatePK { TemplateId = GlobalManager.TemplateId_MyServeDurationStatMoreUrl });
                        items.Add(new WXResponseNewsItem
                        {
                            Title = "更多...",
                            Url = template_MyServeDurationStatMoreUrl.TemplateContent.ReplaceWithKeys(new
                            {
                                DeployAreaCode = GlobalManager.GetDeployArea().code,
                                OpenId = receiveMessage.FromUserName,
                                TimeStamp = DateTimeAdapter.ConvertDateTimeUnix(DateTime.Now).ToString()
                            }.ToStringObjectDictionary(), false, "$")
                        });
                    }
                    items.Insert(0, new WXResponseNewsItem { Title = "服务时长统计结果：" });

                    result = FormatOutputNews(receiveMessage.FromUserName, receiveMessage.ToUserName, items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region 实现 IWeiXinRequestMessage
        #endregion

        #region 实现 IWeiXinDispatchMessage
        public string DesideDispatchWho(WXReceiveTextMessage receiveMessage)
        {
            string result = "";
            string extNo;
            if (!Global.theWXClientMappingExtNos.TryGetValue(receiveMessage.FromUserName, out extNo))
            {
                //不存在任何映射将当前OpenId和最空闲的座席映射起来
                if (Global.theExtNoProcessingNormalUserCount.Count > 0)
                {
                    extNo = Global.theExtNoProcessingNormalUserCount.OrderBy(item => item.Value).ToList().First().Key;
                    Global.theWXClientMappingExtNos.TryAdd(receiveMessage.FromUserName, extNo);
                    Global.theExtNoProcessingNormalUserCount[extNo] = Global.theWXClientMappingExtNos.Where(item => item.Value == extNo).Count();

                    result = FormatOutputText(receiveMessage.FromUserName, receiveMessage.ToUserName, extNo + "为您服务");
                }
            }
            return result;
        }
        public void DispatchText(WXReceiveTextMessage receiveMessage)
        {
            ConcurrentQueue<WXReceiveMessage> queueOfMessages;
            if (!Global.theMessagesFromWeiXin.TryGetValue(receiveMessage.FromUserName, out queueOfMessages))
            {
                queueOfMessages = new ConcurrentQueue<WXReceiveMessage>();
                Global.theMessagesFromWeiXin.TryAdd(receiveMessage.FromUserName, queueOfMessages);
            }
            queueOfMessages.Enqueue(receiveMessage);
        }
        #endregion

    }
}