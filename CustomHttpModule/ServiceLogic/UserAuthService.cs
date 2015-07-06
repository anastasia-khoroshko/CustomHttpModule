using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace CustomHttpModule.ServiceLogic
{
    public static class UserAuthService
    {
        private static CustomPrincipal _principal;
        private static IIdentity _currentUser;
        private const string cookieName = "userName";
        private const string role = "User";

        public static void Authentificate(string userName)
        {
            if (userName != null && !string.IsNullOrEmpty(userName))
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookieName, userName));
                HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddHours(1);
            }
        }

        public static void LogOff()
        {
            _principal=null;
            Thread.CurrentPrincipal = _principal;
        }
        public static IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser== null||_principal==null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = authCookie.Value;
                            _currentUser = new CustomIdentity(ticket, role);
                            _principal = new CustomPrincipal(_currentUser, role);
                        }
                        else
                        {
                            _currentUser = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        _currentUser = null;
                    }
                   
                    Thread.CurrentPrincipal = _principal;
                }

                return _principal;
            }
        }
    }
}