using Database;
using Database.MDLS;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.ADD
{
    public partial class ACertificates : Window
    {
        public ACertificates()
        {
            InitializeComponent();
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
        }

        private void BSave(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDateTime(DatePicker.Text) < DateTime.Today)
            {
                MessageBox.Show(".", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }

            int min, max;
            if (!int.TryParse(Min.Text, out min))
            {
                MessageBox.Show("Проверьте строку 'Минимальное значение'. Оно должно быть целым числом!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(Max.Text, out max))
            {
                MessageBox.Show("Проверьте строку 'Максимальное значение'. Оно должно быть целым числом!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(StandardPerMark.Text) ||
                string.IsNullOrEmpty(Manufacturer.Text) ||
                string.IsNullOrEmpty(ProductStandard.Text) ||
                string.IsNullOrEmpty(Units.Text) ||
                string.IsNullOrEmpty(PropertiesCert.Text))
            {
                MessageBox.Show("Проверьте, ввели ли Вы все значения.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var certificate = new Certificate
            {
                StandardPerMark = StandardPerMark.Text,
                Manufacturer = Manufacturer.Text,
                ProductStandard = ProductStandard.Text,
                DTCertificate = Convert.ToDateTime(DatePicker.Text), 
                Min = min,
                Max = max,
                Units = Units.Text,
                PropertiesCert = PropertiesCert.Text
            };

            using (var context = new ApplicationContext())
            {
                context.Certificate.Add(certificate);
                context.SaveChanges();
            }

            MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}