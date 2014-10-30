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
            //Bedrijf bedrijf = db.Bedrijf.Where(b => b.BedrijfID == id).FirstOrDefault();
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
            bedrijf.Actief = true;
            if (ModelState.IsValid)
            {
                db.Bedrijf.Add(bedrijf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bedrijf);
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
        // GET: /Bedrijf/Delete/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int id = 0)
        {
            Bedrijf bedrijf = db.Bedrijf.Find(id);
            if (bedrijf == null)
            {
                return HttpNotFound();
            }
            return View(bedrijf);
        }

        //
        // POST: /Bedrijf/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            Bedrijf bedrijf = db.Bedrijf.Find(id);
            db.Bedrijf.Remove(bedrijf);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        //
        // GET: /Bedrijf/BulkInvoerBedrijf
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerBedrijf()
        {
            return View();
        }

        [HttpPost]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            if (eh.excelToDS(file, Server) != null)
            {
                DataSet Bedrijven = eh.excelToDS(file, Server);
                ViewData["feedback"] = eh.dataSetToBedrijf(Bedrijven);
            }
            return View("~/Views/Bedrijf/BulkInvoerBedrijf.cshtml");
        }
    }
}