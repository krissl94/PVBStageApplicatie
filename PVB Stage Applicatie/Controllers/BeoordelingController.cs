using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;

namespace PVB_Stage_Applicatie.Controllers
{
    public class BeoordelingController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Beoordeling/

        public ActionResult Index()
        {
            var beoordeling = db.Beoordeling.Include(b => b.Stage1);
            return View(beoordeling.ToList());
        }

        //
        // GET: /Beoordeling/Details/5

        public ActionResult Details(int id = 0)
        {
            Beoordeling beoordeling = db.Beoordeling.Find(id);
            if (beoordeling == null)
            {
                return HttpNotFound();
            }
            return View(beoordeling);
        }


        public ActionResult Selecteer(string medewerkerID)
        {
            List<Periode> Periodelijst = 
                new List<Periode>(db.Periode
                .Where(x => x.Begindatum < DateTime.Now)
                .Where(x => x.Einddatum > DateTime.Now));

            List<PeriodeViewModel> PeriodeViewLijst = new List<PeriodeViewModel>();
            List<StudentViewModel> Studentlijstje = new List<StudentViewModel>();

            foreach (var item in Periodelijst)
	        {
		        PeriodeViewLijst.Add( new PeriodeViewModel(item.Periode1, item.Begindatum, item.Einddatum));
                foreach(var stageItem in item.Stage)
                {
                    Studentlijstje.Add(new StudentViewModel(stageItem.Persoonsgegevens.PersoonsgegevensID, stageItem.Persoonsgegevens.Voornaam, stageItem.Persoonsgegevens.Achternaam, stageItem.StageID));
                }
	        }
            var jSerializer = new JavaScriptSerializer();
            ViewBag.periode = jSerializer.Serialize(PeriodeViewLijst);
            ViewBag.student = jSerializer.Serialize(Studentlijstje);


            //ist<Persoonsgegevens> Studentlijst = new List<Persoonsgegevens>(db.Persoonsgegevens.Where(s => s.Rol == 4).Where(s => s.Stage1.FirstOrDefault().Persoonsgegevens.MedewerkerID == medewerkerID));
            
            //ViewBag.Student = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x=>x.Rol == 4), "PersoonsgegevensID", "Voornaam");
            //ViewBag.Stagedocent = new SelectList(db.Persoonsgegevens.Where(x => x.Actief == true).Where(x => x.Rol == 2), "PersoonsgegevensID", "Voornaam");
            //ViewBag.Bedrijven = new SelectList(db.Bedrijf.Where(b => b.Actief == true), "Bedrijf", "Bedrijf");
           
            return View(Studentlijstje);
        }
        //
        // GET: /Beoordeling/Create

        public ActionResult Create()
        {
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID");
            return View();
        }

        //
        // POST: /Beoordeling/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Beoordeling beoordeling)
        {
            if (ModelState.IsValid)
            {
                db.Beoordeling.Add(beoordeling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", beoordeling.Stage);
            return View(beoordeling);
        }

        //
        // GET: /Beoordeling/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Beoordeling beoordeling = db.Beoordeling.Find(id);
            if (beoordeling == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", beoordeling.Stage);
            return View(beoordeling);
        }

        //
        // POST: /Beoordeling/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Beoordeling beoordeling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beoordeling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", beoordeling.Stage);
            return View(beoordeling);
        }

        //
        // GET: /Beoordeling/Delete/5


        public void Opsturen(string TechnischeAspecten, string Houdingsaspecten, string Verslag, string Handtekeningen)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}