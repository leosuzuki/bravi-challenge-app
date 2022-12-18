using BraviChallenge.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BraviChallenge.Core.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPerson(int id);
        Task<int> SavePerson(Person person);
        Task<bool> UpdatePerson(int id, Person person);
        Task<bool> ExcludePerson(int id);
    }
}
