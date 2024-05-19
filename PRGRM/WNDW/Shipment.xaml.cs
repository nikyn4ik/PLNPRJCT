using PRGRM.EDIT;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;

namespace PRGRM.WNDW
{
    public partial class Shipment : Window
    {
        private readonly ApplicationContext _dbContext;
        private int IdOrder;

        public Shipment()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadShipmentsData();
        }
        private void LoadShipmentsData()
        {
            var shipmentData = (from shipment in _dbContext.Shipment
                               join order in _dbContext.Orders on shipment.IdOrder equals order.IdOrder
                               join storage in _dbContext.Storage on order.IdStorage equals storage.IdStorage
                               select new Database.MDLS.Shipment
                               {
                                   IdOrder = order.IdOrder,
                                   IdShipment = shipment.IdShipment,
                                   DTShipments = shipment.DTShipments,
                                   IdTransport = shipment.IdTransport,
                                   ShipmentTotalAmountTons = shipment.ShipmentTotalAmountTons,
                                   StorageName = storage.Name
                               }).ToList();

            ShipmentGrid.ItemsSource = shipmentData;
        }
        public List<Database.MDLS.Shipment> GetShipmentsData()
        {
            return _dbContext.Shipment.ToList();
        }

        private void BDelivery(object sender, RoutedEventArgs e)
        {
            var selectedShipment = ShipmentGrid.SelectedItem as Database.MDLS.Shipment;

            if (selectedShipment == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }

            if (CheckForEmptyFields(selectedShipment))
            {
                MessageBox.Show("Для выбранного заказа не заполнены данные. Отправка в доставку невозможна.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var idOrder = selectedShipment.IdOrder;

            var shipped = _dbContext.Delivery.Any(s => s.IdOrder == idOrder);
            if (shipped)
            {
                MessageBox.Show("Данный заказ уже был отправлен в доставку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var shipmentData = new Database.MDLS.Delivery { IdOrder = idOrder };

            _dbContext.Delivery.Add(shipmentData);
            _dbContext.SaveChanges();

            MessageBox.Show("Заказ успешно отправлен в доставку!", "Severstal Infocom");
            LoadShipmentsData();
        }


        private bool CheckForEmptyFields(Database.MDLS.Shipment shipment)
        {
            if (shipment.ShipmentTotalAmountTons == null ||
                shipment.IdTransport == null ||
                shipment.DTShipments == null)
            {
                return true;
            }

            return false;
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadShipmentsData();
        }

        private void Select(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = (from shipment in _dbContext.Shipment
                                join order in _dbContext.Orders on shipment.IdOrder equals order.IdOrder
                                join storage in _dbContext.Storage on order.IdStorage equals storage.IdStorage
                                where (shipment.DTShipments != null && shipment.DTShipments.ToString().ToLower().Contains(searchText)) ||
                                      (shipment.ShipmentTotalAmountTons != null && shipment.ShipmentTotalAmountTons.ToString().Contains(searchText)) ||
                                      (shipment.IdTransport != null && shipment.IdTransport.ToString().Contains(searchText)) ||
                                      storage.Name.ToLower().Contains(searchText)
                                select new
                                {
                                    IdOrder = order.IdOrder,
                                    IdShipment = shipment.IdShipment,
                                    DTShipments = shipment.DTShipments,
                                    ShipmentTotalAmountTons = shipment.ShipmentTotalAmountTons,
                                    StorageName = storage.Name
                                }).ToList();

            ShipmentGrid.ItemsSource = filteredData;
        }
        private void BEdit(object sender, RoutedEventArgs e)
        {
            if (ShipmentGrid.SelectedItem is Database.MDLS.Shipment selectedShipment)
            {
                var idOrder = selectedShipment.IdOrder;

                EShipment editWindow = new EShipment(idOrder);
                editWindow.Closed += AddWindow_Closed;
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
            }
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {

        }

    }
}
