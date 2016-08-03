using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.WeiXin.Model;
using System.Net;
using System.Text;
using SmartLife.WeiXin.Pub;
using Newtonsoft.Json.Linq;


namespace SmartLife.WeiXin.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BoundAcconutService : AppBaseWcfService
    {
        #region 普通列表 List   测试待改
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<NormalAccount> List()
        {
           CollectionInvokeResult<NormalAccount> result = new CollectionInvokeResult<NormalAccount> { Success = true };
           StringObjectDictionary filters = new StringObjectDictionary();
           filters["OpenId"] = "oWygrt_VgMWR-S7IERWEVOzCIskM";
            try
            {
                var dictionary = new StringObjectDictionary().MixInJson("{'OpenId':'oWygrt_VgMWR-S7IERWEVOzCIskM'}");
                result.rows = BuilderFactory.DefaultBulder().List<NormalAccount>(dictionary);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                Util.WriteTxt("认证用户查询："+ex.ToString());
            }
            return result;
        }
        #endregion

        #region 用户是否通过验证  
        [WebGet(UriTemplate = "isAuth?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public CollectionInvokeResult<NormalAccount> isAuth(string strParms)
        {      
            JObject _jObject = JObject.Parse(strParms);
            var code = _jObject["code"].ToString();
            var state = _jObject["state"].ToString();
            string openid = getOpenId(code);  //第一时间得到 openid
           // Util.WriteTxt("isAuth openid," + openid);
            CollectionInvokeResult<NormalAccount> result = new CollectionInvokeResult<NormalAccount> { Success = true };
            var dictionary = new StringObjectDictionary().MixInJson("{'OpenId':'" + openid + "'}");
                
            result.rows = BuilderFactory.DefaultBulder().List<NormalAccount>(dictionary);

            result.ErrorMessage = openid;

            return result;
        }

        #endregion

        #region  更新认证数据
        [WebGet(UriTemplate = "GetToken?params={strParms}", ResponseFormat = WebMessageFormat.Json)]
        public string GetToken(string strParms)
        {
            string result = "";
            try
            {
                JObject _jObject = JObject.Parse(strParms);
                var openid = _jObject["openid"].ToString();
                var state = _jObject["state"].ToString();
                var IDNo = _jObject["IDNo"].ToString();
                var Mobile = _jObject["Mobile"].ToString();

                //Util.WriteTxt(openid);
                string access_token = GlobalToken.AccessToken;//定时器 每隔2小时 得到一个 新access_token
                string strUser = Util.GetJson("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + openid);//获取基本信息
                Util.WriteTxt("User:" + strUser);
                OAuthUser user = JsonHelper.ParseFromJson<OAuthUser>(strUser);
                user.openid = openid;
                
                if (user.sex == "0")
                {
                    user.sex = "N";
                }
                else
                {
                    user.sex = (user.sex == "1") ? "M" : "F";
                }
                insertNormalAccount( user, IDNo, Mobile);
               // Util.WriteTxt("1:" + Mobile + ",:IDNo" + IDNo);
            }
            catch (Exception ex)
            {
                Util.WriteTxt(ex.Message);
                result="false";
            }
            return "true";
        }
        #endregion

        #region
        protected void insertNormalAccount(OAuthUser user, string IDNo, string Mobile)
        {
            
            //ModelInvokeResult<NormalAccountPK> result = new ModelInvokeResult<NormalAccountPK> { Success = true };
            NormalAccount normalAccount = new NormalAccount();
            try
            {
                List<IBatisNetBatchStatement> statements = new List<IBatisNetBatchStatement>();
                normalAccount.AccountCode = user.openid + "$" + user.nickname;
                normalAccount.IDNo = IDNo;
                normalAccount.Mobile = Mobile;
                normalAccount.CheckInTime = DateTime.Now;
                normalAccount.NickName = user.nickname;
                normalAccount.Gender = user.sex;
                normalAccount.Language = user.language;
                normalAccount.Country = user.country;
                normalAccount.Province = user.province;
                normalAccount.City = user.city;
                normalAccount.HeadImgUrl = user.headimgurl;
                //更新条件
                var parameterObject = normalAccount.ToStringObjectDictionary(false);
                parameterObject["OpenId"] = user.openid;


                var dictionary = new StringObjectDictionary().MixInJson("{'OpenId':'" + user.openid + "'}");
                int count = BuilderFactory.DefaultBulder().List<NormalAccount>(dictionary).Count;
                Util.WriteTxt("count:"+ count);
                if (count == 0)
                {
                    normalAccount.OpenId = user.openid;
                    Util.WriteTxt("插入1111");
                    statements.Add(new IBatisNetBatchStatement { StatementName = normalAccount.GetCreateMethodName(), ParameterObject = normalAccount.ToStringObjectDictionary(false), Type = SqlExecuteType.INSERT });
                }
                else
                {
                    Util.WriteTxt("更新1111");
                    //Util.WriteTxt("更新" + normalAccount.NickName + "  :  " + normalAccount.Language  + "      openid:     " + user.openid);
                    statements.Add(new IBatisNetBatchStatement { StatementName = "NormalAccount_Update2", ParameterObject = parameterObject, Type = SqlExecuteType.UPDATE });
                }
                BuilderFactory.DefaultBulder().ExecuteNativeSqlNoneQuery(statements);
            }
            catch (Exception ex)
            {
                Util.WriteTxt(ex.ToString());
            }
            //Util.WriteTxt("更新完毕");
        }
        #endregion




        //刷新Token  
        protected AccessToken refresh_token(string Appid, string REFRESH_TOKEN)
        {
            string Str = Util.GetJson("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" + Appid + "&grant_type=refresh_token&refresh_token=" + REFRESH_TOKEN);
            AccessToken Oauth_Token_Model = JsonHelper.ParseFromJson<AccessToken>(Str);
            return Oauth_Token_Model;
        }

        //刷新Token  
        protected string refresh_tokenJson(string Appid, string REFRESH_TOKEN)
        {
            string Str = Util.GetJson("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" + Appid + "&grant_type=refresh_token&refresh_token=" + REFRESH_TOKEN);
            return Str;
        }  

        //获得用户信息  
        protected OAuthUser Get_UserInfo(string REFRESH_TOKEN, string OPENID)
        {
            // Response.Write("获得用户信息REFRESH_TOKEN:" + REFRESH_TOKEN + "||OPENID:" + OPENID);  
            string Str = Util.GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);
            OAuthUser Model = JsonHelper.ParseFromJson<OAuthUser>(Str);
            return Model;
        }

        //获得用户信息  
        protected string Get_UserInfoJson(string REFRESH_TOKEN, string OPENID)
        {
            // Response.Write("获得用户信息REFRESH_TOKEN:" + REFRESH_TOKEN + "||OPENID:" + OPENID);  
            string Str = Util.GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);
            return Str;
        }
        //获得用户的openid
        public string getOpenId(string code) 
        {
            var appid = Global.TheServiceAccount.AppId;
            var secret = Global.TheServiceAccount.AppSecret;
            string str = Util.GetJson("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code");//获取用户基本信息的openid
            
            OAuthUser oAuthUser = JsonHelper.ParseFromJson<OAuthUser>(str);
            return oAuthUser.openid;
        }
    }
}