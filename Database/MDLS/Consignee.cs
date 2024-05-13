using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MDLS
{
    public class Consignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConsignee{ get; set; }
        public string FIO { get; set; }
        public int? IdPayer { get; set; }
        [MaxLength(12)]
        public string phone { get; set; }
        public string? email { get; set; }
        public string Company { get; set; }
    }
}
