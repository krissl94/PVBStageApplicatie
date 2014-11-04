using System;
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
    public class BedrijfController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Bedrijf/
        [Authorize(Roles = "Docent,Beheerder")]
        public ActionResult Index()
        {
            if(HttpContext.User.IsInRole("Docent"))
            {
                var bedrijfResultDocent = db.sp_BedrijfPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                List<Bedrijf> bedrijvenDocent = new List<Bedrijf>();
                foreach (var item in bedrijfResultDocent)
                {
                    bedrijvenDocent.Add(db.Bedrijf.Where(x => x.BedrijfID == item.BedrijfID).FirstOrDefault());
                }
                return View(bedrijvenDocent);
            };

            return View(db.Bedrijf);
        }

        //
        // GET: /Bedrijf/Details/5
        [Authorize(Roles = "Docent,Beheerder")]
        public ActionResult Details(int id = 0)
        {
            Bedrijf bedrijf = db.Bedrijf.Find(id);
            bool bekendBedrijf = false;
            if (HttpContext.User.IsInRole("Docent"))
            {
                var bedrijfResultDocent = db.sp_BedrijfPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                foreach (var item in bedrijfResultDocent)
                {
                    if(item.BedrijfID == id)
                    {
                        bekendBedrijf = true;
                    }
                }
                if (bekendBedrijf)
                    return View(bedrijf);
                else
                    return HttpNotFound();
            }
            
            if (bedrijf == null)
            {
                return HttpNotFound();
            }
            return View(bedrijf);
        }

        //
        // GET: /Bedrijf/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bedrijf/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Bedrijf bedrijf)
        {
            try
            {
                bool bestaatbedrijf;
                if (db.Bedrijf.Where(b => b.Naam == bedrijf.Naam).FirstOrDefault() != null)
                {
                    bestaatbedrijf = true;
                }
                else
                {
                    bestaatbedrijf = false;
                }

                if (bestaatbedrijf == false)
                {
                    bedrijf.Actief = true;
                    if (ModelState.IsValid)
                    {
                        db.Bedrijf.Add(bedrijf);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewData["Foutmelding"] = "Deze bedrijfsnaam staat al in ons systeem";
                    return View();
                }
                return View(bedrijf);
            }
            catch(Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // GET: /Bedrijf/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            Bedrijf bedrijf = db.Bedrijf.Find(id);
            if (bedrijf == null)
            {
                return HttpNotFound();
            }
            return View(bedrijf);
        }

        //
        // POST: /Bedrijf/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Bedrijf bedrijf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bedrijf).State = EntityState.Modified;
                //Zet begeleiders van bedrijf op non-actief
                if(bedrijf.Actief == false)
                {
                    var begeleiders = db.Persoonsgegevens.Where(p => p.Bedrijf1.BedrijfID == bedrijf.BedrijfID);
                    foreach (var item in begeleiders)
                    {
                        item.Actief = false;
                        item.NonActiefReden = "Bedrijf is non-actief gesteld";
                        item.Bedrijf1 = null;
                        db.Entry(item).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(bedrijf);
        }
        

        //
        // GET: /Bedrijf/BulkInvoerBedrijf
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerBedrijf()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet Bedrijven = eh.excelToDS(file, Server);

            if (Bedrijven != null)
            {
                ViewData["feedback"] = eh.dataSetToBedrijf(Bedrijven);
            }
            return View("~/Views/Bedrijf/BulkInvoerBedrijf.cshtml");
        }

        public FileResult download()
        {
            string file = @"~/App_Data/ExcelTemplates/BedrijfInvoegen.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}