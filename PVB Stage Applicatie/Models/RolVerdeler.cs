using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public class RolVerdeler
    {
        private StageApplicatieEntities db = new StageApplicatieEntities();

        public String ZoekRolGebruiker(string id)
        {
            try
            {
                int newId = Convert.ToInt32(id);

                int rol = db.sp_ZoekRol(newId).FirstOrDefault() ?? (int)0;
                switch (rol)
                {
                    case (1):
                        return "Beheerder";
                    case (2):
                        return "Docent";
                    case (3):
                        return "Begeleider";
                    case (4):
                        return "Student";
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            };
        }
    }
}