using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class LoginForm
    {
        public string Gebruikersnaam { get; set; }

        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        public bool correct { get; set; }
    }
}