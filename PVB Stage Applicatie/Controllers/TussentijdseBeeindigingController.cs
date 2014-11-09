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
            try
            {
                TussentijdseBeindeging tussentijdsebeindeging = db.TussentijdseBeindeging.Find(id);
                if (tussentijdsebeindeging == null)
                {
                    return HttpNotFound();
                }
                return View(tussentijdsebeindeging);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // GET: /TussentijdseBeeindiging/CreateBeeindeging
        [Authorize(Roles = "Docent")]
        public ActionResult CreateBeeindeging(int id = 0)
        {
            try
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
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // POST: /TussentijdseBeeindiging/CreateBeeindeging
        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult CreateBeeindeging(TussentijdseEindBeoordelingModel tussentijdseEindBeoordelingModel)
        {
            try
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
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}