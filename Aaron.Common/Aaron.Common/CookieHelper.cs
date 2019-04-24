using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aaron.Common
{
    public class CookieHelper
    {
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            if (expires == null)
            {
                expires = DateTime.Now.AddDays(7);
            }
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName">cookiename</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearAllCookie()
        {
            string cookieName;
            int cookieCount = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < cookieCount; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;
                var delCookie = new HttpCookie(cookieName);
                delCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(delCookie);
            }
        }
    }

}
