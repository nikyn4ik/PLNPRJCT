using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class TypeProduct
    {
        [Key] public int IdTypeProduct { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string ProductDescription { get; set; }
        public string SapProductCode { get; set; }
    }
}