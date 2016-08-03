using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Reflection;

using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Behaviors; 
using SmartLife.Share.Models.WeiXin.Meb;
using SmartLife.Share.Models.WeiXin.Pub;

using e0571.web.core.TypeInherited;
using web.core.share.models.WeiXin.Pub;

namespace SmartLife.WeiXinCloud.OldCare
{
    public class Global : HttpApplication
    {
        //座席登录分机处理的微信客户端数量
        public static ConcurrentDictionary<string, int> theExtNoProcessingNormalUserCount = new ConcurrentDictionary<string, int>();
        //微信客户端用户映射处理分机字典，以微信客户端的OpenId为键，存储座席登录分机
        public static ConcurrentDictionary<string, string> theWXClientMappingExtNos = new ConcurrentDictionary<string, string>();
        
        //微信接收消息字典，以微信客户端的OpenId为键，消息按队列存储
        public static ConcurrentDictionary<string, ConcurrentQueue<WXReceiveMessage>> theMessagesFromWeiXin = new ConcurrentDictionary<string, ConcurrentQueue<WXReceiveMessage>>();
        
        

        public static ServiceAccount TheServiceAccount;
        public static StringStringDictionary TheMapAPIOfInverseAdressParse = new StringStringDictionary();

        void Application_Start(object sender, EventArgs e)
        {
            string accountCode = "";
            StreamReader srForSettings = new StreamReader(HttpContext.Current.Server.MapPath("~/Settings.ini"));
            string[] lines0 = srForSettings.ReadToEnd().Split('\n');
            srForSettings.Close();
            foreach (var line in lines0)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (keyValues[0] == "AccountCode")
                    {
                        accountCode = keyValues[1].Replace("\r", "");
                    }
                }
            }

            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            GlobalManager.InitIndependent();
            CacheManager.Init();
            Assembly assem = Assembly.GetExecutingAssembly();

            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream(assem.GetName().Name + ".ErrorCode.txt"));
            string[] lines = srForErrorMessage.ReadToEnd().Split('\n');
            srForErrorMessage.Close();
            foreach (var line in lines)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (!GlobalManager.errorCodeMessages.ContainsKey(keyValues[0]))
                    {
                        GlobalManager.errorCodeMessages.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                    }
                }
            }
            Implements.IMP_V1 host = new Implements.IMP_V1();
            GlobalManager.InitWeiXin(accountCode, host, host, host, host);

        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("api/share/v1", new WebServiceHostFactory(), typeof(__WeiXinShareV1Service)));
            RouteTable.Routes.Add(new ServiceRoute("api/v1", new WebServiceHostFactory(), typeof(Services.v1API)));
        }
    }
}
