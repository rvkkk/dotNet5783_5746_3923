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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(BO.Order o)
        {
            InitializeComponent();
            tbOrderID.Text = o.ID.ToString();
            tbOrderCustomerN.Text = o.CustomerName!.ToString();
            tbOrderCustomerA.Text = o.CustomerAddress!.ToString();
            tbOrderCustomerE.Text = o.CustomerEmail!.ToString();
            tbOrderDate.Text = o.OrderDate.ToString();
            tbOrderShipDate.Text = o.ShipDate!.ToString();
            tbOrderDeliveryDate.Text = o.DeliveryDate!.ToString();
            tbOrderStatus.Text = o.Status.ToString();
            tbOrderTotalPrice.Text = o.TotalPrice.ToString();
        }

        public OrderWindow(BO.Order o, int n)
        {
            InitializeComponent();
            tbOrderID.Text = o.ID.ToString();
            tbOrderID.IsEnabled = false;
            tbOrderCustomerN.Text = o.CustomerName!.ToString();
            tbOrderCustomerN.IsEnabled = false;
            tbOrderCustomerA.Text = o.CustomerAddress!.ToString();
            tbOrderCustomerA.IsEnabled = false;
            tbOrderCustomerE.Text = o.CustomerEmail!.ToString();
            tbOrderCustomerE.IsEnabled = false;
            tbOrderDate.Text = o.OrderDate.ToString();
            tbOrderDate.IsEnabled = false;
            tbOrderShipDate.Text = o.ShipDate?.ToString();
            tbOrderShipDate.IsEnabled = false;
            tbOrderDeliveryDate.Text = o.DeliveryDate?.ToString();
            tbOrderDeliveryDate.IsEnabled = false;
            tbOrderStatus.Text = o.Status.ToString();
            tbOrderStatus.IsEnabled = false;
            tbOrderTotalPrice.Text = o.TotalPrice.ToString();
            tbOrderTotalPrice.IsEnabled = false;
            ShipUpdateButton.Visibility = Visibility.Hidden;
            DeliveryUpdateButton.Visibility = Visibility.Hidden;
        }

        private void ShipUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Order.ShipUpdate(int.Parse(tbOrderID.Text));
                BO.Order? o = bl?.Order.Get(int.Parse(tbOrderID.Text));
                tbOrderShipDate.Text = o?.ShipDate.ToString();
                tbOrderStatus.Text = o?.Status.ToString();
            }
            catch { MessageBox.Show("you alredy shipped the order"); }
        }

        private void DeliveryUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Order.DeliveryUpdate(int.Parse(tbOrderID.Text));
                BO.Order? o = bl?.Order.Get(int.Parse(tbOrderID.Text));
                tbOrderDeliveryDate.Text = o?.DeliveryDate.ToString();
                tbOrderStatus.Text = o?.Status.ToString();
            }
            catch { MessageBox.Show("you haven't shipped the order yet"); }
        }
    }
}
