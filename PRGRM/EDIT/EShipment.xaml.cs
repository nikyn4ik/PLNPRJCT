using Database;
using System.Windows;
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

                Storage.Items.Add(idStorage);
                Storage.SelectedItem = idStorage;
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

            int.TryParse(ShipmentTotalAmountTons.Text, out int shipmentTotalAmountTons);

            if (selectedStorageName == null || selectedTransportName == null || shipmentDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите значения для всех полей!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DatePicker.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Дата отгрузки не может быть раньше текущей даты!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var shipment = new Shipment
            {
                IdOrder = _idOrder,
                DTShipments = shipmentDate,
                ShipmentTotalAmountTons = shipmentTotalAmountTons
            };

            _dbContext.Shipment.Add(shipment);
            _dbContext.SaveChanges();

            MessageBox.Show("Данные сохранены успешно!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}