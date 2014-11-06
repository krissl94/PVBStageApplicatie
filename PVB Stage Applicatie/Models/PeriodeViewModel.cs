using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{

    public class PeriodeViewModel : Periode
    {

        public int Periode1 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Begindatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Einddatum { get; set; }

        public PeriodeViewModel(int periode, DateTime begindatum, DateTime einddatum)
        {
            //this.Stage = new HashSet<Stage>();
            this.Periode1 = periode;
            this.Begindatum = begindatum;
            this.Einddatum = einddatum;
        }

    }
}