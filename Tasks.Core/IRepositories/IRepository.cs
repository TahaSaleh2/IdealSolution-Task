using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasks.Models.Domains;

namespace Tasks.Core.IRepositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Get();
        public Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         bool disableTracking = true);
        public Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true);
        Task<TEntity> GetById(object id);
        System.Threading.Tasks.Task Insert(TEntity obj, string userId = null);
        void Update(TEntity obj, string userId = null);
        System.Threading.Tasks.Task Delete(object id, string userId = null);
        void Delete(TEntity model, string userId = null);
        void DeleteRange(IEnumerable<TEntity> models, string userId = null);
        System.Threading.Tasks.Task HardDelete(object id);
        void HardDelete(TEntity model);
        void HardDeleteRange(IEnumerable<TEntity> models);
        System.Threading.Tasks.Task SaveChanges();

    }
}
