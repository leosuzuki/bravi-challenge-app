using BraviChallenge.Application.Dtos.Requests;
using BraviChallenge.Application.Dtos.Responses;
using BraviChallenge.Application.Interfaces;
using BraviChallenge.Core.Entities;
using BraviChallenge.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BraviChallenge.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonResponse>> GetPersons()
        {
            IEnumerable<PersonResponse> response = null;

            var persons = await _personRepository.GetPersons();

            if (persons != null && persons.Count() > 0)
            {
                response = persons.Select(p => new PersonResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();
            }

            return response;
        }

        public async Task<PersonResponse> GetPersonById(int id)
        {
            PersonResponse response = null;

            var person = await _personRepository.GetPerson(id);

            if (person != null)
            {
                response = new PersonResponse()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Contacts = person.PersonContacts.Select(s => new ContactResponse()
                    {
                        Id = s.Id,
                        ContactType = s.ContactType,
                        Description = s.Description
                    }).ToList()
                };
            }

            return response;
        }

        public async Task<int> CreatePerson(CreatePersonRequest request)
        {
            var person = new Person()
            {
                Name = request.Name,
                PersonContacts = request.Contacts.Select(s => new PersonContact()
                {
                    ContactType = s.ContactType,
                    Description = s.Description
                }).ToList()
             };

            var personId = await _personRepository.SavePerson(person);

            return personId;
        }

        public async Task<bool> UpdatePerson(int id, UpdatePersonRequest request)
        {
            var person = new Person()
            {
                Name = request.Name,
                PersonContacts = request.Contacts.Select(s => new PersonContact()
                {
                    ContactType = s.ContactType,
                    Description = s.Description
                }).ToList()
            };

            return await _personRepository.UpdatePerson(id, person);
        }

        public async Task<bool> ExcludePerson(int id) 
            => await _personRepository.ExcludePerson(id);
    }
}
