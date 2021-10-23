using AutoMapper;
using Employees.Application.IServices;
using Employees.Core.IRepositories;
using Employees.Models.Domains;
using Employees.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Employee> repository;

        public EmployeeService(IRepository<Employee> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ListResultVM<EmployeeVM>> GetAll(PagingVM model)
        {
            ListResultVM<EmployeeVM> result = new ListResultVM<EmployeeVM>();
            if (model != null)
            {
                var query = repository.Get();
                if (!string.IsNullOrWhiteSpace(model.Search))
                {
                    decimal salary = 0;
                    decimal.TryParse(model.Search,out salary);

                    query = query.Where(x => x.JobTitle.ToLower().StartsWith(model.Search.ToLower())

                    || x.Salary == salary);
                }
                List<Employee> entities = await query.Skip(model.SkipCount).Take(model.MaxResultCount).ToListAsync();
                result.Items = mapper.Map<List<EmployeeVM>>(entities);
                result.Count = result.Items.Count;
                result.TotalCount = result.Count;
                result.PagesCount = (int)Math.Ceiling((double)result.TotalCount / model.MaxResultCount);
                result.PageNumber = model.PageNumber;
            }
            return result;
        }

        public async Task<ListResultVM<EmployeeVM>> GetAll()
        {
            ListResultVM<EmployeeVM> result = new ListResultVM<EmployeeVM>();
            List<Employee> entities =(await repository.GetAll()).ToList();
            result.Items = mapper.Map<List<EmployeeVM>>(entities);
            result.Count = result.Items.Count;
            result.TotalCount = result.Count;
            return result;
        }

        public async Task<EmployeeVM> GetById(int id)
        {
            EmployeeVM result = ToDto(await repository.GetById(id));
            return result;
        }
        public async Task Create(EmployeeVM model, string userId = null)
        {
            var entity = ToDomain(model);
            await repository.Insert(entity);
            await repository.SaveChanges();
            model.Id = entity.Id;
        }
        public async Task Update(EmployeeVM model, string userId = null)
        {
            var entity = await repository.GetFirstOrDefault(x => x.Id == model.Id);
            entity.FName = model.FName;
            entity.LName = model.LName;
            entity.JobTitle = model.JobTitle;
            entity.Email = model.Email;
            entity.BirthDay = model.BirthDay;
            entity.Salary = model.Salary;
            repository.Update(entity, userId);
            await repository.SaveChanges();
        }
        public async Task Delete(int id, string userId = null)
        {
            await repository.Delete(id, userId);
            await repository.SaveChanges();
        }

        #region Helpers

        private EmployeeVM ToDto(Employee model)
        {
            if (model == null) return null;
            var result = mapper.Map<EmployeeVM>(model);

            return result;
        }
        private Employee ToDomain(EmployeeVM model)
        {
            if (model == null) return null;
            var result = mapper.Map<Employee>(model);
            return result;
        }

        #endregion
    }
}
