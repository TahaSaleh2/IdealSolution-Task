using Employees.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ideal.Portal.Views.Shared.Components.EmployeeList
{
    public class EmployeeListViewComponent : ViewComponent
    {
        private readonly IConfiguration configuration;
        private readonly string ApiUrl;

        public EmployeeListViewComponent(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ApiUrl = configuration["ApiUrl"];
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ListResultVM<EmployeeVM> result = new ListResultVM<EmployeeVM>();
            result.Items = new List<EmployeeVM>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("Employee/Getall");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<ListResultVM<EmployeeVM>>();
                }
            }
            return View(result);
        }
    }
}
