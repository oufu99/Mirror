using System;
using System.Collections.Generic;
using System.Text;

namespace RestTemplateCore
{
    /// <summary>
    /// 直接反序列化返回的结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestResponseWithBody<T> : RestResponse
    {
        /// <summary>
        /// 响应报文体json反序列化的内容
        /// </summary>
        public T Body { get; set; }
    }
}
