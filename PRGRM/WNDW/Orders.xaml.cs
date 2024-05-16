using System.Windows;
using System.Windows.Controls;
using Database;
using PRGRM.ADD;
using PRGRM.EDIT;

namespace PRGRM.WNDW
{
    public partial class Orders : Window
    {
        private readonly string fio;
        private readonly ApplicationContext _dbContext;
        private int IdOrder;
        public Orders(string FIO)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadOrders();
            fio = FIO;
        }
        private void LoadOrders()
        {
            OGrid.ItemsSource = GetOrdersData();
        }
        public List<Database.Orders> GetOrdersData()
        {
            return _dbContext.Orders.ToList();
        }
        private void BEdit(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OGrid.SelectedItem as Database.Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }
            else
            {
                var editWindow = new EOrder(selectedOrder);
                editWindow.Closed += AddWindow_Closed;
                editWindow.ShowDialog();
            }
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadOrders();
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredOrders = _dbContext.Orders
                    .Where(order =>
                        order.SystC3.Contains(searchText) ||
                        order.LogC3.Contains(searchText) ||
                        (order.IdPayer != null && order.IdPayer.ToString().Contains(searchText)) ||
                        (order.IdConsignee != null && order.IdConsignee.ToString().Contains(searchText)) ||
                        (order.DTDelivery != null && order.DTDelivery.ToString().Contains(searchText)) ||
                        order.DTReceived.ToString().Contains(searchText) ||
                        (order.DTAdoption != null && order.DTAdoption.ToString().Contains(searchText)) ||
                        order.ThicknessMm.ToString().Contains(searchText) ||
                        order.WidthMm.ToString().Contains(searchText) ||
                        order.LengthMm.ToString().Contains(searchText) ||
                        order.Name.Contains(searchText) ||
                        order.Company.Contains(searchText) ||
                        (order.StatusOrder != null && order.StatusOrder.Contains(searchText)) ||
                        (order.Mark != null && order.Mark.Contains(searchText)) ||
                        (order.IdQuaCertificate != null && order.IdQuaCertificate.ToString().Contains(searchText)) ||
                        (order.AccessStandart != null && order.AccessStandart.Contains(searchText)) ||
                        (order.NameStorage != null && order.NameStorage.Contains(searchText))
                    )
                    .ToList();
                OGrid.ItemsSource = filteredOrders;
            }
            else
            {
                LoadOrders();
            }
        }
        
        private void BDefects(object sender, RoutedEventArgs e)
        {
            EDefects addWindow = new EDefects();
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }
        private void BContainer(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OGrid.SelectedItem as Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }

            var idOrder = selectedOrder.IdOrder;

            var isDefective = _dbContext.Defects.Any(d => d.IdOrder == idOrder.ToString());
            if (isDefective)
            {
                MessageBox.Show("Данный заказ находится в браке или не прошел аттестацию!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadOrders();
                return;
            }

            var packaged = _dbContext.Container.Any(p => p.IdOrder == idOrder);
            if (!packaged)
            {
                var contain = new Database.MDLS.Container { IdOrder = idOrder };
                _dbContext.Container.Add(contain);
                _dbContext.SaveChanges();

                MessageBox.Show("Заказ успешно отправлен в упаковку!", "Severstal Infocom");
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Данный заказ уже был отправлен в упаковку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadOrders();
            }
        }
        private void BAttestation(object sender, RoutedEventArgs e)
        {

        }
    }
}