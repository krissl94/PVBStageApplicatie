﻿using PVB_Stage_Applicatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVB_Stage_Applicatie.Controllers
{
    public class FormulierController : Controller
    {
        StageApplicatieEntities db = new StageApplicatieEntities();

        [Authorize(Roles = "Docent")]
        public ActionResult Index()
        {
            try
            {
                List<Periode> Periodelijst = new List<Periode>(db.Periode.Where(x => x.Begindatum <= DateTime.Now));

                List<StudentViewModel> Studentlijstje = new List<StudentViewModel>();

                foreach (var periode in Periodelijst)
                    foreach (var stage in periode.Stage)
                        if (User.IsInRole("Docent"))
                            if (stage.TussentijdseBeindeging.Count == 0 && stage.Beoordeling.Where(e => e.EindBeoordeling == true).FirstOrDefault() == null)
                                if (stage.Stagedocent.ToString() == User.Identity.Name)
                                    Studentlijstje.Add(new StudentViewModel(
                                        stage.Persoonsgegevens.PersoonsgegevensID,
                                        stage.Persoonsgegevens.Voornaam,
                                        stage.Persoonsgegevens.Achternaam,
                                        stage.StageID,
                                        stage.Persoonsgegevens.Toevoeging,
                                        stage.Persoonsgegevens.StudentNummer));

                ViewBag.DropDownList = new SelectList(Studentlijstje, "StageId", "Naam");

                return View();
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        [Authorize(Roles = "Docent")]
        [HttpPost]
        public ActionResult Index(StudentViewModel student)
        {
            return RedirectToAction("StudentIndex", "Formulier", new { id = student.stageId });
        }


        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult StudentIndex(int id)
        {
            try
            {
                Stage stage = db.Stage.Where(s => s.StageID == id).FirstOrDefault();

                if (stage != null)
                    if (User.IsInRole("Beheerder") || User.Identity.Name == stage.Stagedocent.ToString())
                        return View(stage);

                return RedirectToAction("Index", "Formulier");
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }
    }
}
