using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public static class HttpHelper
    {
        #region Http请求
        /// <summary>
        /// get请求（拼接请求参数）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string GetHttpResponse(string url, object paramData = null, int timeout = 60000)
        {
            var param = GetQueryString(paramData);//拼get参数
            if (!string.IsNullOrWhiteSpace(param))
            {
                url += "?" + param;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = timeout;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramData"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T GetHttpResponse<T>(string url, object paramData = null, int timeout = 60000)
        {
            string retString = null;
            try
            {
                if (paramData == null)
                {
                    retString = GetHttpResponse(url, timeout);
                }
                else
                {
                    retString = GetHttpResponse(url, paramData, timeout);
                }
                if (!string.IsNullOrWhiteSpace(retString))
                {
                    return JsonConvert.DeserializeObject<T>(retString);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"d:\jialin.txt", ex.StackTrace + "=====" + ex.Message);
                throw;
            }
        }


        /// <summary>
        /// post方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="PostData">post数据对象</param>
        /// <param name="timeout">超时时间，默认为6000毫秒（1分钟）</param>        
        /// <returns></returns>
        public static string PostHttpResponse(string url, Object PostData, int timeout = 60000)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求 
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.Timeout = timeout;

            //如果需要POST数据     
            string str = JsonConvert.SerializeObject(PostData);
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(str);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var resultStream = (request.GetResponse() as HttpWebResponse);
            string result = "";
            //获取响应内容  
            using (StreamReader reader = new StreamReader(resultStream.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramData"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T PostHttpResponse<T>(string url, object paramData, int timeout = 60000)
        {
            string retString = null;
            try
            {
                retString = PostHttpResponse(url, paramData, timeout);
                if (!string.IsNullOrWhiteSpace(retString))
                {
                    return JsonConvert.DeserializeObject<T>(retString);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"d:\jialin.txt", ex.StackTrace + "=====" + ex.Message);
                throw;
            }
        }
        #endregion


        #region Http辅助方法
        /// <summary>
        /// 拼接请求参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetQueryString(object obj)
        {
            if (obj == null) return string.Empty;
            List<string> query = new List<string>();
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                if (p.CanRead)
                {
                    var pName = p.Name;
                    foreach (Attribute attr in p.GetCustomAttributes(true))
                    {
                        if (attr is Newtonsoft.Json.JsonPropertyAttribute)
                        {
                            if (((Newtonsoft.Json.JsonPropertyAttribute)attr).NullValueHandling.ToString() == "Ignore")
                            {
                                pName = null;
                                break;
                            }
                            if (!string.IsNullOrWhiteSpace(((Newtonsoft.Json.JsonPropertyAttribute)attr).PropertyName))
                            {
                                pName = ((Newtonsoft.Json.JsonPropertyAttribute)attr).PropertyName;
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(pName))
                    {
                        var value = p.GetValue(obj, null);
                        var v = value != null ? value.ToString() : string.Empty;
                        switch (p.PropertyType.Name)
                        {
                            case "String":
                                v = WebUtility.UrlEncode(v);
                                break;
                        }
                        query.Add($@"{pName}={v}");
                    }
                }
            }
            if (query.Count > 0)
            {
                return string.Join("&", query);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

    }
}
