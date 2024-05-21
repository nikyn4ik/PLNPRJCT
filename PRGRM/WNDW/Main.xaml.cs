using Database;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Linq;
using System.Threading.Tasks;
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

        private async Task OpenPage(Window page)
        {
            if (page is Orders || page is Storage)
            {
                string errorMessage = "";

                if (page is Orders)
                {
                    if (!_dbContext.Orders.Any())
                        errorMessage += "Нет заказа в таблице Orders.\n";
                }
                else if (page is Storage)
                {
                    if (!_dbContext.Storage.Any())
                        errorMessage += "Нет данных в таблице Storage.\n";
                }

                if (!string.IsNullOrEmpty(errorMessage))
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
        private void BOrder(object sender, RoutedEventArgs e) => OpenPage(new Orders(FIO));
        private void BStorage(object sender, RoutedEventArgs e) => OpenPage(new Storage());
        private void BShipment(object sender, RoutedEventArgs e) => OpenPage(new Shipment());

        private void BDelivery(object sender, RoutedEventArgs e) => OpenPage(new Delivery());
        private void BDone(object sender, RoutedEventArgs e) => OpenPage(new Done());

        private void BCertificates(object sender, RoutedEventArgs e) => OpenPage(new Certificates());
        private void BContainer(object sender, RoutedEventArgs e) => OpenPage(new Container());
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
    }
}
