﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.IO;
using System.Data.OleDb;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;

namespace PVB_Stage_Applicatie.Controllers
{
    public class StagiairController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Stagiair/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            try
            {
                if (HttpContext.User.IsInRole("Docent"))
                {
                    var studentResultDocent = db.sp_StagiairPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                    List<Persoonsgegevens> studentenDocent = new List<Persoonsgegevens>();
                    foreach (var item in studentResultDocent)
                    {
                        studentenDocent.Add(db.Persoonsgegevens.Where(x => x.PersoonsgegevensID == item.PersoonsgegevensID).FirstOrDefault());
                    }
                    return View(studentenDocent);
                };
                var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 4);
                return View(persoonsgegevens.OrderBy(p => p.StudentNummer).ToList());
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // GET: /Stagiair/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            try
            {
                Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
                bool bekendStudent = false;
                if (HttpContext.User.IsInRole("Docent"))
                {
                    var studentResultDocent = db.sp_StagiairPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                    foreach (var item in studentResultDocent)
                    {
                        if (item.PersoonsgegevensID == id)
                        {
                            bekendStudent = true;
                        }
                    }
                    if (bekendStudent)
                        return View(persoonsgegevens);
                    else
                        return HttpNotFound();
                }
                if (persoonsgegevens == null)
                {
                    return HttpNotFound();
                }
                return View(persoonsgegevens);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // GET: /Stagiair/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // POST: /Stagiair/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            try
            {
                bool BestaatStudentNr;
                bool BestaatEmail;
                if(db.Persoonsgegevens.Where(p=>p.Email == persoonsgegevens.Email).FirstOrDefault() != null)
                {
                    BestaatEmail = true;
                }
                else
                {
                    BestaatEmail = false;
                }

                if (db.Persoonsgegevens.Where(p => p.StudentNummer == persoonsgegevens.StudentNummer).FirstOrDefault() != null)
                {
                    BestaatStudentNr = true;
                }
                else
                {
                    BestaatStudentNr = false;
                }

                if (BestaatEmail == false && BestaatStudentNr == false)
                {
                    persoonsgegevens.Rol = 4;
                    persoonsgegevens.Actief = true;
                    ModelState.Remove("Bedrijf");
                    ModelState.Remove("MedewerkerID");
                    if (ModelState.IsValid)
                    {
                        db.sp_PersoonToevoegen(4, persoonsgegevens.Voornaam,
                        persoonsgegevens.Achternaam, persoonsgegevens.Tussenvoegsel, persoonsgegevens.Email, 
                        persoonsgegevens.Straat, persoonsgegevens.Huisnummer, persoonsgegevens.Toevoeging, persoonsgegevens.Postcode
                        , persoonsgegevens.Plaats, null, null, persoonsgegevens.StudentNummer, persoonsgegevens.Opleiding, persoonsgegevens.Opleidingsniveau, null);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
                    return View(persoonsgegevens);
                }
                else if(BestaatStudentNr == true)
                {
                    ViewData["Foutmelding"] = "Studentnummer staat al in ons systeem";
                    return View();
                }
                else 
                {
                    ViewData["Foutmelding"] = "Email adres staat al in ons systeem";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
 
        }

        //
        // GET: /Stagiair/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            try
            {
                Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
                if (persoonsgegevens == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
                return View(persoonsgegevens);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // POST: /Stagiair/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            try
            {
                EmailDuplicaatHelper edh = new EmailDuplicaatHelper();
                bool BestaatEmail = edh.bestaatEmail(persoonsgegevens);
                if (BestaatEmail == false)
                {
                    ModelState.Remove("Bedrijf");
                    ModelState.Remove("MedewerkerID");
                    if (ModelState.IsValid)
                    {
                        db.sp_PersoonUpdaten(persoonsgegevens.PersoonsgegevensID,
                            persoonsgegevens.Email, persoonsgegevens.Straat,
                            persoonsgegevens.Huisnummer, persoonsgegevens.Toevoeging,
                            persoonsgegevens.Postcode, persoonsgegevens.Plaats, persoonsgegevens.Opleiding,
                            persoonsgegevens.Actief, persoonsgegevens.NonActiefReden, null);
                        return RedirectToAction("Index");
                    }
                    ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
                    return View("~/Views/Stagiair");
                }
                else 
                {
                    ViewData["Foutmelding"] = "Email adres staat al in ons systeem";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Bedrijf/BulkImport
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerStagiair()
        {
            return View();
        }

        [Authorize(Roles = "Beheerder")]
        public ViewResult BulkNonActiefStagiair()
        {
            return View();
        }

        public FileResult downloadStagiair()
        {
            string file = @"~/App_Data/ExcelTemplates/StagiairInvoegen.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        public FileResult downloadNonActief()
        {
            string file = @"~/App_Data/ExcelTemplates/NonActief.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        public ViewResult BulkNonActief(HttpPostedFileBase file)
        {
            try
            {
                ExcelHelper eh = new ExcelHelper();
                DataSet studentDs = eh.excelToDS(file, Server);
                if (studentDs != null)
                {
                    ViewData["FeedbackNonActief"] = eh.dataSetToNonActiefStudent(studentDs);
                }

                return View("~/Views/Stagiair/BulkInvoerStagiair.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            try
            {
                ExcelHelper eh = new ExcelHelper();
                DataSet studentDs = eh.excelToDS(file, Server);

                if (studentDs != null)
                {
                    List<Persoonsgegevens> lijstje = eh.dataSetToStudent(studentDs);
                    foreach (Persoonsgegevens item in lijstje)
                    {
                        bool BestaatStudentNr;
                        bool BestaatEmail;
                        if (db.Persoonsgegevens.Where(p => p.Email == item.Email).FirstOrDefault() != null)
                        {
                            BestaatEmail = true;
                        }
                        else
                        {
                            BestaatEmail = false;
                        }

                        if (db.Persoonsgegevens.Where(p => p.StudentNummer == item.StudentNummer).FirstOrDefault() != null)
                        {
                            BestaatStudentNr = true;
                        }
                        else
                        {
                            BestaatStudentNr = false;
                        }
                        ModelState.Remove("Bedrijf");
                        ModelState.Remove("MedewerkerID");
                        if (ModelState.IsValid && BestaatEmail == false && BestaatStudentNr == false)
                        {
                            db.sp_PersoonToevoegen(4, item.Voornaam,
                            item.Achternaam, item.Tussenvoegsel, item.Email,
                            item.Straat, item.Huisnummer, item.Toevoeging, item.Postcode
                            , item.Plaats, null, null, item.StudentNummer, null, item.Opleidingsniveau, null);
                            ViewData["FeedbackInvoer"] = "Alle studenten zijn toegevoegd";

                        }
                        else
                        {
                            ViewData["FeedbackInvoer"] = "Er zit verkeerde data in het bestand";
                        }
                    }
                }
                return View("~/Views/Stagiair/BulkInvoerStagiair.cshtml");
            }
            catch
            {
                ViewData["FeedbackInvoer"] = "Er zit verkeerde data in het bestand";
                return View("~/Views/Stagiair/BulkInvoerStagiair.cshtml");
            }
        }

    }
}
