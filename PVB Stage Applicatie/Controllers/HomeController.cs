using PVB_Stage_Applicatie.Models;
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

        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
