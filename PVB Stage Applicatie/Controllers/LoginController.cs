using PVB_Stage_Applicatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVB_Stage_Applicatie.Controllers
{
    public class LoginController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginForm InLogForm)
        {
            var persoonsgegevens = db.sp_Login(InLogForm.Gebruikersnaam, InLogForm.Wachtwoord).ToList();

            if (persoonsgegevens.Count == 1)
            {
                return View("Jawel, ingelogd. Heuj");
            }


            return View(InLogForm);
        }

    }
}
