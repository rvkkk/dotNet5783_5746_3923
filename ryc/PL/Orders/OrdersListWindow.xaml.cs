using BlApi;
using BO;
using BlImplementation;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        readonly BlApi.IBL? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderForList?> Orders
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Orders", typeof(ObservableCollection<OrderForList?>), typeof(Window), new PropertyMetadata(null));

        public OrdersListWindow()
        {
            InitializeComponent();
            var temp = bl?.Order.GetAll();
            Orders = (temp == null) ? new() : new(temp);
            OrderStatusesSelector.ItemsSource = Enum.GetValues(typeof(Enums.OrderStatus));
        }

        private void OrderStatusesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (OrderStatusesSelector.SelectedItem.ToString() == "NONE")
            {
                var temp = bl?.Order.GetAll();
                Orders = (temp == null) ? new() : new(temp);
            }
            else
            {
                var temp = bl?.Order.GetAll((BO.OrderForList? p) => p?.Status == (Enums.OrderStatus)s!.SelectedIndex);
                Orders = (temp == null) ? new() : new(temp);
            }
        }

        private void ViewOrderWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListView l = (ListView)sender;
                OrderForList oL = (OrderForList)l!.SelectedItem;
                Order o = bl?.Order.Get(oL.ID)!;
                new OrderWindow(o).ShowDialog();
                var temp = bl?.Order.GetAll();
                Orders = (temp == null) ? new() : new(temp);
            }
            catch (InvalidID ex) { MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ViewOrderWindowButton(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderForList? oL = ((Button)sender).DataContext as OrderForList;
                Order o = bl?.Order.Get((int)oL?.ID!)!;
                new OrderWindow(o).ShowDialog();
                var temp = bl?.Order.GetAll();
                Orders = (temp == null) ? new() : new(temp);
            }
            catch (InvalidID ex) { MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
