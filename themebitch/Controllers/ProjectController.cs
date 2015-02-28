using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using themebitch.App_Start;
using themebitch.Models;

namespace themebitch.Controllers
{
    public class ProjectController : Controller
    {
        public int projctID { get; set; }
        public string iPath { get; set; }
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //
        // GET: /Project/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(string projtitle, string projdesc, string projprio, DateTime projdue, string projassoc1, string projassoc2, string projassoc3, string projassoc4, string projassoc5, HttpPostedFileBase UploadFile)
        {
            //upload file
            if (UploadFile != null && UploadFile.ContentLength > 0)
                try
                {
                    string filename = Path.GetFileName(UploadFile.FileName);
                    iPath = "Files/" + filename;
                    string path = Path.Combine(Server.MapPath("~/Files"),
                                               Path.GetFileName(UploadFile.FileName));
                    UploadFile.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    Response.Write("File uploaded successfully");

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    Response.Write("File uploaded unsuccessfully");
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
                Response.Write("You have not specified a file.");
            }



            //

            int informID = Convert.ToInt32(Session["id"]);
            string projstatus = "Open";


            Insert insertProject = new Insert();
            insertProject.InsertProject(projtitle, projdesc, projprio, projdue, projstatus, informID, iPath);


            string iQueryy = "SELECT TOP 1 * FROM Project ORDER BY ProjectID DESC ";

            using (SqlConnection connn = new SqlConnection(connString))
            {
                using (SqlCommand commm = new SqlCommand(iQueryy, connn))
                {
                    try
                    {
                        connn.Open();



                        using (SqlDataReader reder = commm.ExecuteReader())
                        {

                            while (reder.Read())
                            {
                                projctID = Convert.ToInt32(reder["ProjectID"]);

                            }


                        }


                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        connn.Close();
                    }
                }
            }

            if (projassoc1 != "")
            {
                Insert invite = new Insert();
                invite.Invite(projassoc1, projctID);
            }

            if (projassoc2 != "")
            {
                Insert invite = new Insert();
                invite.Invite(projassoc2, projctID);
            }

            if (projassoc3 != "")
            {
                Insert invite = new Insert();
                invite.Invite(projassoc3, projctID);
            }

            if (projassoc4 != "")
            {
                Insert invite = new Insert();
                invite.Invite(projassoc4, projctID);
            }

            if (projassoc5 != "")
            {
                Insert invite = new Insert();
                invite.Invite(projassoc5, projctID);

            }


            return View();
        }



        public ActionResult DisplayProjet(string id)
        {
            Project project = new Project();

            Information info = new Information();

            string iQuery = "SELECT * FROM Project WHERE ProjectID = @ProjectID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(iQuery, conn))
                {
                    comm.Parameters.AddWithValue("ProjectID", id);
                    try
                    {
                        conn.Open();

                        using (SqlDataReader rdr = comm.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                                project.ProjectTitle = rdr["ProjectTitle"].ToString();
                                project.DueDate = Convert.ToDateTime(rdr["DueDate"]);
                                project.ProjectStatus = rdr["ProjectStatus"].ToString();
                                project.DateAdded = Convert.ToDateTime(rdr["DateAdded"]);
                                project.InformationID = Convert.ToInt32(rdr["InformationID"]);
                                project.FileUploaded = rdr["FileUploaded"].ToString();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            //info

            string iQueryInfo = "SELECT * FROM Information WHERE InformationID = @InformationID";

            using (SqlConnection conns = new SqlConnection(connString))
            {
                using (SqlCommand comms = new SqlCommand(iQueryInfo, conns))
                {
                    comms.Parameters.AddWithValue("InformationID", project.InformationID);
                    try
                    {
                        conns.Open();

                        using (SqlDataReader rdr = comms.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                          
                                info.FirstName = rdr["FirstName"].ToString();
                                info.LastName = rdr["LastName"].ToString();
                                info.InformationID = Convert.ToInt32(rdr["InformationID"]);
                           
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        conns.Close();
                    }
                }
            }

            Information_Project ip = new Information_Project();
            ip.info = info;
            ip.project = project;

            return View(ip);
        }


    }
}
