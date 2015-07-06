using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace CustomHttpModule.ServiceLogic.Handlers
{
    /// <summary>
    /// Summary description for LogOffHandler
    /// </summary>
    public class LogOffHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Cookies["userName"] != null)
            {
                string cookieValue = String.Empty;
                HttpCookie cookie = new HttpCookie("userName", cookieValue);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddHours(-1);
                context.Response.Cookies.Remove("userName");
                context.Response.Cookies.Add(cookie);
                UserAuthService.LogOff();
            }
            context.Response.StatusCode=200;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}