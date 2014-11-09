using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class EmailDuplicaatHelper
    {
        public bool bestaatEmail(Persoonsgegevens persoon)
        {
            StageApplicatieEntities db = new StageApplicatieEntities();

            try
            {
                return db.Persoonsgegevens.Where(p => p.Email == persoon.Email).FirstOrDefault() != null ? db.Persoonsgegevens.Where(p => p.Email == persoon.Email).FirstOrDefault().PersoonsgegevensID != persoon.PersoonsgegevensID : false;
            }
            catch (Exception Exception)
            {
                string var = Exception.ToString();
                return true;
            }
        }
    }
}