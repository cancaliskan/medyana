using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Medyana.Domain.Entities
{
    public class Clinic : BaseEntity
    {
        public Clinic()
        {
            Equipments = new List<Equipment>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
    }
}