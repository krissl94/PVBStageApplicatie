using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PVB_Stage_Applicatie.Models
{
    public class ExcelHelper
    {
        StageApplicatieEntities db = new StageApplicatieEntities();


        public DataSet excelToDS(HttpPostedFileBase file, HttpServerUtilityBase Server)
        
        {
            try
            {
                
                DataSet ds = new DataSet();
                if (file.ContentLength > 0)
                {
                    string fileExtension =
                                         System.IO.Path.GetExtension(file.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/App_Data/uploads/") + file.FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        file.SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        if (dt == null)
                        {
                            return null;
                        }
                        var Sheet1 = dt.Rows[0].Field<string>("TABLE_NAME");
                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                        return ds;
                    }
                    else
                    {
                        //Verkeerd bestand
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                file.InputStream.Dispose();
                file.InputStream.Close();
            }
            return null;
        }

        public List<Persoonsgegevens> dataSetToStudent(DataSet ds)
        {
            List<Persoonsgegevens> studentlijst = new List<Persoonsgegevens>();
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;
                        studentlijst.Add(new Persoonsgegevens
                        {
                            Rol = 4,
                            Actief = true,
                            Voornaam = values[0].ToString(),
                            Achternaam = values[1].ToString(),
                            Tussenvoegsel = values[2].ToString(),
                            Email = values[3].ToString(),
                            Straat = values[4].ToString(),
                            Huisnummer = Convert.ToInt32(values[5]),
                            Toevoeging = values[6].ToString(),
                            Postcode = values[7].ToString(),
                            Plaats = values[8].ToString(),
                            StudentNummer = values[9].ToString(),
                            Opleiding = values[10].ToString()
                        });
                    }
                }
                return studentlijst;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String dataSetToNonActiefStudent(DataSet ds)
        {
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string studentNummer = row.ItemArray[0].ToString();
                        if (Regex.IsMatch(studentNummer.ToString(), "[0-9]{7}"))
                        {
                            foreach (Persoonsgegevens persoonsgegevens in db.Persoonsgegevens.Where(p => p.StudentNummer == studentNummer))
                            {
                                db.sp_PersoonUpdaten(persoonsgegevens.PersoonsgegevensID,
                                persoonsgegevens.Email, persoonsgegevens.Straat,
                                persoonsgegevens.Huisnummer, persoonsgegevens.Toevoeging,
                                persoonsgegevens.Postcode, persoonsgegevens.Plaats,
                                false, "Geslaagd");
                                //db.Entry(item).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            //db.Dispose();
                            //db = new StageApplicatieEntities();
                            return "Er zit een fout in de data";
                        }

                    }
                }
                //db.SaveChanges();
                return "Alle stagiaires zijn op non-actief gesteld.";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<CreateLoginViewModel> dataSetToDocent(DataSet ds)
        {
            List<CreateLoginViewModel> docentlijst = new List<CreateLoginViewModel>();
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;


                        docentlijst.Add(new CreateLoginViewModel
                            {
                                Docent = new Persoonsgegevens
                                {
                                    Rol = 2,
                                    Actief = true,
                                    Voornaam = values[0].ToString(),
                                    Achternaam = values[1].ToString(),
                                    Tussenvoegsel = values[2].ToString(),
                                    Email = values[3].ToString(),
                                    Straat = values[4].ToString(),
                                    Huisnummer = Convert.ToInt32(values[5]),
                                    Toevoeging = values[6].ToString(),
                                    Postcode = values[7].ToString(),
                                    Plaats = values[8].ToString(),
                                    MedewerkerID = values[9].ToString()
                                },
                                Login = new Login
                                {
                                    Gebruikersnaam = values[10].ToString().ToLower(),
                                    Wachtwoord = values[11].ToString()
                                }
                            });
                        

                    }
                }
                return docentlijst;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Persoonsgegevens> dataSetToBegeleider(DataSet ds)
        {
            List<Persoonsgegevens> begeleiderlijst = new List<Persoonsgegevens>();
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;
                            string bedrijfsnaam = values[9].ToString();
                            Bedrijf bedrijfToAdd = db.Bedrijf.Where(b => b.Naam == bedrijfsnaam).FirstOrDefault();
                            begeleiderlijst.Add(new Persoonsgegevens
                            {
                                Rol = 2,
                                Actief = true,
                                Voornaam = values[0].ToString(),
                                Achternaam = values[1].ToString(),
                                Tussenvoegsel = values[2].ToString(),
                                Email = values[3].ToString(),
                                Straat = values[4].ToString(),
                                Huisnummer = Convert.ToInt32(values[5]),
                                Toevoeging = values[6].ToString(),
                                Postcode = values[7].ToString(),
                                Plaats = values[8].ToString(),
                                Bedrijf = bedrijfToAdd.BedrijfID,
                            });
                    }
                }
                return begeleiderlijst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String dataSetToBedrijf(DataSet ds)
        {
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;
                        {
                            db.Bedrijf.Add(new Bedrijf
                            {
                                Naam = values[0].ToString(),
                                Actief = true,
                                NonActiefReden = null,
                                KvKNummer = values[1].ToString(),
                                Plaats = values[2].ToString(),
                                Straatnaam = values[3].ToString(),
                                Huisnummer = Convert.ToInt32(values[4]),
                                Toevoeging = values[5].ToString(),
                                Postcode = values[6].ToString(),
                                Telefoonnummer = values[7].ToString(),
                                Email = values[8].ToString()
                            });
                        }
                    }
                }
                db.SaveChanges();
                return "Alle begeleiders zijn toegevoegd.";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String dataSetToKoppeling(DataSet ds, PVB_Stage_Applicatie.Models.Periode periode)
        {

            try
            {

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;

                        string persoonId = values[0].ToString();
                        string docentId = values[1].ToString();
                        string begeleiderId = values[2].ToString();
                        int periodeId = periode.Periode1;
                        Periode periodeToAdd = db.Periode.Where(p => p.Periode1 == periodeId).FirstOrDefault();
                        Persoonsgegevens StudentToAdd = db.Persoonsgegevens.Where(s => s.StudentNummer == persoonId).FirstOrDefault();
                        Persoonsgegevens DocentToAdd = db.Persoonsgegevens.Where(d => d.MedewerkerID == docentId).FirstOrDefault();
                        Persoonsgegevens BegeleiderToAdd = db.Persoonsgegevens.Where(b => b.Email == begeleiderId).FirstOrDefault();
                        db.Stage.Add(new Stage
                        {
                            Periode = periodeToAdd,
                            Student = StudentToAdd.PersoonsgegevensID,
                            Stagedocent = DocentToAdd.PersoonsgegevensID,
                            Stagebegeleider = BegeleiderToAdd.PersoonsgegevensID
                        });
                    }
                }
                db.SaveChanges();
                return "Alle koppelingen zijn toegevoegd";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }

    }
}