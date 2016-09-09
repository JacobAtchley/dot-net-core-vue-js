using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jatchley.Samples.Models;

namespace Jatchley.Samples.Data.Interfaces
{

    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<IEnumerable<Person>> GetByLastNameAsync(string lastName);

        Task<Person> AddAsync(Person model);

        Task<Person> GetByIdAsync(Guid id);

        Task DeleteAsync(Person model);
    }

}