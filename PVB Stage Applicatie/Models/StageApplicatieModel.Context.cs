﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PVB_Stage_Applicatie.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class StageApplicatieEntities : DbContext
    {
        public StageApplicatieEntities()
            : base("name=StageApplicatieEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Bedrijf> Bedrijf { get; set; }
        public DbSet<Beoordeling> Beoordeling { get; set; }
        public DbSet<Gespreksformulier> Gespreksformulier { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Opleiding> Opleiding { get; set; }
        public DbSet<OpleidingPersoon> OpleidingPersoon { get; set; }
        public DbSet<Periode> Periode { get; set; }
        public DbSet<Persoonsgegevens> Persoonsgegevens { get; set; }
        public DbSet<RedenOnderwijsinstelling> RedenOnderwijsinstelling { get; set; }
        public DbSet<RedenOrganisatie> RedenOrganisatie { get; set; }
        public DbSet<RedenStudent> RedenStudent { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<TussentijdseBeindeging> TussentijdseBeindeging { get; set; }
        public DbSet<Role> Role { get; set; }
    
        public virtual int sp_BedrijfNonActief(Nullable<int> bedrijfID, Nullable<bool> actief, string nonActiefReden)
        {
            var bedrijfIDParameter = bedrijfID.HasValue ?
                new ObjectParameter("BedrijfID", bedrijfID) :
                new ObjectParameter("BedrijfID", typeof(int));
    
            var actiefParameter = actief.HasValue ?
                new ObjectParameter("Actief", actief) :
                new ObjectParameter("Actief", typeof(bool));
    
            var nonActiefRedenParameter = nonActiefReden != null ?
                new ObjectParameter("NonActiefReden", nonActiefReden) :
                new ObjectParameter("NonActiefReden", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BedrijfNonActief", bedrijfIDParameter, actiefParameter, nonActiefRedenParameter);
        }
    
        public virtual int sp_BedrijfToevoegen(string naam)
        {
            var naamParameter = naam != null ?
                new ObjectParameter("naam", naam) :
                new ObjectParameter("naam", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BedrijfToevoegen", naamParameter);
        }
    
        public virtual int sp_BedrijfUpdaten(string naam, Nullable<bool> actief, string nonActiefReden, Nullable<int> bedrijfID)
        {
            var naamParameter = naam != null ?
                new ObjectParameter("Naam", naam) :
                new ObjectParameter("Naam", typeof(string));
    
            var actiefParameter = actief.HasValue ?
                new ObjectParameter("Actief", actief) :
                new ObjectParameter("Actief", typeof(bool));
    
            var nonActiefRedenParameter = nonActiefReden != null ?
                new ObjectParameter("NonActiefReden", nonActiefReden) :
                new ObjectParameter("NonActiefReden", typeof(string));
    
            var bedrijfIDParameter = bedrijfID.HasValue ?
                new ObjectParameter("BedrijfID", bedrijfID) :
                new ObjectParameter("BedrijfID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BedrijfUpdaten", naamParameter, actiefParameter, nonActiefRedenParameter, bedrijfIDParameter);
        }
    
        public virtual int sp_BeoordelingToevoegen(Nullable<int> stage, Nullable<System.DateTime> datum, Nullable<int> beoordeling, Nullable<int> voorbereidWerk, string voorbereidWerkOpm, Nullable<int> planEnOrganiserenWerk, string planEnOrganiserenWerkOpm, Nullable<int> theoretischInzicht, string theoretischInzichtOpm, Nullable<int> kwalGeleverdWerk, string kwalGeleverdWerkOpm, Nullable<int> rapportWerk, string rapportWerkOpm, Nullable<int> houdtBedrRegels, string houdtBedrRegelsOpm, Nullable<int> houdtARBORegels, string houdtARBORegelsOpm, Nullable<int> werkTempo, string werkTempoOpm, Nullable<int> beoordelingTechAspect, string beoordelingTechAspectOpm, Nullable<int> verloopEersteContact, string verloopEersteContactOpm, Nullable<int> houdingTAVCollegas, string houdingTAVCollegasOpm, Nullable<int> houdingTAVLeiding, string houdingTAVLeidingOpm, Nullable<int> houdingTAVDerden, string houdingTAVDerdenOpm, Nullable<int> omgaanKritiek, string omgaanKritiekOpm, Nullable<int> opTijd, string opTijdOpm, Nullable<int> toontInitiatief, string toontInitiatiefOpm, Nullable<int> inzet, string intzetOpm, Nullable<int> toontBelangstellingVak, string toontBelangstellingVakOpm, Nullable<int> grenzenAangeven, string grenzenAangevenOpm, Nullable<int> komtAfsprakenNa, string komtAfsprakenNaOpm, Nullable<int> zelfstandigWerken, string zelfstandigWerkenOpm, Nullable<int> gemotiveerd, string gemotiveerdOpm, Nullable<int> houdingsAspecten, string houdingAspectenOpm, Nullable<int> verslag, string verslagOpm, byte[] handtekeningDocent, byte[] handtekeningStudent, byte[] handtekeningBegeleider, Nullable<bool> eindeBeoordeling)
        {
            var stageParameter = stage.HasValue ?
                new ObjectParameter("Stage", stage) :
                new ObjectParameter("Stage", typeof(int));
    
            var datumParameter = datum.HasValue ?
                new ObjectParameter("Datum", datum) :
                new ObjectParameter("Datum", typeof(System.DateTime));
    
            var beoordelingParameter = beoordeling.HasValue ?
                new ObjectParameter("Beoordeling", beoordeling) :
                new ObjectParameter("Beoordeling", typeof(int));
    
            var voorbereidWerkParameter = voorbereidWerk.HasValue ?
                new ObjectParameter("VoorbereidWerk", voorbereidWerk) :
                new ObjectParameter("VoorbereidWerk", typeof(int));
    
            var voorbereidWerkOpmParameter = voorbereidWerkOpm != null ?
                new ObjectParameter("VoorbereidWerkOpm", voorbereidWerkOpm) :
                new ObjectParameter("VoorbereidWerkOpm", typeof(string));
    
            var planEnOrganiserenWerkParameter = planEnOrganiserenWerk.HasValue ?
                new ObjectParameter("PlanEnOrganiserenWerk", planEnOrganiserenWerk) :
                new ObjectParameter("PlanEnOrganiserenWerk", typeof(int));
    
            var planEnOrganiserenWerkOpmParameter = planEnOrganiserenWerkOpm != null ?
                new ObjectParameter("PlanEnOrganiserenWerkOpm", planEnOrganiserenWerkOpm) :
                new ObjectParameter("PlanEnOrganiserenWerkOpm", typeof(string));
    
            var theoretischInzichtParameter = theoretischInzicht.HasValue ?
                new ObjectParameter("TheoretischInzicht", theoretischInzicht) :
                new ObjectParameter("TheoretischInzicht", typeof(int));
    
            var theoretischInzichtOpmParameter = theoretischInzichtOpm != null ?
                new ObjectParameter("TheoretischInzichtOpm", theoretischInzichtOpm) :
                new ObjectParameter("TheoretischInzichtOpm", typeof(string));
    
            var kwalGeleverdWerkParameter = kwalGeleverdWerk.HasValue ?
                new ObjectParameter("KwalGeleverdWerk", kwalGeleverdWerk) :
                new ObjectParameter("KwalGeleverdWerk", typeof(int));
    
            var kwalGeleverdWerkOpmParameter = kwalGeleverdWerkOpm != null ?
                new ObjectParameter("KwalGeleverdWerkOpm", kwalGeleverdWerkOpm) :
                new ObjectParameter("KwalGeleverdWerkOpm", typeof(string));
    
            var rapportWerkParameter = rapportWerk.HasValue ?
                new ObjectParameter("RapportWerk", rapportWerk) :
                new ObjectParameter("RapportWerk", typeof(int));
    
            var rapportWerkOpmParameter = rapportWerkOpm != null ?
                new ObjectParameter("RapportWerkOpm", rapportWerkOpm) :
                new ObjectParameter("RapportWerkOpm", typeof(string));
    
            var houdtBedrRegelsParameter = houdtBedrRegels.HasValue ?
                new ObjectParameter("HoudtBedrRegels", houdtBedrRegels) :
                new ObjectParameter("HoudtBedrRegels", typeof(int));
    
            var houdtBedrRegelsOpmParameter = houdtBedrRegelsOpm != null ?
                new ObjectParameter("HoudtBedrRegelsOpm", houdtBedrRegelsOpm) :
                new ObjectParameter("HoudtBedrRegelsOpm", typeof(string));
    
            var houdtARBORegelsParameter = houdtARBORegels.HasValue ?
                new ObjectParameter("HoudtARBORegels", houdtARBORegels) :
                new ObjectParameter("HoudtARBORegels", typeof(int));
    
            var houdtARBORegelsOpmParameter = houdtARBORegelsOpm != null ?
                new ObjectParameter("HoudtARBORegelsOpm", houdtARBORegelsOpm) :
                new ObjectParameter("HoudtARBORegelsOpm", typeof(string));
    
            var werkTempoParameter = werkTempo.HasValue ?
                new ObjectParameter("WerkTempo", werkTempo) :
                new ObjectParameter("WerkTempo", typeof(int));
    
            var werkTempoOpmParameter = werkTempoOpm != null ?
                new ObjectParameter("WerkTempoOpm", werkTempoOpm) :
                new ObjectParameter("WerkTempoOpm", typeof(string));
    
            var beoordelingTechAspectParameter = beoordelingTechAspect.HasValue ?
                new ObjectParameter("BeoordelingTechAspect", beoordelingTechAspect) :
                new ObjectParameter("BeoordelingTechAspect", typeof(int));
    
            var beoordelingTechAspectOpmParameter = beoordelingTechAspectOpm != null ?
                new ObjectParameter("BeoordelingTechAspectOpm", beoordelingTechAspectOpm) :
                new ObjectParameter("BeoordelingTechAspectOpm", typeof(string));
    
            var verloopEersteContactParameter = verloopEersteContact.HasValue ?
                new ObjectParameter("VerloopEersteContact", verloopEersteContact) :
                new ObjectParameter("VerloopEersteContact", typeof(int));
    
            var verloopEersteContactOpmParameter = verloopEersteContactOpm != null ?
                new ObjectParameter("VerloopEersteContactOpm", verloopEersteContactOpm) :
                new ObjectParameter("VerloopEersteContactOpm", typeof(string));
    
            var houdingTAVCollegasParameter = houdingTAVCollegas.HasValue ?
                new ObjectParameter("HoudingTAVCollegas", houdingTAVCollegas) :
                new ObjectParameter("HoudingTAVCollegas", typeof(int));
    
            var houdingTAVCollegasOpmParameter = houdingTAVCollegasOpm != null ?
                new ObjectParameter("HoudingTAVCollegasOpm", houdingTAVCollegasOpm) :
                new ObjectParameter("HoudingTAVCollegasOpm", typeof(string));
    
            var houdingTAVLeidingParameter = houdingTAVLeiding.HasValue ?
                new ObjectParameter("HoudingTAVLeiding", houdingTAVLeiding) :
                new ObjectParameter("HoudingTAVLeiding", typeof(int));
    
            var houdingTAVLeidingOpmParameter = houdingTAVLeidingOpm != null ?
                new ObjectParameter("HoudingTAVLeidingOpm", houdingTAVLeidingOpm) :
                new ObjectParameter("HoudingTAVLeidingOpm", typeof(string));
    
            var houdingTAVDerdenParameter = houdingTAVDerden.HasValue ?
                new ObjectParameter("HoudingTAVDerden", houdingTAVDerden) :
                new ObjectParameter("HoudingTAVDerden", typeof(int));
    
            var houdingTAVDerdenOpmParameter = houdingTAVDerdenOpm != null ?
                new ObjectParameter("HoudingTAVDerdenOpm", houdingTAVDerdenOpm) :
                new ObjectParameter("HoudingTAVDerdenOpm", typeof(string));
    
            var omgaanKritiekParameter = omgaanKritiek.HasValue ?
                new ObjectParameter("OmgaanKritiek", omgaanKritiek) :
                new ObjectParameter("OmgaanKritiek", typeof(int));
    
            var omgaanKritiekOpmParameter = omgaanKritiekOpm != null ?
                new ObjectParameter("OmgaanKritiekOpm", omgaanKritiekOpm) :
                new ObjectParameter("OmgaanKritiekOpm", typeof(string));
    
            var opTijdParameter = opTijd.HasValue ?
                new ObjectParameter("OpTijd", opTijd) :
                new ObjectParameter("OpTijd", typeof(int));
    
            var opTijdOpmParameter = opTijdOpm != null ?
                new ObjectParameter("OpTijdOpm", opTijdOpm) :
                new ObjectParameter("OpTijdOpm", typeof(string));
    
            var toontInitiatiefParameter = toontInitiatief.HasValue ?
                new ObjectParameter("ToontInitiatief", toontInitiatief) :
                new ObjectParameter("ToontInitiatief", typeof(int));
    
            var toontInitiatiefOpmParameter = toontInitiatiefOpm != null ?
                new ObjectParameter("ToontInitiatiefOpm", toontInitiatiefOpm) :
                new ObjectParameter("ToontInitiatiefOpm", typeof(string));
    
            var inzetParameter = inzet.HasValue ?
                new ObjectParameter("Inzet", inzet) :
                new ObjectParameter("Inzet", typeof(int));
    
            var intzetOpmParameter = intzetOpm != null ?
                new ObjectParameter("IntzetOpm", intzetOpm) :
                new ObjectParameter("IntzetOpm", typeof(string));
    
            var toontBelangstellingVakParameter = toontBelangstellingVak.HasValue ?
                new ObjectParameter("ToontBelangstellingVak", toontBelangstellingVak) :
                new ObjectParameter("ToontBelangstellingVak", typeof(int));
    
            var toontBelangstellingVakOpmParameter = toontBelangstellingVakOpm != null ?
                new ObjectParameter("ToontBelangstellingVakOpm", toontBelangstellingVakOpm) :
                new ObjectParameter("ToontBelangstellingVakOpm", typeof(string));
    
            var grenzenAangevenParameter = grenzenAangeven.HasValue ?
                new ObjectParameter("GrenzenAangeven", grenzenAangeven) :
                new ObjectParameter("GrenzenAangeven", typeof(int));
    
            var grenzenAangevenOpmParameter = grenzenAangevenOpm != null ?
                new ObjectParameter("GrenzenAangevenOpm", grenzenAangevenOpm) :
                new ObjectParameter("GrenzenAangevenOpm", typeof(string));
    
            var komtAfsprakenNaParameter = komtAfsprakenNa.HasValue ?
                new ObjectParameter("KomtAfsprakenNa", komtAfsprakenNa) :
                new ObjectParameter("KomtAfsprakenNa", typeof(int));
    
            var komtAfsprakenNaOpmParameter = komtAfsprakenNaOpm != null ?
                new ObjectParameter("KomtAfsprakenNaOpm", komtAfsprakenNaOpm) :
                new ObjectParameter("KomtAfsprakenNaOpm", typeof(string));
    
            var zelfstandigWerkenParameter = zelfstandigWerken.HasValue ?
                new ObjectParameter("ZelfstandigWerken", zelfstandigWerken) :
                new ObjectParameter("ZelfstandigWerken", typeof(int));
    
            var zelfstandigWerkenOpmParameter = zelfstandigWerkenOpm != null ?
                new ObjectParameter("ZelfstandigWerkenOpm", zelfstandigWerkenOpm) :
                new ObjectParameter("ZelfstandigWerkenOpm", typeof(string));
    
            var gemotiveerdParameter = gemotiveerd.HasValue ?
                new ObjectParameter("Gemotiveerd", gemotiveerd) :
                new ObjectParameter("Gemotiveerd", typeof(int));
    
            var gemotiveerdOpmParameter = gemotiveerdOpm != null ?
                new ObjectParameter("GemotiveerdOpm", gemotiveerdOpm) :
                new ObjectParameter("GemotiveerdOpm", typeof(string));
    
            var houdingsAspectenParameter = houdingsAspecten.HasValue ?
                new ObjectParameter("HoudingsAspecten", houdingsAspecten) :
                new ObjectParameter("HoudingsAspecten", typeof(int));
    
            var houdingAspectenOpmParameter = houdingAspectenOpm != null ?
                new ObjectParameter("HoudingAspectenOpm", houdingAspectenOpm) :
                new ObjectParameter("HoudingAspectenOpm", typeof(string));
    
            var verslagParameter = verslag.HasValue ?
                new ObjectParameter("Verslag", verslag) :
                new ObjectParameter("Verslag", typeof(int));
    
            var verslagOpmParameter = verslagOpm != null ?
                new ObjectParameter("VerslagOpm", verslagOpm) :
                new ObjectParameter("VerslagOpm", typeof(string));
    
            var handtekeningDocentParameter = handtekeningDocent != null ?
                new ObjectParameter("HandtekeningDocent", handtekeningDocent) :
                new ObjectParameter("HandtekeningDocent", typeof(byte[]));
    
            var handtekeningStudentParameter = handtekeningStudent != null ?
                new ObjectParameter("HandtekeningStudent", handtekeningStudent) :
                new ObjectParameter("HandtekeningStudent", typeof(byte[]));
    
            var handtekeningBegeleiderParameter = handtekeningBegeleider != null ?
                new ObjectParameter("HandtekeningBegeleider", handtekeningBegeleider) :
                new ObjectParameter("HandtekeningBegeleider", typeof(byte[]));
    
            var eindeBeoordelingParameter = eindeBeoordeling.HasValue ?
                new ObjectParameter("EindeBeoordeling", eindeBeoordeling) :
                new ObjectParameter("EindeBeoordeling", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BeoordelingToevoegen", stageParameter, datumParameter, beoordelingParameter, voorbereidWerkParameter, voorbereidWerkOpmParameter, planEnOrganiserenWerkParameter, planEnOrganiserenWerkOpmParameter, theoretischInzichtParameter, theoretischInzichtOpmParameter, kwalGeleverdWerkParameter, kwalGeleverdWerkOpmParameter, rapportWerkParameter, rapportWerkOpmParameter, houdtBedrRegelsParameter, houdtBedrRegelsOpmParameter, houdtARBORegelsParameter, houdtARBORegelsOpmParameter, werkTempoParameter, werkTempoOpmParameter, beoordelingTechAspectParameter, beoordelingTechAspectOpmParameter, verloopEersteContactParameter, verloopEersteContactOpmParameter, houdingTAVCollegasParameter, houdingTAVCollegasOpmParameter, houdingTAVLeidingParameter, houdingTAVLeidingOpmParameter, houdingTAVDerdenParameter, houdingTAVDerdenOpmParameter, omgaanKritiekParameter, omgaanKritiekOpmParameter, opTijdParameter, opTijdOpmParameter, toontInitiatiefParameter, toontInitiatiefOpmParameter, inzetParameter, intzetOpmParameter, toontBelangstellingVakParameter, toontBelangstellingVakOpmParameter, grenzenAangevenParameter, grenzenAangevenOpmParameter, komtAfsprakenNaParameter, komtAfsprakenNaOpmParameter, zelfstandigWerkenParameter, zelfstandigWerkenOpmParameter, gemotiveerdParameter, gemotiveerdOpmParameter, houdingsAspectenParameter, houdingAspectenOpmParameter, verslagParameter, verslagOpmParameter, handtekeningDocentParameter, handtekeningStudentParameter, handtekeningBegeleiderParameter, eindeBeoordelingParameter);
        }
    
        public virtual int sp_GespreksformulierToevoegen(Nullable<int> stage, Nullable<System.DateTime> datum, Nullable<int> contactType, string gesprek, byte[] handtekeningStudent, byte[] handtekeningDocent, byte[] handtekeningBegeleider)
        {
            var stageParameter = stage.HasValue ?
                new ObjectParameter("Stage", stage) :
                new ObjectParameter("Stage", typeof(int));
    
            var datumParameter = datum.HasValue ?
                new ObjectParameter("Datum", datum) :
                new ObjectParameter("Datum", typeof(System.DateTime));
    
            var contactTypeParameter = contactType.HasValue ?
                new ObjectParameter("ContactType", contactType) :
                new ObjectParameter("ContactType", typeof(int));
    
            var gesprekParameter = gesprek != null ?
                new ObjectParameter("Gesprek", gesprek) :
                new ObjectParameter("Gesprek", typeof(string));
    
            var handtekeningStudentParameter = handtekeningStudent != null ?
                new ObjectParameter("HandtekeningStudent", handtekeningStudent) :
                new ObjectParameter("HandtekeningStudent", typeof(byte[]));
    
            var handtekeningDocentParameter = handtekeningDocent != null ?
                new ObjectParameter("HandtekeningDocent", handtekeningDocent) :
                new ObjectParameter("HandtekeningDocent", typeof(byte[]));
    
            var handtekeningBegeleiderParameter = handtekeningBegeleider != null ?
                new ObjectParameter("HandtekeningBegeleider", handtekeningBegeleider) :
                new ObjectParameter("HandtekeningBegeleider", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_GespreksformulierToevoegen", stageParameter, datumParameter, contactTypeParameter, gesprekParameter, handtekeningStudentParameter, handtekeningDocentParameter, handtekeningBegeleiderParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_Login(string gebruikersnaam, string wachtwoord)
        {
            var gebruikersnaamParameter = gebruikersnaam != null ?
                new ObjectParameter("Gebruikersnaam", gebruikersnaam) :
                new ObjectParameter("Gebruikersnaam", typeof(string));
    
            var wachtwoordParameter = wachtwoord != null ?
                new ObjectParameter("Wachtwoord", wachtwoord) :
                new ObjectParameter("Wachtwoord", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Login", gebruikersnaamParameter, wachtwoordParameter);
        }
    
        public virtual int sp_LoginToevoegen(string gebruikersnaam, string wachtwoord, Nullable<int> persoonsgegevens)
        {
            var gebruikersnaamParameter = gebruikersnaam != null ?
                new ObjectParameter("Gebruikersnaam", gebruikersnaam) :
                new ObjectParameter("Gebruikersnaam", typeof(string));
    
            var wachtwoordParameter = wachtwoord != null ?
                new ObjectParameter("Wachtwoord", wachtwoord) :
                new ObjectParameter("Wachtwoord", typeof(string));
    
            var persoonsgegevensParameter = persoonsgegevens.HasValue ?
                new ObjectParameter("Persoonsgegevens", persoonsgegevens) :
                new ObjectParameter("Persoonsgegevens", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_LoginToevoegen", gebruikersnaamParameter, wachtwoordParameter, persoonsgegevensParameter);
        }
    
        public virtual int sp_PeriodeToevoegen(Nullable<System.DateTime> begindatum, Nullable<System.DateTime> eindatum)
        {
            var begindatumParameter = begindatum.HasValue ?
                new ObjectParameter("Begindatum", begindatum) :
                new ObjectParameter("Begindatum", typeof(System.DateTime));
    
            var eindatumParameter = eindatum.HasValue ?
                new ObjectParameter("Eindatum", eindatum) :
                new ObjectParameter("Eindatum", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PeriodeToevoegen", begindatumParameter, eindatumParameter);
        }
    
        public virtual int sp_PeriodeUpdaten(Nullable<int> periode, Nullable<System.DateTime> beginDatum, Nullable<System.DateTime> eindDatum)
        {
            var periodeParameter = periode.HasValue ?
                new ObjectParameter("Periode", periode) :
                new ObjectParameter("Periode", typeof(int));
    
            var beginDatumParameter = beginDatum.HasValue ?
                new ObjectParameter("BeginDatum", beginDatum) :
                new ObjectParameter("BeginDatum", typeof(System.DateTime));
    
            var eindDatumParameter = eindDatum.HasValue ?
                new ObjectParameter("EindDatum", eindDatum) :
                new ObjectParameter("EindDatum", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PeriodeUpdaten", periodeParameter, beginDatumParameter, eindDatumParameter);
        }
    
        public virtual int sp_PeriodeVerwijderen(Nullable<int> stagePeriodeID)
        {
            var stagePeriodeIDParameter = stagePeriodeID.HasValue ?
                new ObjectParameter("StagePeriodeID", stagePeriodeID) :
                new ObjectParameter("StagePeriodeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PeriodeVerwijderen", stagePeriodeIDParameter);
        }
    
        public virtual int sp_PersoonNonActief(Nullable<int> persoonID, Nullable<bool> actief, string nonActiefReden)
        {
            var persoonIDParameter = persoonID.HasValue ?
                new ObjectParameter("PersoonID", persoonID) :
                new ObjectParameter("PersoonID", typeof(int));
    
            var actiefParameter = actief.HasValue ?
                new ObjectParameter("Actief", actief) :
                new ObjectParameter("Actief", typeof(bool));
    
            var nonActiefRedenParameter = nonActiefReden != null ?
                new ObjectParameter("NonActiefReden", nonActiefReden) :
                new ObjectParameter("NonActiefReden", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PersoonNonActief", persoonIDParameter, actiefParameter, nonActiefRedenParameter);
        }
    
        public virtual int sp_PersoonToevoegen(Nullable<int> rol, string voornaam, string achternaam, string tussenvoegsel, string email, string straat, Nullable<int> huisnummer, string toevoeging, string postcode, string plaats, string nonActiefReden, Nullable<int> medewerkerID, string studentNummer, Nullable<int> opleiding, Nullable<int> opleidingsniveau, Nullable<int> bedrijf)
        {
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            var voornaamParameter = voornaam != null ?
                new ObjectParameter("Voornaam", voornaam) :
                new ObjectParameter("Voornaam", typeof(string));
    
            var achternaamParameter = achternaam != null ?
                new ObjectParameter("Achternaam", achternaam) :
                new ObjectParameter("Achternaam", typeof(string));
    
            var tussenvoegselParameter = tussenvoegsel != null ?
                new ObjectParameter("Tussenvoegsel", tussenvoegsel) :
                new ObjectParameter("Tussenvoegsel", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var straatParameter = straat != null ?
                new ObjectParameter("Straat", straat) :
                new ObjectParameter("Straat", typeof(string));
    
            var huisnummerParameter = huisnummer.HasValue ?
                new ObjectParameter("Huisnummer", huisnummer) :
                new ObjectParameter("Huisnummer", typeof(int));
    
            var toevoegingParameter = toevoeging != null ?
                new ObjectParameter("Toevoeging", toevoeging) :
                new ObjectParameter("Toevoeging", typeof(string));
    
            var postcodeParameter = postcode != null ?
                new ObjectParameter("Postcode", postcode) :
                new ObjectParameter("Postcode", typeof(string));
    
            var plaatsParameter = plaats != null ?
                new ObjectParameter("Plaats", plaats) :
                new ObjectParameter("Plaats", typeof(string));
    
            var nonActiefRedenParameter = nonActiefReden != null ?
                new ObjectParameter("NonActiefReden", nonActiefReden) :
                new ObjectParameter("NonActiefReden", typeof(string));
    
            var medewerkerIDParameter = medewerkerID.HasValue ?
                new ObjectParameter("MedewerkerID", medewerkerID) :
                new ObjectParameter("MedewerkerID", typeof(int));
    
            var studentNummerParameter = studentNummer != null ?
                new ObjectParameter("StudentNummer", studentNummer) :
                new ObjectParameter("StudentNummer", typeof(string));
    
            var opleidingParameter = opleiding.HasValue ?
                new ObjectParameter("Opleiding", opleiding) :
                new ObjectParameter("Opleiding", typeof(int));
    
            var opleidingsniveauParameter = opleidingsniveau.HasValue ?
                new ObjectParameter("Opleidingsniveau", opleidingsniveau) :
                new ObjectParameter("Opleidingsniveau", typeof(int));
    
            var bedrijfParameter = bedrijf.HasValue ?
                new ObjectParameter("Bedrijf", bedrijf) :
                new ObjectParameter("Bedrijf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PersoonToevoegen", rolParameter, voornaamParameter, achternaamParameter, tussenvoegselParameter, emailParameter, straatParameter, huisnummerParameter, toevoegingParameter, postcodeParameter, plaatsParameter, nonActiefRedenParameter, medewerkerIDParameter, studentNummerParameter, opleidingParameter, opleidingsniveauParameter, bedrijfParameter);
        }
    
        public virtual int sp_PersoonUpdaten(Nullable<int> persoonsgegevensID, string email, string straat, Nullable<int> huisnummer, string toevoeging, string postcode, string plaats, Nullable<bool> actief, string nonActiefReden)
        {
            var persoonsgegevensIDParameter = persoonsgegevensID.HasValue ?
                new ObjectParameter("PersoonsgegevensID", persoonsgegevensID) :
                new ObjectParameter("PersoonsgegevensID", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var straatParameter = straat != null ?
                new ObjectParameter("Straat", straat) :
                new ObjectParameter("Straat", typeof(string));
    
            var huisnummerParameter = huisnummer.HasValue ?
                new ObjectParameter("Huisnummer", huisnummer) :
                new ObjectParameter("Huisnummer", typeof(int));
    
            var toevoegingParameter = toevoeging != null ?
                new ObjectParameter("Toevoeging", toevoeging) :
                new ObjectParameter("Toevoeging", typeof(string));
    
            var postcodeParameter = postcode != null ?
                new ObjectParameter("Postcode", postcode) :
                new ObjectParameter("Postcode", typeof(string));
    
            var plaatsParameter = plaats != null ?
                new ObjectParameter("Plaats", plaats) :
                new ObjectParameter("Plaats", typeof(string));
    
            var actiefParameter = actief.HasValue ?
                new ObjectParameter("Actief", actief) :
                new ObjectParameter("Actief", typeof(bool));
    
            var nonActiefRedenParameter = nonActiefReden != null ?
                new ObjectParameter("NonActiefReden", nonActiefReden) :
                new ObjectParameter("NonActiefReden", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PersoonUpdaten", persoonsgegevensIDParameter, emailParameter, straatParameter, huisnummerParameter, toevoegingParameter, postcodeParameter, plaatsParameter, actiefParameter, nonActiefRedenParameter);
        }
    
        public virtual int sp_StageToevoegen(Nullable<int> student, Nullable<int> stagedocent, Nullable<int> stageperiode, Nullable<int> tagebegeleider)
        {
            var studentParameter = student.HasValue ?
                new ObjectParameter("Student", student) :
                new ObjectParameter("Student", typeof(int));
    
            var stagedocentParameter = stagedocent.HasValue ?
                new ObjectParameter("Stagedocent", stagedocent) :
                new ObjectParameter("Stagedocent", typeof(int));
    
            var stageperiodeParameter = stageperiode.HasValue ?
                new ObjectParameter("Stageperiode", stageperiode) :
                new ObjectParameter("Stageperiode", typeof(int));
    
            var tagebegeleiderParameter = tagebegeleider.HasValue ?
                new ObjectParameter("tagebegeleider", tagebegeleider) :
                new ObjectParameter("tagebegeleider", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_StageToevoegen", studentParameter, stagedocentParameter, stageperiodeParameter, tagebegeleiderParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_ZoekRol(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_ZoekRol", iDParameter);
        }
    
        public virtual ObjectResult<sp_BedrijfPerDocent_Result> sp_BedrijfPerDocent(Nullable<int> docent)
        {
            var docentParameter = docent.HasValue ?
                new ObjectParameter("Docent", docent) :
                new ObjectParameter("Docent", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BedrijfPerDocent_Result>("sp_BedrijfPerDocent", docentParameter);
        }
    
        public virtual ObjectResult<sp_BegeleiderPerDocent_Result> sp_BegeleiderPerDocent(Nullable<int> docent)
        {
            var docentParameter = docent.HasValue ?
                new ObjectParameter("Docent", docent) :
                new ObjectParameter("Docent", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BegeleiderPerDocent_Result>("sp_BegeleiderPerDocent", docentParameter);
        }
    
        public virtual ObjectResult<sp_StagiairPerDocent_Result> sp_StagiairPerDocent(Nullable<int> stagedocent)
        {
            var stagedocentParameter = stagedocent.HasValue ?
                new ObjectParameter("Stagedocent", stagedocent) :
                new ObjectParameter("Stagedocent", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_StagiairPerDocent_Result>("sp_StagiairPerDocent", stagedocentParameter);
        }
        public virtual Persoonsgegevens ZoekPersoon(int id)
        {
            return Persoonsgegevens.Where(x => x.PersoonsgegevensID == id).SingleOrDefault();
        }
    }
}
