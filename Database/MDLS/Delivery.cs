using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.MDLS
{
    public class Delivery
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdDelivery { get; set; }
        public int IdOrder { get; set; }
        public string? EarlyDelivery { get; set; }
        public DateTime? DateOfDelivery { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
    }
}
