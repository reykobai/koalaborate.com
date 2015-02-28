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
            string userid = session["id"].tostring();
            string iquery = "select * from project where informationid = @userid";

            using (sqlconnection conn = new sqlconnection(connstring))
            {
               using (sqlcommand comm = new sqlcommand(iquery, conn))
               {
                   comm.parameters.addwithvalue("userid", userid);
                   try
                   {
                       conn.open();

                       using (sqldatareader rdr = comm.executereader())
                       {
                           //diri ang i.break nu? ang project value. any ffield.. aww sige2 :) waits :)
                           while (rdr.read())
                           {
                               project.projectid = convert.toint32(rdr["projectid"]);
                               project.projecttitle = rdr["projecttitle"].tostring();
                               project.duedate = convert.todatetime(rdr["duedate"]);
                               project.projectstatus = rdr["projectstatus"].tostring();
                               project.dateadded = convert.todatetime(rdr["dateadded"]);
                               project.projectpriority = rdr["projectpriority"].tostring();
                               project.informationid = convert.toint32(rdr["informationid"]);
                               project.fileuploaded = rdr["fileuploaded"].tostring();


                               projects.add(project);
                          
                               

                           }


                       }
                   }
                   catch (exception)
                   {
                       throw;
                   }
                   finally
                   {
                       conn.close();
                   }
               }
            }

        

            return View(projects);
        }


	}
}