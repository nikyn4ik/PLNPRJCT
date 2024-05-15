using Database;
using PRGRM.ADD;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.WNDW
{
    public partial class Storage : Window
    {
        private readonly ApplicationContext _dbContext;
        public Storage()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadStoragesData();
        }

        private void LoadStoragesData()
        {
            StorageGrid.ItemsSource = GetStoragesData();
        }
        public List<Database.Storage> GetStoragesData()
        {
            return _dbContext.Storage.ToList();
        }
        private void BAdd(object sender, RoutedEventArgs e)
        {
            AStorage addWindow = new AStorage();
            addWindow.Closed += AddWindow_Closed;
            addWindow.ShowDialog();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadStoragesData();
        }

        private void BDelete(object sender, RoutedEventArgs e)
        {
            var selectedStorage = StorageGrid.SelectedItem as Database.Storage;
            if (selectedStorage == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данный склад из базы?", "Severstal Infocom", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Storage.Attach(selectedStorage);
                _dbContext.Storage.Remove(selectedStorage);
                _dbContext.SaveChanges();

                MessageBox.Show("Склад удалён.", "Severstal Infocom");
                LoadStoragesData();
            }
        }
        private void Select(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();

            var filteredData = _dbContext.Storage
                .Where(s => s.Name.ToLower().Contains(searchText) ||
                            s.Address.ToLower().Contains(searchText) ||
                            s.Phone.ToLower().Contains(searchText) ||
                            (s.FIOResponsible != null && s.FIOResponsible.ToLower().Contains(searchText)) ||
                            (s.DateAddStorage != null && s.DateAddStorage.ToString().ToLower().Contains(searchText)) ||
                            s.Company.ToLower().Contains(searchText))
                .ToList();
            StorageGrid.ItemsSource = filteredData;
        }
    }
}
