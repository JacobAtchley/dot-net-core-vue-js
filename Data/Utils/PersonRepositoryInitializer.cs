using System;
using System.Linq;
using System.Collections.Generic;
using Jatchley.Samples.Data.Implementations;
using Jatchley.Samples.Data.Interfaces;
using Jatchley.Samples.Models;

namespace Jatchley.Samples.Data.Utils 
{
    public static class PersonRepositoryInitializer
    {
        private static string[] _firstNames =
        {
            "Fox",
            "Dana",
            "Raphael",
            "Bob",
            "Sue",
            "Kenneth",
            "Jane",
            "Emmit",
        };

        private static string[] _lastNames =
        {
            "Mulder",
            "Scully",
            "Michelangelo",
            "Smith",
            "James",
            "Williams",
            "Stevens",
        };


        private static Random rand = new Random();

        private static string GetRandomString(string[] array)
        {
            return array[rand.Next(array.Length-1)];
        }

        private static IEnumerable<Person> GetPeople(int numberOfPeople)
        {
            for (var i = 0; i < numberOfPeople; i++)
            {
                var first = GetRandomString(_firstNames);
                var last = GetRandomString(_lastNames);

                yield return new Person
                {
                    Email = $"{first}.{last}@mail.com",
                    FirstName = first,
                    LastName = last,
                    Id = Guid.NewGuid()
                };
            }
        }

        public static IPersonRepository GetRepo(int numberOfPeople)
        {
            return new PersonRepository(GetPeople(numberOfPeople).ToList());
        }
    }
}