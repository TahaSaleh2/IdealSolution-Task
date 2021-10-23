using Employees.Application.IServices;
using Employees.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Microservice.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> GetAll([FromBody]PagingVM model)
        {
            var result = await service.GetAll(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody]EmployeeVM model)
        {
            await service.Create(model);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody] EmployeeVM model)
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
