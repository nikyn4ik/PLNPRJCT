using Database;
using Database.MDLS;
using PRGRM.EDIT;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.IO;
using Microsoft.EntityFrameworkCore;
using iTextSharp.xmp.impl.xpath;

namespace PRGRM.WNDW
{
    public partial class Delivery : Window
    {
        private readonly ApplicationContext _dbContext;
        private int IdOrder;
        public Delivery()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadDelivData();
        }
        private void LoadDelivData()
        {
            var deliveries = _dbContext.Delivery.ToList();
            var orders = _dbContext.Orders.ToList();

            var deliveryData = (from delivery in deliveries
                                join order in orders on delivery.IdOrder equals order.IdOrder
                                select new Database.MDLS.Delivery
                                {
                                    IdOrder = order.IdOrder,
                                    IdDelivery = delivery.IdDelivery,
                                    EarlyDelivery = delivery.EarlyDelivery,
                                    DateOfDelivery = delivery.DateOfDelivery,
                                    ProductName = order.Name
                                }).ToList();

            DeliveryGrid.ItemsSource = deliveryData;
        }

        public List<Database.MDLS.Delivery> GetDelivData()
        {
            return _dbContext.Delivery.ToList();
        }
        private void EditWindow_DataSaved(object sender, EventArgs e)
        {
            LoadDelivData();
        }

        private void BEdit(object sender, RoutedEventArgs e)
        {
            if (DeliveryGrid.SelectedItem is Database.MDLS.Delivery selectedDeliv)
            {
                var idOrder = selectedDeliv.IdOrder;

                EDelivery editWindow = new EDelivery(idOrder);
                editWindow.DataSaved += EditWindow_DataSaved;
                editWindow.Closed += AddWindow_Closed;
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
            }
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadDelivData();
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredDeliv = GetDelivData()
                    .Where(deliv =>
                        (deliv.IdDelivery != 0 && deliv.IdDelivery.ToString().Contains(searchText)) ||
                        (deliv.IdOrder != 0 && deliv.IdOrder.ToString().Contains(searchText)) ||
                        (deliv.EarlyDelivery != null && deliv.EarlyDelivery.Contains(searchText)) ||
                        (deliv.DateOfDelivery != null && deliv.DateOfDelivery.ToString().Contains(searchText)) ||
                        (deliv.ProductName != null && deliv.ProductName.Contains(searchText))
                    )
                    .ToList();
                DeliveryGrid.ItemsSource = filteredDeliv;
            }
            else
            {
                LoadDelivData();
            }
        }
        private void BPDF(object sender, RoutedEventArgs e)
        {
            if (DeliveryGrid.SelectedItem is Database.MDLS.Delivery selectedDelivery)
            {
                PDFOUT(selectedDelivery.IdOrder);
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
            }
        }
        private void PDFOUT(int idOrder)
        {
            var order = _dbContext.Orders
        .FirstOrDefault(o => o.IdOrder == idOrder);

            if (order == null)
            {
                MessageBox.Show("Заказ не найден.", "Ошибка");
                return;
            }
            var shipment = _dbContext.Shipment.FirstOrDefault(s => s.IdOrder == idOrder);

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string documentationFolder = Path.Combine(projectRoot, "Documentation");

            if (!Directory.Exists(documentationFolder))
            {
                Directory.CreateDirectory(documentationFolder);
            }

            string fileName = $"Заказ № {order.IdOrder}.pdf";
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

                Font font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                doc1.Add(new Paragraph("Информация о заказе", font));
                doc1.Add(new Paragraph($"ID: {order.IdOrder}", font));
                doc1.Add(new Paragraph($"Date Adoption: {order.DTAdoption}", font));
                //doc1.Add(new Paragraph($"Date Received: {order.DTReceived}", font));
                //doc1.Add(new Paragraph($"Date Container: {order.DTContainer}", font));
                //doc1.Add(new Paragraph($"Date Shipments: {shipment.DTShipments}", font));
                //doc1.Add(new Paragraph($"Date Delivery: {order.DateOfDelivery}", font));
                //doc1.Add(new Paragraph($"EarlyDelivery: {order.EarlyDelivery}", font));

                MessageBox.Show("PDF-документ сохранен", "Severstal Infocom");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании PDF-файла: " + ex.Message, "Severstal Infocom");
            }
            finally
            {
                doc1.Close();
            }
        }
        private void BExcel(object sender, RoutedEventArgs e)
        {

        }

    }
}