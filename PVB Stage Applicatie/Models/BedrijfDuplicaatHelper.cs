using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public static class BedrijfDuplicaatHelper
    {
        public static bool NaamIsUniek(Bedrijf bedrijf)
        {
            try
            {
                StageApplicatieEntities db = new StageApplicatieEntities();

                return db.Bedrijf.Where(b => b.Naam == bedrijf.Naam).FirstOrDefault() != null ? db.Bedrijf.Where(b => b.Naam == bedrijf.Naam).FirstOrDefault().BedrijfID == bedrijf.BedrijfID : true;
            }
            catch
            {
                return false;
            }
        }
    }
}