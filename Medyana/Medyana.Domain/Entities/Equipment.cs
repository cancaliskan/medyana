using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medyana.Domain.Entities
{
    public sealed class Equipment : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? ProvideDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0.00, 100, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal UsageRate { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}