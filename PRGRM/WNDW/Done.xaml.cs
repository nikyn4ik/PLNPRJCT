using Database;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;

namespace PRGRM.WNDW
{
    public partial class Done : Window
    {
        private readonly ApplicationContext _dbContext;
        public Done()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadData();
        }

        private void LoadData()
        {
            var filteredOrders = (from order in _dbContext.Orders
                                  join delivery in _dbContext.Delivery
                                  on order.IdOrder equals delivery.IdOrder
                                  join payer in _dbContext.Payer
                                  on order.IdPayer equals payer.IdPayer into payerJoin
                                  from payer in payerJoin.DefaultIfEmpty()
                                  join consignee in _dbContext.Consignee
                                  on order.IdConsignee equals consignee.IdConsignee into consigneeJoin
                                  from consignee in consigneeJoin.DefaultIfEmpty()
                                  where order.StatusOrder == "Заказ выполнен" && delivery.DateOfDelivery.HasValue
                                  select new
                                  {
                                      IdOrder = order.IdOrder,
                                      Name = order.Name,
                                      PayerFIO = payer != null ? payer.FIO : string.Empty,
                                      ConsigneeFIO = consignee != null ? consignee.FIO : string.Empty,
                                      DateOfDelivery = delivery.DateOfDelivery.Value
                                  }).ToList();

            DocGrid.ItemsSource = filteredOrders;
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DocGrid.SelectedItem as dynamic;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ для открытия документации.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string documentationFolder = Path.Combine(projectRoot, "Documentation");

            string fileName = $"Заказ № {selectedOrder.IdOrder}.pdf";
            string pdfPath = Path.Combine(documentationFolder, fileName);

            if (File.Exists(pdfPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии документации: {ex.Message}", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Документация для выбранного заказа не найдена.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredOrders = (from order in _dbContext.Orders
                                      join delivery in _dbContext.Delivery
                                      on order.IdOrder equals delivery.IdOrder
                                      join payer in _dbContext.Payer
                                      on order.IdPayer equals payer.IdPayer into payerJoin
                                      from payer in payerJoin.DefaultIfEmpty()
                                      join consignee in _dbContext.Consignee
                                      on order.IdConsignee equals consignee.IdConsignee into consigneeJoin
                                      from consignee in consigneeJoin.DefaultIfEmpty()
                                      where order.StatusOrder == "Заказ выполнен"
                                            && delivery.DateOfDelivery.HasValue
                                            && (order.Name.ToLower().Contains(searchText)
                                                || payer.FIO.ToLower().Contains(searchText)
                                                || consignee.FIO.ToLower().Contains(searchText)
                                                || order.IdOrder.ToString().Contains(searchText)
                                                || delivery.DateOfDelivery.Value.ToString().Contains(searchText))
                                      select new
                                      {
                                          IdOrder = order.IdOrder,
                                          Name = order.Name,
                                          PayerFIO = payer != null ? payer.FIO : string.Empty,
                                          ConsigneeFIO = consignee != null ? consignee.FIO : string.Empty,
                                          DateOfDelivery = delivery.DateOfDelivery.Value
                                      }).ToList();
                DocGrid.ItemsSource = filteredOrders;
            }
            else
            {
                LoadData();
            }
        }
    }
}