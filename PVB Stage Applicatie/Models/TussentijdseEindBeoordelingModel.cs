using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class TussentijdseEindBeoordelingModel
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        public TussentijdseBeindeging TussentijdseBeeindegingModel { get; set; }
         
        public DbSet<RedenStudent> RedenStudent 
        { 
            get 
            {
                return db.RedenStudent;
            } 
        }
        public DbSet<RedenOrganisatie> RedenOrganisatie
        {
            get
            {
                return db.RedenOrganisatie;
            }
        }
        public DbSet<RedenOnderwijsinstelling> RedenOnderwijsinstelling
        {
            get
            {
                return db.RedenOnderwijsinstelling;
            }
        }

        //[RegularExpression(@"[0-9\,]", ErrorMessage = "Oi, niet hacken.")]
        public string RedenenOrganisatie { get; set; }

        //[RegularExpression(@"[0-9\,]", ErrorMessage = "Oi, niet hacken.")]
        public string RedenenOnderwijsinstelling { get; set; }

        //[RegularExpression(@"[0-9\,]", ErrorMessage = "Oi, niet hacken.")]
        public string RedenenStudent { get; set; }
    }
}