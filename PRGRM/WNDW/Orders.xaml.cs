﻿using System;
using System.Windows;
using System.Windows.Controls;
using Database;
using Microsoft.EntityFrameworkCore;

namespace PRGRM.WNDW
{
    public partial class Orders : Window
    {

        public string FIO;
        private readonly ApplicationContext _dbContext;
        public Orders(string fio)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            Grid_SelectionChanged();
            LoadOrders();
            FIO = fio;
        }

        private void Grid_SelectionChanged()
        {
            OGrid.ItemsSource = _dbContext.Orders.ToList();
        }
        private void LoadOrders()
        {
            var orders = _dbContext.Orders.ToList();
            OGrid.ItemsSource = orders;
        }
        private void BSshipment(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadOrders();
                MessageBox.Show("Заявка успешно отправлена в отгрузку!", "Severstal Infocom");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данная заявка уже отправлена в отгрузку", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void UpdateGrid()
        {
            LoadOrders();
        }
        private void BShipment(object sender, RoutedEventArgs e)
        {
            //var item = OGrid.SelectedItem;
            //if (item == null)
            //{
            //    MessageBox.Show("Выберите строчку", "Severstal Infocom");
            //    return;
            //}

            //var selectedOrder = (Orders)item;
            //var idOrders = selectedOrder.IdOrder;

            //var countDefects = _dbContext.DefectProducts.Count(d => d.IdOrder == idOrders);
            //if (countDefects == 1)
            //{
            //    MessageBox.Show("Данный заказ находится в браке или не пройдена аттестация!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    UpdateGrid();
            //    return;
            //}

            //var countPackages = _dbContext.Packages.Count(p => p.IdOrder == idOrders);
            //if (countPackages == 0)
            //{
            //    _dbContext.Package.Add(new Package { IdOrder = idOrders });
            //    _dbContext.SaveChanges();

            //    MessageBox.Show("Заказ успешно отправлен в упаковку!", "Severstal Infocom");
            //    UpdateGrid();
            //}
            //else
            //{
            //    MessageBox.Show("Данный заказ уже был отправлен в упаковку!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    UpdateGrid();
            //}
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var searchText = ((TextBox)sender).Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredOrders = _dbContext.Orders
                    .Where(order =>
                        order.SystC3.Contains(searchText) ||
                        order.LogC3.Contains(searchText) ||
                        (order.IdPayer != null && order.IdPayer.ToString().Contains(searchText)) ||
                        (order.IdConsignee != null && order.IdConsignee.ToString().Contains(searchText)) ||
                        (order.DTDelivery != null && order.DTDelivery.ToString().Contains(searchText)) ||
                        order.DTReceived.ToString().Contains(searchText) ||
                        (order.DTAdoption != null && order.DTAdoption.ToString().Contains(searchText)) ||
                        order.ThicknessMm.ToString().Contains(searchText) ||
                        order.WidthMm.ToString().Contains(searchText) ||
                        order.LengthMm.ToString().Contains(searchText) ||
                        order.Name.Contains(searchText) ||
                        order.Company.Contains(searchText) ||
                        (order.StatusOrder != null && order.StatusOrder.Contains(searchText)) ||
                        (order.Mark != null && order.Mark.Contains(searchText)) ||
                        (order.IdQuaCertificate != null && order.IdQuaCertificate.ToString().Contains(searchText)) ||
                        (order.AccessStandart != null && order.AccessStandart.Contains(searchText)) ||
                        (order.NameStorage != null && order.NameStorage.Contains(searchText))
                    )
                    .ToList();
                OGrid.ItemsSource = filteredOrders;
            }
            else
            {
                LoadOrders();
            }
        }
        private void BDefects(object sender, RoutedEventArgs e)
        {
        }

        private void BPackage(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BAttestation(object sender, RoutedEventArgs e)
        {

        }

        private void BEdit(object sender, RoutedEventArgs e)
        {

        }
    }
}