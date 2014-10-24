﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;

namespace PVB_Stage_Applicatie.Controllers
{
    public class PeriodeController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Periode/

        public ActionResult Index()
        {
            return View(db.Periode.ToList());
        }

        //
        // GET: /Periode/Details/5

        public ActionResult Details(int id = 0)
        {
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        //
        // GET: /Periode/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Periode/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Periode periode)
        {
            if (ModelState.IsValid)
            {
                db.Periode.Add(periode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periode);
        }

        //
        // GET: /Periode/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        //
        // POST: /Periode/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Periode periode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periode);
        }

        //
        // GET: /Periode/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        //
        // POST: /Periode/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periode periode = db.Periode.Find(id);
            db.Periode.Remove(periode);
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