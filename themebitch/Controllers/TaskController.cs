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

    public class TaskController : Controller
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        ProjectManagementSystemEntities db = new ProjectManagementSystemEntities();

        public int iID { get; set; }
        public string iPath { get; set; }
        //
        // GET: /Task/

        


         public ActionResult Index(string id)
        {

            List<Information> inform = new List<Information>();
           
            string iQueryy = "SELECT * FROM Team WHERE ProjectID=@id";

            using (SqlConnection connn = new SqlConnection(connString))
            {
                using (SqlCommand commm = new SqlCommand(iQueryy, connn))
                {
                    commm.Parameters.AddWithValue("id", id);
                    try
                    {
                        connn.Open();



                        using (SqlDataReader reder = commm.ExecuteReader())
                        {

                            while (reder.Read())
                            {
                                Team teams = new Team();


                               teams.ProjectID = Convert.ToInt32(reder["ProjectID"]);
                               teams.InformationID = Convert.ToInt32(reder["InformationID"]);


                                //information---------------------------

                               string InfoQuery = "SELECT * FROM Information WHERE InformationID=@id";

                               using (SqlConnection InfoConn = new SqlConnection(connString))
                               {
                                   using (SqlCommand InfoComm = new SqlCommand(InfoQuery, InfoConn))
                                   {
                                       InfoComm.Parameters.AddWithValue("id", teams.InformationID);
                                       try
                                       {
                                           InfoConn.Open();



                                           using (SqlDataReader Infordr = InfoComm.ExecuteReader())
                                           {

                                               while (Infordr.Read())
                                               {
                                                   Information information = new Information();


                                                   information.FirstName = Infordr["FirstName"].ToString();
                                                   information.LastName = Infordr["LastName"].ToString();
                                                   information.EmailAddress = Infordr["EmailAddress"].ToString();
                                                   information.InformationID = Convert.ToInt32(Infordr["InformationID"]);


                                                  


                                               }


                                           }


                                       }
                                       catch (Exception)
                                       {
                                           throw;
                                       }
                                       finally
                                       {
                                           InfoConn.Close();
                                       }
                                   }
                               }
                             
                 
                         //end info
                             
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

         
          

            return View(inform);

            
        }





         [HttpPost]
         public ActionResult Index(Project cities, int id, string tasktitle, string taskdesc, string taskprio, DateTime taskdue, string taskassign, HttpPostedFileBase taskUploadFile)
         {
             int informID = Convert.ToInt32(Session["id"]);
             string projstatus = "Open";
             Insert insertTask = new Insert();

             try
             {
                 insertTask.UploadFile(tasktitle, taskUploadFile);
                 iPath = insertTask.iPath;
             }
             catch (Exception)
             {
                 throw;
             }
             //insertProject.InsertProject(projtitle, projdesc, projprio, projdue, projstatus, informID, iPath);
             insertTask.InsertTask(tasktitle, taskdesc, taskprio, taskdue, taskassign, id, iPath);


             return RedirectToAction("DisplayProjet", "Project", new { id = id });
         }



         public ActionResult DisplayTask(string id)
         {

            Task task = new Task();

            Information info = new Information();
            List<Information> infoing = new List<Information>();
            List<Task> tasking = new List<Task>();

            string iQuery = "SELECT * FROM Task WHERE TaskID = @TaskID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(iQuery, conn))
                {
                    comm.Parameters.AddWithValue("TaskID", id);
                    try
                    {
                        conn.Open();

                        using (SqlDataReader rdr = comm.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                   task.InformationID = Convert.ToInt32(rdr["InformationID"]);
                                task.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                                task.TaskID = Convert.ToInt32(rdr["TaskID"]);
                                 task.TaskTitle = rdr["TaskTitle"].ToString();
                                task.DueDate = Convert.ToDateTime(rdr["DueDate"]);
                                task.TaskDescription = rdr["TaskDescription"].ToString();
                                task.TaskStatus = rdr["TaskStatus"].ToString();
                                task.DateAdded = Convert.ToDateTime(rdr["DateAdded"]);
                                task.TaskPriority = rdr["TaskPriority"].ToString();
                                task.FileUploaded =  rdr["FileUploaded"].ToString();
                          
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
                    comms.Parameters.AddWithValue("InformationID", task.InformationID);
                    try
                    {
                        conns.Open();

                        using (SqlDataReader rdrs = comms.ExecuteReader())
                        {
                            while (rdrs.Read())
                            {
                          
                                info.FirstName = rdrs["FirstName"].ToString();
                                info.LastName = rdrs["LastName"].ToString();
                                info.InformationID = Convert.ToInt32(rdrs["InformationID"]);
                           
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

            //task

             
           
               string taskQuery = "SELECT * FROM Subtask WHERE TaskID = @proj";
               
    using(SqlConnection taskconn = new SqlConnection(connString)) 
    {
        using (SqlCommand taskcomm = new SqlCommand(taskQuery, taskconn))
        {
            taskcomm.Parameters.AddWithValue("proj", task.TaskID);
            try
            {
                taskconn.Open();
                using (SqlDataReader taskrdr = taskcomm.ExecuteReader())
                {
                    while(taskrdr.Read())
                    {

                        Subtask subtask = new Subtask();

                     

                        subtask.SubtaskID = Convert.ToInt32(taskrdr["SubtaskID"]);
                        subtask.SubtaskTitle =  taskrdr["SubtaskTitle"].ToString();
                        subtask.DueDate = Convert.ToDateTime(taskrdr["DueDate"]);
                        subtask.SubtaskDescription =  taskrdr["SubtaskDescription"].ToString();
                        subtask.SubtaskStatus = taskrdr["SubtaskStatus"].ToString();
                        subtask.DateAdded = Convert.ToDateTime(taskrdr["DateAdded"]);
                        subtask.SubtaskPriority = taskrdr["SubtaskPriority"].ToString();
                        subtask.InformationID = Convert.ToInt32(taskrdr["InformationID"]);
                        subtask.TaskID =  Convert.ToInt32(taskrdr["TaskID"]);
                        subtask.FileUploaded = taskrdr["FileUploaded"].ToString();
                    

                        //information---------------------------

                        string InfoQuery = "SELECT * FROM Information WHERE InformationID=@id";

                        using (SqlConnection InfoConn = new SqlConnection(connString))
                        {
                            using (SqlCommand InfoComm = new SqlCommand(InfoQuery, InfoConn))
                            {
                                InfoComm.Parameters.AddWithValue("id", task.InformationID);
                                try
                                {
                                    InfoConn.Open();



                                    using (SqlDataReader Infordr = InfoComm.ExecuteReader())
                                    {

                                        while (Infordr.Read())
                                        {
                                            Information information = new Information();


                                            information.FirstName = Infordr["FirstName"].ToString();
                                            information.LastName = Infordr["LastName"].ToString();
                                            information.EmailAddress = Infordr["EmailAddress"].ToString();
                                            information.InformationID = Convert.ToInt32(Infordr["InformationID"]);


                                          
                                         


                                        }


                                    }


                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    InfoConn.Close();
                                }
                            }
                        }


                        //end info

                        tasking.Add(task);
                          

                    }
                }
            }
        catch (Exception)
            {
            throw;
        }
        finally
            {
                taskconn.Close();
            }
        }
    }

            Information_Project ip = new Information_Project();
            ip.info = info;
            ip.project = project;
            ip.task = tasking;
            ip.information = infoing;

            return View(ip);
        }
         }

    
    }
 }