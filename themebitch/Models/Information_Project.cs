using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace themebitch.Models
{
    public class Information_Project
    {

        public Information info { get; set; }
        public Project project { get; set; }

        public List<Task> task { get; set; }
        public List<Information> information { get; set; }
    }
}