using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class GespreksformulierViewModel
    {
        public Stage StageID { get; set; }
        [DisplayFormat(DataFormatString="{0:dd-MM-yyyy}")]
        public DateTime Datum { get { return DateTime.Now.Date; } }
        public string Gesprek { get; set; }
        public int Type { get; set; }
        public string HandtekeningStudent { get; set; }
        public string HandtekeningDocent { get; set; }
        public string HandtekeningBegeleider { get; set; }

        public GespreksformulierViewModel()
        { 
        }

        public GespreksformulierViewModel(Stage stageID, string gesprek, int type, string handtekeningstudent, string handtekeningdocent, string handtekeningbegeleider)
        {
            this.StageID = stageID;
            this.Gesprek = gesprek;
            this.Type = type;
            this.HandtekeningStudent = handtekeningstudent;
            this.HandtekeningDocent = handtekeningdocent;
            this.HandtekeningBegeleider = handtekeningbegeleider;
        }

        public GespreksformulierViewModel(Stage stageID, string gesprek, int type)
        {
            this.StageID = stageID;
            this.Gesprek = gesprek;
            this.Type = type;
        }
    }

    public enum TypeContact
    {
        Telefonisch = 1,
        Bezoek = 2
    }
}