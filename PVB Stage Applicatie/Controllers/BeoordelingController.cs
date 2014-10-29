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

        public ActionResult Selecteer()
        {
            List<Periode> Periodelijst = 
                new List<Periode>(db.Periode
                .Where(x => x.Begindatum < DateTime.Now)
                .Where(x => x.Einddatum > DateTime.Now));

            List<StudentViewModel> Studentlijstje = new List<StudentViewModel>();
            StudentViewModel svm = new StudentViewModel();
            foreach (var item in Periodelijst)
	        {
                foreach(var stageItem in item.Stage)
                {
                    if (stageItem.Stagedocent.ToString() == User.Identity.Name)
                        Studentlijstje.Add(new StudentViewModel(stageItem.Persoonsgegevens.PersoonsgegevensID, stageItem.Persoonsgegevens.Voornaam, stageItem.Persoonsgegevens.Achternaam, stageItem.StageID, stageItem.Persoonsgegevens.Toevoeging, stageItem.Persoonsgegevens.StudentNummer));
                }
	        }
            ViewBag.DropDownList = new SelectList(Studentlijstje, "StageId", "Naam");
            return View(svm);
        }

        [HttpPost]
        public ActionResult Selecteer(StudentViewModel student)
        {
            Stage Stage = db.Stage.Where(s => s.StageID == student.stageId).FirstOrDefault();
            return View("~/Views/Beoordeling/CreateFormulier.cshtml", Stage);
        }
        //
        // GET: /Beoordeling/Create

        public ActionResult Create()
        {
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID");
            return View();
        }

        //
        // POST: /Beoordeling/CreateFormulier
        [HttpPost]
        public ActionResult CreateFormulier(StudentViewModel stage)
        {
            int stageId = Convert.ToInt32(stage);
            Stage Stage = db.Stage.Find(stageId);
            return View("~/Views/Beoordeling/CreateFormulier", stage);


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


        public void Opsturen(string technischeAspectenJson, string houdingsaspectenJson, string verslagJson, string handtekeningenJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            BeoordelingsJson.Handtekeningen handtekeningen = serializer.Deserialize<BeoordelingsJson.Handtekeningen>(handtekeningenJson);
            BeoordelingsJson.Verslagen verslag = serializer.Deserialize<BeoordelingsJson.Verslagen>(verslagJson);
            BeoordelingsJson.Houdingsaspecten houdingsaspecten = serializer.Deserialize<BeoordelingsJson.Houdingsaspecten>(houdingsaspectenJson);
            BeoordelingsJson.TechnischeAspecten technischeAspecten = serializer.Deserialize<BeoordelingsJson.TechnischeAspecten>(technischeAspectenJson);

            Beoordeling beoordeling = new Beoordeling();

            //beoordeling.Beoordeling1 = technischeAspecten.Beoordeling1.beoordeling;
            //beoordeling.BeoordelingTechAspect = technischeAspecten.

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}