using Database;
using Microsoft.EntityFrameworkCore;
using PRGRM.ADD;
using PRGRM.EDIT;
using System.Windows;
using System.Windows.Controls;

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
            ShipmentGrid.ItemsSource = GetShipmentsData();
        }
        public List<Database.Shipment> GetShipmentsData()
        {
            return _dbContext.Shipment.ToList();
        }

        private void BDelivery(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadShipmentsData();
        }

        private void Select(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = _dbContext.Shipment
                .Where(s =>
                    (s.DTShipments != null && s.DTShipments.ToString().ToLower().Contains(searchText)) ||
                    (s.ShipmentTotalAmountTons != null && s.ShipmentTotalAmountTons.ToString().Contains(searchText)) ||
                    (s.IdTransport != null && s.IdTransport.ToString().Contains(searchText)))
                .ToList();
            ShipmentGrid.ItemsSource = filteredData;
        }

        private void BEdit(object sender, RoutedEventArgs e)
        {
            EShipment addWindow = new EShipment(idOrder);
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {

        }

    }
}
