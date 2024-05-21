using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.MDLS
{
    public class Certificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdQuaCertificate { get; set; }
        public string StandardPerMark { get; set; }
        public string Manufacturer { get; set; }
        public string ProductStandard { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Units { get; set; }
        public string? PropertiesCert { get; set; }
        
        public DateTime DTCertificate { get; set; }
    }
}