using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Application.IServices;
using Tasks.Models.ViewModels;

namespace Tasks.Microservice.Controllers
{
    public class TaskController : ControllerBase
    {

        private readonly ITaskService service;

        public TaskController(ITaskService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await service.GetAll();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(TaskVM model)
        {
            await service.Create(model);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(TaskVM model)
        {
            await service.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await service.Delete(id);
            return Ok();
        }
    }
}
