using CustomHttpModule.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CustomHttpModule.ServiceLogic.Handlers
{
    /// <summary>
    /// Summary description for RegisterHandler
    /// </summary>
    public class RegisterHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userEmail = context.Request.Params["email"];
            var userPassword = context.Request.Params["password"];
            try
            {
                using (var dbContext = new EntityModel())
                {
                    User user = dbContext.Set<User>().Where(u => u.Email == userEmail).FirstOrDefault();
                    if (user == null)
                    {
                        dbContext.Set<User>().Add(new User()
                        {
                            Email = userEmail,
                            Password = Crypto.HashPassword(userPassword)
                        });
                        dbContext.SaveChanges();
                        context.Response.StatusCode=200;
                    }
                    else context.Response.StatusCode=400;
                }
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 400;
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