using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasks.Core.IRepositories;
using Tasks.Models.Domains;

namespace Tasks.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDBContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(AppDBContext _context)
        {
            this.context = _context;
            entities = context.Set<TEntity>();
        }
        public IQueryable<TEntity> Get()
        {
            return entities.Where(x => !x.IsDeleted);
        }

        /// <summary>
        /// Gets the GetAll entities based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = entities.Where(x => !x.IsDeleted);
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true)
        {
            IQueryable<TEntity> query = entities.Where(x => !x.IsDeleted);
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }
        public async Task<TEntity> GetById(object id) => await entities.FindAsync(id);
        public async System.Threading.Tasks.Task Insert(TEntity obj, string userId = null)
        {
            obj.CreationDate = DateTime.Now;
            await entities.AddAsync(obj);
        }
        public void Update(TEntity obj, string userId = null)
        {
            obj.ModificationDate = DateTime.Now;
            context.Entry(obj).State = EntityState.Modified;
        }
        public async System.Threading.Tasks.Task Delete(object id, string userId = null)
        {
            var entity = await entities.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = DateTime.Now;
                context.Entry(entity).State = EntityState.Modified;
            }
        }
        public void Delete(TEntity model, string userId = null)
        {
            if (model != null)
            {
                model.IsDeleted = true;
                model.DeletionDate = DateTime.Now;
                context.Entry(model).State = EntityState.Modified;
            }
        }
        public void DeleteRange(IEnumerable<TEntity> models, string userId = null)
        {
            if (models != null && models.Count() > 0)
            {
                foreach (var item in models)
                {
                    item.IsDeleted = true;
                    item.DeletionDate = DateTime.Now;
                    context.Entry(item).State = EntityState.Modified;
                }
            }
        }
        public async System.Threading.Tasks.Task HardDelete(object id)
        {
            var entity = await entities.FindAsync(id);
            if (entity != null)
                context.Entry(entity).State = EntityState.Deleted;
        }
        public void HardDelete(TEntity model)
        {
            if (model != null)
                context.Entry(model).State = EntityState.Deleted;
        }
        public void HardDeleteRange(IEnumerable<TEntity> models)
        {
            if (models != null && models.Count() > 0)
            {
                entities.RemoveRange(models);
            }
        }
        public async System.Threading.Tasks.Task SaveChanges() => await context.SaveChangesAsync();

    }
}
