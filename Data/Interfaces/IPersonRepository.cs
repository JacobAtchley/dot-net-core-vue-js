using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jatchley.Samples.Models;

namespace Jatchley.Samples.Data.Interfaces
{
    public interface IPersonRepository : IRepository<Guid,Person>
    {        
        Task<IEnumerable<Person>> GetByLastNameAsync(string lastName);
    }
}