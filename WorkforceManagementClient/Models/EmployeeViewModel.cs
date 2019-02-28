using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkforceManagementClient.Models
{
    public class EmployeeViewModel
    {
        [Display(Name = "ID")]
        public int EmployeeID { get; set; }

        [Display(Name = "Title")]
        public string EmployeeTitle { get; set; }

        [Display(Name = "First Name")]
        public string EmployeeFirstName { get; set; }

        [Display(Name = "Surname")]
        public string EmployeeSurname { get; set; }

        [Display(Name = "Social Security Number")]
        public string EmployeeSocialSecurityNumber { get; set; }

        [Display(Name = "Address")]
        public string EmployeeAddress { get; set; }

        [Display(Name = "Phone")]
        public string EmployeePhone { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHireDate { get; set; }

        public List<EmployeeViewModel> Skills { get; set; }
    }
}