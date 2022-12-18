using BraviChallenge.Core.Enums;
using System.Collections.Generic;

namespace BraviChallenge.Application.Dtos.Requests
{
    public class CreatePersonRequest
    {
        public string Name { get; set; }
        public IEnumerable<CreateContact> Contacts { get; set; }
    }

    public class CreateContact
    {
        public ContactType ContactType { get; set; }
        public string Description { get; set; }
    }
}
