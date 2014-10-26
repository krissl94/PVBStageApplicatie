using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PVB_Stage_Applicatie.Models
{
    public static class Hashing
    {
        public static string HashString(string username, string password)
        {
            MD5 md5 = MD5.Create();

            byte[] SaltedPassword = Encoding.ASCII.GetBytes(Saltify(username, password));
            byte[] hash = md5.ComputeHash(SaltedPassword);

            StringBuilder hashString = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                hashString.Append(hash[i].ToString("x2"));
            }

            md5.Clear();
            return hashString.ToString();
        }

        private static string Saltify(string username, string password)
        {
            StringBuilder rawPassword = new StringBuilder();
            int length = username.Length > password.Length ? username.Length : password.Length;

            for (int i = 0; i < length; i++)
            {
                if (i < username.Length)
                {
                    rawPassword.Append(username[i]);
                }
                if (i < password.Length)
                {
                    rawPassword.Append(password[i]);
                }
            }

            return rawPassword.ToString();
        }
    }
}