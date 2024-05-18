using Database;
using Microsoft.Identity.Client.Extensions.Msal;
using PRGRM.ADD;
using PRGRM.WNDW;
using System.IO.Packaging;
using System.Windows;

namespace PRGRM.WNDW

{
    public partial class Main : Window
{
    private Login loginWindow;
    public string FIO;
    public Main(string fio)
    {
        InitializeComponent();
        loginWindow = null;
        FIO = fio;
        lplogin.Content = fio;
        }

    private void OpenPage(Window page)
    {
        Hide();
        page.Owner = this;
        page.ShowDialog();
        Show();
    }
        private void B_order(object sender, RoutedEventArgs e) => OpenPage(new Orders(FIO));
        private void B_shipment(object sender, RoutedEventArgs e) => OpenPage(new Shipment());

        private void B_Delivery(object sender, RoutedEventArgs e) => OpenPage(new Delivery());
        private void B_Done(object sender, RoutedEventArgs e) => OpenPage(new Done());
        private void B_Storage(object sender, RoutedEventArgs e) => OpenPage(new Storage());

        private void B_Certificates(object sender, RoutedEventArgs e) => OpenPage(new Certificates());
        private void BContainer(object sender, RoutedEventArgs e) => OpenPage(new Container());

        //    private void Button_RabMestoMastera(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Функционал недоступен для десктоп", "Sevestal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
        //}


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Sevestal Infocom", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (loginWindow == null)
                {
                    loginWindow = new Login();
                    loginWindow.Closed += LoginWindow_Closed;
                }

                loginWindow.Show();
                Hide();
            }
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BDocuments(object sender, RoutedEventArgs e)
        {

        }

        
    }
}