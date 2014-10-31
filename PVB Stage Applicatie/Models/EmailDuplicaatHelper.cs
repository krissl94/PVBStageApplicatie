using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class EmailDuplicaatHelper
    {
        StageApplicatieEntities db = new StageApplicatieEntities();
        public bool bestaatEmail(Persoonsgegevens persoon)
        {
            try
            {
                bool BestaatEmail;
                if (db.Persoonsgegevens.Where(p => p.Email == persoon.Email).FirstOrDefault().Email != null)
                {
                    if (db.Persoonsgegevens.Where(p => p.Email == persoon.Email).FirstOrDefault().PersoonsgegevensID == persoon.PersoonsgegevensID)
                    {
                        BestaatEmail = false;
                    }
                    else
                    {
                        BestaatEmail = true;
                    }
                }
                else
                {
                    BestaatEmail = false;
                }

                if (BestaatEmail == false){
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Exception)
            {
                return true;
            }

    }
    }
}