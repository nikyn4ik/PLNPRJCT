using Database;
using Database.MDLS;
using PRGRM.EDIT;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
            var deliveryData = (from delivery in _dbContext.Delivery
                                join order in _dbContext.Orders on delivery.IdOrder equals order.IdOrder
                                select new Database.MDLS.Delivery
                                {
                                    IdOrder = order.IdOrder,
                                    IdDelivery = delivery.IdDelivery,
                                    EarlyDelivery = delivery.EarlyDelivery,
                                    DTDelivery = delivery.DTDelivery,
                                    ProductName = order.Name
                                }).ToList();

            DeliveryGrid.ItemsSource = deliveryData;
            DeliveryGrid.Items.Refresh();
        }

        public List<Database.MDLS.Delivery> GetDelivData(ApplicationContext context)
        {
            return context.Delivery.ToList();
        }
        private void EDel_Closed(object sender, EventArgs e)
        {
            LoadDelivData();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadDelivData();
        }
        private void BEdit(object sender, RoutedEventArgs e)
        {
            if (DeliveryGrid.SelectedItem is Database.MDLS.Delivery selectedDeliv)
            {
                var idOrder = selectedDeliv.IdOrder;

                EDelivery editWindow = new EDelivery(idOrder);
                editWindow.Closed += AddWindow_Closed;
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredDeliv = (from deliv in _dbContext.Delivery
                                 join order in _dbContext.Orders on deliv.IdOrder equals order.IdOrder
                                 where (deliv.IdDelivery != 0 && deliv.IdDelivery.ToString().Contains(searchText)) ||
                                       (deliv.IdOrder != 0 && deliv.IdOrder.ToString().Contains(searchText)) ||
                                       (deliv.EarlyDelivery != null && deliv.EarlyDelivery.Contains(searchText)) ||
                                       (deliv.DTDelivery != null && deliv.DTDelivery.ToString().Contains(searchText)) ||
                                       (order.Name != null && order.Name.ToLower().Contains(searchText))
                                 select new
                                 {
                                     IdOrder = order.IdOrder,
                                     IdDelivery = deliv.IdDelivery,
                                     EarlyDelivery = deliv.EarlyDelivery,
                                     DTDelivery = deliv.DTDelivery,
                                     ProductName = order.Name
                                 }).ToList();

            DeliveryGrid.ItemsSource = filteredDeliv;
        }
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        private void BPDF(object sender, RoutedEventArgs e)
        {
           if (DeliveryGrid.SelectedItem is Database.MDLS.Delivery selectedDeliv)
            {
                PDFOUT(selectedDeliv.IdOrder);
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
            string imgPath = Path.Combine(projectRoot, "IMG", "SeverstalPDF.jpg");
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

                doc1.Add(new Paragraph("Информация о заказе", titleFont) { Alignment = Element.ALIGN_CENTER });
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($"Cист С3: {order.SystC3}", regularFont));
                doc1.Add(new Paragraph($"Лог С3: {order.LogC3}", regularFont));
                doc1.Add(new Paragraph($"Log C3: {order.LogC3}", regularFont));
                doc1.Add(new Paragraph($"Дата получения: {order.DTReceived}", regularFont));
                doc1.Add(new Paragraph($"Дата принятия: {order.DTAdoption}", regularFont));
                doc1.Add(new Paragraph($"Толщина (мм): {order.ThicknessMm}", regularFont));
                doc1.Add(new Paragraph($"Ширина (мм): {order.WidthMm}", regularFont));
                doc1.Add(new Paragraph($"Длина (мм): {order.LengthMm}", regularFont));
                doc1.Add(new Paragraph($"Наименование: {order.Name}", regularFont));
                doc1.Add(new Paragraph($"Марка: {order.Mark}", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (payer != null)
                {
                    doc1.Add(new Paragraph("Информация о заказчике", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"ФИО: {payer.FIOPayer}", regularFont));
                    doc1.Add(new Paragraph($"Телефон: {payer.PhoneP}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (company != null)
                {
                    doc1.Add(new Paragraph("Информация о компании", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Наименование: {company.NameCompany}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (consignee != null)
                {
                    doc1.Add(new Paragraph("Информация о грузоперевозчике", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"ФИО: {consignee.FIOConsignee}", regularFont));
                    doc1.Add(new Paragraph($"Телефон: {consignee.PhoneCons}", regularFont));
                    doc1.Add(new Paragraph($"Email: {consignee.Email}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (storage != null)
                {
                    doc1.Add(new Paragraph("Информация об складе", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Наименование: {storage.NameStorage}", regularFont));
                    doc1.Add(new Paragraph($"Адрес: {storage.Address}", regularFont));
                    doc1.Add(new Paragraph($"Телефон: {storage.Phone}", regularFont));
                    doc1.Add(new Paragraph($"Ответственное лицо: {storage.FIOResponsible}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (container != null)
                {
                    doc1.Add(new Paragraph("Информация о сборке", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Тип модели: {container.TypeModel}", regularFont));
                    doc1.Add(new Paragraph($"Контейнер: {container.MarkContainer}", regularFont));
                    doc1.Add(new Paragraph($"Дата сборки: {container.DTContainer}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (shipment != null)
                {
                    doc1.Add(new Paragraph("Информация об отгрузке", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Отгруженно (тонн): {shipment.ShipmentTotalAmountTons}", regularFont));
                    doc1.Add(new Paragraph($"Дата отгрузки: {shipment.DTShipments}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (transport != null)
                {
                    doc1.Add(new Paragraph("Информация о транспорте", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Транспорт: {transport.NameTransport}", regularFont));
                    doc1.Add(new Paragraph($"Номер: {transport.VehicleRegistration}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                if (delivery != null)
                {
                    doc1.Add(new Paragraph("Информация о доставке", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc1.Add(new Paragraph($" " + " ", regularFont));
                    doc1.Add(new Paragraph($"Ранняя доставка: {delivery.EarlyDelivery}", regularFont));
                    doc1.Add(new Paragraph($"Дата доставки: {delivery.DTDelivery}", regularFont));
                }
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($"Ф.И.О. получателя (разборчиво)      __________________        __________________", regularFont));
                doc1.Add(new Paragraph($" " + "                                                                          Ф.И.О                                подпись                      ", regularFont));

                MessageBox.Show("PDF документ успешно сохранён!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании PDF-файла: " + ex.Message, "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                doc1.Close();
            }
        }

    }
}