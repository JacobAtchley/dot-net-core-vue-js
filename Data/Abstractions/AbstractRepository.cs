using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jatchley.Samples.Data.Interfaces;

namespace Jatchley.Samples.Data.Abstractions 
{
    public abstract class InMemoryRepository<TKey, TModel> : IRepository<TKey, TModel> where TModel : class
    {
        protected ICollection<TModel> Store {get;set;}

        protected abstract Func<TModel,TKey> GetIdFunc {get;}

        public InMemoryRepository(ICollection<TModel> models)
        {
            Store = models;
        }

        public Task<TModel> AddAsync(TModel model)
        {
            Store.Add(model);
            return Task.FromResult(model);
        }

        public Task DeleteAsync(TModel model)
        {
            Store.Remove(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TModel>> GetAllAsync()
        {
            return Task.FromResult(Store.AsEnumerable());
        }

        public Task<TModel> GetByIdAsync(TKey id)
        {
            return Task.FromResult(Store.FirstOrDefault(x => GetIdFunc(x).Equals(id)));
        }
    }
}