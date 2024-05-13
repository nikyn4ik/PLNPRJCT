using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Orders
    {
        [Key] public int IdOrder { get; set; }
        public string SystC3 { get; set; }
        public string LogC3 { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public DateTime DateOfEntrance { get; set; }
        public DateTime DateOfAdoption { get; set; }
        public double ThicknessMm { get; set; }
        public double WidthMm { get; set; }
        public double LengthMm { get; set; }
        public string NameProduct { get; set; }
        public string NameConsignee { get; set; }
        public string StatusOrder { get; set; }
        public int IdPayer { get; set; }
        public int IdConsignee { get; set; }
        public string Mark { get; set; }
        public int IdQuaCertificate { get; set; }
        public string AccessStandart { get; set; }
        public string NameStorage { get; set; }
    }
}