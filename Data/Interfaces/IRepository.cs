
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jatchley.Samples.Data.Interfaces
{

    public interface IRepository<TKey, TModel> where TModel:class
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> AddAsync(TModel model);

        Task<TModel> GetByIdAsync(TKey id);

        Task DeleteAsync(TModel model);
    }
}
