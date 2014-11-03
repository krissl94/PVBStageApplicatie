using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;

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
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Docent/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            return View(persoonsgegevens);
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

                        if (ModelState.IsValid)
                        {
                            persoonsgegevens.Login.Wachtwoord = Hashing.HashString(persoonsgegevens.Login.Gebruikersnaam, persoonsgegevens.Login.Wachtwoord);
                            db.Persoonsgegevens.Add(persoonsgegevens.Docent);
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
                persoonsgegevens.Rol = 2;
                bool BestaatEmail = edh.bestaatEmail(persoonsgegevens);
                //Check of email uniek is
                if (BestaatEmail == false)
                {
                    //Check of gebruiker docent is en  rechten heeft
                    if (HttpContext.User.IsInRole("Docent") && persoonsgegevens.PersoonsgegevensID.ToString() == HttpContext.User.Identity.Name)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(persoonsgegevens).State = EntityState.Modified;
                            db.SaveChanges();
                            return View("~/Views/Docent/Index.cshtml", persoonsgegevensList);
                        }
                        return View(persoonsgegevensList);
                    }
                        //Check of beheerder is
                    else if(HttpContext.User.IsInRole("Beheerder"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(persoonsgegevens).State = EntityState.Modified;
                            db.SaveChanges();
                            return View("~/Views/Docent/Index.cshtml", persoonsgegevensList);
                        }
                        return View(persoonsgegevensList);
                    }
                    if (persoonsgegevens == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
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
            if (eh.excelToDS(file, Server) != null)
            {
                DataSet Docenten = eh.excelToDS(file, Server);
                ViewData["feedback"] = eh.dataSetToDocent(Docenten);
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