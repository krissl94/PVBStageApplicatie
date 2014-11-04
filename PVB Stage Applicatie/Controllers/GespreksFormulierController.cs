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
    public class GespreksFormulierController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /GespreksFormulier/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            if (gespreksformulier == null)
            {
                return HttpNotFound();
            }
            return View(gespreksformulier);
        }

        [Authorize(Roles = "Docent")]
        public ActionResult CreateGespreksFormulier(int stageID = 0)
        {
            GespreksformulierViewModel GespreksFormulierStage = new GespreksformulierViewModel();
            GespreksFormulierStage.StageID = db.Stage.Where(i => i.StageID == stageID).FirstOrDefault();

            Stage stage = db.Stage.Where(s => s.StageID == stageID).FirstOrDefault();

            if (stage != null)
                if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                    if (User.Identity.Name == stage.Stagedocent.ToString())
                        return View(GespreksFormulierStage);

            return RedirectToAction("StudentIndex", "Formulier", new { stageID = stageID });
        }
        
        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult CreateGespreksFormulier(GespreksformulierViewModel formulier)
         {
            Stage stageToAdd = db.Stage.Where(s => s.StageID == formulier.StageID.StageID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                Gespreksformulier toAdd = (new Gespreksformulier
                {
                    GespreksformulierID = 0,
                    ContactType = formulier.Type,
                    Datum = formulier.Datum,
                    Gesprek = formulier.Gesprek,
                    HandtekeningBegeleider = System.Text.Encoding.ASCII.GetBytes(formulier.HandtekeningBegeleider),
                    HandtekeningDocent = System.Text.Encoding.ASCII.GetBytes(formulier.HandtekeningDocent),
                    HandtekeningStudent = System.Text.Encoding.ASCII.GetBytes(formulier.HandtekeningStudent),
                    Stage1 = stageToAdd,
                    Stage = formulier.StageID.StageID
                });
                db.Gespreksformulier.Add(toAdd);

                
                db.SaveChanges();
                return View("~/Views/Formulier/StudentIndex.cshtml", stageToAdd);
            }

            return View("~/Views/Formulier/StudentIndex.cshtml", stageToAdd);
        }

        [Authorize(Roles = "Docent")]
        public ActionResult Edit(int id = 0)
        {
            Gespreksformulier gesprek = db.Gespreksformulier.Where(i => i.GespreksformulierID == id).FirstOrDefault();
            Stage stage = gesprek.Stage1;

            if (stage != null)
                if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                    if (User.Identity.Name == stage.Stagedocent.ToString())
                        return View(gesprek);

            return RedirectToAction("StudentIndex", "Formulier", new { stageID = stage.StageID });
        }

        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult Edit(Gespreksformulier gesprek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gesprek).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("StudentIndex", "Formulier", new { stageID = gesprek.Stage });
            }

            return View(gesprek);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}