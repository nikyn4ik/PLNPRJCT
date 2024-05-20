using System.Windows;
using System.Windows.Controls;
using Database;
using Microsoft.EntityFrameworkCore;
using PRGRM.ADD;
using PRGRM.EDIT;
using Database.MDLS;
using System.Linq;
using System;

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
            OGrid.ItemsSource = null;
            OGrid.ItemsSource = GetOrdersData();
        }

        public List<Database.MDLS.Orders> GetOrdersData()
        {
            var orders = _dbContext.Orders
                .Include(o => o.Storage)
                .Include(o => o.Company)
                .ToList();

            foreach (var order in orders)
            {
                var consignee = _dbContext.Consignee.FirstOrDefault(c =>
                    c.IdConsignee == order.IdConsignee);

                var storage = _dbContext.Storage.FirstOrDefault(s => s.IdStorage == order.IdStorage);
                if (storage != null)
                {
                    order.Storage = storage;
                    order.StorageName = storage.Name;
                }

                if (consignee != null)
                {
                    var consigneeStorage = _dbContext.Storage.FirstOrDefault(s =>
                        s.IdCompany == consignee.IdCompany);

                    if (consigneeStorage != null)
                    {
                        order.Storage = consigneeStorage;
                        order.IdStorage = consigneeStorage.IdStorage;
                        order.StorageName = consigneeStorage.Name;
                    }

                    var payer = _dbContext.Payer.FirstOrDefault(p =>
                        p.IdPayer == consignee.IdPayer);

                    if (payer != null)
                    {
                        order.Payer = payer;
                    }
                }
            }

            return orders;
        }


        private void EOrder_Closed(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void BEdit(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OGrid.SelectedItem as Database.MDLS.Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }
            if (!selectedOrder.IdQuaCertificate.HasValue || !selectedOrder.DTAttestation.HasValue)
            {
                MessageBox.Show("Проверьте, заполнили ли Вы данные.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedOrder.StatusOrder == "Заказ в браке")
            {
                MessageBox.Show("Невозможно редактировать заказ, находящийся в браке.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new EOrder(selectedOrder);
            editWindow.Closed += EOrder_Closed;
            editWindow.ShowDialog();
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
                        (order.IdCompany != null && order.IdCompany.ToString().Contains(searchText)) ||
                        (order.IdConsignee != null && order.IdConsignee.ToString().Contains(searchText)) ||
                        (order.IdStorage != null && order.IdStorage.ToString().Contains(searchText)) ||
                        order.DTReceived.ToString().Contains(searchText) ||
                        (order.DTAdoption != null && order.DTAdoption.ToString().Contains(searchText)) ||
                        order.ThicknessMm.ToString().Contains(searchText) ||
                        order.WidthMm.ToString().Contains(searchText) ||
                        order.LengthMm.ToString().Contains(searchText) ||
                        order.Name.Contains(searchText) ||
                        (order.StatusOrder != null && order.StatusOrder.Contains(searchText)) ||
                        (order.Mark != null && order.Mark.Contains(searchText)) ||
                        (order.IdQuaCertificate != null && order.IdQuaCertificate.ToString().Contains(searchText))
                    )
                    .ToList();
                OGrid.ItemsSource = filteredOrders;
            }
            else
            {
                LoadOrders();
            }
        }

        private void BDefect(object sender, RoutedEventArgs e)
        {
            Defect addWindow = new Defect();
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }

        private void BContainer(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OGrid.SelectedItem as Database.MDLS.Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }
            if (!selectedOrder.IdQuaCertificate.HasValue || !selectedOrder.DTAttestation.HasValue)
            {
                MessageBox.Show("Проверьте, заполнили ли Вы данные.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var idOrder = selectedOrder.IdOrder;

            var isDefective = _dbContext.Defects.Any(d => d.IdOrder == idOrder);
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
            var selectedOrder = OGrid.SelectedItem as Database.MDLS.Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var certificates = _dbContext.Certificate.ToList();
            bool isCertified = false;

            foreach (var certificate in certificates)
            {
                double thickness_mm = selectedOrder.ThicknessMm;
                double width_mm = selectedOrder.WidthMm;
                double length_mm = selectedOrder.LengthMm;

                if (thickness_mm >= certificate.Min && thickness_mm <= certificate.Max &&
                    width_mm >= certificate.Min && width_mm <= certificate.Max &&
                    length_mm >= certificate.Min && length_mm <= certificate.Max)
                {
                    isCertified = true;
                    break;
                }
            }
            if (selectedOrder.IdQuaCertificate.HasValue)
            {
                var certificate = _dbContext.Certificate
                    .FirstOrDefault(c => c.IdQuaCertificate == selectedOrder.IdQuaCertificate.Value);

                if (certificate != null)
                {
                    MessageBox.Show($"StandardPerMark: {certificate.StandardPerMark}", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (isCertified)
            {
                MessageBox.Show("Заказ проходит по нормам аттестации и соответствует сертификату.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                selectedOrder.StatusOrder = "Заказ на выполнении";
                var editWindow = new EAttestation(selectedOrder);
                editWindow.Closed += AddWindow_Closed;
                editWindow.ShowDialog();
            }
            else
            {
                var isAlreadyDefective = _dbContext.Defects.Any(d => d.IdOrder == selectedOrder.IdOrder);
                if (isAlreadyDefective)
                {
                    MessageBox.Show("Данный заказ уже находится в браке!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Заказ не проходит по нормам аттестации!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                    selectedOrder.StatusOrder = "Заказ в браке";
                    _dbContext.SaveChanges();
                    LoadOrders();
                    var defectWindow = new SendingDefect(selectedOrder, fio);
                    defectWindow.Closed += AddWindow_Closed;
                    defectWindow.ShowDialog();
                }
            }
        }
    }
}
