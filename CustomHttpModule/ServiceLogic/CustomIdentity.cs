using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CustomHttpModule.ServiceLogic
{
    public class CustomIdentity:IIdentity
    {
        private string email;
        private string roles;
        private bool isLogin;
 
        public CustomIdentity(string email, string roles)
        {
            this.email = email;
            this.roles = roles;
            this.isLogin = true;
        }

        public CustomIdentity()
        {
            this.email = string.Empty;
            this.roles = string.Empty;
            this.isLogin = false;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return isLogin; }
            set { isLogin = value; }
        }

        public string Name
        {
            get { return email; }
            set { email = value; }
        }

        public string Role
        {
            get { return roles; }
        }
    }
}