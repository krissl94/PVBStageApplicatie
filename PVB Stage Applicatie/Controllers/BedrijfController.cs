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
        public ActionResult Index()
        {
            return View(db.Bedrijf.ToList());
        }

        //
        // GET: /Bedrijf/Details/5

        public ActionResult Details(int id = 0)
        {
            Bedrijf bedrijf = db.Bedrijf.Find(id);
            if (bedrijf == null)
            {
                return HttpNotFound();
            }
            return View(bedrijf);
        }

        //
        // GET: /Bedrijf/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bedrijf/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bedrijf bedrijf)
        {
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
        public ActionResult Edit(Bedrijf bedrijf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bedrijf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bedrijf);
        }

        //
        // GET: /Bedrijf/Delete/5

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
    }
}