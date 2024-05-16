using Database;
using Microsoft.EntityFrameworkCore;
using PRGRM.ADD;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.WNDW
{
    public partial class Shipment : Window
    {
        private readonly ApplicationContext _dbContext;
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
        .Where(s => s.Consignee.ToLower().Contains(searchText) ||
                    (s.DTShipments != null && s.DTShipments.ToString().ToLower().Contains(searchText)) ||
                    (s.ShipmentTotalAmountTons != null && s.ShipmentTotalAmountTons.ToString().Contains(searchText)) ||
                    (s.IdTransport != null && s.IdTransport.ToString().Contains(searchText)) ||
                    (s.NumberOfShipmentsPerMonthTons != null && s.NumberOfShipmentsPerMonthTons.ToString().Contains(searchText)))
        .ToList();
            ShipmentGrid.ItemsSource = filteredData;
        }

        private void BEdit(object sender, RoutedEventArgs e)
        {
            //EDelivery addWindow = new EDelivery();
            //addWindow.Closed += AddWindow_Closed;
            //addWindow.ShowDialog();
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {

        }

    }
}
