using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkforceManagementClient.Models
{
    public class SkillViewModel
    {
        [Display(Name = "ID")]
        public int SkillID { get; set; }

        [Display(Name = "Skill Name")]
        public string SkillName { get; set; }

        [Display(Name = "Skill Description")]
        public string SkillDescription { get; set; }

        [Display(Name = "Date Of Creation")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime SkillDateOfCreation { get; set; }

        public bool Selected { get; set; }
    }
}