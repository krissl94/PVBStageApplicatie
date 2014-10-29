using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    
    public class StudentViewModel
    {
        public int PersoonsgegevensID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int stageId { get; set; }

        public string Tussenvoegsel { get; set; }
        public string StudentNummer { get; set; }

        public StudentViewModel(int persoonsid, string voornaam, string achternaam, int stageid)
        {
            this.PersoonsgegevensID = persoonsid;
            this.Voornaam = voornaam;
            this.Achternaam = achternaam;
            this.stageId = stageid;
        }
        public StudentViewModel(int persoonsid, string voornaam, string achternaam, int stageid, string tussenvoegsel, string studentNummer)
        {
            this.PersoonsgegevensID = persoonsid;
            this.Voornaam = voornaam;
            this.Achternaam = achternaam;
            this.stageId = stageid;
            this.Tussenvoegsel = tussenvoegsel;
            this.StudentNummer = studentNummer;
        }
    }
}