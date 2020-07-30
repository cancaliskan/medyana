using System;

namespace Medyana.Application.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ProvideDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UsageRate { get; set; }
        public int ClinicId { get; set; }
    }
}