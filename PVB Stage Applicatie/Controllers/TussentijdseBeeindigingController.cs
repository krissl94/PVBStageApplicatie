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
        [Authorize(Roles = "Beheerder,Docent")]
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
        // GET: /TussentijdseBeeindiging/CreateBeeindeging
        [Authorize(Roles = "Docent")]
        public ActionResult CreateBeeindeging(int id = 0)
        {
            Stage stage = db.Stage.Where(s => s.StageID == id).FirstOrDefault();

            if (stage != null)
                if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                    if (User.Identity.Name == stage.Stagedocent.ToString())
                    {
                        TussentijdseEindBeoordelingModel thisIsTheEndMyOnlyFriendTheEnd = new TussentijdseEindBeoordelingModel()
                        {
                            TussentijdseBeeindegingModel = new TussentijdseBeindeging()
                            {
                                Stage = stage.StageID,
                                Stage1 = stage
                            }
                        };

                        return View(thisIsTheEndMyOnlyFriendTheEnd);
                    }

            return RedirectToAction("StudentIndex", "Formulier", new { id = id });
        }

        //
        // POST: /TussentijdseBeeindiging/CreateBeeindeging
        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult CreateBeeindeging(TussentijdseEindBeoordelingModel tussentijdseEindBeoordelingModel)
        {
            TussentijdseBeindeging tb = tussentijdseEindBeoordelingModel.TussentijdseBeeindegingModel;

            if (ModelState.IsValid)
            {

                if (tussentijdseEindBeoordelingModel.RedenenStudent != null)
                {
                    foreach (string item in tussentijdseEindBeoordelingModel.RedenenStudent.Split(','))
                    {
                        int id = Convert.ToInt32(item);
                        tb.RedenStudent.Add(db.RedenStudent.Where(i => i.RedenID == id).SingleOrDefault());
                    }
                }

                if (tussentijdseEindBeoordelingModel.RedenenOnderwijsinstelling != null)
                {
                    foreach (string item in tussentijdseEindBeoordelingModel.RedenenOnderwijsinstelling.Split(','))
                    {
                        int id = Convert.ToInt32(item);
                        tb.RedenOnderwijsinstelling.Add(db.RedenOnderwijsinstelling.Where(i => i.RedenID == id).SingleOrDefault());
                    }
                }

                if (tussentijdseEindBeoordelingModel.RedenenOrganisatie != null)
                {
                    foreach (string item in tussentijdseEindBeoordelingModel.RedenenOrganisatie.Split(','))
                    {
                        int id = Convert.ToInt32(item);
                        tb.RedenOrganisatie.Add(db.RedenOrganisatie.Where(i => i.RedenOrganisatie1 == id).SingleOrDefault());
                    }
                }

                tb.Stage1 = db.Stage.Where(s => s.StageID == tb.Stage).FirstOrDefault();

                db.TussentijdseBeindeging.Add(tb);

                db.SaveChanges();

                return RedirectToAction("StudentIndex", "Formulier", new { id = tb.Stage });
            }

            return CreateBeeindeging(tb.Stage);

            //Stage stage = db.Stage.Where(s => s.StageID == id).FirstOrDefault();

            //if (stage != null)
            //    if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
            //        if (User.Identity.Name == stage.Stagedocent.ToString())
            //        {
            //            TussentijdseEindBeoordelingModel thisIsTheEndMyOnlyFriendTheEnd = new TussentijdseEindBeoordelingModel()
            //            {
            //                TussentijdseBeeindegingModel = new TussentijdseBeindeging()
            //                {
            //                    Stage = stage.StageID,
            //                    Stage1 = stage
            //                }
            //            };

            //            return View(thisIsTheEndMyOnlyFriendTheEnd);
            //        }

            //return RedirectToAction("StudentIndex", "Formulier", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = "Docent")]
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