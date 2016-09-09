using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jatchley.Samples.Models;
using Jatchley.Samples.Data.Interfaces;

namespace Jatchley.Samples.Data.Implementations
{

    public class PersonRepository : IPersonRepository
    {
        List<Person> _people = new List<Person>
        {
            new Person {
                FirstName = "Jacob",
                LastName = "Atchley",
                Id = Guid.NewGuid()
            },

            new Person {
                FirstName = "David",
                LastName = "Woodring",
                Id = Guid.NewGuid()
            }
        };
        public Task<IEnumerable<Person>> GetAllAsync()
        {

            return Task.FromResult(_people.AsEnumerable());
        }

        public Task<IEnumerable<Person>> GetByLastNameAsync(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Task.FromResult(_people.AsEnumerable());
            }

            var filter = _people
            .Where(x => x.LastName != null)
            .Where(x => x.LastName.StartsWith(lastName, StringComparison.CurrentCultureIgnoreCase))
            ;
            
            return Task.FromResult(filter);
        }

        public Task<Person> AddAsync(Person model)
        {
            model.Id = Guid.NewGuid();
            _people.Add(model);
            return Task.FromResult(model);
        }

        public Task<Person> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_people.FirstOrDefault(x => x.Id == id));
        }

        public Task DeleteAsync(Person model)
        {
            _people.Remove(model);
            return Task.FromResult(1);
        }
    }

}