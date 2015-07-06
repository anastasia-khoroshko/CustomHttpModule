using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomHttpModule.ServiceLogic
{
    public class AuthModule:IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.Authentificate);
        }

        protected void Authentificate(Object source,EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            context.User = UserAuthService.CurrentUser;
        }
    }
}