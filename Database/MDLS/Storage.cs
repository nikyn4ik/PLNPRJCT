using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdStorage { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [MaxLength(12)] public string Phone { get; set; }
        public string? FIOResponsible { get; set; }
        public DateTime? DateAddStorage { get; set; }
        public string Company { get; set; }
    }
}
