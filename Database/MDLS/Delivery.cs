using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Delivery
    {

        [Key] public int IdDelivery { get; set; }
        public string Consignee { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public string ProductStandard { get; set; }
    }
}
