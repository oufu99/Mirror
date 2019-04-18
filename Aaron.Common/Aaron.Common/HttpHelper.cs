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
    public class HttpHelper
    {
        public static string GetHttpResponse(string url, int timeout = 60000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = timeout;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            return retString;

        }

        /// <summary>
        /// 使用键值对传值  可以兼容List<T>的情况
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string PostHttpResponse(string url, IDictionary<string, string> parameters, int timeout = 60000)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求 
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.Timeout = timeout;

            //如果需要POST数据     
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.GetEncoding("utf-8").GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
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
        /// Post请求 传入json对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PostData"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string PostHttpResponse(string url, object PostData, int timeout = 60000)
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
    }
}
