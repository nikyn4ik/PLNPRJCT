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
            //LoadCont();
        }
        //private void LoadCont()
        //{
        //    ContainerGrid.ItemsSource = GetContData();
        //}
        //public List<Container> GetContData()
        //{
        //    //return _dbContext.Container.ToList();
        //}
        //private void ECont_Closed(object sender, EventArgs e)
        //{
        //    LoadCont();
        //}
        private void AEdit(object sender, RoutedEventArgs e)
        {
            //var selectedOrder = ContainerGrid.SelectedItem as Database.Orders;
            //if (selectedOrder == null)
            //{
            //    MessageBox.Show("Выберите строку!", "Severstal Infocom");
            //    return;
            //}

            //else
            //{
            //    var editWindow = new EContainer();
            //    editWindow.Closed += ECont_Closed;
            //    editWindow.ShowDialog();
            //}
        }
        //private void AddWindow_Closed(object sender, EventArgs e)
        //{
        //    LoadCont();
        //}
        private void BShipment(object sender, RoutedEventArgs e)
        {

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
