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
    public class StagiairController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Stagiair/

        public ActionResult Index()
        {
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 4);
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Stagiair/Details/5

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
        // GET: /Stagiair/Create

        public ActionResult Create()
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
            return View();
        }

        //
        // POST: /Stagiair/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            persoonsgegevens.Rol = 4;
            if (ModelState.IsValid)
            {
                db.Persoonsgegevens.Add(persoonsgegevens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // GET: /Stagiair/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // POST: /Stagiair/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persoonsgegevens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // GET: /Stagiair/Delete/5

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
        // POST: /Stagiair/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            db.Persoonsgegevens.Remove(persoonsgegevens);
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