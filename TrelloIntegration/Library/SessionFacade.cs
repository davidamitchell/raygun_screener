using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrelloIntegration.Library
{
    public class SessionFacade
    {
        private const string AuthTokenKey = "AuthToken";
        public static void Store(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static string Get(string key)
        {
            return (string)HttpContext.Current.Session[key];
        }

        public static void StoreAuthToken(string authToken)
        {
            Store(AuthTokenKey, authToken);
        }

        public static string GetAuthToken()
        {
            return Get(AuthTokenKey);
        }


    }
}