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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Voornaam is een verplicht veld")]
        [RegularExpression(@"[A-z]{1,50}", ErrorMessage = "Vul alleen letters in")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is een verplicht veld")]
        [RegularExpression(@"[A-z' ']{1,150}", ErrorMessage = "Vul alleen letters in")]
        public string Achternaam { get; set; }

        [RegularExpression(@"[A-z]{1,10}", ErrorMessage = "Vul alleen letters in")]
        public string Tussenvoegsel { get; set; }

        [Required(ErrorMessage = "Email is een verplicht veld")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter proper contact details.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Straatnaam is een verplicht veld")]
        [RegularExpression(@"[A-z0-9' ']{1,70}", ErrorMessage = "Please enter proper contact details.")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "Huisnummer is een verplicht veld")]
        [RegularExpression(@"[0-9]{1,}", ErrorMessage = "Vul een geldig huisnummer in")]
        public int Huisnummer { get; set; }

        [RegularExpression(@"[A-z]{1}", ErrorMessage = "Vul een geldige toevoeging in")]
        public string Toevoeging { get; set; }

        [Required(ErrorMessage = "Postcode is een verplicht veld")]
        [RegularExpression(@"[0-9]{4}[A-z]{2}", ErrorMessage = "Vul een geldige postcode in")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Plaats is een verplicht veld")]
        [RegularExpression(@"[A-z]{1,50}", ErrorMessage = "Vul alleen letters in")]
        public string Plaats { get; set; }

        public bool Actief { get; set; }
        public string NonActiefReden { get; set; }

        //Student specifiek
        [Required]
        [RegularExpression(@"[0-9]{7}", ErrorMessage = "Een geldig studentummer bestaat uit 7 nummers")]
        public string StudentNummer { get; set; }

        [Required]
        [RegularExpression(@"[A-z]{1,50}", ErrorMessage = "Vul alleen letters in")]
        public string Opleiding { get; set; }

        [Required]
        [RegularExpression(@"[1-4]{1,50}", ErrorMessage = "Kies uit niveau 1 t/m 4")]
        public Nullable<int> Opleidingsniveau { get; set; }
        

        //Begeleider specifiek
        [Required]
        public Nullable<int> Bedrijf { get; set; }

        //Docent specifiek
        [Required]
        [RegularExpression(@"[A-z0-9]{1,50}", ErrorMessage = "Vul een geldig medewerkerID in")]
        public string MedewerkerID { get; set; }

        public virtual Bedrijf Bedrijf1 { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<OpleidingPersoon> OpleidingPersoon { get; set; }
        public virtual ICollection<Stage> Stage { get; set; }
        public virtual ICollection<Stage> Stage1 { get; set; }
        public virtual ICollection<Stage> Stage2 { get; set; }
        public virtual Role Role { get; set; }

    }

}
