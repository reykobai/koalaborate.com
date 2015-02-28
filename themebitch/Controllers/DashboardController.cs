using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using themebitch.Models;

namespace themebitch.Controllers
{
     
    public class DashboardController : Controller
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //
        // GET: /Dashboard/
        public ActionResult Index()
      {

        
            Project project = new Project();
           


            List<Project> projects = new List<Project>();
            //string userid = Session["id"].ToString();
            //string iQuery = "SELECT * FROM Project WHERE InformationID = @userid";

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlCommand comm = new SqlCommand(iQuery, conn))
            //    {
            //        comm.Parameters.AddWithValue("userid", userid);
            //        try
            //        {
            //            conn.Open();

            //            using (SqlDataReader rdr = comm.ExecuteReader())
            //            {
            //                //diri ang i.break nu? ang project value. any ffield.. aww sige2 :) waits :)
            //                while (rdr.Read())
            //                {
            //                    project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
            //                    project.ProjectTitle = rdr["ProjectTitle"].ToString();
            //                    project.DueDate = Convert.ToDateTime(rdr["DueDate"]);
            //                    project.ProjectStatus = rdr["ProjectStatus"].ToString();
            //                    project.DateAdded = Convert.ToDateTime(rdr["DateAdded"]);
            //                    project.ProjectPriority = rdr["ProjectPriority"].ToString();
            //                    project.InformationID = Convert.ToInt32(rdr["InformationID"]);
            //                    project.FileUploaded = rdr["FileUploaded"].ToString();


            //                    projects.Add(project);
                          
                               

            //                }


            //            }
            //        }
            //        catch (Exception)
            //        {
            //            throw;
            //        }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

        

            return View();
        }


	}
}