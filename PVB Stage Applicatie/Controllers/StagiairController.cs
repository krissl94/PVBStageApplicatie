using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.IO;
using System.Data.OleDb;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;

namespace PVB_Stage_Applicatie.Controllers
{
    public class StagiairController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Stagiair/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            if (HttpContext.User.IsInRole("Docent"))
            {
                var studentResultDocent = db.sp_StagiairPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                List<Persoonsgegevens> studentenDocent = new List<Persoonsgegevens>();
                foreach (var item in studentResultDocent)
                {
                    studentenDocent.Add(db.Persoonsgegevens.Where(x => x.PersoonsgegevensID == item.PersoonsgegevensID).FirstOrDefault());
                }
                return View(studentenDocent);
            };
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 4);
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Stagiair/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            bool bekendStudent = false;
            if (HttpContext.User.IsInRole("Docent"))
            {
                var studentResultDocent = db.sp_StagiairPerDocent(Convert.ToInt32(HttpContext.User.Identity.Name));
                foreach (var item in studentResultDocent)
                {
                    if (item.PersoonsgegevensID == id)
                    {
                        bekendStudent = true;
                    }
                }
                if (bekendStudent)
                    return View(persoonsgegevens);
                else
                    return HttpNotFound();
            }
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            return View(persoonsgegevens);
        }

        //
        // GET: /Stagiair/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");
            return View();
        }

        //
        // POST: /Stagiair/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            persoonsgegevens.Rol = 4;
            if (ModelState.IsValid)
            {
                db.Persoonsgegevens.Add(persoonsgegevens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // GET: /Stagiair/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        //
        // POST: /Stagiair/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persoonsgegevens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Bedrijf/BulkImport
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerStagiair()
        {
            return View();
        }
        public ViewResult BulkNonActiefStagiair()
        {
            return View();
        }
        [HttpPost]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet studentDs = eh.excelToDS(file, Server);
            eh.dataSetToStudent(studentDs);
            return View("~/Views/Stagiair/BulkImportStagiair.cshtml");
        }

        [HttpPost]
        public ViewResult BulkNonActief(HttpPostedFileBase file)
        {
            ExcelHelper eh = new ExcelHelper();
            DataSet studentDs = eh.excelToDS(file, Server);
            eh.dataSetToNonActiefStudent(studentDs);
            return View("~/Views/Stagiair/BulkNonActiefStagiair.cshtml");
        }
            
        
    }
}
