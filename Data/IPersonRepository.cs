using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using docker_web_test.Models;

namespace docker_web_test.Data
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