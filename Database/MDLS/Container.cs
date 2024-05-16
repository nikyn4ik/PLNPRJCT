using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Container
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContainer { get; set; }
        public int IdOrder { get; set; }
        public string? TypeModel { get; set; }
        public string? MarkContainer { get; set; }
        public DateTime? DTContainer { get; set; }

    }
}
