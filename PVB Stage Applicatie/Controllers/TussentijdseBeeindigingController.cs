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
    }
}