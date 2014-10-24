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
    public class GespreksFormulierController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /GespreksFormulier/

        public ActionResult Index()
        {
            var gespreksformulier = db.Gespreksformulier.Include(g => g.Stage1);
            return View(gespreksformulier.ToList());
        }

        //
        // GET: /GespreksFormulier/Details/5

        public ActionResult Details(int id = 0)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            if (gespreksformulier == null)
            {
                return HttpNotFound();
            }
            return View(gespreksformulier);
        }

        //
        // GET: /GespreksFormulier/Create

        public ActionResult Create()
        {
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID");
            return View();
        }

        //
        // POST: /GespreksFormulier/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gespreksformulier gespreksformulier)
        {
            if (ModelState.IsValid)
            {
                db.Gespreksformulier.Add(gespreksformulier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", gespreksformulier.Stage);
            return View(gespreksformulier);
        }

        //
        // GET: /GespreksFormulier/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            if (gespreksformulier == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", gespreksformulier.Stage);
            return View(gespreksformulier);
        }

        //
        // POST: /GespreksFormulier/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gespreksformulier gespreksformulier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gespreksformulier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", gespreksformulier.Stage);
            return View(gespreksformulier);
        }

        //
        // GET: /GespreksFormulier/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            if (gespreksformulier == null)
            {
                return HttpNotFound();
            }
            return View(gespreksformulier);
        }

        //
        // POST: /GespreksFormulier/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            db.Gespreksformulier.Remove(gespreksformulier);
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