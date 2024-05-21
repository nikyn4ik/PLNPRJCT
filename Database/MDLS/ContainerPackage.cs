using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class ContainerPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContainerPackage { get; set; }
        public string? TypeModel { get; set; }
        public string? MarkContainer { get; set; }
    }
}
