using Database;
using Database.MDLS;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.EDIT
{
    public partial class SendingDefect : Window
    {
        private readonly ApplicationContext _dbContext;
        private List<Orders> _orders;
        private readonly int orderId;
        private readonly string fio;
        public SendingDefect(Orders selectedOrder, string fio)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            orderId = selectedOrder.IdOrder;
            this.fio = fio;
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
        }

        private void BSaved(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Reason.Text))
            {
                DateTime ProductSending;
                if (!DateTime.TryParse(DatePicker.Text, out ProductSending))
                {
                    MessageBox.Show("Введите корректную дату.", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }

                if (ProductSending < DateTime.Today)
                {
                    MessageBox.Show("Дата не может быть меньше текущей даты.", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }
                var defect = new Database.MDLS.Defects
                {
                    IdOrder = orderId,
                    Reasons = Reason.Text,
                    ProductSending = ProductSending,
                    FIO = fio
                };

                _dbContext.Defects.Add(defect);
                _dbContext.SaveChanges();

                MessageBox.Show("Заказ отправлен в брак!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); 
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите причину брака!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}