using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }

    }
}