using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class BeoordelingModel
    {
        public Beoordeling Beoordeling { get; set; }

        private string handtekeningDocent;

        public string HandtekeningDocent
        {
            get { return handtekeningDocent; }
            set 
            {
                handtekeningDocent = value;
                Beoordeling.HandtekeningDocent = System.Text.Encoding.ASCII.GetBytes(value);
            }
        }

        private string handtekeningBegeleider;

	    public string HandtekeningBegeleider
	    {
		    get { return handtekeningBegeleider;}
		    set 
            {
                handtekeningBegeleider = value;
                Beoordeling.HandtekeningBegeleider = System.Text.Encoding.ASCII.GetBytes(value);
            }
	    }

        private string handtekeningStudent;

        public string HandtekeningStudent
        {
            get { return handtekeningStudent; }
            set 
            { 
                handtekeningStudent = value;
                Beoordeling.HandtekeningStudent = System.Text.Encoding.ASCII.GetBytes(value);
            }
        }
    }
}