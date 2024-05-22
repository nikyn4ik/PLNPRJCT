using Database;
using PRGRM.ADD;
using Database.MDLS;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.ComponentModel;

namespace PRGRM.WNDW
{
    public partial class Container : Window
    {
        private readonly ApplicationContext _dbContext;
        public Container()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadCont();
        }
        private void LoadCont()
        {
            using (var context = new ApplicationContext())
            {
                ContainerGrid.ItemsSource = GetContData(context);
                ContainerGrid.Items.Refresh();
            }
        }
        public List<Database.MDLS.Container> GetContData(ApplicationContext context)
        {
            return context.Container.ToList();
        }
        private void ECont_Closed(object sender, EventArgs e)
        {
            LoadCont();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadCont();
        }
        private void AEdit(object sender, RoutedEventArgs e)
        {
            var selectedContainer = ContainerGrid.SelectedItem as Database.MDLS.Container;
            if (selectedContainer == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var editWindow = new EContainer(selectedContainer);
                editWindow.Closed += ECont_Closed;
                editWindow.ShowDialog();
            }
        }
        private void BShipment(object sender, RoutedEventArgs e)
        {
            var selectedContainer = ContainerGrid.SelectedItem as Database.MDLS.Container;
            if (selectedContainer == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CheckForEmptyFields(selectedContainer))
            {
                MessageBox.Show("Для выбранного заказа не заполнены данные. Отправка в отгрузку невозможна.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var idOrder = selectedContainer.IdOrder;

            var shipped = _dbContext.Shipment.Any(s => s.IdOrder == idOrder);
            if (shipped)
            {
                MessageBox.Show("Данный заказ уже был отправлен в отгрузку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var shipment = new Database.MDLS.Shipment { IdOrder = idOrder };
            _dbContext.Shipment.Add(shipment);
            _dbContext.SaveChanges();

            MessageBox.Show("Заказ успешно отправлен в отгрузку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        private bool CheckForEmptyFields(Database.MDLS.Container container)
        {
            if (string.IsNullOrWhiteSpace(container.TypeModel) ||
               string.IsNullOrWhiteSpace(container.MarkContainer) ||
               container.DTContainer == null)
            {
                return true;
            }

            return false;
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = _dbContext.Container
                .Where(c => c.TypeModel.ToLower().Contains(searchText) ||
                            c.MarkContainer.ToLower().Contains(searchText) ||
                            c.DTContainer.ToString().ToLower().Contains(searchText))
                .ToList();
            ContainerGrid.ItemsSource = filteredData;
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {
            if (ContainerGrid.SelectedItem is Database.MDLS.Container selectedContainer) 
            {
                PDFOUT(selectedContainer.IdOrder);
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
                MessageBox.Show("Заказ не найден.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var container = _dbContext.Container.FirstOrDefault(c => c.IdOrder == order.IdOrder);

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string documentationFolder = Path.Combine(projectRoot, "Documentation");

            if (!Directory.Exists(documentationFolder))
            {
                Directory.CreateDirectory(documentationFolder);
            }

            string fileName = $"УпаковкаЗаказ № {order.IdOrder}.pdf";
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

                doc1.Add(new Paragraph("Информация об упаковке", titleFont) { Alignment = Element.ALIGN_CENTER });
                doc1.Add(new Paragraph($" " + " ", regularFont));
                doc1.Add(new Paragraph($"ID заказа: {order.IdOrder}", regularFont));
                doc1.Add(new Paragraph($"Cист С3: {order.SystC3}", regularFont));
                doc1.Add(new Paragraph($"Лог С3: {order.LogC3}", regularFont));
                doc1.Add(new Paragraph($"Толщина (мм): {order.ThicknessMm}", regularFont));
                doc1.Add(new Paragraph($"Ширина (мм): {order.WidthMm}", regularFont));
                doc1.Add(new Paragraph($"Длина (мм): {order.LengthMm}", regularFont));
                doc1.Add(new Paragraph($"Наименование: {order.Name}", regularFont));
                doc1.Add(new Paragraph($"Тип модели: {container.TypeModel}", regularFont));
                doc1.Add(new Paragraph($"Контейнер: {container.MarkContainer}", regularFont));
                doc1.Add(new Paragraph($"Дата сборки: {container.DTContainer}", regularFont));
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