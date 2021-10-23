using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Models.ViewModels;

namespace Tasks.Application.IServices
{
    public interface IBaseService<TEntityVM> where TEntityVM : class
    {
        Task Create(TEntityVM model, string userId = null);
        Task Update(TEntityVM model, string userId = null);
        Task Delete(int id, string userId = null);
        Task<ListResultVM<TEntityVM>> GetAll(PagingVM model);
        Task<ListResultVM<TEntityVM>> GetAll();
        Task<TEntityVM> GetById(int id);
    }
}
