using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.MDLS
{
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdShipment { get; set; }
        public int IdOrder { get; set; }
        public DateTime? DTShipments { get; set; }
        public int? ShipmentTotalAmountTons { get; set; }
        public int? IdTransport { get; set; }
        [NotMapped]
        public string StorageName { get; set; }
    }
}
