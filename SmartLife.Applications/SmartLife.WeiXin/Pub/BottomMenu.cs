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

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.WeiXin.Pub;
using SmartLife.Share.Models.WeiXin.Pub;
using System.Dynamic;
using Newtonsoft.Json;

namespace SmartLife.CertManage.ManageServices.WeiXin
{
    public class BottomMenu
    {
        public static void createMenu()
        {

            //https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx674ad58e14b195fb&secret=ab0874eec47ff9eeccdc5503509cb20c  通过此链接获得token，替换好内容后直接复制到浏览器打开
            string acc_token = "tfHSrCXjgWlc4icU8AZMMnCcINhhET__btIUtPCvIVN1YhBlw6yrq3SELAYuvY8sY3HMuO89M2AZ-pYys1Kywg5cmkBwxU5PldB0s8ceXgmB0vinvFRhOkZ6_ZD_Ncjw5o03g6Vt4iiDtWppR2vuOQ";//此部分设置access_token


            //string acc_token = GetAccessToken.timmer();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + acc_token;
            //Util.WriteTxt(acc_token);
            //Util.WriteTxt(url);

            String responeJsonStr = "{" + "\"button\":["
                + "{\"name\":\"菜单名称1\"," + "\"sub_button\":["
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单1\"," + "\"key\":\"V1001_HELLO_WORLD\"" + "},"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单2\"," + "\"key\":\"V1001_HELLO_WORLD2\"" + "},"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单3\"," + "\"key\":\"V1001_HELLO_WORL3\"" + "},"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单4\"," + "\"key\":\"V1001_HELLO_WORL4\"" + "}]},"

                + "{\"name\":\"菜单名称2\"," + "\"sub_button\":["
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单1\"," + "\"key\":\"V1001_HELLO_WORL5\"" + "},"
                + "{" + "\"type\":\"view\"," + "\"name\":\"首页\"," + "\"url\":\"http://www.leblue.cn/weixin/views/oldman-bind.htm\"" + "},"
                + "{" + "\"type\":\"view\"," + "\"name\":\"wangjian\"," + "\"url\":\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx674ad58e14b195fb&redirect_uri=http://www.leblue.cn/weixin/views/children-auth.htm&response_type=code&scope=snsapi_base&state=1#wechat_redirect\"" + "},"
                + "{" + "\"type\":\"view\"," + "\"name\":\"绑定\"," + "\"url\":\"http://www.leblue.cn/weixin/views/search.htm\"" + "}]},"

                + "{\"name\":\"菜单名称3\"," + "\"sub_button\":["
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单1\"," + "\"key\":\"V1001_HELLO_WORLD6\"" + " },"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单2\"," + "\"key\":\"V1001_HELLO_WORL7\"" + " },"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单3\"," + "\"key\":\"V1001_HELLO_WORLD8\"" + " },"
                + "{" + "\"type\":\"click\"," + "\"name\":\"小菜单4\"," + "\"key\":\"V1001_HELLO_WORLD9\"" + " }]}]}";

               string a = PostWebRequest(url, responeJsonStr, Encoding.UTF8);
        }

        public static string createMenuStrAsXml()
        {
            GetForCreateRet result = new GetForCreateRet { };

            var all_buttons = BuilderFactory.DefaultBulder().List<WXButton>().ToList();
             

            result.button = all_buttons.Where(item => item.Levels == 1).OrderBy(item => item.OrderNo).ToList().Select(item =>
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

            string jsonStr = JsonConvert.SerializeObject(result);

            return jsonStr;
        }

        

        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }


        public class WXMenu
        {
            public string name { get; set; }
            public IList<dynamic> sub_button { get; set; }
        }

        public class WXSubMenu
        {
            public string type { get; set; }
            public string name { get; set; }
        }

        public class WXSubMenuAsClick : WXSubMenu
        {

            public string key { get; set; }
        }

        public class WXSubMenuAsView : WXSubMenu
        {
            public string url { get; set; }
        }

        public class GetForCreateRet
        {
            public IList<WXMenu> button { get; set; }
        }

    }
}