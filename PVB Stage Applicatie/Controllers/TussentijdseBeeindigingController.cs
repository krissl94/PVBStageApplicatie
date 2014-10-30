using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.Web.Script.Serialization;

namespace PVB_Stage_Applicatie.Controllers
{
    public class TussentijdseBeeindigingController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();


        //
        // GET: /TussentijdseBeeindiging/Details/5

        public ActionResult Details(int id = 0)
        {
            TussentijdseBeindeging tussentijdsebeindeging = db.TussentijdseBeindeging.Find(id);
            if (tussentijdsebeindeging == null)
            {
                return HttpNotFound();
            }
            return View(tussentijdsebeindeging);
        }

        //
        // GET: /TussentijdseBeeindiging/CreateBeindeging

        public ActionResult CreateBeeindeging(int stageID = 0)
        {
            return View(db.Stage.Where(i => i.StageID == stageID).FirstOrDefault());
        }

        [HttpPost]
        public int Opsturen(string tussentijdseBeeindigingJson, int stageID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            BeeindigingJson bj = serializer.Deserialize<BeeindigingJson>(tussentijdseBeeindigingJson);
            TussentijdseBeindeging be = new TussentijdseBeindeging();

            be.CREBO = bj.crebo;
            be.Leerweg = bj.leerweg;
            be.Einddatum = Convert.ToDateTime(bj.WerkelijkeEindDatum);
            be.Stage = stageID;

            foreach (int item in bj.onderwijs)
            {
                be.RedenOnderwijsinstelling.Add(db.RedenOnderwijsinstelling.Where(i => i.RedenID == item).SingleOrDefault());
            }

            foreach (int item in bj.organisatie)
            {
                be.RedenOrganisatie.Add(db.RedenOrganisatie.Where(i => i.RedenOrganisatie1 == item).SingleOrDefault());
            }

            foreach (int item in bj.student)
            {
                be.RedenStudent.Add(db.RedenStudent.Where(i => i.RedenID == item).SingleOrDefault());
            }

            db.TussentijdseBeindeging.Add(be);
            db.SaveChanges();

            Stage stageToPost = db.Stage.Where(i => i.StageID == stageID).FirstOrDefault();
            RedirectToAction("StudentIndex", "Formulier", stageToPost);
            return stageToPost.StageID;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}