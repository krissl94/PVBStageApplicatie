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
using System.Collections.Generic;

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
            Beoordeling beoordeling = db.Beoordeling.Find(id);
            
            if (beoordeling == null)
            {
                return HttpNotFound();
            }

            return View(beoordeling);
        }

        //
        // POST: /Beoordeling/CreateFormulier
        [Authorize(Roles = "Docent")]
        public ActionResult CreateFormulier(int stageID = 0)
        {
            return View(db.Stage.Where(i => i.StageID == stageID).FirstOrDefault());
        }

        //
        // POST: /Beoordeling/CreateEindbeoordeling
        [Authorize(Roles = "Docent")]
        public ActionResult CreateEindbeoordeling(int stageID = 0)
        {
            return View(db.Stage.Where(i => i.StageID == stageID).FirstOrDefault());
        }

        //
        // GET: /Beoordeling/Edit/5
        [Authorize(Roles = "Docent")]
        public ActionResult Edit(int id = 0)
        {
            Beoordeling beoordeling = db.Beoordeling.Find(id);
            if (beoordeling == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", beoordeling.Stage);
            return View(beoordeling);
        }

        //
        // POST: /Beoordeling/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Docent")]
        public ActionResult Edit(Beoordeling beoordeling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beoordeling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stage = new SelectList(db.Stage, "StageID", "StageID", beoordeling.Stage);
            return View(beoordeling);
        }

        //
        // GET: /Beoordeling/Delete/5
        [HttpPost]
        [Authorize(Roles = "Docent")]
        public int OpsturenEind(string beoordelingenJson, string handtekeningenJson, int stageID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Beoordelingen b = serializer.Deserialize<Beoordelingen>(beoordelingenJson);
            Handetekeningen h = serializer.Deserialize<Handetekeningen>(handtekeningenJson);

            Beoordeling bobj = new Beoordeling();

            bobj.Datum = DateTime.Today;
            bobj.EindBeoordeling = true;
            bobj.Stage = stageID;

            bobj.Beoordeling1 = b.Beoordeling1.beoordeling;
            bobj.BeoordelingTechAspect = b.BeoordelingTechAspect.beoordeling;
            bobj.BeoordelingTechAspectOpm = b.BeoordelingTechAspect.opmerking;
            bobj.Gemotiveerd = b.Gemotiveerd.beoordeling;
            bobj.GemotiveerdOpm = b.Gemotiveerd.opmerking;
            bobj.GrenzenAangeven = b.GrenzenAangeven.beoordeling;
            bobj.GrenzenAangevenOpm = b.GrenzenAangeven.opmerking;
            bobj.HoudingAspectenOpm = b.HoudingsAspecten.opmerking;
            bobj.HoudingsAspecten = b.HoudingsAspecten.beoordeling;
            bobj.HoudingTAVCollegas = b.HoudingTAVCollegas.beoordeling;
            bobj.HoudingTAVCollegasOpm = b.HoudingTAVCollegas.opmerking;
            bobj.HoudingTAVDerden = b.HoudingTAVDerden.beoordeling;
            bobj.HoudingTAVDerdenOpm = b.HoudingTAVDerden.opmerking;
            bobj.HoudingTAVLeiding = b.HoudingTAVLeiding.beoordeling;
            bobj.HoudingTAVLeidingOpm = b.HoudingTAVLeiding.opmerking;
            bobj.HoudtARBORegels = b.HoudtARBORegels.beoordeling;
            bobj.HoudtARBORegelsOpm = b.HoudtARBORegels.opmerking;
            bobj.HoudtBedrRegels = b.HoudtBedrRegels.beoordeling;
            bobj.HoudtBedrRegelsOpm = b.HoudtBedrRegels.opmerking;
            bobj.IntzetOpm = b.Inzet.opmerking;
            bobj.Inzet = b.Inzet.beoordeling;
            bobj.KomtAfsprakenNa = b.KomtAfsprakenNa.beoordeling;
            bobj.KomtAfsprakenNaOpm = b.KomtAfsprakenNa.opmerking;
            bobj.KwalGeleverdWerk = b.KwalGeleverdWerk.beoordeling;
            bobj.KwalGeleverdWerkOpm = b.KwalGeleverdWerk.opmerking;
            bobj.OmgaanKritiek = b.OmgaanKritiek.beoordeling;
            bobj.OmgaanKritiekOpm = b.OmgaanKritiek.opmerking;
            bobj.OpTijd = b.OpTijd.beoordeling;
            bobj.OpTijdOpm = b.OpTijd.opmerking;
            bobj.PlanEnOrganiserenWerk = b.PlanEnOrganiserenWerk.beoordeling;
            bobj.PlanEnOrganiserenWerkOpm = b.PlanEnOrganiserenWerk.opmerking;
            bobj.RapportWerk = b.RapportWerk.beoordeling;
            bobj.RapportWerkOpm = b.RapportWerk.opmerking;
            bobj.TechnischInzicht = b.TechnischInzicht.beoordeling;
            bobj.TechnischInzichtOpm = b.TheoretischInzicht.opmerking;
            bobj.TheoretischInzicht = b.TheoretischInzicht.beoordeling;
            bobj.TheoretischInzichtOpm = b.TheoretischInzicht.opmerking;
            bobj.ToontBelangstellingVak = b.ToontBelangstellingVak.beoordeling;
            bobj.ToontBelangstellingVakOpm = b.ToontBelangstellingVak.opmerking;
            bobj.ToontInitiatief = b.ToontInitiatief.beoordeling;
            bobj.ToontInitiatiefOpm = b.ToontInitiatief.opmerking;
            bobj.VerloopEersteContact = b.VerloopEersteContact.beoordeling;
            bobj.VerloopEersteContactOpm = b.VerloopEersteContact.opmerking;
            bobj.Verslag = b.Verslag.beoordeling;
            bobj.VerslagOpm = b.Verslag.opmerking;
            bobj.VoorbereidWerk = b.VoorbereidWerk.beoordeling;
            bobj.VoorbereidWerkOpm = b.VoorbereidWerk.opmerking;
            bobj.WerkTempo = b.WerkTempo.beoordeling;
            bobj.WerkTempoOpm = b.WerkTempo.opmerking;
            bobj.ZelfstandigWerken = b.ZelfstandigWerken.beoordeling;
            bobj.ZelfstandigWerkenOpm = b.ZelfstandigWerken.opmerking;

            bobj.HandtekeningBegeleider = System.Text.Encoding.ASCII.GetBytes(h.b.handtekening);
            bobj.HandtekeningDocent = System.Text.Encoding.ASCII.GetBytes(h.d.handtekening);
            bobj.HandtekeningStudent = System.Text.Encoding.ASCII.GetBytes(h.s.handtekening);

            db.Beoordeling.Add(bobj);
            db.SaveChanges();

            Stage stageToPost = db.Stage.Where(i => i.StageID == stageID).FirstOrDefault();
            RedirectToAction("StudentIndex", "Formulier", stageToPost);
            return stageToPost.StageID;
        }

        [HttpPost]
        [Authorize(Roles = "Docent")]
        public int Opsturen(string beoordelingenJson, string handtekeningenJson, int stageID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Beoordelingen b = serializer.Deserialize<Beoordelingen>(beoordelingenJson);
            Handetekeningen h = serializer.Deserialize<Handetekeningen>(handtekeningenJson);

            Beoordeling bobj = new Beoordeling();

            bobj.Datum = DateTime.Today;
            bobj.EindBeoordeling = false;
            bobj.Stage = stageID;

            bobj.Beoordeling1 = b.Beoordeling1.beoordeling;
            bobj.BeoordelingTechAspect = b.BeoordelingTechAspect.beoordeling;
            bobj.BeoordelingTechAspectOpm = b.BeoordelingTechAspect.opmerking;
            bobj.Gemotiveerd = b.Gemotiveerd.beoordeling;
            bobj.GemotiveerdOpm = b.Gemotiveerd.opmerking;
            bobj.GrenzenAangeven = b.GrenzenAangeven.beoordeling;
            bobj.GrenzenAangevenOpm = b.GrenzenAangeven.opmerking;
            bobj.HoudingAspectenOpm = b.HoudingsAspecten.opmerking;
            bobj.HoudingsAspecten = b.HoudingsAspecten.beoordeling;
            bobj.HoudingTAVCollegas = b.HoudingTAVCollegas.beoordeling;
            bobj.HoudingTAVCollegasOpm = b.HoudingTAVCollegas.opmerking;
            bobj.HoudingTAVDerden = b.HoudingTAVDerden.beoordeling;
            bobj.HoudingTAVDerdenOpm = b.HoudingTAVDerden.opmerking;
            bobj.HoudingTAVLeiding = b.HoudingTAVLeiding.beoordeling;
            bobj.HoudingTAVLeidingOpm = b.HoudingTAVLeiding.opmerking;
            bobj.HoudtARBORegels =  b.HoudtARBORegels.beoordeling;
            bobj.HoudtARBORegelsOpm = b.HoudtARBORegels.opmerking;
            bobj.HoudtBedrRegels = b.HoudtBedrRegels.beoordeling;
            bobj.HoudtBedrRegelsOpm = b.HoudtBedrRegels.opmerking;
            bobj.IntzetOpm = b.Inzet.opmerking;
            bobj.Inzet = b.Inzet.beoordeling;
            bobj.KomtAfsprakenNa = b.KomtAfsprakenNa.beoordeling;
            bobj.KomtAfsprakenNaOpm = b.KomtAfsprakenNa.opmerking;
            bobj.KwalGeleverdWerk = b.KwalGeleverdWerk.beoordeling;
            bobj.KwalGeleverdWerkOpm = b.KwalGeleverdWerk.opmerking;
            bobj.OmgaanKritiek = b.OmgaanKritiek.beoordeling;
            bobj.OmgaanKritiekOpm = b.OmgaanKritiek.opmerking;
            bobj.OpTijd = b.OpTijd.beoordeling;
            bobj.OpTijdOpm = b.OpTijd.opmerking;
            bobj.PlanEnOrganiserenWerk = b.PlanEnOrganiserenWerk.beoordeling;
            bobj.PlanEnOrganiserenWerkOpm = b.PlanEnOrganiserenWerk.opmerking;
            bobj.RapportWerk = b.RapportWerk.beoordeling;
            bobj.RapportWerkOpm = b.RapportWerk.opmerking;
            bobj.TechnischInzicht = b.TechnischInzicht.beoordeling;
            bobj.TechnischInzichtOpm = b.TheoretischInzicht.opmerking;
            bobj.TheoretischInzicht = b.TheoretischInzicht.beoordeling;
            bobj.TheoretischInzichtOpm = b.TheoretischInzicht.opmerking;
            bobj.ToontBelangstellingVak = b.ToontBelangstellingVak.beoordeling;
            bobj.ToontBelangstellingVakOpm = b.ToontBelangstellingVak.opmerking;
            bobj.ToontInitiatief = b.ToontInitiatief.beoordeling;
            bobj.ToontInitiatiefOpm = b.ToontInitiatief.opmerking;
            bobj.VerloopEersteContact = b.VerloopEersteContact.beoordeling;
            bobj.VerloopEersteContactOpm = b.VerloopEersteContact.opmerking;
            bobj.Verslag = b.Verslag.beoordeling;
            bobj.VerslagOpm = b.Verslag.opmerking;
            bobj.VoorbereidWerk = b.VoorbereidWerk.beoordeling;
            bobj.VoorbereidWerkOpm = b.VoorbereidWerk.opmerking;
            bobj.WerkTempo = b.WerkTempo.beoordeling;
            bobj.WerkTempoOpm = b.WerkTempo.opmerking;
            bobj.ZelfstandigWerken = b.ZelfstandigWerken.beoordeling;
            bobj.ZelfstandigWerkenOpm = b.ZelfstandigWerken.opmerking;
            bobj.HandtekeningBegeleider = System.Text.Encoding.ASCII.GetBytes(h.b.handtekening);
            bobj.HandtekeningDocent = System.Text.Encoding.ASCII.GetBytes(h.d.handtekening);
            bobj.HandtekeningStudent = System.Text.Encoding.ASCII.GetBytes(h.s.handtekening);

            db.Beoordeling.Add(bobj);
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