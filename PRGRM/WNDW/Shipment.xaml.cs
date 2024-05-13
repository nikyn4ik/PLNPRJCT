using Database;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace PRGRM.WNDW
{
    public partial class Shipment : Window
    {
        private readonly ApplicationContext _context;
        public Shipment()
        {
            InitializeComponent();
            _context = new ApplicationContext();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            //var searchText = pole.Text.Trim();
            //var shipments = _context.Shipments
            //    .Where(s => s.Consignee.Contains(searchText) || s.OtherProperty.Contains(searchText))
            //    .ToList();
            //ShipmentGrid.ItemsSource = shipments;
        }

        private void BGTDelivery(object sender, RoutedEventArgs e)
        {

        }

        private void B_Search(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void Pole_textchange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
