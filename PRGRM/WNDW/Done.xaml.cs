using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRGRM.WNDW
{
    public partial class Done : Window
    {
        private readonly ApplicationContext _dbContext;
        public Done()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadData();
        }

        private void LoadData()
        {
            var filteredOrders = (from order in _dbContext.Orders
                                  join delivery in _dbContext.Delivery
                                  on order.IdOrder equals delivery.IdOrder
                                  join payer in _dbContext.Payer
                                  on order.IdPayer equals payer.IdPayer into payerJoin
                                  from payer in payerJoin.DefaultIfEmpty()
                                  join consignee in _dbContext.Consignee
                                  on order.IdConsignee equals consignee.IdConsignee into consigneeJoin
                                  from consignee in consigneeJoin.DefaultIfEmpty()
                                  where order.StatusOrder == "Заказ выполнен" && delivery.DateOfDelivery.HasValue
                                  select new
                                  {
                                      IdOrder = order.IdOrder,
                                      Name = order.Name,
                                      PayerFIO = payer != null ? payer.FIO : string.Empty,
                                      ConsigneeFIO = consignee != null ? consignee.FIO : string.Empty,
                                      DateOfDelivery = delivery.DateOfDelivery.Value
                                  }).ToList();

            DocGrid.ItemsSource = filteredOrders;
        }

        private void BPDF(object sender, RoutedEventArgs e)
        {

        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredOrders = (from order in _dbContext.Orders
                                      join delivery in _dbContext.Delivery
                                      on order.IdOrder equals delivery.IdOrder
                                      join payer in _dbContext.Payer
                                      on order.IdPayer equals payer.IdPayer into payerJoin
                                      from payer in payerJoin.DefaultIfEmpty()
                                      join consignee in _dbContext.Consignee
                                      on order.IdConsignee equals consignee.IdConsignee into consigneeJoin
                                      from consignee in consigneeJoin.DefaultIfEmpty()
                                      where order.StatusOrder == "Заказ выполнен"
                                            && delivery.DateOfDelivery.HasValue
                                            && (order.Name.ToLower().Contains(searchText)
                                                || payer.FIO.ToLower().Contains(searchText)
                                                || consignee.FIO.ToLower().Contains(searchText)
                                                || order.IdOrder.ToString().Contains(searchText)
                                                || delivery.DateOfDelivery.Value.ToString().Contains(searchText))
                                      select new
                                      {
                                          IdOrder = order.IdOrder,
                                          Name = order.Name,
                                          PayerFIO = payer != null ? payer.FIO : string.Empty,
                                          ConsigneeFIO = consignee != null ? consignee.FIO : string.Empty,
                                          DateOfDelivery = delivery.DateOfDelivery.Value
                                      }).ToList();
                DocGrid.ItemsSource = filteredOrders;
            }
            else
            {
                LoadData();
            }
        }
    }
}