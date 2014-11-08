﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.IO;

namespace PVB_Stage_Applicatie.Controllers
{
    public class DocentController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Docent/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            if (HttpContext.User.IsInRole("Docent"))
            {
                return RedirectToAction("Details/" + HttpContext.User.Identity.Name);
            };
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 2);
            return View(persoonsgegevens.ToList().OrderBy(x=>x.MedewerkerID));
        }



        public FileResult download()
        {
            string file = @"~/App_Data/ExcelTemplates/DocentInvoegen.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        //
        // GET: /Docent/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            Login login = db.Login.Where(l => l.Persoonsgegevens == id).FirstOrDefault();
            CreateLoginViewModel clvm = new CreateLoginViewModel { Docent = persoonsgegevens, Login = login };
            ViewData["Gebruikersnaam"] = login.Gebruikersnaam;
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            return View(clvm);
        }

        //
        // GET: /Docent/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Docent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(CreateLoginViewModel persoonsgegevens)
        {
            try
            {
                persoonsgegevens.Login.Gebruikersnaam = persoonsgegevens.Login.Gebruikersnaam.ToLower();

                if (db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Docent.Email).FirstOrDefault() == null)
                {
                    if (db.Login.Where(l => l.Gebruikersnaam == persoonsgegevens.Login.Gebruikersnaam).FirstOrDefault() == null)
                    {
                        persoonsgegevens.Docent.Rol = 2;
                        persoonsgegevens.Docent.Actief = true;
                        persoonsgegevens.Login.Persoonsgegevens = persoonsgegevens.Docent.PersoonsgegevensID;
                        ModelState.Remove("Docent.StudentNummer");
                        ModelState.Remove("Docent.Opleiding");
                        ModelState.Remove("Docent.Opleidingsniveau");
                        ModelState.Remove("Docent.Bedrijf");
                        if (ModelState.IsValid)
                        {
                            persoonsgegevens.Login.Wachtwoord = Hashing.HashString(persoonsgegevens.Login.Gebruikersnaam, persoonsgegevens.Login.Wachtwoord);
                             db.sp_PersoonToevoegen(2, persoonsgegevens.Docent.Voornaam,
                            persoonsgegevens.Docent.Achternaam, persoonsgegevens.Docent.Tussenvoegsel, persoonsgegevens.Docent.Email,
                            persoonsgegevens.Docent.Straat, persoonsgegevens.Docent.Huisnummer, persoonsgegevens.Docent.Toevoeging, persoonsgegevens.Docent.Postcode
                            , persoonsgegevens.Docent.Plaats, null, persoonsgegevens.Docent.MedewerkerID, null, null, null, null);

                             persoonsgegevens.Login.Persoonsgegevens = db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Docent.Email).FirstOrDefault().PersoonsgegevensID;
                            db.Login.Add(persoonsgegevens.Login);
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }

                        return View(persoonsgegevens);
                    }
                    else
                    {
                        ViewData["Foutmelding"] = "Deze gebruikers naam is reeds gebruikt";
                        return View();
                    }
                }
                else
                {
                    ViewData["Foutmelding"] = "Dit email-adres is reeds gebruikt";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // GET: /Docent/Edit/5
        [Authorize(Roles = "Beheerder, Docent")]
        public ActionResult Edit(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            return View(persoonsgegevens);
        }

        //
        // POST: /Docent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder, Docent")]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            var persoonsgegevensList = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 2);

            EmailDuplicaatHelper edh = new EmailDuplicaatHelper();
            try
            {
                if (!edh.bestaatEmail(persoonsgegevens))
                {
                    ModelState.Remove("StudentNummer");
                    ModelState.Remove("Opleiding");
                    ModelState.Remove("Opleidingsniveau");
                    ModelState.Remove("Bedrijf");

                    if (ModelState.IsValid)
                    {
                        db.sp_PersoonUpdaten(
                            persoonsgegevens.PersoonsgegevensID,
                            persoonsgegevens.Email,
                            persoonsgegevens.Straat,
                            persoonsgegevens.Huisnummer,
                            persoonsgegevens.Toevoeging,
                            persoonsgegevens.Postcode,
                            persoonsgegevens.Plaats,
                            persoonsgegevens.Actief,
                            persoonsgegevens.NonActiefReden);

                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    return View(persoonsgegevens);
                }
                else
                {
                    ViewData["Foutmelding"] = "Email adres staat al in ons systeem";
                    return View(persoonsgegevens);
                }
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View(persoonsgegevens);
            }
        }

        //
        // GET: /Docent/BulkInvoerDocent
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerDocent()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet DocentDs = eh.excelToDS(file, Server);

            if (DocentDs != null)
            {
                List<CreateLoginViewModel> lijstje = eh.dataSetToDocent(DocentDs);

                foreach (CreateLoginViewModel persoonsgegevens in lijstje)
                {
                    if (db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Docent.Email).FirstOrDefault() == null)
                    {
                        persoonsgegevens.Docent.Rol = 2;
                        persoonsgegevens.Docent.Actief = true;
                        persoonsgegevens.Login.Persoonsgegevens = persoonsgegevens.Docent.PersoonsgegevensID;
                        ModelState.Remove("Docent.StudentNummer");
                        ModelState.Remove("Docent.Opleiding");
                        ModelState.Remove("Docent.Opleidingsniveau");
                        ModelState.Remove("Docent.Bedrijf");
                        if (ModelState.IsValid)
                        {
                           db.sp_PersoonToevoegen(2, persoonsgegevens.Docent.Voornaam,
                           persoonsgegevens.Docent.Achternaam, persoonsgegevens.Docent.Tussenvoegsel, persoonsgegevens.Docent.Email,
                           persoonsgegevens.Docent.Straat, persoonsgegevens.Docent.Huisnummer, persoonsgegevens.Docent.Toevoeging, persoonsgegevens.Docent.Postcode
                           , persoonsgegevens.Docent.Plaats, null, persoonsgegevens.Docent.MedewerkerID, null, null, null, null);


                           db.Login.Add(new Login
                           {
                               Gebruikersnaam = persoonsgegevens.Login.Gebruikersnaam,
                               Wachtwoord = Hashing.HashString(persoonsgegevens.Login.Gebruikersnaam, persoonsgegevens.Login.Wachtwoord),
                               Persoonsgegevens = db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Docent.Email).FirstOrDefault().PersoonsgegevensID
                           });
                           db.SaveChanges();
                        }
                    }
                }
                List<Persoonsgegevens> lijst = eh.dataSetToStudent(DocentDs);
                ViewData["feedback"] = "Alle docenten zijn toegevoegd";
            }
            return View("~/Views/Docent/BulkInvoerDocent.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}