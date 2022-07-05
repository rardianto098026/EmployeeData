using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeData.Models
{
    public class ListViewModels
    {
        public string Username { get; set; }
        public string No { get; set; }
        public string NIK { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string EmployeeName { get; set; }
        public string Entity { get; set; }
        public string EntityID { get; set; }
        public IEnumerable<SelectListItem> EntitySearch { get; set; }
        public string EntityIDSearch { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public IEnumerable<SelectListItem> DepartmentID { get; set; }
        public string Flag { get; set; }
        public string FlagID { get; set; }
        public string CreatedDate { get; set; }
        public string ApprovalDate { get; set; }
        public string ApprovalBy { get; set; }
    }
}