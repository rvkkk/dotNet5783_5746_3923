using BO;
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
    public partial class OrderTrackingWindow : Window
    {
        readonly BlApi.IBL? bl = BlApi.Factory.Get();
        public OrderTracking OrderTracking
        {
            get => (OrderTracking)GetValue(oTDependency);
            private set => SetValue(oTDependency, value);
        }
        public static readonly DependencyProperty oTDependency = DependencyProperty.Register("OrderTracking", typeof(OrderTracking), typeof(Window), new PropertyMetadata(null));

        public OrderTrackingWindow(OrderTracking oT)
        {
            InitializeComponent();
            OrderTracking = oT;
        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderWindow(bl?.Order.Get(OrderTracking.ID)!, "customer").ShowDialog();
            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
