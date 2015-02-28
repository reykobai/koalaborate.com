using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using themebitch.Models;

namespace themebitch.Controllers
{

    public class TaskController : Controller
    {
        ProjectManagementSystemEntities db = new ProjectManagementSystemEntities();
        //
        // GET: /Task/

        
        public ActionResult Index()
        {

            ViewBag.GenderId = new SelectList(db.Teams, "taskassign", "InformationID");
            return View();
        }

        [HttpPost]
        public ActionResult index(string tasktitle, string taskdesc, string taskprio, DateTime taskdue, string taskassign, HttpPostedFileBase taskUploadFile)
        {


            return View();
        }


	}


}