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
            fio = FIO;
            LoadOrders();
        }
        private void LoadOrders()
        {
            using (var context = new ApplicationContext())
            {
                OGrid.ItemsSource = GetOrdersData(context);
                OGrid.Items.Refresh();
            }
        }

        public List<Database.MDLS.Orders> GetOrdersData(ApplicationContext context)
        {
            var orders = context.Orders
                .Include(o => o.Storage)
                .Include(o => o.Company)
                .Select(o => new
                {
                    Order = o,
                    StorageName = context.Storage.Where(s => s.IdStorage == o.IdStorage).Select(s => s.NameStorage).FirstOrDefault(),
                    CertificateName = context.Certificate.Where(c => c.IdQuaCertificate == o.IdQuaCertificate).Select(c => c.StandardPerMark).FirstOrDefault()
                })
                .ToList()
                .Select(o => {
                    o.Order.StorageName = o.StorageName;
                    o.Order.CertificateName = o.CertificateName;
                    return o.Order;
                })
                .ToList();

            return orders;
        }

        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void BEdit(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OGrid.SelectedItem as Database.MDLS.Orders;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!selectedOrder.IdQuaCertificate.HasValue || !selectedOrder.DTAttestation.HasValue)
            {
                MessageBox.Show("Проверьте, заполнены ли данные.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedOrder.StatusOrder == "Заказ в браке")
            {
                MessageBox.Show("Невозможно редактировать заказ, находящийся в браке.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EOrder addWindow = new EOrder(selectedOrder);
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text;
            DateTime parsedDate;
            bool isDate = DateTime.TryParseExact(searchText,
                new[] { "dd.MM.yyyy", "d.M.yyyy", "dd.MM.yy", "d.M.yy" },
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out parsedDate);

            var filteredOrders = _dbContext.Orders
                .Include(o => o.Storage)
                .Include(o => o.Company)
                .Select(o => new
                {
                    Order = o,
                    StorageName = _dbContext.Storage.Where(s => s.IdStorage == o.IdStorage).Select(s => s.NameStorage).FirstOrDefault(),
                    CertificateName = _dbContext.Certificate.Where(c => c.IdQuaCertificate == o.IdQuaCertificate).Select(c => c.StandardPerMark).FirstOrDefault()
                })
                .Where(o =>
                    (o.Order.SystC3 != null && EF.Functions.Like(o.Order.SystC3, $"%{searchText}%")) ||
                    (o.Order.LogC3 != null && EF.Functions.Like(o.Order.LogC3, $"%{searchText}%")) ||
                    (o.Order.IdPayer != null && o.Order.IdPayer.ToString().Contains(searchText)) ||
                    (o.Order.IdCompany != null && o.Order.IdCompany.ToString().Contains(searchText)) ||
                    (o.Order.IdConsignee != null && o.Order.IdConsignee.ToString().Contains(searchText)) ||
                    (o.Order.IdStorage != null && o.Order.IdStorage.ToString().Contains(searchText)) ||
                    (!isDate && o.Order.DTReceived.ToString().Contains(searchText)) ||
                    (!isDate && o.Order.DTAdoption.HasValue && o.Order.DTAdoption.Value.ToString().Contains(searchText)) ||
                    (!isDate && o.Order.DTAttestation.HasValue && o.Order.DTAttestation.Value.ToString().Contains(searchText)) ||
                    (isDate && o.Order.DTReceived.Date == parsedDate.Date) ||
                    (isDate && o.Order.DTAdoption.HasValue && o.Order.DTAdoption.Value.Date == parsedDate.Date) ||
                    (isDate && o.Order.DTAttestation.HasValue && o.Order.DTAttestation.Value.Date == parsedDate.Date) ||
                    o.Order.ThicknessMm.ToString().Contains(searchText) ||
                    o.Order.WidthMm.ToString().Contains(searchText) ||
                    o.Order.LengthMm.ToString().Contains(searchText) ||
                    (o.Order.Name != null && EF.Functions.Like(o.Order.Name, $"%{searchText}%")) ||
                    (o.Order.StatusOrder != null && EF.Functions.Like(o.Order.StatusOrder, $"%{searchText}%")) ||
                    (o.Order.Mark != null && EF.Functions.Like(o.Order.Mark, $"%{searchText}%")) ||
                    (o.Order.IdQuaCertificate != null && o.Order.IdQuaCertificate.ToString().Contains(searchText)) ||
                    (o.CertificateName != null && EF.Functions.Like(o.CertificateName, $"%{searchText}%")) ||
                    (o.StorageName != null && EF.Functions.Like(o.StorageName, $"%{searchText}%"))
                )
                .ToList()
                .Select(o => {
                    o.Order.StorageName = o.StorageName;
                    o.Order.CertificateName = o.CertificateName;
                    return o.Order;
                })
                .ToList();

            OGrid.ItemsSource = filteredOrders;
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
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!selectedOrder.IdQuaCertificate.HasValue || !selectedOrder.DTAttestation.HasValue)
            {
                MessageBox.Show("Проверьте, заполнены ли данные.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var idOrder = selectedOrder.IdOrder;

            var isDefective = _dbContext.Defects.Any(d => d.IdOrder == idOrder);
            if (isDefective)
            {
                MessageBox.Show("Данный заказ находится в браке или не прошел аттестацию!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var packaged = _dbContext.Container.Any(p => p.IdOrder == idOrder);
            if (!packaged)
            {
                var contain = new Database.MDLS.Container { IdOrder = idOrder };
                _dbContext.Container.Add(contain);
                _dbContext.SaveChanges();

                MessageBox.Show("Заказ успешно отправлен в упаковку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Данный заказ уже был отправлен в упаковку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show($"Ранее был выбран сертификат: {certificate.StandardPerMark}", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("Данный заказ уже находится в браке!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Заказ не проходит по нормам аттестации!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
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
