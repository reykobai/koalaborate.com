using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace themebitch.Models
{
    public class Task_information
    {
        public Information_Project ip { get; set; }
        public List<Task> task { get; set; }
        public List<Information> informations { get; set; }
      
    }
}