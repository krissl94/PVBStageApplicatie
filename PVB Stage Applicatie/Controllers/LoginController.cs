﻿using PVB_Stage_Applicatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

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
        public ActionResult Index(LoginForm inLogForm)
        {
            string wachtwoord = Hashing.HashString(inLogForm.Gebruikersnaam, inLogForm.Wachtwoord);

            var id = db.sp_Login(inLogForm.Gebruikersnaam, wachtwoord).ToList();

            if (id.Count == 1)
            {
                Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id[0]);

                if (persoonsgegevens == null)
                {
                    return HttpNotFound();
                }

                Session.Add("persoonsgegevens", persoonsgegevens);

                Session.Add("Rol", persoonsgegevens.Rol);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, persoonsgegevens.PersoonsgegevensID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, persoonsgegevens.Rol.ToString(), "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

                Response.Cookies.Add(cookie);
            }
           
            return View(inLogForm);
        }

        [Authorize(Roles=Rollen.Beheerder)]
        public ActionResult LoggedIn()
        {
            return View();
        }
    }
}
