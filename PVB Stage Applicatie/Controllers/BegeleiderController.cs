using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVB_Stage_Applicatie.Models;
using System.IO;

namespace PVB_Stage_Applicatie.Controllers
{
    public class BegeleiderController : Controller
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        //
        // GET: /Begeleider/
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Index()
        {
            var persoonsgegevens = db.Persoonsgegevens.Include(p => p.Bedrijf1).Where(p => p.Rol == 3);
            return View(persoonsgegevens.ToList());
        }

        //
        // GET: /Begeleider/Details/5
        [Authorize(Roles = "Beheerder,Docent")]
        public ActionResult Details(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }
            return View(persoonsgegevens);
        }

        //
        // GET: /Begeleider/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");

            return View();
        }

        //
        // POST: /Begeleider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create(Persoonsgegevens persoonsgegevens)
        {
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam");

            try
            {
                if (db.Persoonsgegevens.Where(p => p.Email == persoonsgegevens.Email).FirstOrDefault() == null)
                {
                    persoonsgegevens.Rol = 3;
                    persoonsgegevens.Actief = true;

                    ModelState.Remove("StudentNummer");
                    ModelState.Remove("Opleiding");
                    ModelState.Remove("Opleidingsniveau");
                    ModelState.Remove("MedewerkerID");
                    if (ModelState.IsValid)
                    {
                        db.sp_PersoonToevoegen(
                            persoonsgegevens.Rol,
                            persoonsgegevens.Voornaam,
                            persoonsgegevens.Achternaam,
                            persoonsgegevens.Tussenvoegsel,
                            persoonsgegevens.Email,
                            persoonsgegevens.Straat,
                            persoonsgegevens.Huisnummer,
                            persoonsgegevens.Toevoeging,
                            persoonsgegevens.Postcode,
                            persoonsgegevens.Plaats,
                            null, null, null, null, null,
                            persoonsgegevens.Bedrijf);

                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    return View(persoonsgegevens);
                }
                else
                {
                    ViewData["Foutmelding"] = "Email adres staat al in ons systeem";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View();
            }


        }

        //
        // GET: /Begeleider/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int id = 0)
        {
            Persoonsgegevens persoonsgegevens = db.Persoonsgegevens.Find(id);
            if (persoonsgegevens == null)
            {
                return HttpNotFound();
            }

            ViewBag.Bedrijf = new SelectList(db.Bedrijf.Where(p => p.Actief), "BedrijfID", "Naam", persoonsgegevens.Bedrijf);
            return View(persoonsgegevens);
        }



        public FileResult download()
        {
            string file = @"C:\Users\Kris\Desktop\PVBStageApplicatie\PVB Stage Applicatie\App_Data\ExcelTemplates\BegeleiderInvoegen.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        //
        // POST: /Begeleider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(Persoonsgegevens persoonsgegevens)
        {
            EmailDuplicaatHelper edh = new EmailDuplicaatHelper();
            ViewBag.Bedrijf = new SelectList(db.Bedrijf, "BedrijfID", "Naam", persoonsgegevens.Bedrijf);

            try
            {
                if (!edh.bestaatEmail(persoonsgegevens))
                {
                    persoonsgegevens.Rol = 3;
                    persoonsgegevens.Actief = true;

                    ModelState.Remove("StudentNummer");
                    ModelState.Remove("Opleiding");
                    ModelState.Remove("Opleidingsniveau");
                    ModelState.Remove("MedewerkerID");

                    if (ModelState.IsValid)
                    {
                        db.sp_PersoonUpdaten(
                            persoonsgegevens.PersoonsgegevensID,
                            persoonsgegevens.Email,
                            persoonsgegevens.Straat,
                            persoonsgegevens.Huisnummer,
                            persoonsgegevens.Toevoeging,
                            persoonsgegevens.Postcode,
                            persoonsgegevens.Plaats,
                            persoonsgegevens.Actief,
                            persoonsgegevens.NonActiefReden);

                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    return View(persoonsgegevens);
                }
                else
                {
                    ViewData["Foutmelding"] = "Email adres staat al in ons systeem";
                    return View(persoonsgegevens);
                }
            }
            catch (Exception ex)
            {
                ViewData["Foutmelding"] = ex.ToString();
                return View(persoonsgegevens);
            }
        }


        //
        // GET: /Begeleider/BulkInvoerBegeleider
        [Authorize(Roles = "Beheerder")]
        public ActionResult BulkInvoerBegeleider()
        {
            return View();
        }

        [HttpPost]
        public ViewResult BulkInvoer(HttpPostedFileBase file)
        {
            try
            {
                ExcelHelper eh = new ExcelHelper();
                DataSet begeleiderDs = eh.excelToDS(file, Server);

                if (begeleiderDs != null)
                {
                    List<Persoonsgegevens> lijstje = eh.dataSetToBegeleider(begeleiderDs);

                    foreach (Persoonsgegevens item in lijstje)
                    {
                        if (db.Persoonsgegevens.Where(p => p.Email == item.Email).FirstOrDefault() == null)
                        {
                            int bedrijfsId = db.Bedrijf.Where(b => b.BedrijfID == item.Bedrijf).FirstOrDefault().BedrijfID;
                            ModelState.Remove("Docent.StudentNummer");
                            ModelState.Remove("Docent.Opleiding");
                            ModelState.Remove("Docent.Opleidingsniveau");
                            ModelState.Remove("MedewerkerID");
                            if (ModelState.IsValid)
                            {
                                db.sp_PersoonToevoegen(3, item.Voornaam,
                                item.Achternaam, item.Tussenvoegsel, item.Email,
                                item.Straat, item.Huisnummer, item.Toevoeging, item.Postcode
                                , item.Plaats, null, null, null, null, null, bedrijfsId);

                            }
                        }


                        //ViewData["feedback"] = eh.dataSetToBegeleider(Begeleiders);
                    }
                }
                return View("~/Views/Begeleider/BulkInvoerBegeleider.cshtml");

            }
            catch {
                return View("~/Views/Begeleider/BulkInvoerBegeleider.cshtml");

                }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}