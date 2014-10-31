using PVB_Stage_Applicatie.Models;
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

        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            List<Periode> Periodelijst =
                new List<Periode>(db.Periode
                .Where(x => x.Begindatum < DateTime.Now)
                .Where(x => x.Einddatum > DateTime.Now));

            List<StudentViewModel> Studentlijstje = new List<StudentViewModel>();
            StudentViewModel svm = new StudentViewModel();
            foreach (var item in Periodelijst)
            {
                foreach (var stageItem in item.Stage)
                {
                    if (User.IsInRole("docent"))
                    {
                        if (stageItem.Stagedocent.ToString() == User.Identity.Name)
                            Studentlijstje.Add(new StudentViewModel(
                                stageItem.Persoonsgegevens.PersoonsgegevensID,
                                stageItem.Persoonsgegevens.Voornaam,
                                stageItem.Persoonsgegevens.Achternaam,
                                stageItem.StageID,
                                stageItem.Persoonsgegevens.Toevoeging,
                                stageItem.Persoonsgegevens.StudentNummer));
                    }
                    else
                    {
                        Studentlijstje.Add(new StudentViewModel(
                            stageItem.Persoonsgegevens.PersoonsgegevensID,
                            stageItem.Persoonsgegevens.Voornaam,
                            stageItem.Persoonsgegevens.Achternaam,
                            stageItem.StageID,
                            stageItem.Persoonsgegevens.Toevoeging,
                            stageItem.Persoonsgegevens.StudentNummer));
                    }
                }
            }

            ViewBag.DropDownList = new SelectList(Studentlijstje, "StageId", "Naam");
            return View(svm);
        }

        [Authorize(Roles = "Beheerder,Docent")]
        [HttpPost]
        public ActionResult Index(StudentViewModel student)
        {
            Stage stage = db.Stage.Where(s => s.StageID == student.stageId).FirstOrDefault();

            ViewData["Stage"] = stage;

            return View("~/Views/Formulier/StudentIndex.cshtml", stage);
        }

        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult StudentIndex(int stageID)
        {
            Stage stage = db.Stage.Where(s => s.StageID == stageID).FirstOrDefault();

            return View(stage);
        }
    }
}
