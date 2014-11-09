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
            ViewBag.Stageperiode = new SelectList
            (
                (
                    from p in db.Periode.Where(x => x.Begindatum > DateTime.Now).ToList()
                    select new { Periode = p.Begindatum.ToString("dd/MM/yyyy") + " - " + p.Einddatum.ToString("dd/MM/yyyy"), Periode1 = p.Periode1 }
                ),
                "Periode1", "Periode"
            );

            ViewBag.Student = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x=>x.Rol == 4).ToList()
                    select new { Voornaam = s.Voornaam + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ), 
                "PersoonsgegevensID", "Voornaam"
            );
            
            ViewBag.Stagedocent = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 2).ToList()
                    select new { Voornaam = s.Voornaam + " " + s.Tussenvoegsel + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ),
                "PersoonsgegevensID", "Voornaam"
            );
            
            ViewBag.Stagebegeleider = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 3).ToList()
                    select new { Voornaam = s.Bedrijf1.Naam + " - " + s.Voornaam + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ), 
                "PersoonsgegevensID", "Voornaam"
            );

            return View();
        }

        //
        // POST: /Koppeling/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Stage stage)
        {
            if (db.Stage.Where(p => p.Student == stage.Student).Where(p => p.Stageperiode == stage.Stageperiode).FirstOrDefault() == null)
            {
                var fromP = db.Periode.Where(p => p.Periode1 == stage.Stageperiode).FirstOrDefault().Begindatum;
                var toP = db.Periode.Where(p => p.Periode1 == stage.Stageperiode).FirstOrDefault().Einddatum;
                var temp1 = db.Stage.Where(s => s.Student == stage.Student).Where(p => fromP > p.Periode.Begindatum).Where(p => fromP < p.Periode.Einddatum);
                if (db.Stage.Where(s => s.Student == stage.Student).Where(p => fromP > p.Periode.Begindatum).Where(p => fromP < p.Periode.Einddatum).FirstOrDefault() == null)
                    if (db.Stage.Where(s => s.Student == stage.Student).Where(p => toP > p.Periode.Begindatum).Where(p => toP < p.Periode.Einddatum).FirstOrDefault() == null)
                        if (db.Stage.Where(s => s.Student == stage.Student).Where(p => fromP < p.Periode.Begindatum).Where(p => toP > p.Periode.Einddatum).FirstOrDefault() == null)
                            if (ModelState.IsValid)
                            {
                                db.Stage.Add(stage);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
            }

            ViewData["Foutmelding"] = "Deze student is al op stage tijdens de aangegeven periode";

            ViewBag.Stageperiode = new SelectList
            (
                (
                    from p in db.Periode.Where(x => x.Begindatum > DateTime.Now).ToList()
                    select new { Periode = p.Begindatum.ToString("dd/MM/yyyy") + " - " + p.Einddatum.ToString("dd/MM/yyyy"), Periode1 = p.Periode1 }
                ),
                "Periode1", "Periode"
            );

            ViewBag.Student = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 4).ToList()
                    select new { Voornaam = s.Voornaam + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ),
                "PersoonsgegevensID", "Voornaam"
            );

            ViewBag.Stagedocent = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 2).ToList()
                    select new { Voornaam = s.Voornaam + " " + s.Tussenvoegsel + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ),
                "PersoonsgegevensID", "Voornaam"
            );

            ViewBag.Stagebegeleider = new SelectList
            (
                (
                    from s in db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 3).ToList()
                    select new { Voornaam = s.Bedrijf1.Naam + " - " + s.Voornaam + " " + s.Tussenvoegsel + " " + s.Achternaam, PersoonsgegevensID = s.PersoonsgegevensID }
                ),
                "PersoonsgegevensID", "Voornaam"
            );

            return View();
        }

        //
        // GET: /Koppeling/BulkInvoerKoppeling
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerKoppeling()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerKoppeling(HttpPostedFileBase file, Periode periode)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet Koppelingen = eh.excelToDS(file, Server);

            if (Koppelingen != null)
            {
                Periode fullPeriode = db.Periode.Where(p => p.Periode1 == periode.Periode1).SingleOrDefault();
                ViewData["feedback"] = eh.dataSetToKoppeling(Koppelingen, fullPeriode);
            }
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}