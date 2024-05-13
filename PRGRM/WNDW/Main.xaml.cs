using PRGRM.MDLS;
using System.Windows;

namespace PRGRM.WNDW

{
    public partial class Main : Window
{
    private Login loginWindow;
    public Main()
    {
        InitializeComponent();
        loginWindow = null;
    }

    private void OpenPage(Window page)
    {
        Hide();
        page.Owner = this;
        page.ShowDialog();
        Show();
    }
        private void B_order(object sender, RoutedEventArgs e)
        {
            Hide();
            var window = new Orders();
            window.ShowDialog();
            Show();
        }
        private void B_shipment(object sender, RoutedEventArgs e)
        {
            Hide();
            var window = new Shipment();
            window.ShowDialog();
            Show();
        }

        private void B_Delivery(object sender, RoutedEventArgs e)
        {

        }

        private void B_Done_Orders(object sender, RoutedEventArgs e)
        {

        }

        private void B_Storage(object sender, RoutedEventArgs e)
        {

        }

        private void B_Certificates(object sender, RoutedEventArgs e)
        {

        }
        //private void Button_Shipment(object sender, RoutedEventArgs e) => OpenPage(new ShipmentPage());

        //private void Button_Storage(object sender, RoutedEventArgs e) => OpenPage(new StoragePage());

        //private void Button_Certificates(object sender, RoutedEventArgs e) => OpenPage(new Certificates());

        //private void Button_order(object sender, RoutedEventArgs e) => OpenPage(new OrdersPage());

        //private void Button_Delivery(object sender, RoutedEventArgs e) => OpenPage(new DeliveryPage());

        //    private void Button_RabMestoMastera(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Функционал недоступен для десктоп", "Sevestal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
        //}


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Sevestal Infocom", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;

                case MessageBoxResult.Yes:
                    if (loginWindow == null)
                    {
                        loginWindow = new Login();
                        loginWindow.Closed += LoginWindow_Closed;
                    }

                    loginWindow.Show();
                    Hide();
                    break;
            }
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}