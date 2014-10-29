using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class GespreksformulierViewModel
    {
        public Stage StageID { get; set; }
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
}