using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdOrder { get; set; }
        public string SystC3 { get; set; }
        public string LogC3 { get; set; }
        public int? IdPayer { get; set; }
        public int? IdConsignee { get; set; }
        public DateTime? DTDelivery { get; set; }
        public DateTime DTReceived { get; set; }
        public DateTime? DTAdoption { get; set; }
        public double ThicknessMm { get; set; }
        public double WidthMm { get; set; }
        public double LengthMm { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string? StatusOrder { get; set; }
        public string? Mark { get; set; }
        public int? IdQuaCertificate { get; set; }
        public string? AccessStandart { get; set; }
        public string? NameStorage { get; set; }
    }
}