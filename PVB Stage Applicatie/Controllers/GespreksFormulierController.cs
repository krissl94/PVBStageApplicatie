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

        public ActionResult Details(int id = 0)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            if (gespreksformulier == null)
            {
                return HttpNotFound();
            }
            return View(gespreksformulier);
        }

        public ActionResult CreateGespreksFormulier(int stageID = 0)
        {
            GespreksformulierViewModel GespreksFormulierStage = new GespreksformulierViewModel();
            GespreksFormulierStage.StageID = db.Stage.Where(i => i.StageID == stageID).FirstOrDefault();

            return View(GespreksFormulierStage);
        }
        
        [HttpPost]
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

            // IS FOUT GEGAAN G
            return View("~/Views/Formulier/StudentIndex.cshtml", stageToAdd);
        }
        

        // POST: /GespreksFormulier/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gespreksformulier gespreksformulier = db.Gespreksformulier.Find(id);
            db.Gespreksformulier.Remove(gespreksformulier);
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