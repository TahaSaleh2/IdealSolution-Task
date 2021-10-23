using AutoMapper;
using Employees.Models.Domains;
using Employees.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Application.Profilers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeVM>();

            CreateMap<EmployeeVM, Employee>();
        }
    }
}
