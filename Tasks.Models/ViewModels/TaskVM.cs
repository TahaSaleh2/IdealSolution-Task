using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models.ViewModels
{
    public class TaskVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public DateTime? DeadlineDate { get; set; }
    }
}
