using System;

namespace PRGRM.MDLS
{
    public class Shipment
    {
        public int IdShipment { get; set; }
        public string Consignee { get; set; }
        public DateTime DateOfShipments { get; set; }
        public int ShipmentTotalAmountTons { get; set; }
        public float NumberOfShipmentsPerMonthTons { get; set; }
    }
}
