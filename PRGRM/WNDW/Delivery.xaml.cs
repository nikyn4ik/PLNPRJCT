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
            var payer = _dbContext.Payer.FirstOrDefault(p => p.IdPayer == order.IdPayer);
            var company = _dbContext.Company.FirstOrDefault(c => c.IdCompany == order.IdCompany);
            var consignee = _dbContext.Consignee.FirstOrDefault(c => c.IdConsignee == order.IdConsignee);
            var storage = _dbContext.Storage.FirstOrDefault(s => s.IdStorage == order.IdStorage);
            var container = _dbContext.Container.FirstOrDefault(c => c.IdOrder == order.IdOrder);
            var shipment = _dbContext.Shipment.FirstOrDefault(s => s.IdOrder == order.IdOrder);
            var transport = _dbContext.Transport.FirstOrDefault(t => t.IdTransport == shipment.IdTransport);
            var delivery = _dbContext.Delivery.FirstOrDefault(d => d.IdOrder == order.IdOrder);

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

                Font font = FontFactory.GetFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                doc1.Add(new Paragraph("Order Information", font));

                doc1.Add(new Paragraph($"Order ID: {order.IdOrder}", font));
                doc1.Add(new Paragraph($"System C3: {order.SystC3}", font));
                doc1.Add(new Paragraph($"Log C3: {order.LogC3}", font));
                doc1.Add(new Paragraph($"Received Date: {order.DTReceived}", font));
                doc1.Add(new Paragraph($"Adoption Date: {order.DTAdoption}", font));
                doc1.Add(new Paragraph($"Thickness (mm): {order.ThicknessMm}", font));
                doc1.Add(new Paragraph($"Width (mm): {order.WidthMm}", font));
                doc1.Add(new Paragraph($"Length (mm): {order.LengthMm}", font));
                doc1.Add(new Paragraph($"Name: {order.Name}", font));
                doc1.Add(new Paragraph($"Mark: {order.Mark}", font));

                if (payer != null)
                {
                    doc1.Add(new Paragraph("Payer Information", font));
                    doc1.Add(new Paragraph($"Name: {payer.FIO}", font));
                    doc1.Add(new Paragraph($"Phone: {payer.phone}", font));
                }

                if (company != null)
                {
                    doc1.Add(new Paragraph("Company Information", font));
                    doc1.Add(new Paragraph($"Name: {company.Name}", font));
                }

                if (consignee != null)
                {
                    doc1.Add(new Paragraph("Consignee Information", font));
                    doc1.Add(new Paragraph($"Name: {consignee.FIO}", font));
                    doc1.Add(new Paragraph($"Phone: {consignee.phone}", font));
                    doc1.Add(new Paragraph($"Email: {consignee.email}", font));
                }

                if (storage != null)
                {
                    doc1.Add(new Paragraph("Storage Information", font));
                    doc1.Add(new Paragraph($"Name: {storage.Name}", font));
                    doc1.Add(new Paragraph($"Address: {storage.Address}", font));
                    doc1.Add(new Paragraph($"Phone: {storage.Phone}", font));
                    doc1.Add(new Paragraph($"Responsible Person: {storage.FIOResponsible}", font));
                }

                if (container != null)
                {
                    doc1.Add(new Paragraph("Container Information", font));
                    doc1.Add(new Paragraph($"Model Type: {container.TypeModel}", font));
                    doc1.Add(new Paragraph($"Container Mark: {container.MarkContainer}", font));
                    doc1.Add(new Paragraph($"Container Date: {container.DTContainer}", font));
                }

                if (shipment != null)
                {
                    doc1.Add(new Paragraph("Shipment Information", font));
                    doc1.Add(new Paragraph($"Total Shipment Amount (tons): {shipment.ShipmentTotalAmountTons}", font));
                    doc1.Add(new Paragraph($"Shipment Date: {shipment.DTShipments}", font));
                }

                if (transport != null)
                {
                    doc1.Add(new Paragraph("Transport Information", font));
                    doc1.Add(new Paragraph($"Name: {transport.Name}", font));
                    doc1.Add(new Paragraph($"Registration Number: {transport.VehicleRegistration}", font));
                }

                if (delivery != null)
                {
                    doc1.Add(new Paragraph("Delivery Information", font));
                    doc1.Add(new Paragraph($"Early Delivery: {delivery.EarlyDelivery}", font));
                    doc1.Add(new Paragraph($"Delivery Date: {delivery.DateOfDelivery}", font));
                }
                MessageBox.Show("PDF документ успешно сохранён!", "Severstal Infocom");
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

    }
}