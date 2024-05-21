using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;

namespace PRGRM.EDIT
{
    public partial class EOrder : Window
    {
        private readonly ApplicationContext _dbContext;
        private List<Orders> _orders;
        private readonly int orderId;
        private readonly string fio;
        public EOrder(Orders selectedOrder)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            orderId = selectedOrder.IdOrder;
            LoadStorageData();
            InitializeOrderData();
        }
        private void InitializeOrderData()
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == orderId);

            if (order != null)
            {
                var company = _dbContext.Company.FirstOrDefault(c => c.IdCompany == order.IdCompany);

                if (company != null)
                {
                    Company.Text = company.NameCompany;
                }
                else
                {
                    MessageBox.Show("Компания не найдена.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
            var storage = _dbContext.Storage.FirstOrDefault(s => s.IdStorage == order.IdStorage);
            if (storage != null)
            {
                Storage.SelectedValue = storage.IdStorage;
            }
        }
        private void LoadStorageData()
        {
            var storages = _dbContext.Storage.ToList();
            Storage.ItemsSource = storages;
            Storage.DisplayMemberPath = "NameStorage";
            Storage.SelectedValuePath = "IdStorage";
        }
        private void BSaved(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Company.Text))
            {
                DateTime Adoption;
                if (!DateTime.TryParse(DatePicker.Text, out Adoption))
                {
                    MessageBox.Show("Введите корректную дату.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Adoption < DateTime.Today)
                {
                    MessageBox.Show("Дата не может быть меньше текущей даты.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var selectedStorage = Storage.SelectedItem as Storage;
                if (selectedStorage == null)
                {
                    MessageBox.Show("Выберите склад.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var selectedCompany = Company.Text;

                var orderToUpdate = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == orderId);
                if (orderToUpdate != null)
                {
                    orderToUpdate.IdStorage = selectedStorage.IdStorage;
                    orderToUpdate.DTAdoption = Adoption;
                    orderToUpdate.StatusOrder = "Заказ на выполнении";

                    _dbContext.SaveChanges();

                    MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
        }
    }
}