using BraviChallenge.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BraviChallenge.Core.Entities
{
    [Table("person_contacts")]
    public class PersonContact
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("contact_type")]
        public ContactType ContactType { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
