using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class TechnischeAspectenViewModel
    {
        public int Beoordeling { get; set; }
        public string Opmerking { get; set; }

        public TechnischeAspectenViewModel(int beoordeling, string opmerking)
        {
            this.Beoordeling = beoordeling;
            this.Opmerking = opmerking;
        }
    }
}