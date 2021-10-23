using Employees.Models.ViewModels;
using Ideal.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ideal.Portal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly string ApiUrl;

        public EmployeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ApiUrl = configuration["ApiUrl"];
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeList(int pageNumber)
        {
            return ViewComponent("EmployeeList", new { pageNumber = pageNumber, maxResultCount = 10 });
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeList(PagingVM model)
        {
            ListResultVM<EmployeeVM> result = new ListResultVM<EmployeeVM>();
            result.Items = new List<EmployeeVM>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.PostAsJsonAsync("Employee/Getall",model);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<ListResultVM<EmployeeVM>>();
                }
            }
            return Json(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.PostAsJsonAsync("Employee/Insert", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new AjaxResultVM
                        {
                            Title = "Success",
                            Message = "Employee Added Successfully"
                        });
                    }
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            EmployeeVM model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("Employee/Get/" + id);
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<EmployeeVM>();
                }
            }

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.PostAsJsonAsync("Employee/Update", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new AjaxResultVM
                        {
                            Title = "Success",
                            Message = "Employee Updated Successfully"
                        });
                    }
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            EmployeeVM model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("Employee/Get/" + id);
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<EmployeeVM>();
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel<int> model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("Employee/Delete/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new AjaxResultVM
                    {
                        Title = "Success",
                        Message = "Employee Deleted Successfully"
                    });
                }
            }
            return PartialView(model);

            //return ViewComponent("ProductList", new { pageNumber = 0, maxResultCount = 10 });
        }

    }
}
