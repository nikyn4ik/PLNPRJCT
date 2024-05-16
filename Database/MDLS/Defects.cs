﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.MDLS
{
    public class Defects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDefect { get; set; }
        public string IdOrder { get; set; }
        public string? Reasons { get; set; }
        public DateTime? ProductSending { get; set; }
        public string? FIO { get; set; }
    }
}
