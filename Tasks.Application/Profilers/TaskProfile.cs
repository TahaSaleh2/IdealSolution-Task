using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Models.ViewModels;

namespace Tasks.Application.Profilers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Models.Domains.Task, TaskVM>();

            CreateMap<TaskVM, Models.Domains.Task>();
        }
    }
}
