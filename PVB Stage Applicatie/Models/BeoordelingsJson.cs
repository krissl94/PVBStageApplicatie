using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class BeoordelingsJson
    {
        public class D
        {
            public string handtekening { get; set; }
        }

        public class B
        {
            public string handtekening { get; set; }
        }

        public class S
        {
            public string handtekening { get; set; }
        }


        public class BeoordelingTechAspect
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class Verslag
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }


        public class VerloopEersteContact
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudingTAVCollegas
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudingTAVLeiding
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudingTAVDerden
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class OmgaanKritiek
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class OpTijd
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class ToontInitiatief
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class Inzet
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class ToontBelangstellingVak
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class GrenzenAangeven
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class KomtAfsprakenNa
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class ZelfstandigWerken
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class Gemotiveerd
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudingsAspecten
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }


        public class Beoordeling1
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class VoorbereidWerk
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class PlanEnOrganiserenWerk
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class TheoretischInzicht
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class TechnischInzicht
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class KwalGeleverdWerk
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class RapportWerk
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudtBedrRegels
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class HoudtARBORegels
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class WerkTempo
        {
            public object beoordeling { get; set; }
            public string opmerking { get; set; }
        }

        public class TechnischeAspecten
        {
            public Beoordeling1 Beoordeling1 { get; set; }
            public VoorbereidWerk VoorbereidWerk { get; set; }
            public PlanEnOrganiserenWerk PlanEnOrganiserenWerk { get; set; }
            public TheoretischInzicht TheoretischInzicht { get; set; }
            public TechnischInzicht TechnischInzicht { get; set; }
            public KwalGeleverdWerk KwalGeleverdWerk { get; set; }
            public RapportWerk RapportWerk { get; set; }
            public HoudtBedrRegels HoudtBedrRegels { get; set; }
            public HoudtARBORegels HoudtARBORegels { get; set; }
            public WerkTempo WerkTempo { get; set; }
        }

        public class Verslagen
        {
            public BeoordelingTechAspect BeoordelingTechAspect { get; set; }
            public Verslag Verslag { get; set; }
        }

        public class Houdingsaspecten
        {
            public VerloopEersteContact VerloopEersteContact { get; set; }
            public HoudingTAVCollegas HoudingTAVCollegas { get; set; }
            public HoudingTAVLeiding HoudingTAVLeiding { get; set; }
            public HoudingTAVDerden HoudingTAVDerden { get; set; }
            public OmgaanKritiek OmgaanKritiek { get; set; }
            public OpTijd OpTijd { get; set; }
            public ToontInitiatief ToontInitiatief { get; set; }
            public Inzet Inzet { get; set; }
            public ToontBelangstellingVak ToontBelangstellingVak { get; set; }
            public GrenzenAangeven GrenzenAangeven { get; set; }
            public KomtAfsprakenNa KomtAfsprakenNa { get; set; }
            public ZelfstandigWerken ZelfstandigWerken { get; set; }
            public Gemotiveerd Gemotiveerd { get; set; }
            public HoudingsAspecten HoudingsAspecten { get; set; }
        }
        public class Handtekeningen
        {
            public D d { get; set; }
            public B b { get; set; }
            public S s { get; set; }
        }
    }
}