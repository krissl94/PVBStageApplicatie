using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PVB_Stage_Applicatie.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            using (var usersContext = new StageApplicatieEntities())
            {
                var login = usersContext.Login.SingleOrDefault(u => u.Gebruikersnaam == username);
                var gebruiker = usersContext.Persoonsgegevens.SingleOrDefault(p => p.PersoonsgegevensID == login.Persoonsgegevens);

                if (login == null)
                    return false;
                return gebruiker.Rol != null && gebruiker.Role.RolNaam == roleName;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var usersContext = new StageApplicatieEntities())
            {
                int userID = Convert.ToInt32(username);
                var login = usersContext.Login.SingleOrDefault(u => u.Persoonsgegevens1.PersoonsgegevensID == userID);
                var gebruiker = login.Persoonsgegevens1;

                if (login == null)
                    return new string[] { };
                string[] temparray = new string[1];
                if(gebruiker.Role != null)
                temparray[0] = gebruiker.Role.RolNaam;
                return temparray;
                  
            }
        }

        // -- Snip --

        public override string[] GetAllRoles()
        {
            using (var usersContext = new StageApplicatieEntities())
            {
                return usersContext.Role.Select(r => r.RolNaam).ToArray();
            }
        }

        // -- Snip --

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}