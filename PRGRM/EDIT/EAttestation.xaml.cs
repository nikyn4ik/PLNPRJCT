using Database;
using System.Windows;
using System.Windows.Controls;
using Database.MDLS;
using System;
using System.Linq;

namespace PRGRM.EDIT
{
    public partial class EAttestation : Window
    {
        private readonly ApplicationContext _context;
        private readonly Orders _order;
        public EAttestation(Database.MDLS.Orders selectedOrder)
        {
            InitializeComponent();
            _context = new ApplicationContext();
            _order = _context.Orders.FirstOrDefault(o => o.IdOrder == selectedOrder.IdOrder);
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
            LoadStandardMark();
            StandardPerMark.SelectionChanged += StandardPerMarkSelectionChanged;
        }

        private void LoadStandardMark()
        {
            var certificates = _context.Certificate.ToList();
            foreach (var certificate in certificates)
            {
                if (_order.ThicknessMm >= certificate.Min && _order.ThicknessMm <= certificate.Max &&
                    _order.WidthMm >= certificate.Min && _order.WidthMm <= certificate.Max &&
                    _order.LengthMm >= certificate.Min && _order.LengthMm <= certificate.Max)
                {
                    if (!StandardPerMark.Items.Contains(certificate.StandardPerMark))
                    {
                        StandardPerMark.Items.Add(certificate.StandardPerMark);
                    }
                    if (!ProductStandard.Items.Contains(certificate.ProductStandard))
                    {
                        ProductStandard.Items.Add(certificate.ProductStandard);
                    }
                }
            }
        }

        private void BAdd(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Неверная дата.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(StandardPerMark.Text) ||
                string.IsNullOrEmpty(Units.Text) ||
                string.IsNullOrEmpty(ProductStandard.Text) ||
                DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Введите значения во все поля", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var certificate = _context.Certificate.FirstOrDefault(c => c.StandardPerMark == StandardPerMark.Text);
            if (certificate != null)
            {
                _order.IdQuaCertificate = certificate.IdQuaCertificate;
                _order.DTAttestation = DatePicker.SelectedDate.Value;
                _context.Orders.Update(_order);
                _context.SaveChanges();
                MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }

        private void StandardPerMarkSelectionChanged(object sender, SelectionChangedEventArgs e)
       {
            if (StandardPerMark.SelectedIndex > -1)
            {
                var selectedMark = StandardPerMark.SelectedItem.ToString();
                var certificate = _context.Certificate.FirstOrDefault(c => c.StandardPerMark == selectedMark);
                if (certificate != null)
                {
                    ProductStandard.SelectedItem = certificate.ProductStandard;
                    Units.Text = certificate.Units;
                    _order.IdQuaCertificate = certificate.IdQuaCertificate;
                    _context.SaveChanges();
                }
            }
        }
    }
}