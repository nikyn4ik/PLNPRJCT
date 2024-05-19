using Database;
using Database.MDLS;
using PRGRM.WNDW;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.EDIT
{
    public partial class EDelivery : Window
    {
        private readonly ApplicationContext _dbContext;
        private readonly int _idOrder;
        public event EventHandler DataSaved;
        public EDelivery(int idOrder)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            _idOrder = idOrder;

            var order = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == _idOrder);
            
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void BSaved(object sender, RoutedEventArgs e)
        {
            var selectedEarlyDelivery = EarlyDelivery.SelectedItem as ComboBoxItem;
            var selectedStatusOrder = StatusOrder.Text;
            var DeliveryDateOfDelivery = DatePicker.SelectedDate;

            bool isValid = true;
            string errorMessage = "Пожалуйста, заполните следующие поля корректно:\n";

            if (selectedEarlyDelivery == null)
            {
                isValid = false;
                errorMessage += "- Ранняя доставка\n";
            }

            if (string.IsNullOrEmpty(selectedStatusOrder))
            {
                isValid = false;
                errorMessage += "- Статус заказа\n";
            }

            if (DeliveryDateOfDelivery == null)
            {
                isValid = false;
                errorMessage += "- Дата доставки\n";
            }
            else if (DeliveryDateOfDelivery < DateTime.Today)
            {
                isValid = false;
                errorMessage += "- Дата доставки не может быть раньше текущей даты\n";
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var delivery = _dbContext.Delivery.FirstOrDefault(s => s.IdOrder == _idOrder);
            if (delivery != null)
            {
                delivery.DateOfDelivery = DeliveryDateOfDelivery;
                delivery.EarlyDelivery = selectedEarlyDelivery.Content.ToString();

                var orderToUpdate = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == _idOrder);
                if (orderToUpdate != null)
                {
                    orderToUpdate.StatusOrder = "Заказ выполнен";

                    _dbContext.SaveChanges();
                    DataSaved?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);

                    Close();
                }
            }
        }
    }
}