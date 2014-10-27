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
    public class BedrijfController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Bedrijf/
        [Authorize(Roles=Rollen.Docent + "," + Rollen.Beheerder)]
        public ActionResult Index()
        {
            return View(db.Bedrijf.ToList());
        }

        //
        // GET: /Bedrijf/Details/5
        [Authorize(Roles = Rollen.Docent + "," + Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bedrijf/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
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