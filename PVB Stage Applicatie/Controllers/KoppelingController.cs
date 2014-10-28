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
    public class KoppelingController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Koppeling/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            var stage = db.Stage.Include(s => s.Periode).Include(s => s.Persoonsgegevens).Include(s => s.Persoonsgegevens1).Include(s => s.Persoonsgegevens2);
            return View(stage.ToList());
        }

        //
        // GET: /Koppeling/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Stage stage = db.Stage.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        //
        // GET: /Koppeling/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {

            ViewBag.Stageperiode = new SelectList(db.Periode.Where(x => x.Begindatum > DateTime.Now), "Periode1", "Periode1");
            ViewBag.Student = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x=>x.Rol == 4), "PersoonsgegevensID", "Voornaam");
            ViewBag.Stagedocent = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 2), "PersoonsgegevensID", "Voornaam");
            ViewBag.Stagebegeleider = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 3), "PersoonsgegevensID", "Voornaam");
            return View();
        }

        //
        // POST: /Koppeling/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Stage.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Stageperiode = new SelectList(db.Periode.Where(x=> x.Einddatum < DateTime.Now), "Periode1", "Periode1", stage.Stageperiode);
            //ViewBag.Student = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true), "PersoonsgegevensID", "Voornaam", stage.Student);
            //ViewBag.Stagedocent = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true), "PersoonsgegevensID", "Voornaam", stage.Stagedocent);
            //ViewBag.Stagebegeleider = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true), "PersoonsgegevensID", "Voornaam", stage.Stagebegeleider);
            return View(stage);
        }

        //
        // GET: /Koppeling/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            Stage stage = db.Stage.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }

            ViewBag.Stageperiode = new SelectList(db.Periode.Where(x => x.Begindatum > DateTime.Now), "Periode1", "Periode1");
            ViewBag.Student = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 4), "PersoonsgegevensID", "Voornaam");
            ViewBag.Stagedocent = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 2), "PersoonsgegevensID", "Voornaam");
            ViewBag.Stagebegeleider = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 3), "PersoonsgegevensID", "Voornaam");
          
            return View(stage);
        }

        //
        // POST: /Koppeling/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Stageperiode = new SelectList(db.Periode, "Periode1", "Periode1", stage.Stageperiode);
            //ViewBag.Student = new SelectList(db.Persoonsgegevens, "PersoonsgegevensID", "Voornaam", stage.Student);
            //ViewBag.Stagedocent = new SelectList(db.Persoonsgegevens, "PersoonsgegevensID", "Voornaam", stage.Stagedocent);
            //ViewBag.Stagebegeleider = new SelectList(db.Persoonsgegevens, "PersoonsgegevensID", "Voornaam", stage.Stagebegeleider);
            return View(stage);
        }

        //
        // GET: /Koppeling/Delete/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int id = 0)
        {
            Stage stage = db.Stage.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        //
        // POST: /Koppeling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stage stage = db.Stage.Find(id);
            db.Stage.Remove(stage);
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