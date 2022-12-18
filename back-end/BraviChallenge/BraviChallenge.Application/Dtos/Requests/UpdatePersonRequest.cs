using BraviChallenge.Core.Enums;
using System.Collections.Generic;

namespace BraviChallenge.Application.Dtos.Requests
{
    public class UpdatePersonRequest
    {
        public string Name { get; set; }
        public IEnumerable<UpdateContact> Contacts { get; set; }
    }

    public class UpdateContact
    {
        public ContactType ContactType { get; set; }
        public string Description { get; set; }
    }
}
