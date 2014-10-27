using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace PVB_Stage_Applicatie
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object send, EventArgs a)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] rol = { authTicket.UserData };

                GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), rol);

                Context.User = userPrincipal;

                if (!Roles.RoleExists(rol[0]))
                {
                    Roles.CreateRole(rol[0]);
                }

                if (Roles.GetRolesForUser(Context.User.Identity.Name).Count() == 0)
                { 
                    Roles.AddUserToRole(Context.User.Identity.Name, "1");
                }
            }
        }
    }
}