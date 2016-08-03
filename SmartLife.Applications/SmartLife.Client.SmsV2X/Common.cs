using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

using System.Web;
using System.IO;
using System.Net;

using log4net;
using Newtonsoft.Json;
using win.core.utils;

namespace SmartLife.Client.SmsV2X
{
    public class Common
    {
        //记录日志
        public static readonly ILog LogError = LogManager.GetLogger("Sms");

        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string INI_KEY_AUTHSMS_END_POINT = "AuthSmsEndPoint";
        public const string INI_KEY_SMS_END_POINT = "SmsEndPoint";
        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_TXT_SMS_SETTING = "TxtSmsSetting";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public const string ERROR_USER_CANCEL = "用户放弃操作";



        /// <summary>
        /// 分割字符转成键值对
        /// </summary>
        /// <param name="strings">被分割字符</param>
        /// <param name="separators">分割符号</param>
        /// <returns></returns>
        public static Dictionary<string, string> StrToDictionary(string strings, char separators)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(strings)) return retdictionary;

            foreach (var item in strings.Split(separators))
            {
                if (string.IsNullOrEmpty(item)) continue;
                int mark = item.IndexOf("=");
                retdictionary.Add(item.Substring(0, mark), item.Substring(mark + 1, item.Length - mark - 1));
            }

            return retdictionary;
        }

        //获取枚举的文本值和描述
        public static Dictionary<string, string> GetEnumDesc(Enum EnumType)
        {
            Dictionary<string, string> smsErrorDic = new Dictionary<string, string>();
            if (EnumType == null) LogError.Error(new ArgumentNullException("EnumType"));
            if (!EnumType.GetType().IsEnum) LogError.Error("参数类型不正确");

            FieldInfo[] fieldinfo = EnumType.GetType().GetFields();
            foreach (FieldInfo item in fieldinfo)
            {
                if (item.IsSpecialName) continue;
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (obj != null && obj.Length > 0)
                {
                    DescriptionAttribute da = (DescriptionAttribute)obj[0];
                    smsErrorDic.Add(item.GetRawConstantValue().ToString(), da.Description);
                }
            }

            if (smsErrorDic == null || smsErrorDic.Count <= 0) return new Dictionary<string, string>();

            return smsErrorDic;
        }

        //
        public static System.Collections.ArrayList StrSplitToArray(string content)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();

            string[] arr = content.Split('/');
            if (arr.Length > 0)
            {
                list.AddRange(arr);
            }
            return list;
        }

        public static void getSyncTo(string url, HttpCallbackSuccess success)
        {
            getSyncTo(url, null, null, success);
        }

        public static void getSyncTo(string url, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallbackSuccess success)
        {
            httpTo(false, "GET", null, url, parms, headers, success, null);
        }

        public static void postSyncAsForm(string url, IDictionary<string, object> parms, HttpCallbackSuccess success)
        {
            postSyncAsForm(url, parms, null, success);
        }

        public static void postSyncAsForm(string url, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallbackSuccess success)
        {
            httpTo(false, "POST", "application/x-www-form-urlencoded", url, parms, headers, success, null);
        }

        public static void httpTo(bool async, string method, string contentType, string url, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallbackSuccess success, HttpCallbackFail fail)
        {
            string str;
            Exception exception;
            Func<string, string> selector = null;
            Func<string, string> func2 = null;
            AsyncCallback callback = null;
            if (ServicePointManager.DefaultConnectionLimit < 200)
            {
                ServicePointManager.DefaultConnectionLimit = 200;
            }
            if ((method.ToUpper() == "GET") && ((parms != null) && (parms.Count > 0)))
            {
                if (selector == null)
                {
                    selector = item => HttpUtility.UrlEncode((item + "=" + parms[item]) ?? "");
                }
                str = string.Join("&", parms.Keys.Select<string, string>(selector));
                if (url.IndexOf("?") != -1)
                {
                    url = url + "?" + str;
                }
                else
                {
                    url = url + "&" + str;
                }
            }
            GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.KeepAlive = false;
            request.Timeout = 0x7530;
            if ((headers != null) && (headers.Count > 0))
            {
                foreach (string str2 in headers.Keys)
                {
                    request.Headers.Add(str2, Convert.ToString(headers[str2]));
                }
            }
            if (method.ToUpper() == "POST")
            {
                request.ContentType = contentType;
                if ((parms != null) && (parms.Count > 0))
                {
                    byte[] bytes = null;
                    if (contentType.ToLower() == "application/x-www-form-urlencoded")
                    {
                        if (func2 == null)
                        {
                            func2 = item => ((item + "=" + parms[item]) ?? "");
                        }
                        str = string.Join("&", parms.Keys.Select<string, string>(func2));
                        bytes = Encoding.UTF8.GetBytes(str);
                    }
                    else if (contentType.ToLower() == "application/json")
                    {
                        bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parms));
                    }
                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Close();
                    }
                }
            }
            if (async)
            {
                try
                {
                    if (callback == null)
                    {
                        callback = delegate(IAsyncResult item)
                        {
                            using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(item))
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                    {
                                        if (response.ContentType.Contains("application/json"))
                                        {
                                            success(JsonConvert.DeserializeObject(reader.ReadToEnd()), response);
                                        }
                                        else
                                        {
                                            success(reader.ReadToEnd(), response);
                                        }
                                    }
                                }
                                else if (fail != null)
                                {
                                    fail(new HttpException((int)response.StatusCode, "请求错误"));
                                }
                            }
                        };
                    }
                    request.BeginGetResponse(callback, null);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    if (fail != null)
                    {
                        fail(new HttpException(exception.Message, exception));
                    }
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }
            else
            {
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                if (response.ContentType.Contains("application/json"))
                                {
                                    success(JsonConvert.DeserializeObject(reader.ReadToEnd()), response);
                                }
                                else
                                {
                                    success(reader.ReadToEnd(), response);
                                }
                            }
                        }
                        else if (fail != null)
                        {
                            fail(new HttpException((int)response.StatusCode, "请求错误"));
                        }
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    if (fail != null)
                    {
                        fail(new HttpException(exception.Message, exception));
                    }
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }
        }
    }


    public class SendSmsEndPoint {
        public string ApplicationId { get; set; }
        public string Url {get;set;}
        public string Token {get;set;}
        public string NodeId { get; set; }
    }
}
