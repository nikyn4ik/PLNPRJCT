using Database;
using PRGRM.ADD;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.WNDW
{
    public partial class Certificates : Window
    {

        private readonly ApplicationContext _dbContext;
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
                MessageBox.Show("Выберите строчку", "Severstal Infocom");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный сертификат из базы?", "Severstal Infocom", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Certificate.Attach(selectedCertificate);
                _dbContext.Certificate.Remove(selectedCertificate);
                _dbContext.SaveChanges();

                MessageBox.Show("Сертификат удалён.", "Severstal Infocom");
                LoadCertificatesData();
            }
        }
        private void outpdf(object sender, RoutedEventArgs e)
        {

        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = _dbContext.Certificate
                .Where(c => c.StandardPerMark.ToLower().Contains(searchText) ||
                            c.ProductStandard.ToLower().Contains(searchText) ||
                            c.Manufacturer.ToLower().Contains(searchText) ||
                            c.DateAddCertificate.ToString().ToLower().Contains(searchText))
                .ToList();

            CertificatesGrid.ItemsSource = filteredData;
        }
    }
}