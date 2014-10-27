//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public partial class Persoonsgegevens
    {
        public Persoonsgegevens()
        {
            this.Login = new HashSet<Login>();
            this.OpleidingPersoon = new HashSet<OpleidingPersoon>();
            this.Stage = new HashSet<Stage>();
            this.Stage1 = new HashSet<Stage>();
            this.Stage2 = new HashSet<Stage>();
        }
    
        public int PersoonsgegevensID { get; set; }
        public int Rol { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Email { get; set; }
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string Toevoeging { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public bool Actief { get; set; }
        public string NonActiefReden { get; set; }
        public string MedewerkerID { get; set; }
        public string StudentNummer { get; set; }
        public string Opleiding { get; set; }
        public Nullable<int> Opleidingsniveau { get; set; }
        public Nullable<int> Bedrijf { get; set; }
    
        public virtual Bedrijf Bedrijf1 { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<OpleidingPersoon> OpleidingPersoon { get; set; }
        public virtual ICollection<Stage> Stage { get; set; }
        public virtual ICollection<Stage> Stage1 { get; set; }
        public virtual ICollection<Stage> Stage2 { get; set; }

    }

}
