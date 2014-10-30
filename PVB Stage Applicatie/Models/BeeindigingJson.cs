using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class BeeindigingJson
    {
        public List<int> onderwijs { get; set; }
        public List<int> organisatie { get; set; }
        public List<int> student { get; set; }
        public string WerkelijkeEindDatum { get; set; }
        public string opleiding { get; set; }
        public int leerweg { get; set; }
        public string crebo { get; set; }
    }
}