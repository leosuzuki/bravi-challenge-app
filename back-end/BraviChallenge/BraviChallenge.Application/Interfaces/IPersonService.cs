using BraviChallenge.Application.Dtos.Requests;
using BraviChallenge.Application.Dtos.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BraviChallenge.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonResponse>> GetPersons();
        Task<PersonResponse> GetPersonById(int id);
        Task<int> CreatePerson(CreatePersonRequest request);
        Task<bool> UpdatePerson(int id, UpdatePersonRequest request);
        Task<bool> ExcludePerson(int id);
    }
}
