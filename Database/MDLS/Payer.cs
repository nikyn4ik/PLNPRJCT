using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Payer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPayer { get; set; }
        public string FIO { get; set; }
        [MaxLength(12)]
        public string phone { get; set; }
    }
}
