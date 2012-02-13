using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;

namespace Fusion.Core
{
    public static class Helpers
    {
        public static string MD5(string value)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToLower();
        }

        public static string ToQueryString(NameValueCollection nameValueCollection)
        {
            // Don't return question mark because our Uri Builder already creates it
            return string.Join("&", Array.ConvertAll(nameValueCollection.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nameValueCollection[key]))));
        }
    }
}