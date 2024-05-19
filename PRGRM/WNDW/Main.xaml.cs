using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace PRGRM.WNDW

{
    public partial class Main : Window
{
    private readonly ApplicationContext _dbContext;
    private Login loginWindow;
    public string FIO;
    public Main(string fio)
    {
        InitializeComponent();
        loginWindow = null;
        _dbContext = new ApplicationContext();
         FIO = fio;
        lplogin.Content = fio;
        }

        private bool CheckRequiredData(out string errorMessage)
        {
            errorMessage = "";
            if (!_dbContext.Certificate.Any())
                errorMessage += "Нет данных в таблице Certificate.\n";
            if (!_dbContext.Storage.Any())
                errorMessage += "Нет данных в таблице Storage.\n";
            if (!_dbContext.Payer.Any())
                errorMessage += "Нет данных в таблице Payer.\n";
            if (!_dbContext.ContainerPackage.Any())
                errorMessage += "Нет данных в таблице ContainerPackage.\n";
            if (!_dbContext.Company.Any())
                errorMessage += "Нет данных в таблице Company.\n";
            if (!_dbContext.Transport.Any())
                errorMessage += "Нет данных в таблице Transport.\n";
            if (!_dbContext.Consignee.Any())
                errorMessage += "Нет данных в таблице Consignee.\n";

            return string.IsNullOrEmpty(errorMessage);
        }
        private void OpenPage(Window page)
    {
            if (page is Orders)
            {
                if (!CheckRequiredData(out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Sevestal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
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