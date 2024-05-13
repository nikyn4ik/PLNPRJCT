using System;
using System.Windows;
using System.Windows.Navigation;
using Database;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using PRGRM.PG;

namespace PRGRM.WNDW
{
    public partial class Orders : Window
    {
        private readonly ApplicationContext _dbContext;
        public Orders()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadOrders();
        }
        private void LoadOrders()
        {
            var orders = _dbContext.Orders.ToList();
            OGrid.ItemsSource = orders;
        }
        private void BSshipment(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadOrders();
                MessageBox.Show("Заявка успешно отправлена в отгрузку!", "Severstal Infocom");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данная заявка уже отправлена в отгрузку", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BShipment(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Search(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void BDefects(object sender, RoutedEventArgs e)
        {
            Hide();
            var window = new Defects();
            Show();
        }

        private void BPackage(object sender, RoutedEventArgs e)
        {

        }
    }
}