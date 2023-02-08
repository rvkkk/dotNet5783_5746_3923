using BlApi;
using BlImplementation;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public OrdersListWindow()
        {
            InitializeComponent();
            DataContext = bl?.Order.GetAll();
            OrderStatusesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
        }

        private void OrderStatusesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (OrderStatusesSelector.SelectedItem.ToString() == "NONE")
                OrdersListView.ItemsSource = bl?.Order.GetAll();
            else
                OrdersListView.ItemsSource = bl?.Order.GetAll(p => p?.Status == (BO.Enums.OrderStatus)s!.SelectedIndex);
        }

        private void ViewOrderWindow(object sender, MouseButtonEventArgs e)
        {
            ListView l = (ListView)sender;
            BO.OrderForList oL = (BO.OrderForList)l!.SelectedItem;
            BO.Order o = bl?.Order.Get(oL.ID)!;
            new OrderWindow(o).ShowDialog();
            OrdersListView.ItemsSource = bl?.Order.GetAll();
        }
    }
}
