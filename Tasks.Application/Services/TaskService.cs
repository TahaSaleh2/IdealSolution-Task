using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Application.IServices;
using Tasks.Core.IRepositories;
using Tasks.Models.ViewModels;

namespace Tasks.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Models.Domains.Task> repository;

        public TaskService(IRepository<Models.Domains.Task> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ListResultVM<TaskVM>> GetAll(PagingVM model)
        {
            ListResultVM<TaskVM> result = new ListResultVM<TaskVM>();
            if (model != null)
            {
                var query = repository.Get();
                if (!string.IsNullOrWhiteSpace(model.Search))
                {

                    query = query.Where(x => x.Name.ToLower().StartsWith(model.Search.ToLower()));
                }
                List<Models.Domains.Task> entities = await query.Skip(model.SkipCount).Take(model.MaxResultCount).ToListAsync();
                result.Items = mapper.Map<List<TaskVM>>(entities);
                result.Count = result.Items.Count;
                result.TotalCount = result.Count;
                result.PagesCount = (int)Math.Ceiling((double)result.TotalCount / model.MaxResultCount);
                result.PageNumber = model.PageNumber;
            }
            return result;
        }

        public async Task<ListResultVM<TaskVM>> GetAll()
        {
            ListResultVM<TaskVM> result = new ListResultVM<TaskVM>();
            List<Models.Domains.Task> entities =(await repository.GetAll()).ToList();
            result.Items = mapper.Map<List<TaskVM>>(entities);
            result.Count = result.Items.Count;
            result.TotalCount = result.Count;
            return result;
        }

        public async Task<TaskVM> GetById(int id)
        {
            TaskVM result = ToDto(await repository.GetById(id));
            return result;
        }
        public async System.Threading.Tasks.Task Create(TaskVM model, string userId = null)
        {
            var entity = ToDomain(model);
            await repository.Insert(entity);
            await repository.SaveChanges();
            model.Id = entity.Id;
        }
        public async System.Threading.Tasks.Task Update(TaskVM model, string userId = null)
        {
            var entity = await repository.GetFirstOrDefault(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.EmployeeId = model.EmployeeId;
            repository.Update(entity, userId);
            await repository.SaveChanges();
        }
        public async System.Threading.Tasks.Task Delete(int id, string userId = null)
        {
            await repository.Delete(id, userId);
            await repository.SaveChanges();
        }

        #region Helpers

        private TaskVM ToDto(Models.Domains.Task model)
        {
            if (model == null) return null;
            var result = mapper.Map<TaskVM>(model);

            return result;
        }
        private Models.Domains.Task ToDomain(TaskVM model)
        {
            if (model == null) return null;
            var result = mapper.Map<Models.Domains.Task>(model);
            return result;
        }

        #endregion
    }
}
