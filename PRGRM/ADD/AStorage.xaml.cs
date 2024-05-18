using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;

namespace PRGRM.ADD
{
    public partial class AStorage : Window
    {
        private readonly ApplicationContext _dbContext;
        private List<Company> _companies;

        public AStorage()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            _companies = _dbContext.Company.ToList();
            Company.ItemsSource = _companies;
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
        }

        private void BSaved(object sender, RoutedEventArgs e)
        {
            var selectedCompany = Company.SelectedItem as Company;

            if (selectedCompany != null)
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
                    Company = selectedCompany,
                    IdCompany = selectedCompany.IdCompany
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
                var selectedCompany = Company.SelectedItem as Company;
                if (selectedCompany != null)
                {
                    Company.Text = selectedCompany.Name;
                }
            }
        }
    }
}