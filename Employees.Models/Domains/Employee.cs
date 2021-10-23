using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Models.Domains
{
    public class Employee : BaseEntity
    {

        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? BirthDay { get; set; }

    }
}
