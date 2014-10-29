using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class Beoordeling1
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class VoorbereidWerk
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class PlanEnOrganiserenWerk
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class TheoretischInzicht
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class TechnischInzicht
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class KwalGeleverdWerk
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class RapportWerk
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudtBedrRegels
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudtARBORegels
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class WerkTempo
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class VerloopEersteContact
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudingTAVCollegas
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudingTAVLeiding
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudingTAVDerden
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class OmgaanKritiek
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class OpTijd
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class ToontInitiatief
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class Inzet
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class ToontBelangstellingVak
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class GrenzenAangeven
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class KomtAfsprakenNa
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class ZelfstandigWerken
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class Gemotiveerd
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class HoudingsAspecten
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class BeoordelingTechAspect
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

    public class Verslag
    {
        public int beoordeling { get; set; }
        public string opmerking { get; set; }
    }

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

    public class Beoordelingen
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
        public BeoordelingTechAspect BeoordelingTechAspect { get; set; }
        public Verslag Verslag { get; set; }
    }

    public class Handetekeningen
    {
        public D d { get; set; }
        public B b { get; set; }
        public S s { get; set; }
    }
}