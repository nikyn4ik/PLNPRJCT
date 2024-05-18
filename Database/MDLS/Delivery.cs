using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Delivery
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdDelivery { get; set; }
        public int IdOrder { get; set; }
        public string? Consignee { get; set; }
        public DateTime? DateOfDelivery { get; set; }
    }
}
