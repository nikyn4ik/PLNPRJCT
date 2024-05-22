using Database;
using PRGRM.ADD;
using System.Windows;
using System.Windows.Controls;
using Database.MDLS;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PRGRM.WNDW
{
    public partial class Certificates : Window
    {

        private readonly ApplicationContext _dbContext;
        private int IdQuaCertificate;
        public Certificates()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadCertificatesData();
        }

        private void LoadCertificatesData()
        {
            CertificatesGrid.ItemsSource = GetCertificatesData();
        }
            public List<Certificate> GetCertificatesData()
            {
                return _dbContext.Certificate.ToList();
            }
        private void BAdd(object sender, RoutedEventArgs e)
        {
            ACertificates addWindow = new ACertificates();
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadCertificatesData();
        }
        private void BDelete(object sender, RoutedEventArgs e)
        {
            var selectedCertificate = CertificatesGrid.SelectedItem as Certificate;
            if (selectedCertificate == null)
            {
                MessageBox.Show("Выберите строчку", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный сертификат из базы?", "Severstal Infocom", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Certificate.Attach(selectedCertificate);
                _dbContext.Certificate.Remove(selectedCertificate);
                _dbContext.SaveChanges();

                MessageBox.Show("Сертификат удалён.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadCertificatesData();
            }
        }
        private void BPDF(object sender, RoutedEventArgs e)
        {
            if (CertificatesGrid.SelectedItem is Database.MDLS.Certificate selectedContainer)
            {
                PDFOUT(selectedContainer.IdQuaCertificate);
            }
            else
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void PDFOUT(int idQuaCertificate)
        {
            var certif = _dbContext.Certificate
        .FirstOrDefault(o => o.IdQuaCertificate == idQuaCertificate);

            if (certif == null)
            {
                MessageBox.Show("Сертификат не найден.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var container = _dbContext.Certificate.FirstOrDefault(c => c.IdQuaCertificate == certif.IdQuaCertificate);

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string documentationFolder = Path.Combine(projectRoot, "Documentation");

            if (!Directory.Exists(documentationFolder))
            {
                Directory.CreateDirectory(documentationFolder);
            }

            string fileName = $"Сертификат № {certif.IdQuaCertificate}.pdf";
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
                doc1.Add(new Paragraph($"ID сертификата: {certif.IdQuaCertificate}", regularFont));
                doc1.Add(new Paragraph($"Стандарт на марку: {certif.StandardPerMark}", regularFont));
                doc1.Add(new Paragraph($"Стандарт на продукцию: {certif.Manufacturer}", regularFont));
                doc1.Add(new Paragraph($"Минимальное значение: {certif.ProductStandard}", regularFont));
                doc1.Add(new Paragraph($"Максимальное значение: {certif.Min}", regularFont));
                doc1.Add(new Paragraph($"Единица измерения: {certif.Max}", regularFont));
                doc1.Add(new Paragraph($"Свойства: {certif.Units}", regularFont));
                doc1.Add(new Paragraph($"Изготовитель: {certif.PropertiesCert}", regularFont));
                doc1.Add(new Paragraph($"Дата добавления: {certif.DTCertificate}", regularFont));
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

        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = _dbContext.Certificate
                .Where(c => c.StandardPerMark.ToLower().Contains(searchText) ||
                            c.ProductStandard.ToLower().Contains(searchText) ||
                            c.Manufacturer.ToLower().Contains(searchText) ||
                            c.DTCertificate.ToString().ToLower().Contains(searchText))
                .ToList();

            CertificatesGrid.ItemsSource = filteredData;
        }
    }
}