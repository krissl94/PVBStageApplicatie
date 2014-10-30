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
        // GET: /TussentijdseBeeindiging/

        public ActionResult Index()
        {
            var tussentijdsebeindeging = db.TussentijdseBeindeging.Include(t => t.Stage1);
            return View(tussentijdsebeindeging.ToList());
        }

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
        // GET: /TussentijdseBeeindiging/Create

        public ActionResult Create()
        {
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID");
            return View();
        }

        //
        // GET: /TussentijdseBeeindiging/CreateBeindeging

        public ActionResult CreateBeeindeging(Stage stage)
        {
            return View(db.Stage.Where(i => i.StageID == stage.StageID));
        }

        //
        // POST: /TussentijdseBeeindiging/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TussentijdseBeindeging tussentijdsebeindeging)
        {
            if (ModelState.IsValid)
            {
                db.TussentijdseBeindeging.Add(tussentijdsebeindeging);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", tussentijdsebeindeging.Stage);
            return View(tussentijdsebeindeging);
        }

        //
        // GET: /TussentijdseBeeindiging/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TussentijdseBeindeging tussentijdsebeindeging = db.TussentijdseBeindeging.Find(id);
            if (tussentijdsebeindeging == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", tussentijdsebeindeging.Stage);
            return View(tussentijdsebeindeging);
        }

        //
        // POST: /TussentijdseBeeindiging/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TussentijdseBeindeging tussentijdsebeindeging)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tussentijdsebeindeging).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", tussentijdsebeindeging.Stage);
            return View(tussentijdsebeindeging);
        }

        //
        // GET: /TussentijdseBeeindiging/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TussentijdseBeindeging tussentijdsebeindeging = db.TussentijdseBeindeging.Find(id);
            if (tussentijdsebeindeging == null)
            {
                return HttpNotFound();
            }
            return View(tussentijdsebeindeging);
        }

        //
        // POST: /TussentijdseBeeindiging/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TussentijdseBeindeging tussentijdsebeindeging = db.TussentijdseBeindeging.Find(id);
            db.TussentijdseBeindeging.Remove(tussentijdsebeindeging);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
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
                foreach (var stageItem in item.Stage)
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
            return View("~/Views/TussentijdseBeeindiging/CreateBeeindeging.cshtml", Stage);
        }

        [HttpPost]
        public ActionResult Opsturen(string tussentijdseBeeindigingJson, int stageID)
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

            return null;
        }
    }
}