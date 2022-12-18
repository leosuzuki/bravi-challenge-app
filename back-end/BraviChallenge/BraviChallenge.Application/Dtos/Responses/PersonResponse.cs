using BraviChallenge.Core.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BraviChallenge.Application.Dtos.Responses
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ContactResponse> Contacts { get; set; }
    }

    public class ContactResponse
    {
        public int Id { get; set; }
        public ContactType ContactType { get; set; }
        public string Description { get; set; }
    }
}
