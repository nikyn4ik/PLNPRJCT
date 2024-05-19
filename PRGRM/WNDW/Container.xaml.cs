using Database;
using PRGRM.ADD;
using System.Windows;
using System.Windows.Controls;

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
            ContainerGrid.ItemsSource = GetContData();
        }
        public List<Database.MDLS.Container> GetContData()
        {
            return _dbContext.Container.ToList();
        }
        private void ECont_Closed(object sender, EventArgs e)
        {
            LoadCont();
        }
        private void AEdit(object sender, RoutedEventArgs e)
        {
            var selectedContainer = ContainerGrid.SelectedItem as Database.MDLS.Container;
            if (selectedContainer == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
                return;
            }
            else
            {
                var editWindow = new EContainer(selectedContainer);
                editWindow.Closed += ECont_Closed;
                editWindow.ShowDialog();
                LoadCont();
            }
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadCont();
        }
        private void BShipment(object sender, RoutedEventArgs e)
        {
            var selectedContainer = ContainerGrid.SelectedItem as Database.MDLS.Container;
            if (selectedContainer == null)
            {
                MessageBox.Show("Выберите строку!", "Severstal Infocom");
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

            MessageBox.Show("Заказ успешно отправлен в отгрузку!", "Severstal Infocom");
            LoadCont();
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
    }
}
