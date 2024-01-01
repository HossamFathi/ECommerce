using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace DataBaseLayer
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        Task SaveChangesAsync();
    }

    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        Task InsertEntityAsync(TEntity entity);

        Task<TEntity> SingleOrDefaultAsync(
              Expression<Func<TEntity, bool>> predicate = null,
              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
              int index = 0,
              int size = 20,
              bool enableTracking = true,
              CancellationToken cancellationToken = default,
              bool ignoreQueryFilters = false);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IPaginate<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 100000,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);
        Task<bool> Delete(int id);
        Task<bool> update(TEntity entity);
        Task<IPaginate<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int index = 0, int size = 20, bool enableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false) where TResult : class;
    }

    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ECommerceDB _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(ECommerceDB dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<bool> Delete(int id)
        {
            var Entity = await _dbSet.FindAsync(id);
            if (Entity == null)
                return false;
            _dbSet.Remove(Entity);
            return await Commit();
        }
        public async Task<bool> Commit()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public Task<TEntity> SingleOrDefaultAsync(
             Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
             int index = 0,
             int size = 20,
             bool enableTracking = true,
             CancellationToken cancellationToken = default,
             bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            if (orderBy != null)
                return orderBy(query).FirstOrDefaultAsync();

            return query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
          return  await _dbSet.ToListAsync();
        }

        public async Task InsertEntityAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Commit();

        }

        public Task<IPaginate<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
             int index = 0,
             int size = 100000,
             bool enableTracking = true,
             CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToPaginateAsync(index, size, 0, cancellationToken);
            return query.ToPaginateAsync(index, size, 0, cancellationToken);
        }



        public Task<IPaginate<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
            where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            if (orderBy != null)
                return orderBy(query).Select(selector).ToPaginateAsync(index, size, 0, cancellationToken);

            return query.Select(selector).ToPaginateAsync(index, size, 0, cancellationToken);
        }
        public async Task<bool> update(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Commit();
        }

        
    }
}
