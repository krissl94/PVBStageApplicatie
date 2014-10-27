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
        [Authorize(Roles = Rollen.Beheerder + "," + Rollen.Docent)]
        public ActionResult Index()
        {
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 3);
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Begeleider/Details/5
        [Authorize(Roles = Rollen.Beheerder + "," + Rollen.Docent)]
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
        [Authorize(Roles = Rollen.Beheerder)]
        public ActionResult Create()
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
            return View();
        }

        //
        // POST: /Begeleider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Rollen.Beheerder)]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            persoonsgegevens.Rol = 3;
            persoonsgegevens.Actief = true;
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
        // GET: /Begeleider/Edit/5
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            persoonsgegevens.Rol = 3;
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
        // GET: /Begeleider/Delete/5
        [Authorize(Roles = Rollen.Beheerder)]
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
        [Authorize(Roles = Rollen.Beheerder)]
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