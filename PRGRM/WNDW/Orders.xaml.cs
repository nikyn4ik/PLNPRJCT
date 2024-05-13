using PRGRM.MDLS;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace PRGRM.WNDW
{
    public partial class Orders : Window
    {
        //private readonly OrdersContext _context = new OrdersContext();
        public Orders()
        {
            InitializeComponent();
            LoadOrders();
        }
        private void LoadOrders()
        {
            // OGrid.ItemsSource = _context.Orders.ToList();
        }
        private void BSshipment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OGrid.SelectedItems.Count > 0)
                {
                    var selectedOrder = (Orders)OGrid.SelectedItem;
                    var shipment = new Shipment
                    {
                       // IdShipment = selectedOrder.IdOrder,
                       // Consignee = selectedOrder.NameConsignee,
                      //  DateOfShipments = selectedOrder.DateOfDelivery
                    };

                    //_context.Add(shipment);
                   // _context.SaveChanges();
                    LoadOrders();
                    MessageBox.Show("Заявка успешно отправлена в отгрузку!", "Severstal Infocom");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данная заявка уже отправлена в отгрузку", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BShipment(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}