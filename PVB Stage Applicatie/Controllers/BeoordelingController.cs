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
        // GET: /Beoordeling/Details/5

        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            try
            {
                Beoordeling beoordeling = db.Beoordeling.Find(id);
                if (beoordeling == null)
                {
                    return HttpNotFound();
                }

                return View(beoordeling);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // POST: /Beoordeling/CreateFormulier
        [Authorize(Roles = "Docent")]
        public ActionResult CreateFormulier(int id = 0)
        {
            try
            {
                Stage stage = db.Stage.Where(s => s.StageID == id).FirstOrDefault();

                if (stage != null)
                    if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                        if (User.Identity.Name == stage.Stagedocent.ToString())
                        {
                            BeoordelingModel beoordlingModel = new BeoordelingModel()
                            {
                                Beoordeling = new Beoordeling()
                                {
                                    Stage = stage.StageID,
                                    Stage1 = stage
                                }
                            };

                            return View(beoordlingModel);
                        }

                return RedirectToAction("StudentIndex", "Formulier", new { id = id });
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult CreateFormulier(BeoordelingModel beoordelingModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    beoordelingModel.Beoordeling.Datum = DateTime.Now;
                    beoordelingModel.Beoordeling.Stage1 = db.Stage.Where(i => i.StageID == beoordelingModel.Beoordeling.Stage).FirstOrDefault();
                    beoordelingModel.Beoordeling.EindBeoordeling = false;

                    db.Beoordeling.Add(beoordelingModel.Beoordeling);

                    db.SaveChanges();

                    return RedirectToAction("StudentIndex", "Formulier", new { id = beoordelingModel.Beoordeling.Stage });
                }

                return View(beoordelingModel);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        [Authorize(Roles = "Docent")]
        public ActionResult CreateEindbeoordeling(int id = 0)
        {
            try
            {
                Stage stage = db.Stage.Where(s => s.StageID == id).FirstOrDefault();

                if (stage != null)
                    if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                        if (User.Identity.Name == stage.Stagedocent.ToString())
                        {
                            BeoordelingModel beoordlingModel = new BeoordelingModel()
                            {
                                Beoordeling = new Beoordeling()
                                {
                                    Stage = stage.StageID,
                                    Stage1 = stage
                                }
                            };

                            return View(beoordlingModel);
                        }

                return RedirectToAction("StudentIndex", "Formulier", new { id = id });
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Docent")]
        public ActionResult CreateEindbeoordeling(BeoordelingModel beoordelingModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    beoordelingModel.Beoordeling.Datum = DateTime.Now;
                    beoordelingModel.Beoordeling.Stage1 = db.Stage.Where(i => i.StageID == beoordelingModel.Beoordeling.Stage).FirstOrDefault();
                    beoordelingModel.Beoordeling.EindBeoordeling = true;

                    db.Beoordeling.Add(beoordelingModel.Beoordeling);

                    db.SaveChanges();

                    return RedirectToAction("StudentIndex", "Formulier", new { id = beoordelingModel.Beoordeling.Stage });
                }

                return View(beoordelingModel);
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