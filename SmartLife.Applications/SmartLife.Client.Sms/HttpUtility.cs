using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SmartLife.Client.Sms
{
    public delegate void HttpCallback(dynamic result, HttpWebResponse response);

    public class HttpUtility
    {
        /// <summary>
        /// 异步验证请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="headers">请求头</param>
        /// <param name="callback">异步委托</param>
        public static void optionsAsyncTo(string url,Encoding encoding ,IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(true, "OPTIONS", null, encoding, url, null, headers, callback);
        }

        /// <summary>
        /// 同步验证请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="headers">请求头</param>
        /// <param name="callback">同步委托</param>
        public static void optionsSyncTo(string url, Encoding encoding, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(false, "OPTIONS", null, encoding, url, null, headers, callback);
        }

        /// <summary>
        /// get 异步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">异步执行委托</param>
        public static void getAsyncTo(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(true, "GET", null, encoding, url, parms, headers, callback);
        }

        /// <summary>
        /// get 同步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">同步执行委托</param>
        public static void getSyncTo(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(false, "GET", null, encoding, url, parms, headers, callback);
        }

        /// <summary>
        /// post表单 异步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">异步执行委托</param>
        public static void postAsyncAsForm(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(true, "POST", "application/x-www-form-urlencoded", encoding, url, parms, headers, callback);
        }

        /// <summary>
        /// post json参数 异步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">异步执行委托</param>
        public static void postAsyncAsJSON(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(true, "POST", "application/json", encoding, url, parms, headers, callback);
        }


        /// <summary>
        /// post表单 同步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">同步执行委托</param>
        public static void postSyncAsForm(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(false, "POST", "application/x-www-form-urlencoded", encoding, url, parms, headers, callback);
        }

        /// <summary>
        /// post json参数 同步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="parms">请求参数</param>
        /// <param name="headers">请求头部参数</param>
        /// <param name="callback">同步执行委托</param>
        public static void postSyncAsJSON(string url, Encoding encoding, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            httpTo(false, "POST", "application/json", encoding, url, parms, headers, callback);
        }



        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="async">异步开启关闭</param>
        /// <param name="method">请求方法</param>
        /// <param name="contentType"></param>
        /// <param name="url">地址</param>
        /// <param name="parms">参数</param>
        /// <param name="headers">请求头</param>
        /// <param name="callback">异步执行方法</param>
        public static void httpTo(bool async, string method, string contentType, Encoding encoding, string url, IDictionary<string, object> parms, IDictionary<string, object> headers, HttpCallback callback)
        {
            string str;
            Func<string, string> selector = null;
            Func<string, string> func2 = null;
            if ((method.ToUpper() == "GET") && ((parms != null) && (parms.Count > 0)))
            {
                if (selector == null)
                {
                    selector = item => (item + "=" + parms[item]) ?? "";
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
            //System.Net.ServicePointManager.DefaultConnectionLimit = 10;
            //垃圾回收
            System.GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            //request.Proxy = null;   //代理设为空
            request.KeepAlive = false;  //不保持连接
            if ((headers != null) && (headers.Count > 0))
            {
                foreach (string str2 in headers.Keys)
                {
                    request.Headers.Add(str2, Convert.ToString(headers[str2]));
                }
            }
            //编码方式
            Encoding enCode = Encoding.UTF8;
            if (encoding != null) enCode = Encoding.Default;

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
                            func2 = item => (item + "=" + parms[item]) ?? "";
                        }
                        str = string.Join("&", parms.Keys.Select<string, string>(func2));
                        bytes = enCode.GetBytes(str);
                    }
                    else if (contentType.ToLower() == "application/json")
                    {
                        bytes = enCode.GetBytes(JsonConvert.SerializeObject(parms));
                    }

                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        request.ContentLength = bytes.Length;
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Close();
                    }
                }
            }

            //执行请求
            ExecHttpTo(async, enCode, request, callback);
            
        }

        private static void ExecHttpTo(bool async, Encoding encoding, HttpWebRequest request, HttpCallback callback)
        {
            dynamic ret;
            AsyncCallback callback2 = null;
            if (async)
            {
                if (callback2 == null)
                {
                    callback2 = delegate(IAsyncResult item)
                    {
                        try
                        {
                            using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(item))
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                                    {
                                        string req = reader.ReadToEnd();
                                        try
                                        {
                                            ret = JsonConvert.DeserializeObject(req) ?? req;
                                        }
                                        catch
                                        {
                                            ret = req;
                                        }
                                        callback(ret, response);
                                    }
                                }
                                else
                                {
                                    callback("请求错误：" + response.StatusCode.ToString(), response);
                                }
                            }
                            if (request != null)
                            {
                                request.Abort();
                            }
                        }
                        catch (Exception ex)
                        {
                            BaseUtility.LogError.Error(request.RequestUri + ":" + ex.ToString());
                        }
                    };
                }
                request.BeginGetResponse(callback2, null);
            }
            else
            {
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                            {
                                string req = reader.ReadToEnd();
                                try
                                {
                                    ret = JsonConvert.DeserializeObject(req) ?? req;
                                }
                                catch
                                {
                                    ret = req;
                                }

                                callback(ret, response);
                            }
                        }
                        else
                        {
                            callback("请求错误：" + response.StatusCode.ToString(), response);
                        }
                    }
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
                catch (Exception ex)
                {
                    BaseUtility.LogError.Error(request.RequestUri + ":" + ex.ToString());
                }
            }
        }
    }
}
