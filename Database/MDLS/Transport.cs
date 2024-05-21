using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Transport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransport { get; set; }
        public string? NameTransport { get; set; }
        [MaxLength(8)] public string? VehicleRegistration { get; set; }
    }
}
