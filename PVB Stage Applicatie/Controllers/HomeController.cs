using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PVB_Stage_Applicatie.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            MenuSelector();

            return View();
        }
        public ActionResult MenuSelector()
        {
            RolePrincipal rp = (RolePrincipal)User;
            var rol = rp.GetRoles();
            return View("/Menu/AdminMenuPartial.cshtml");
        }


    }
}
