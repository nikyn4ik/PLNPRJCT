using Database;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using Database.MDLS;
using System;

namespace PRGRM.WNDW
{
    public partial class Defect : Window
    {
        private readonly ApplicationContext _dbContext;
        private readonly int sendId;
        public Defect(int sendId )
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            this.sendId = sendId;
            LoadOrdersData();
        }

        private void LoadOrdersData()
        {
            var filteredData = GetOrdersData();
            DGrid.ItemsSource = filteredData;
        }
        public List<Database.MDLS.Defects> GetOrdersData()
        {
            return _dbContext.Defects
                     .Include(d => d.Orders)
                     .ToList();
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
                .Include(d => d.Orders)
                .Where(s => (isNumeric && s.IdDefect == idDefect) ||
                            (isNumeric && s.IdOrder == idDefect) ||
                            (s.Orders != null && s.Orders.Name.ToLower().Contains(searchText)) ||
                            s.Reasons.ToLower().Contains(searchText) ||
                            (s.FIOSend != null && s.FIOSend.ToLower().Contains(searchText)) ||
                            (s.DTProductSending != null && s.DTProductSending.ToString().ToLower().Contains(searchText)))
                .ToList();
            DGrid.ItemsSource = filteredData;
        }
        private void SaveDefect(int orderId, string reasons, DateTime sendingDate, string fioSend)
        {
            var defect = new Defects
            {
                IdOrder = orderId,
                Reasons = reasons,
                DTProductSending = sendingDate,
                FIOSend = fioSend,
                IDSend = sendId
            };

            _dbContext.Defects.Add(defect);
            _dbContext.SaveChanges();
        }
    }
}
