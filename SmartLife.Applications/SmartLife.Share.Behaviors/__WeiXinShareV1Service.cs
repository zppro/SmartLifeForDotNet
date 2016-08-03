using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Security;
using System.Text;
using System.IO;
using System.Dynamic;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using win.core.utils;
using Newtonsoft.Json;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.WeiXin.Pub;
using web.core.share.models.WeiXin.Pub;
namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __WeiXinShareV1Service : BaseWcfService
    {
        #region 打印消息
        [WebGet(UriTemplate = "PrintDebugInfo/{type}", BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream PrintDebugInfo(string type)
        {
            string result = GlobalManager.TheWeiXinDebugger.Print(type);
            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return new MemoryStream(resultBytes);
        } 
        #endregion

        #region 认证Token
        [WebGet(UriTemplate = "?signature={signature}&timestamp={timestamp}&nonce={nonce}&echostr={echostr}", BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream CheckSignature(string signature, string timestamp, string nonce, string echostr)
        {
            string ret = "novalid";
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            if (_CheckSignature(signature, timestamp, nonce))
            {
                ret = echostr;
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(ret);
            return new MemoryStream(resultBytes);
        }
        public static bool _CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { GlobalManager.TheServiceAccount.Token, timestamp, nonce };
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
        #endregion

        #region 接收消息 并回复
        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream ReceiveWXMessage(Stream s)
        {
            string result = ""; 
            using (StreamReader sr = new StreamReader(s))
            {
                result = sr.ReadToEnd();
            }
            //result = OperationContext.Current.RequestContext.RequestMessage.ToString();
            if (result != "")
            {
                WXReceiveMessage receiveMessage = XmlSerializeAdapter.Deserialize<WXReceiveMessage>(result);
                /*
                XmlSerializer serializer = new XmlSerializer(typeof(WXReceiveMessage));
                using (StringReader rdr = new StringReader(result))
                {
                    WXReceiveMessage requestMessage = serializer.Deserialize(rdr) as WXReceiveMessage;
                    
                }
                */
                if (receiveMessage.MsgType.Equals("text"))
                {
                    WXReceiveTextMessage requestTextMessage = XmlSerializeAdapter.Deserialize<WXReceiveTextMessage>(result);
                    result = GlobalManager.TheWeiXinResponser.ResponseText(requestTextMessage);
                    if (string.IsNullOrEmpty(result))
                    {
                        result = GlobalManager.TheWeiXinDispatcher.DesideDispatchWho(requestTextMessage);
                        GlobalManager.TheWeiXinDispatcher.DispatchText(requestTextMessage);
                    }
                    
                }
                else if (receiveMessage.MsgType.Equals("event"))
                {
                    WXReceiveEventMessage requestEventMessage = XmlSerializeAdapter.Deserialize<WXReceiveEventMessage>(result);
                    if (requestEventMessage.Event.Equals("subscribe"))
                    {
                        #region 关注
                        NormalAccount normalAccount = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<NormalAccount>(new NormalAccount { OpenId = requestEventMessage.FromUserName, AccountCode = GlobalManager.TheServiceAccount.AccountCode }).FirstOrDefault();
                        if (normalAccount == null)
                        {
                            //第一次关注
                            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Create<NormalAccount>(new NormalAccount { AccountCode = GlobalManager.TheServiceAccount.AccountCode, OpenId = requestEventMessage.FromUserName, Gender = "F", SubscribeTime = DateTime.Now });
                            result = GlobalManager.TheWeiXinResponser.FormatOutputText(requestEventMessage.FromUserName, requestEventMessage.ToUserName, "欢迎加入" + GlobalManager.TheServiceAccount.Name);
                        }
                        else
                        {
                            //曾经关注过
                            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Update<NormalAccount>(new NormalAccount { FollowStatus = 1, SubscribeTime = DateTime.Now, Id = normalAccount.Id });
                            result = GlobalManager.TheWeiXinResponser.FormatOutputText(normalAccount.OpenId, requestEventMessage.ToUserName, "欢迎重新加入" + GlobalManager.TheServiceAccount.Name);
                        }
                         
                        _RefreshWXUserInfo(requestEventMessage.FromUserName, (ret) =>
                        {
                            string gender = "N";
                            int iGender = int.Parse(ret.sex.ToString());
                            if (iGender == 1)
                            {
                                gender = "M";
                            }
                            else if (iGender == 2)
                            {
                                gender = "F";
                            } 
                            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(new IBatisNetBatchStatement
                            {
                                StatementName = "NormalAccount_UpdateByOpenId",
                                ParameterObject = new NormalAccount
                                {
                                    OpenId = ret.openid,
                                    NickName = ret.nickname,
                                    Gender = gender,
                                    Language = ret.language,
                                    Country = ret.country,
                                    Province = ret.province,
                                    City = ret.city,
                                    HeadImgUrl = ret.headimgurl
                                }.ToStringObjectDictionary(false),
                                Type = SqlExecuteType.UPDATE
                            });
                        }, null);
                        #endregion
                    }
                    else if (requestEventMessage.Event.Equals("unsubscribe"))
                    {
                        #region 取消关注
                        NormalAccount normalAccount = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<NormalAccount>(new NormalAccount { OpenId = requestEventMessage.FromUserName, AccountCode = GlobalManager.TheServiceAccount.AccountCode }).FirstOrDefault();
                        if (normalAccount != null)
                        {
                            BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Update<NormalAccount>(new NormalAccount { FollowStatus = 0, SubscribeTime = DateTime.Now, Id = normalAccount.Id });
                        }
                        #endregion
                    }
                    else if (requestEventMessage.Event.Equals("LOCATION"))
                    {
                        WXReceiveLocationMessage requestLocationMessage = XmlSerializeAdapter.Deserialize<WXReceiveLocationMessage>(result);
                        var spParam = new { SourceTable = new NormalAccount().GetMappingTableName(), SourceId = requestLocationMessage.FromUserName, LocateType = "WX-Location-Report", Longitude = requestLocationMessage.Longitude, Latitude = requestLocationMessage.Latitude }.ToSPParam();
                        try
                        {
                            BuilderFactory.DefaultBulder().ExecuteSPNoneQuery("SP_Pub_InsertPubLocation", spParam);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else
                    {
                        result = GlobalManager.TheWeiXinResponser.ResponseEvent(requestEventMessage);
                    }
                }
                else if (receiveMessage.MsgType.Equals("location"))
                {

                }
                else if (receiveMessage.MsgType == "image")
                {

                }
                else if (receiveMessage.MsgType == "voice")
                {

                }
            }
            else
            {
                result = "hello";
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return new MemoryStream(resultBytes);
        }
        #endregion

        #region 发送客服消息 48小时内有效
        [WebInvoke(UriTemplate = "SendWXMessageSchedule", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<string> SendWXMessageSchedule()
        {
            InvokeResult<string> result = new InvokeResult<string>();
            try
            {
                IList<WXSend> messagesToSend = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ListByPage<WXSend>(new WXSend { Status = 0 }.ToStringObjectDictionary(false), new ListPager { PageNo = 1, PageSize = 10, OrderByClause = "ScheduleTime" });
                if (messagesToSend.Count == 0)
                {
                    //无消息需要发送 
                    result.ret = "无消息需要发送";
                }
                else
                {
                    string batchNum = RandomAdapter.GetRandomString(8);
                    List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                    List<string> errMsgs = new List<string>();
                    foreach (WXSend messageToSend in messagesToSend)
                    {
                        string payload;
                        if (messageToSend.SendCatalog.ToLower() == WXRequestMessageType.text.ToString())
                        {
                            payload = new WXRequestTextMessage { touser = messageToSend.toUserName, msgtype = WXRequestMessageType.text.ToString(), text = new WXTextMessageWrapper { content = messageToSend.SendContent.ReplaceEmoji() } }.ToJson();
                        }
                        else if (messageToSend.SendCatalog.ToLower() == WXRequestMessageType.news.ToString())
                        {
                            WXRequestNewsItem newsItem = JsonConvert.DeserializeObject<WXRequestNewsItem>(messageToSend.SendContent.ReplaceEmoji());
                            payload = new WXRequestNewsMessage { touser = messageToSend.toUserName, msgtype = WXRequestMessageType.news.ToString(), news = new WXNewsMessageWrapper { articles = new List<WXRequestNewsItem> { newsItem } } }.ToJson();
                        }
                        else
                        {
                            payload = "";
                        }
                        _SendWXMessage(payload, () =>
                        {
                            statements.Add(new IBatisNetBatchStatement { StatementName = messageToSend.GetUpdateMethodName(), ParameterObject = new { Id = messageToSend.Id, BatchNum = batchNum, SendTime = DateTime.Now, Status = 1, SendResult = 0 }.ToStringObjectDictionary(), Type = SqlExecuteType.UPDATE });
                        }, (errCode, errMsg) =>
                        {
                            errMsgs.Add(errMsg);
                            statements.Add(new IBatisNetBatchStatement { StatementName = messageToSend.GetUpdateMethodName(), ParameterObject = new { Id = messageToSend.Id, BatchNum = batchNum, SendTime = DateTime.Now, Status = 2, SendResult = errCode }.ToStringObjectDictionary(), Type = SqlExecuteType.UPDATE });
                        });
                    }

                    if (statements.Count > 0)
                    {
                        BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(statements);
                    }
                    result.ret = "发送成功->发送批号："+batchNum;
                    if (errMsgs.Count > 0)
                    {
                        result.ret = "发送失败->发送批号：" + batchNum + " 错误:" + string.Join(";", errMsgs.Distinct().ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorCode = 59997;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        [WebInvoke(UriTemplate = "SendWXMessage", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult SendWXMessage(Stream messageStream)
        {
            InvokeResult result = new InvokeResult();
            
            string payload = "";
            using (StreamReader sr = new StreamReader(messageStream))
            {
                payload = sr.ReadToEnd();
            }
            if (payload != "")
            {
                _SendWXMessage(payload, () =>
                {
                    result.Success = true;
                }, (errCode, errMsg) =>
                {
                    result.ErrorCode = errCode;
                    result.ErrorMessage = errMsg;
                });
            }
            else
            {
                result.ErrorCode = 59995;
            }
            return result;
        }
        private void _SendWXMessage(string  payload, Action success, Action<int, string> fail)
        {
            string sendCustomMessageUrl1 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIPOSTKey_Msg_SendCustomMessage], GlobalManager.TheServiceAccount.AccessToken);
            HttpAdapter.postSyncStr(sendCustomMessageUrl1, payload, (ret1, res1) =>
            {
                int errCode1 = int.Parse(ret1.errcode.ToString());
                string errMsg1 = ret1.errmsg.ToString();
                if (errCode1 == 0)
                {
                    if (success != null)
                    {
                        success();
                    }
                }
                else if (errCode1 == 42001 || errCode1 == 41001)
                {
                    if (_RefreshAccessToken())
                    {
                        string sendCustomMessageUrl2 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIPOSTKey_Msg_SendCustomMessage], GlobalManager.TheServiceAccount.AccessToken);
                        HttpAdapter.postSyncStr(sendCustomMessageUrl2,payload, (ret2, res2) =>
                        {
                            int errCode2 = int.Parse(ret2.errcode.ToString());
                            string errMsg2 = ret2.errmsg.ToString();
                            if (errCode2 == 0)
                            {
                                if (success != null)
                                {
                                    success();
                                }
                            }
                            else
                            {
                                if (fail != null)
                                {
                                    fail(errCode2, errMsg2);
                                }
                            }
                        }, (he) =>
                        {
                            if (fail != null)
                            {
                                fail(-99, he.Message);
                            }
                        });
                    }
                    else
                    {
                        if (fail != null)
                        {
                            fail(GlobalManager.ERR_RefreshAccessTokenFaild, "刷新AccessToken失败");
                        }

                    }
                }
                else
                {
                    if (fail != null)
                    {
                        fail(errCode1, errMsg1);
                    }
                }
            }, (he) =>
            {
                if (fail != null)
                {
                    fail(-99, he.Message);
                }
            });
        }
        #endregion

        #region 刷新AccessToken
        [WebInvoke(UriTemplate = "RefreshAccessToken", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult RefreshAccessToken()
        {
            InvokeResult result = new InvokeResult { Success = _RefreshAccessToken() };
            return result;
        }
        private bool _RefreshAccessToken()
        {
            string accessTokenUrl = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_Bas_GetAccessToken], GlobalManager.TheServiceAccount.AppId, GlobalManager.TheServiceAccount.AppSecret);
            HttpAdapter.getSyncTo(accessTokenUrl, (ret, res) =>
            {
                GlobalManager.TheServiceAccount.AccessToken = ret.access_token;
                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).Update<ServiceAccount>(new ServiceAccount { AccountCode = GlobalManager.TheServiceAccount.AccountCode, AccessToken = ret.access_token });
            });
            return true;
        }
        #endregion

        #region 创建菜单
        [WebInvoke(UriTemplate = "CreateWXButtons", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult CreateWXButtons()
        {
            InvokeResult result = new InvokeResult();
            WXButtonsCreateResult createResult = new WXButtonsCreateResult { };

            var all_buttons = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<WXButton>(new WXButton { Status = 1 }).ToList();
            createResult.button = all_buttons.Where(item => item.Levels == 1).OrderBy(item => item.OrderNo).ToList().Select(item =>
            {
                WXMenu x = new WXMenu { name = item.ButtonName };
                x.sub_button = all_buttons.Where(subitem => subitem.ParentId == item.ButtonId).OrderBy(subitem => subitem.OrderNo).Select(subitem =>
                {
                    dynamic y = new ExpandoObject();
                    y.name = subitem.ButtonName;
                    y.type = subitem.Type;

                    if (subitem.Type == "click")
                    {
                        y.key = subitem.KeyOrUrl;
                    }
                    else if (subitem.Type == "view")
                    {
                        y.url = subitem.KeyOrUrl;
                    }
                    return y;
                }).ToList();
                return x;
            }).ToList();

            string jsonStr = JsonConvert.SerializeObject(createResult);
            _CreateWXButtonsToWeiXin(jsonStr,() =>
            {
                result.Success = true;
            }, (errCode, errMsg) =>
            {
                result.ErrorCode = errCode;
                result.ErrorMessage = errMsg;
            });
             
            return result;
        }
        private void _CreateWXButtonsToWeiXin(string payload, Action success, Action<int, string> fail)
        {
            string createCustomMenuUrl1 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIPOSTKey_Cmu_CreateCustomMenu], GlobalManager.TheServiceAccount.AccessToken);
            HttpAdapter.postSyncStr(createCustomMenuUrl1, payload, (ret1, res1) =>
            {
                int errCode1 = int.Parse(ret1.errcode.ToString());
                string errMsg1 = ret1.errmsg.ToString();
                if (errCode1 == 0)
                {
                    if (success != null)
                    {
                        success();
                    }
                }
                else if (errCode1 == 42001 || errCode1 == 41001)
                {
                    if (_RefreshAccessToken())
                    {
                        string createCustomMenuUrl2 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIPOSTKey_Cmu_CreateCustomMenu], GlobalManager.TheServiceAccount.AccessToken);
                        HttpAdapter.postSyncStr(createCustomMenuUrl2, payload, (ret2, res2) =>
                        {
                            int errCode2 = int.Parse(ret2.errcode.ToString());
                            string errMsg2 = ret2.errmsg.ToString();
                            if (errCode2 == 0)
                            {
                                if (success != null)
                                {
                                    success();
                                }
                            }
                            else
                            {
                                if (fail != null)
                                {
                                    fail(errCode2, errMsg2);
                                }
                            }
                        }, (he) =>
                        {
                            if (fail != null)
                            {
                                fail(-99, he.Message);
                            }
                        });
                    }
                    else
                    {
                        if (fail != null)
                        {
                            fail(GlobalManager.ERR_RefreshAccessTokenFaild, "刷新AccessToken失败");
                        }

                    }
                }
                else
                {
                    if (fail != null)
                    {
                        fail(errCode1, errMsg1);
                    }
                }

            }, (he) =>
            {
                if (fail != null)
                {
                    fail(-99, he.Message);
                }
            });
        }
         
        #endregion

        #region 删除菜单
        [WebInvoke(UriTemplate = "DeleteWXButtons", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult DeleteWXButtons()
        {
            InvokeResult result = new InvokeResult();
            _DeleteWXButtonsToWeiXin(() =>
            {
                result.Success = true;
            }, (errCode, errMsg) =>
            {
                result.ErrorCode = errCode;
                result.ErrorMessage = errMsg;
            });
             
            return result;
        }
        private void _DeleteWXButtonsToWeiXin(Action success, Action<int, string> fail)
        {
            string deleteCustomMenuUrl1 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_Cmu_DeleteCustomMenu], GlobalManager.TheServiceAccount.AccessToken);
            HttpAdapter.getSyncTo(deleteCustomMenuUrl1, (ret1, res1) =>
            {
                int errCode1 = int.Parse(ret1.errcode.ToString());
                string errMsg1 = ret1.errmsg.ToString();
                if (errCode1 == 0)
                {
                    if (success != null)
                    {
                        success();
                    }
                }
                else if (errCode1 == 42001 || errCode1 == 41001)
                {
                    if (_RefreshAccessToken())
                    {
                        string deleteCustomMenuUrl2 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_Cmu_DeleteCustomMenu], GlobalManager.TheServiceAccount.AccessToken);
                        HttpAdapter.getSyncTo(deleteCustomMenuUrl2, (ret2, res2) =>
                        {
                            int errCode2 = int.Parse(ret2.errcode.ToString());
                            string errMsg2 = ret2.errmsg.ToString();
                            if (errCode2 == 0)
                            {
                                if (success != null)
                                {
                                    success();
                                }
                            }
                            else
                            {
                                if (fail != null)
                                {
                                    fail(errCode2, errMsg2);
                                }
                            }
                        });
                    }
                    else
                    {
                        if (fail != null)
                        {
                            fail(GlobalManager.ERR_RefreshAccessTokenFaild, "刷新AccessToken失败");
                        }

                    }
                }
                else
                {
                    if (fail != null)
                    {
                        fail(errCode1, errMsg1);
                    }
                }
            });
        }
         
        #endregion

        #region 刷新用户信息
        [WebInvoke(UriTemplate = "RefreshWXUserInfo/{openId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult RefreshWXUserInfo(string openId)
        {
            InvokeResult result = new InvokeResult();
            _RefreshWXUserInfo(openId, (ret) =>
            {
                BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).ExecuteNativeSqlNoneQuery(new IBatisNetBatchStatement { StatementName = "NormalAccount_UpdateByOpenId", ParameterObject = new NormalAccount { OpenId = openId, IDNo = "" } });
                result.Success = true;
            }, (errCode1, errMsg1) => {
                result.ErrorCode = errCode1;
                result.ErrorMessage = errMsg1;
            });
            return result;
        }

        //针对变化的格式
        private void _RefreshWXUserInfo(string openId, Action<dynamic> success, Action<int, string> fail)
        {
            string getInfoOfNormalAccountUrl1 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_Meb_GetInfoOfNormalAccount], GlobalManager.TheServiceAccount.AccessToken, openId);
            HttpAdapter.getSyncTo(getInfoOfNormalAccountUrl1, (ret1, res1) =>
            {
                if (ret1.errcode == null)
                {
                    if (success != null)
                    {
                        success(ret1);
                    }
                }
                else
                {
                    int errCode = int.Parse(ret1.errcode.ToString());
                    string errMsg = ret1.errmsg.ToString();
                    if (errCode == 42001 || errCode == 41001)
                    {
                        //AccessToken过期或缺失
                        if (_RefreshAccessToken())
                        {
                            string getInfoOfNormalAccountUrl2 = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_Meb_GetInfoOfNormalAccount], GlobalManager.TheServiceAccount.AccessToken, openId);
                            HttpAdapter.getSyncTo(getInfoOfNormalAccountUrl2, (ret2, res2) =>
                            {
                                if (ret2.errcode == null)
                                {
                                    if (success != null)
                                    {
                                        success(ret2);
                                    }
                                }
                            });
                        }
                        else
                        {
                            if (fail != null)
                            {
                                fail(GlobalManager.ERR_RefreshAccessTokenFaild, "刷新AccessToken失败");
                            }

                        }
                    }
                    else
                    {
                        if (fail != null)
                        {
                            fail(errCode, errMsg);
                        }
                    }
                }
               
            });
        }
        #endregion

        #region 获取微信用户信息
        [WebGet(UriTemplate = "GetWXUserInfo/{openId}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<NormalAccount> GetWXUserInfo(string openId)
        {
            InvokeResult<NormalAccount> result = new InvokeResult<NormalAccount> { Success = true };
            NormalAccount normalAccount = _GetWXUserInfo(openId);
            if (normalAccount != null)
            {
                result.ret = normalAccount;
            }
            return result;
        }
        /**跨域调用**/
        [WebGet(UriTemplate = "GetWXUserInfoAsJSONP/{openId}?jsoncallback={callback}", BodyStyle = WebMessageBodyStyle.Bare)]
        public Stream GetWXUserInfoAsJSONP(string openId, string callback)
        {
            InvokeResult<NormalAccount> result = new InvokeResult<NormalAccount> { Success = true };
            NormalAccount normalAccount = _GetWXUserInfo(openId);
            if (normalAccount != null)
            {
                result.ret = normalAccount;
            }
            byte[] resultBytes = Encoding.UTF8.GetBytes(callback + "(" + result.ToJson() + ")");
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }

        private NormalAccount _GetWXUserInfo(string openId)
        {
            return BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<NormalAccount>(new NormalAccount { OpenId = openId, AccountCode = GlobalManager.TheServiceAccount.AccountCode }).FirstOrDefault();
        }
        #endregion

        #region 获取网页授权OpenId
        [WebGet(UriTemplate = "GetOpenIdByOAuth2/{code}", ResponseFormat = WebMessageFormat.Json)]
        public InvokeResult<string> GetOpenIdByOAuth2(string code)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            string accessTokenUrl = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_OAuth2_GetAccessToken], GlobalManager.TheServiceAccount.AppId, GlobalManager.TheServiceAccount.AppSecret, code);
            GlobalManager.inputLogger.Info("GetOpenIdByOAuth2 invoke:" + accessTokenUrl);
            HttpAdapter.getSyncTo(accessTokenUrl, (ret, res) =>
            {
                GlobalManager.inputLogger.Info("GetOpenIdByOAuth2 result: in " );
                result.ret = JsonConvert.DeserializeObject(ret).openid.ToString();
            });
            GlobalManager.inputLogger.Info("GetOpenIdByOAuth2 result:" + result.ret);
            return result;
        }
        /**跨域调用**/
        [WebGet(UriTemplate = "GetOpenIdByOAuth2AsJSONP/{code}?jsoncallback={callback}", ResponseFormat = WebMessageFormat.Json)]
        public Stream GetOpenIdByOAuth2AsJSONP(string code, string callback)
        {
            InvokeResult<string> result = new InvokeResult<string> { Success = true };
            string accessTokenUrl = string.Format(GlobalManager.TheWeiXinAPIOfV1[GlobalManager.APIGETKey_OAuth2_GetAccessToken], GlobalManager.TheServiceAccount.AppId, GlobalManager.TheServiceAccount.AppSecret, code);
            HttpAdapter.getSyncTo(accessTokenUrl, (ret, res) =>
            { 
                result.ret = JsonConvert.DeserializeObject(ret).openid.ToString();
            });
            byte[] resultBytes = Encoding.UTF8.GetBytes(callback + "(" + result.ToJson() + ")");
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(resultBytes);
        }
        #endregion

    }

    public interface IWeiXinDebugger
    {
        string Print(string printType);
    }

    public interface IWeiXinResponseMessage
    {
        string FormatOutputText(string toUserName, string fromUserName, string content);

        string FormatOutputNews(string toUserName, string fromUserName, List<WXResponseNewsItem> newsItems);

        string ResponseText(WXReceiveTextMessage receiveMessage);

        string ResponseEvent(WXReceiveEventMessage receiveMessage);
    }

    public interface IWeiXinRequestMessage
    {
    }

    public interface IWeiXinDispatchMessage
    {
        string DesideDispatchWho(WXReceiveTextMessage receiveMessage);
        void DispatchText(WXReceiveTextMessage receiveMessage);
    }
}
