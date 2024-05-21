using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Defects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDefect { get; set; }
        public int IdOrder { get; set; }
        public string? Reasons { get; set; }
        public DateTime? DTProductSending { get; set; }
        public string? FIOSend { get; set; }

        [ForeignKey("IdOrder")]
        public Orders Orders { get; set; }
    }
}
