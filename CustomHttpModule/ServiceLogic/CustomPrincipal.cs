using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CustomHttpModule.ServiceLogic
{
    public class CustomPrincipal:IPrincipal
    {
        private IIdentity identity;
        private string role;

        public CustomPrincipal(IIdentity identity,string role)
        {
            this.identity = identity;
            this.role = role;
        }
        public IIdentity Identity
        {
            get { return identity; }
            set { identity = value; }
        }

        public bool IsInRole(string role)
        {
            return this.role.Equals(role);
        }
    }
}