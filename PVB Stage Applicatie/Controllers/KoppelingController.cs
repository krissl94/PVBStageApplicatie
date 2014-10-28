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
    public class KoppelingController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Koppeling/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            IQueryable<PVB_Stage_Applicatie.Models.Stage> stage;
            if (User.IsInRole("Docent"))
            {
                int userId = Convert.ToInt32(User.Identity.Name);
                stage = db.Stage.Include(s => s.Periode).Include(s => s.Persoonsgegevens).Include(s => s.Persoonsgegevens1).Include(s => s.Persoonsgegevens2).Where(s => s.Persoonsgegevens1.PersoonsgegevensID == userId);                
            }
            else
            {
                stage = db.Stage.Include(s => s.Periode).Include(s => s.Persoonsgegevens).Include(s => s.Persoonsgegevens1).Include(s => s.Persoonsgegevens2);
            }
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

            ViewBag.Stageperiode = new SelectList(db.Periode, "Periode1", "Periode1");
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

       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}