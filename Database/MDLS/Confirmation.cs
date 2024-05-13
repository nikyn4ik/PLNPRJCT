using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Confirmation
    {
        [Key] public int IdConfirmation { get; set; }
        public string ProductStandard { get; set; }
        public string DoneDelivery { get; set; }
    }
}