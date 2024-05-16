using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;

namespace PRGRM.ADD
{
    public partial class AStorage : Window
    {
        private readonly ApplicationContext _dbContext;
        private List<Consignee> _consignees;
        public AStorage()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            _consignees = _dbContext.Consignee.ToList();
            Company.ItemsSource = _consignees.Select(c => c.Company);
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
        }

        private void BSaved(object sender, RoutedEventArgs e)
        {
            var selectedConsignee = _consignees.FirstOrDefault(c => c.Company == Company.SelectedItem?.ToString());

            if (selectedConsignee != null)
            {
                string phone = Phone.Text;
                if (string.IsNullOrWhiteSpace(phone) || !phone.All(char.IsDigit))
                {
                    MessageBox.Show("Некорректный формат телефона.", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }

                DateTime dateAddStorage;
                if (!DateTime.TryParse(DatePicker.Text, out dateAddStorage))
                {
                    MessageBox.Show("Дата меньше текущей", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }

                var storage = new Storage
                {
                    Name = Name.Text,
                    Address = Address.Text,
                    Phone = phone,
                    FIOResponsible = FIOResponsible.Text,
                    DateAddStorage = dateAddStorage,
                    Company = Company.SelectedItem?.ToString()
                };
                _dbContext.Storage.Add(storage);
                _dbContext.SaveChanges();

                MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK);
                Close();
            }
            else
            {
                MessageBox.Show("Укажите грузоперевозчика.", "Severstal Infocom", MessageBoxButton.OK);
            }
        }


        private void Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Company.SelectedItem != null)
            {
                string selectedCompany = Company.SelectedItem.ToString();
                Company.Text = selectedCompany;
            }

        }
    }
}
