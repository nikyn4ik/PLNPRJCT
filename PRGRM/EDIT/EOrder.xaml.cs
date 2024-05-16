using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.MDLS;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PRGRM.EDIT
{
    public partial class EOrder : Window
    {
        private readonly ApplicationContext _dbContext;
        private List<Orders> _orders;
        private readonly int orderId;
        private readonly string fio;
        public EOrder(Database.Orders selectedOrder)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            orderId = selectedOrder.IdOrder;
            InitializeOrderData();
        }
        private void InitializeOrderData()
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == orderId);

            if (order != null)
            {
                Company.Text = order.Company;
            }

            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.Text = DateTime.Today.ToString();
        }
        private void BSaved(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Company.Text))
            {
                DateTime Adoption;
                if (!DateTime.TryParse(DatePicker.Text, out Adoption))
                {
                    MessageBox.Show("Введите корректную дату.", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }

                if (Adoption < DateTime.Today)
                {
                    MessageBox.Show("Дата не может быть меньше текущей даты.", "Severstal Infocom", MessageBoxButton.OK);
                    return;
                }

                var selectedCompany = Company.Text;

                var orderToUpdate = _dbContext.Orders.FirstOrDefault(o => o.IdOrder == orderId);
                if (orderToUpdate != null)
                {
                    orderToUpdate.DTAdoption = Adoption;
                    orderToUpdate.StatusOrder = "Заказ на выполнении";
                    orderToUpdate.Company = selectedCompany;

                    _dbContext.SaveChanges();

                    MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK);
                    Close();
                }
                else
                {
                    MessageBox.Show("Не удалось найти заказ для обновления.", "Severstal Infocom", MessageBoxButton.OK);
                }
            }
        }
    }
}