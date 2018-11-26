using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API_BoilerPlate.DAL.Entities;
using API_BoilerPlate.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API_BoilerPlate.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TestContext _dbContext;
        private bool _disposed = false;

        public Repository(TestContext dbContext)
        {
            _dbContext = dbContext;
        }


        public TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
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
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }


        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
            // var records = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            // return records.AsQueryable();

        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
               // .AsNoTracking()
               .FindAsync(id);
        }

        public async Task<TEntity> GetById(string id)
        {
            //return await _dbContext.Set<TEntity>()
            return await _dbContext.Set<TEntity>()
                //.AsNoTracking()
                .FindAsync(id);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                //.AsNoTracking()
                .FindAsync(id);
        }

        //public async Task Create(TEntity entity)
        public async Task<int> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task Update(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(TEntity entity)
        {
            //_dbContext.Set<TEntity>().Remove(entity);
            _dbContext.Set<TEntity>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public int Count()
        {
            return _dbContext.Set<TEntity>().Count();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}