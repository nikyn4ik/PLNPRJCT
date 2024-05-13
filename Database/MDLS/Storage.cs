using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Storage
    {
        [Key] public int IdStorage { get; set; }
        public string NameStorage { get; set; }
        public string Address { get; set; }
        public string PhoneStorage { get; set; }
        public string Remainder { get; set; }
        public DateTime DateAddStorage { get; set; }
    }
}
