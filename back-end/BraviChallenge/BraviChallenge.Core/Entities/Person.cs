using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BraviChallenge.Core.Entities
{
    [Table(name: "persons")]
    public class Person
    {
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        public virtual ICollection<PersonContact> PersonContacts { get; set; }
    }
}
