using CustomHttpModule.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CustomHttpModule.ServiceLogic.Handlers
{
    /// <summary>
    /// Summary description for LoginHandler1
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userEmail = context.Request.Params["email"];
            var userPassword = context.Request.Params["password"];
            using (var dbContext = new EntityModel())
            {
                User user = dbContext.Set<User>().Where(u => u.Email == userEmail).FirstOrDefault();
                if (user != null && Crypto.VerifyHashedPassword(user.Password, userPassword))
                {
                    UserAuthService.Authentificate(userEmail);
                    context.Response.StatusCode = 200;
                }
                else context.Response.StatusCode=400;
            }
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