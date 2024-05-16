using Database;
using Database.MDLS;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.ADD
{
    public partial class EDefects : Window
    {
        private readonly ApplicationContext _dbContext;
        public EDefects()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadOrdersData();
        }

        private void LoadOrdersData()
        {
            var filteredData = GetOrdersData();
            DGrid.ItemsSource = filteredData;
        }
        public List<Defects> GetOrdersData()
        {
            return _dbContext.Defects.ToList();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadOrdersData();
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string searchText = textBox.Text.ToLower();
            int idDefect;
            bool isNumeric = int.TryParse(searchText, out idDefect);

            var filteredData = _dbContext.Defects
                .Where(s => (isNumeric && s.IdDefect == idDefect) ||
                s.IdOrder.ToLower().Contains(searchText) ||
                s.Reasons.ToLower().Contains(searchText) ||
            (s.FIO != null && s.FIO.ToLower().Contains(searchText)) ||
            (s.ProductSending != null && s.ProductSending.ToString().ToLower().Contains(searchText)))
                .ToList();
            DGrid.ItemsSource = filteredData;

        }
    }
}
