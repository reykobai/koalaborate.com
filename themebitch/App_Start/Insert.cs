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

namespace themebitch.App_Start
{
    public class Insert
    {
        public string first { get; set; }
        public string last { get; set; }
        public string user { get; set; }
        public string iPath { get; set; }
        public string taskstatus { get; set; }

        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public void signup(string fname, string lname, string contact, string email, string username, string pass, string specialty, string imagePath)
        {


            string iQuery = "INSERT INTO Information (FirstName,LastName,ContactNumber,EmailAddress,Username,Pword,Specialty,ImagePath) VALUES (@fname,@lname,@contact,@email,@username,@pass,@specialty,@imagePath)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(iQuery, conn))
                {
                    comm.Parameters.AddWithValue("fname", fname);
                    comm.Parameters.AddWithValue("lname", lname);
                    comm.Parameters.AddWithValue("contact", contact);
                    comm.Parameters.AddWithValue("email", email);
                    comm.Parameters.AddWithValue("username", username);
                    comm.Parameters.AddWithValue("pass", pass);
                    comm.Parameters.AddWithValue("specialty", specialty);
                    comm.Parameters.AddWithValue("imagePath", imagePath);


                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        first = fname;
                        user = username;

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
        }

        //invite

        public void Invite(string projectassoc, int projectid)
        {
            string iQueryyy = "INSERT INTO Invite (EmailAdd, ProjectID) VALUES (@projassoc, @projctID)";

            using (SqlConnection connnn = new SqlConnection(connString))
            {
                using (SqlCommand commmm = new SqlCommand(iQueryyy, connnn))
                {
                    commmm.Parameters.AddWithValue("projassoc", projectassoc);
                    commmm.Parameters.AddWithValue("projctID", projectid);



                    try
                    {
                        connnn.Open();
                        commmm.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        connnn.Close();
                    }
                }
            }
        }

        public void InsertProject(string projtitle, string projdesc, string projprio, DateTime projdue, string projstatus, int informID, string UploadFile)
        {
            string iQuery = "INSERT INTO Project (ProjectTitle,DueDate,ProjectDescription,ProjectStatus,ProjectPriority,InformationID, FileUploaded) VALUES (@projtitle,@projdue,@projdesc,@projstatus,@projprio,@informID,@FileUploaded)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(iQuery, conn))
                {
                    comm.Parameters.AddWithValue("projtitle", projtitle);
                    comm.Parameters.AddWithValue("projdue", projdue);
                    comm.Parameters.AddWithValue("projdesc", projdesc);
                    comm.Parameters.AddWithValue("projstatus", projstatus);
                    comm.Parameters.AddWithValue("projprio", projprio);
                    comm.Parameters.AddWithValue("informID", informID);
                    comm.Parameters.AddWithValue("FileUploaded", UploadFile);


                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

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
        }


        //addtask

        public void InsertTask(string tasktitle, string taskdesc, string taskprio, DateTime taskdue,  string informID, int projectID, string taskUploadFile)
        {
            taskstatus = "Open";

            string iQuery = "INSERT INTO Task (TaskTitle,DueDate,TaskDescription,TaskStatus,TaskPriority,InformationID,ProjectID, FileUploaded) VALUES (@tasktitle,@taskdue,@taskdesc,@taskstatus,@taskprio,@informID,@projectID,@taskUploadFile)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(iQuery, conn))
                {
                    comm.Parameters.AddWithValue("tasktitle", tasktitle);
                    comm.Parameters.AddWithValue("taskdue", taskdue);
                    comm.Parameters.AddWithValue("taskdesc", taskdesc);
                    comm.Parameters.AddWithValue("taskstatus", taskstatus);
                    comm.Parameters.AddWithValue("taskprio", taskprio);
                    comm.Parameters.AddWithValue("informID", informID);
                  comm.Parameters.AddWithValue("projectID", projectID);
                    comm.Parameters.AddWithValue("taskUploadFile", taskUploadFile);


                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

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
        }

        public void UploadFile(string title, HttpPostedFileBase taskUploadFile)
        {

            //upload file


            if (taskUploadFile != null && taskUploadFile.ContentLength > 0)
                try
                {
                    string filename = Path.GetFileName(taskUploadFile.FileName);
                    iPath = "Files/" + title + "/" + filename;
                   // string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(taskUploadFile.FileName));
                    taskUploadFile.SaveAs(iPath);



                }
                catch (Exception)
                {

                }
            else
            {

            }

        }
    }
}