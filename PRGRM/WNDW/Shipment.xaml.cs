using PRGRM.EDIT;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Identity.Client.Extensions.Msal;

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
                                    StorageName = storage.NameStorage
                                }).ToList();

            ShipmentGrid.ItemsSource = shipmentData;
            ShipmentGrid.Items.Refresh();
        }

        public List<Database.MDLS.Shipment> GetShipmentsData(ApplicationContext context)
        {
            return context.Shipment.ToList();
        }
        private void EShip_Closed(object sender, EventArgs e)
        {
            LoadShipmentsData();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadShipmentsData();
        }
        private void BDelivery(object sender, RoutedEventArgs e)
        {
            var selectedShipment = ShipmentGrid.SelectedItem as Database.MDLS.Shipment;

            if (selectedShipment == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CheckForEmptyFields(selectedShipment))
            {
                MessageBox.Show("Для выбранного заказа не заполнены данные. Отправка в доставку невозможна.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var idOrder = selectedShipment.IdOrder;

            var shipped = _dbContext.Delivery.Any(s => s.IdOrder == idOrder);
            if (shipped)
            {
                MessageBox.Show("Заказ уже отправлен в доставку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var shipmentData = new Database.MDLS.Delivery { IdOrder = idOrder };

            _dbContext.Delivery.Add(shipmentData);
            _dbContext.SaveChanges();

            MessageBox.Show("Заказ успешно отправлен в доставку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
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
                                      storage.NameStorage.ToLower().Contains(searchText)
                                select new
                                {
                                    IdOrder = order.IdOrder,
                                    IdShipment = shipment.IdShipment,
                                    DTShipments = shipment.DTShipments,
                                    ShipmentTotalAmountTons = shipment.ShipmentTotalAmountTons,
                                    StorageName = storage.NameStorage
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
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {
            if (ShipmentGrid.SelectedItem is Database.MDLS.Shipment selectedShipment)
            {
                PDFOUT(selectedShipment.IdOrder);
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void PDFOUT(int idOrder)
        {
            var order = _dbContext.Orders
        .FirstOrDefault(o => o.IdOrder == idOrder);

            if (order == null)
            {
                MessageBox.Show("Заказ не найден.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var storage = _dbContext.Storage.FirstOrDefault(s => s.IdStorage == order.IdStorage);
            var shipment = _dbContext.Shipment.FirstOrDefault(s => s.IdOrder == order.IdOrder);
            var transport = _dbContext.Transport.FirstOrDefault(t => t.IdTransport == shipment.IdTransport);

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string documentationFolder = Path.Combine(projectRoot, "Documentation");

            if (!Directory.Exists(documentationFolder))
            {
                Directory.CreateDirectory(documentationFolder);
            }

            string fileName = $"ОтгрузкаЗаказ № {order.IdOrder}.pdf";
            // string imgPath = Path.Combine(directory, "IMG", "SeverstalPDF.jpg");
            string imgPath = @"C:\Users\nikab\source\repos\PLNPRJCT\PRGRM\IMG\SeverstalPDF.jpg";
            string pdfPath = Path.Combine(documentationFolder, fileName);

            Document doc1 = new Document(PageSize.A4);

            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(pdfPath, FileMode.Create));
                doc1.Open();

                var img = iTextSharp.text.Image.GetInstance(imgPath);
                img.ScaleToFit(300f, 280f);
                img.SpacingBefore = 10f;
                img.SpacingAfter = 1f;
                img.Alignment = Element.ALIGN_CENTER;
                doc1.Add(img);

                Font titleFont = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 16);
                Font regularFont = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 12);

                doc1.Add(new Paragraph("Информация об отгрузке", titleFont) { Alignment = Element.ALIGN_CENTER });
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($"ID заказа: {order.IdOrder}", regularFont));
                doc1.Add(new Paragraph($"Cист С3: {order.SystC3}", regularFont));
                doc1.Add(new Paragraph($"Лог С3: {order.LogC3}", regularFont));
                doc1.Add(new Paragraph($"Наименование: {order.Name}", regularFont));
                doc1.Add(new Paragraph($"Наименование склада: {storage.NameStorage}", regularFont));
                doc1.Add(new Paragraph($"Адрес: {storage.Address}", regularFont));
                doc1.Add(new Paragraph($"Телефон: {storage.Phone}", regularFont));
                doc1.Add(new Paragraph($"Ответственное лицо: {storage.FIOResponsible}", regularFont));
                doc1.Add(new Paragraph($"Транспорт: {transport.NameTransport}", regularFont));
                doc1.Add(new Paragraph($"Номер: {transport.VehicleRegistration}", regularFont));
                doc1.Add(new Paragraph($"Отгруженно (тонн): {shipment.ShipmentTotalAmountTons}", regularFont));
                doc1.Add(new Paragraph($"Дата отгрузки: {shipment.DTShipments}", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($"Ф.И.О. получателя (разборчиво)      __________________        __________________", regularFont));
                doc1.Add(new Paragraph($" " + "                                                                          Ф.И.О                                подпись                      ", regularFont));

                MessageBox.Show("PDF документ успешно сохранён!", "Severstal Infocom");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании PDF-файла: " + ex.Message, "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                doc1.Close();
            }
        }

    }
}