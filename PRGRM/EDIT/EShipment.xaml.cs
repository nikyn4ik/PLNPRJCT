using Database;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace PRGRM.EDIT
{
    public partial class EShipment : Window
    {
        private readonly ApplicationContext _dbContext;
        private readonly int _idOrder;
        public EShipment(int idOrder)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            _idOrder = idOrder;

            var order = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == _idOrder);

            if (order != null)
            {
                int idStorage = order.IdStorage ?? 0;
                var storage = _dbContext.Storage.FirstOrDefault(s => s.IdStorage == idStorage);
                Storage.Items.Add(storage.Name);
                Storage.SelectedItem = storage.Name;
            }
            var transports = _dbContext.Transport.Select(t => t.Name).ToList();
            foreach (var transport in transports)
            {
                Transport.Items.Add(transport);
            }
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void BSaved(object sender, RoutedEventArgs e)
        {
            var selectedStorageName = Storage.SelectedItem as string;
            var selectedTransportName = Transport.SelectedItem as string;
            var shipmentDate = DatePicker.SelectedDate;

            bool isValid = true;
            string errorMessage = "Пожалуйста, заполните следующие поля корректно:\n";

            int shipmentTotalAmountTons = 0;

            if (string.IsNullOrEmpty(ShipmentTotalAmountTons.Text))
            {
                isValid = false;
                errorMessage += "- Кол-во отгрузки (тонн) (не должно быть пустым)\n";
            }
            else if (!int.TryParse(ShipmentTotalAmountTons.Text, out shipmentTotalAmountTons))
            {
                isValid = false;
                errorMessage += "- Кол-во отгрузки (тонн) (должно быть числом)\n";
            }

            if (string.IsNullOrEmpty(selectedStorageName))
            {
                isValid = false;
                errorMessage += "- Склад\n";
            }

            if (string.IsNullOrEmpty(selectedTransportName))
            {
                isValid = false;
                errorMessage += "- Транспорт\n";
            }

            if (shipmentDate == null)
            {
                isValid = false;
                errorMessage += "- Дата отгрузки\n";
            }
            else if (shipmentDate < DateTime.Today)
            {
                isValid = false;
                errorMessage += "- Дата отгрузки не может быть раньше текущей даты\n";
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var shipment = _dbContext.Shipment.FirstOrDefault(s => s.IdOrder == _idOrder);
            if (shipment != null)
            {
                shipment.DTShipments = shipmentDate;
                shipment.ShipmentTotalAmountTons = shipmentTotalAmountTons;

                var selectedTransport = _dbContext.Transport.FirstOrDefault(t => t.Name == selectedTransportName);

                if (selectedTransport != null)
                {
                    shipment.IdTransport = selectedTransport.IdTransport;
                }

                _dbContext.SaveChanges();

                MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
    }
}