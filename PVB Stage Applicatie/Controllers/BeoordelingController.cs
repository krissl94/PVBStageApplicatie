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

            List<StudentViewModel> Studentlijstje = new List<StudentViewModel>();

            foreach (var item in Periodelijst)
	        {
                foreach(var stageItem in item.Stage)
                {
                    if (stageItem.Stagedocent.ToString() == User.Identity.Name)
                        Studentlijstje.Add(new StudentViewModel(stageItem.Persoonsgegevens.PersoonsgegevensID, stageItem.Persoonsgegevens.Voornaam, stageItem.Persoonsgegevens.Achternaam, stageItem.StageID, stageItem.Persoonsgegevens.Toevoeging, stageItem.Persoonsgegevens.StudentNummer));
                }
	        }
            var jSerializer = new JavaScriptSerializer();
            
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
        // POST: /Beoordeling/CreateFormulier
        [HttpPost]
        public ActionResult CreateFormulier(string stage)
        {
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


        public void Opsturen(string technischeAspecten, string houdingsaspecten, string verslag, string handtekeningen)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

           var Handtekeningen = ser.Deserialize<BeoordelingsJson.Handtekeningen>(handtekeningen);
           var Verslag = ser.Deserialize<BeoordelingsJson.Verslagen>(verslag);
           var Houdingsaspecten = ser.Deserialize<BeoordelingsJson.Houdingsaspecten>(houdingsaspecten);
           var TechnischeAspecten = ser.Deserialize<BeoordelingsJson.TechnischeAspecten>(technischeAspecten);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}