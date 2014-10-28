using PVB_Stage_Applicatie.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PVB_Stage_Applicatie.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult LogOn(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Gebruikersnaam, model.Wachtwoord))
                {
                    SetupFormsAuthTicket(model.Persoonsgegevens, false); //Rememberme = false
                    // -- Snip --
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("",
                  "The user name or password provided is incorrect.");
            }
            return View(model);
        }

        // -- Snip --

        private Persoonsgegevens SetupFormsAuthTicket(int GebruikerID, bool persistanceFlag)
        {
            Persoonsgegevens persoon;
            Login login;
            using (var usersContext = new StageApplicatieEntities())
            {
                persoon = usersContext.ZoekPersoon(GebruikerID);
                login = usersContext.ZoekPersoon(GebruikerID).Login.FirstOrDefault();
            }
            //var userId =  .UserId;
            var userData = GebruikerID.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                login.Gebruikersnaam, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                                persistanceFlag, //Persistent
                                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return persoon;
        }

    }
}
