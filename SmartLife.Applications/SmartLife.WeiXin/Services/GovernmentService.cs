using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using SmartLife.WeiXin.Pub;
using SmartLife.WeiXin.Model;
using e0571.web.core.Wcf;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.Sys;
using e0571.web.core.Model;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SmartLife.WeiXin.xml;
using System.ServiceModel.Web;
using Newtonsoft.Json.Linq;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using System.Web.Script.Serialization;

namespace SmartLife.WeiXin.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GovernmentService : AppBaseWcfService
    {
        #region 查询政府服务 项目
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<DictionaryItem> GovernmentServiceItem()
        {
            CollectionInvokeResult<DictionaryItem> result = new CollectionInvokeResult<DictionaryItem> { Success = true };
            try
            {
                var dictionary = new { DictionaryId="11013" , WhereClause= "ItemCode like '01%'"}.ToStringObjectDictionary();
                result.rows = BuilderFactory.DefaultBulder().List<DictionaryItem>(dictionary);
            }
            catch (Exception ex)
            {

                Util.WriteTxt("查询政府服务项目:" + ex.ToString());
            }
            return result;
        }
        #endregion 

        #region 判断用户是否验证  是否绑定老人  返回相应的文本的信息
        public string isAuthOrBound(RequestXML requestXML)
        {
            var AppID = Global.TheServiceAccount.AppId;
            var openId = requestXML.FromUserName;//获取openid
            var dictionaryAuth = new { OpenId = openId, Status = 1}.ToStringObjectDictionary();
            //判断用户是否认证
            IList<NormalAccount> normalAccounts = BuilderFactory.DefaultBulder().List<NormalAccount>(dictionaryAuth);
            if (TypeConverter.ChangeString(normalAccounts[0].IDNo)=="" || TypeConverter.ChangeString(normalAccounts[0].Mobile)=="")
            {
                return new ReplyContent().replyContent(requestXML.FromUserName, requestXML.ToUserName, "您还未认证,<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri=http://www.leblue.cn/weixin/views/children-auth.htm&response_type=code&scope=snsapi_base&state=1#wechat_redirect\">点击此处认证</a>");
            }

            //判断用户是否绑定老人  
            var dictionaryBound = new { OpenId = openId }.ToStringObjectDictionary();
            IList<NormalAccountFollow> normalAccountFollows = BuilderFactory.DefaultBulder().List<NormalAccountFollow>(dictionaryBound);
            if (normalAccountFollows.Count == 0) {
                return new ReplyContent().replyContent(requestXML.FromUserName, requestXML.ToUserName, "您还未绑定老人,<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri=http://www.leblue.cn/weixin/views/oldman-bind.htm&response_type=code&scope=snsapi_base&state=1#wechat_redirect\">点击此处绑定老人</a>");  
            }

            //绑定老人 回复 老人 图文信息  ReplyNew(string Title, string Description, string PicUrl, string Url)
            List<ReplyNew> news = new List<ReplyNew>();
            news.Add(new ReplyNew("可查询政府服务记录", "", "", ""));
            foreach(NormalAccountFollow accountFollow in normalAccountFollows){
                news.Add(new ReplyNew(accountFollow.FollowToName, "", Util.baseUrl() + "images/test.jpg", Util.baseUrl() + "views/search-home.htm?openid=" + TypeConverter.ChangeString(normalAccounts[0].OpenId) + "&type=Government&FollowToIDNo=" + accountFollow.FollowToIDNo));
            }
            Util.WriteTxt(Util.baseUrl() + "views/search-home.htm?openid=" + TypeConverter.ChangeString(normalAccounts[0].OpenId) + "&type=Government");
            string str = new ReplyNew().replyNews(requestXML.FromUserName, requestXML.ToUserName, (normalAccountFollows.Count + 1).ToString(), news);
            //string str = new ReplyNew().replyNew(requestXML.FromUserName, requestXML.ToUserName, "这里是一个标题", "这里是摘要", "http://wjweixin.ngrok.com/SmartLife.WeiXin/images/test.jpg", "http://www.4ugood.net");
            return str;
        }
        #endregion

    }
}