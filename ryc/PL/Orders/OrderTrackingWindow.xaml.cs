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
    /// Interaction logic for OrderTrcaking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public OrderTracking(BO.Order o)
        {
            InitializeComponent();
            BO.OrderTracking oT = bl?.Order.OrderTracking(o.ID)!;
            tbOrderID.Text = oT.ID.ToString();
            tbOrderStatus.Text = oT.Status.ToString();
            OrderTrackingListLV.ItemsSource = oT.list;
        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow(bl?.Order.Get(int.Parse(tbOrderID.Text))!, 1).ShowDialog();
        }
    }
}
