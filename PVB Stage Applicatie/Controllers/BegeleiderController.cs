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
    public class BegeleiderController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Begeleider/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 3);
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Begeleider/Details/5
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
        // GET: /Begeleider/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
            return View();
        }

        //
        // POST: /Begeleider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
            try
            {
                bool BestaatEmail;
                if (db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Email).FirstOrDefault() != null)
                {
                    BestaatEmail = true;
                }
                else
                {
                    BestaatEmail = false;
                }

                if (BestaatEmail == false)
                {
                    persoonsgegevens.Rol = 3;
                    persoonsgegevens.Actief = true;
                    if (ModelState.IsValid)
                    {
                        db.Persoonsgegevens.Add(persoonsgegevens);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(persoonsgegevens);
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
        // GET: /Begeleider/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }

            ViewBag.Bedrijf = new SelectList(db.Bedrijf.Where(p => p.Actief) , "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // POST: /Begeleider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            EmailDuplicaatHelper edh = new EmailDuplicaatHelper();
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);

            try
            {
                bool BestaatEmail = edh.bestaatEmail(persoonsgegevens);

                if (BestaatEmail == false)
                {
                    persoonsgegevens.Rol = 3;
                    if (ModelState.IsValid)
                    {
                        db.Entry(persoonsgegevens).State = EntityState.Modified;
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
        // GET: /Begeleider/Delete/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            return View(persoonsgegevens);
        }

        //
        // POST: /Begeleider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            db.Persoonsgegevens.Remove(persoonsgegevens);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Begeleider/BulkInvoerBegeleider
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerBegeleider()
        {
            return View();
        }

        [HttpPost]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet Begeleiders = eh.excelToDS(file, Server);
            ViewData["feedback"] = eh.dataSetToBegeleider(Begeleiders);
            return View("~/Views/Begeleider/BulkInvoerBegeleider.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}