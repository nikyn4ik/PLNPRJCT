using Database;
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
                MessageBox.Show("Дата меньше текущей.", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }

            int min, max;
            if (!int.TryParse(Min.Text, out min))
            {
                MessageBox.Show("Проверьте строку 'Минимальное значение'. Оно должно быть целым числом!", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }

            if (!int.TryParse(Max.Text, out max))
            {
                MessageBox.Show("Проверьте строку 'Максимальное значение'. Оно должно быть целым числом!", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }

            var certificate = new Certificate
            {
                StandardPerMark = StandardPerMark.Text,
                Manufacturer = Manufacturer.Text,
                ProductStandard = ProductStandard.Text,
                DateAddCertificate = Convert.ToDateTime(DatePicker.Text),
                Min = min,
                Max = max,
                Units = Units.Text,
                properties_cert = properties_cert.Text
            };

            using (var context = new ApplicationContext())
            {
                context.Certificate.Add(certificate);
                context.SaveChanges();
            }

            MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK);
            this.Close();
        }
    }
}