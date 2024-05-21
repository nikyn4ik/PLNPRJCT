using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
    
namespace Database.MDLS
{
    public class Consignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConsignee{ get; set; }
        public string FIOConsignee { get; set; }
        public int? IdPayer { get; set; }
        [MaxLength(12)]
        public string PhoneCons { get; set; }
        public string? Email { get; set; }
        public int IdCompany { get; set; }
        public Company Company { get; set; }
    }
}
