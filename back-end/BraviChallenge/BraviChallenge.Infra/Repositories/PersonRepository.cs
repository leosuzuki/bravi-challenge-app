using BraviChallenge.Core.Entities;
using BraviChallenge.Core.Repositories;
using BraviChallenge.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BraviChallenge.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                var persons = _context.Persons.ToList().OrderByDescending(o => o.Id);

                return persons;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Person> GetPerson(int id)
        {
            try
            {
                var person = await _context.Persons
                    .Include(p => p.PersonContacts)
                    .SingleOrDefaultAsync(s => s.Id == id);

                return person;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> SavePerson(Person person)
        {
            using (IDbContextTransaction ts = _context.Database.BeginTransaction())
            {
                try
                {
                    int personId = 0;

                    var inserted = _context.Persons.Add(person);
                    _context.SaveChanges();

                    if (inserted != null)
                        personId = inserted.Entity.Id;

                    ts.Commit();

                    return personId;
                }
                catch (Exception e)
                {
                    ts.Rollback();
                    throw new Exception(e.Message);
                }
                finally
                {
                    ts.Dispose();
                }
            }
        }

        public async Task<bool> UpdatePerson(int id, Person person)
        {
            var entity = await GetPerson(id);

            using (IDbContextTransaction ts = _context.Database.BeginTransaction())
            {
                try
                {
                    bool success = false;

                    _context.PersonContacts.RemoveRange(entity.PersonContacts);
                    _context.SaveChanges();

                    entity.Name = person.Name;
                    entity.PersonContacts = person.PersonContacts.Select(s => new PersonContact()
                    {
                        PersonId = entity.Id,
                        ContactType = s.ContactType,
                        Description = s.Description
                    }).ToList();

                    _context.Persons.Update(entity);
                    success = _context.SaveChanges() > 0;

                    ts.Commit();

                    return success;
                }
                catch (Exception e)
                {
                    ts.Rollback();
                    throw new Exception(e.Message);
                }
                finally
                {
                    ts.Dispose();
                }
            }
        }

        public async Task<bool> ExcludePerson(int id)
        {
            Person entity = await GetPerson(id);

            using (IDbContextTransaction ts = _context.Database.BeginTransaction())
            {
                try
                {
                    bool success = false;

                    _context.Persons.Remove(entity);
                    success = _context.SaveChanges() > 0;

                    ts.Commit();

                    return success;
                }
                catch (Exception e)
                {
                    ts.Rollback();
                    throw new Exception(e.Message);
                }
                finally
                {
                    ts.Dispose();
                }
            }
        }
    }
}
