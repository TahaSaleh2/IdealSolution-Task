using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Models.ViewModels
{
    public class EmployeeVM : BaseEntityVM
    {
        [Required(ErrorMessage = "First name is required")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
