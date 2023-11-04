using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        Task<TEntity> singleOrDefault(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> getAll();
        Task<IEnumerable<TEntity>> getAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<bool> Delete(int id);
        Task<bool> update(TEntity entity);
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
        public async Task<TEntity> singleOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return  await _dbSet.FirstOrDefaultAsync(predicate);
            
        }

        public async Task<IEnumerable<TEntity>> getAll()
        {
          return  await _dbSet.ToListAsync();
        }

        public async Task InsertEntityAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Commit();

        }

        public async Task<IEnumerable<TEntity>> getAll(Expression<Func<TEntity, bool>> predicate = null)
        {
           return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> update(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Commit();
        }
    }
}
