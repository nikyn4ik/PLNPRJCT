using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompany { get; set; }
        public string Name { get; set; }
        public ICollection<Storage> Storage { get; set; }
        public ICollection<Consignee> Consignee { get; set; }
    }
}
