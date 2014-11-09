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
    public class PeriodeController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Periode/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            try
            {
                return View(db.Periode.ToList());
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }
        }

        //
        // GET: /Periode/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            try
            {
                Periode periode = db.Periode.Find(id);
                if (periode == null)
                {
                    return HttpNotFound();
                }
                return View(periode);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // GET: /Periode/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Periode/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Periode periode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Periode.Add(periode);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(periode);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // GET: /Periode/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            try
            {
                Periode periode = db.Periode.Find(id);
                bool periodeLeeg = false;
                if (periode.Stage.Count() == 0)
                {
                    periodeLeeg = true;
                }
                if (periode == null)
                {
                    return HttpNotFound();
                }
                if (periodeLeeg)
                {
                    return View(periode);
                }
                else
                {
                    ViewData["KanAanpassen"] = "De periode kan niet worden aangepast, omdat er andere objecten afhankelijk van zijn. ";

                    return View("~/Views/Periode/Index.cshtml", db.Periode.ToList());
                }
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // POST: /Periode/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Periode periode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(periode).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(periode);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // GET: /Periode/Delete/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int id = 0)
        {
            try
            {
                Periode periode = db.Periode.Find(id);
                if (periode == null)
                {
                    return HttpNotFound();
                }
                return View(periode);
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }

        }

        //
        // POST: /Periode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Periode periode = db.Periode.Find(id);
                db.Periode.Remove(periode);
                db.SaveChanges();
                return RedirectToAction("Index");
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