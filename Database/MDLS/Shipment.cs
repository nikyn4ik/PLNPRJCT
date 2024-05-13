using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Shipment
    {
        [Key] public int IdShipment { get; set; }
        public string Consignee { get; set; }
        public DateTime DateOfShipments { get; set; }
        public int ShipmentTotalAmountTons { get; set; }
        public float NumberOfShipmentsPerMonthTons { get; set; }
    }
}
