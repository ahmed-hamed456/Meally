﻿using Meally.core;
using Meally.core.Entities;
using Meally.core.Repository.Contract;
using Meally.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repository;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repository.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repository.Add(key, repository);
            }
            return _repository[key] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompeleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}