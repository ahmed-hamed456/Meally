using Meally.core.Entities.Identity;
using Meally.core.Repository.Contract;
using Meally.core.Specifications;
using Meally.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppIdentityDbContext _dbContext;

        public GenericRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetEntityAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncSpec(ISpecification<T> spec)
        {
           return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetEntityAsyncSpec(ISpecification<T> spec)
        {
           return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuary(_dbContext.Set<T>(), spec);
        }

        public async Task AddEntity(T entity)
          => await _dbContext.AddAsync(entity);

        public void UpdateEntity(T entity)
         =>  _dbContext.Update(entity);

        public void DeleteEntity(T entity)
         => _dbContext.Remove(entity);
    }
}
