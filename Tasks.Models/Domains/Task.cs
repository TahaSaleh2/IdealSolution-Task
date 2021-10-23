using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models.Domains
{
    public class Task : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
