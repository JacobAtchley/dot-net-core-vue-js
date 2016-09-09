using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jatchley.Samples.Models;
using Jatchley.Samples.Data.Interfaces;
using Jatchley.Samples.Data.Abstractions;

namespace Jatchley.Samples.Data.Implementations
{

    public class PersonRepository : InMemoryRepository<Guid,Person>, IPersonRepository
    {
        public PersonRepository(ICollection<Person> models) : base(models)
        {
        }

        protected override Func<Person, Guid> GetIdFunc {get;} = x => x.Id;

        public Task<IEnumerable<Person>> GetByLastNameAsync(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Task.FromResult(Store.AsEnumerable());
            }

            var filter = Store
            .Where(x => x.LastName != null)
            .Where(x => x.LastName.StartsWith(lastName, StringComparison.CurrentCultureIgnoreCase))
            ;
            
            return Task.FromResult(filter);
        }
    }
}