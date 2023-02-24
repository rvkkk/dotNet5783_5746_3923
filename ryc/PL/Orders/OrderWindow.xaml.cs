using System;
using BO;
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
using static BO.Enums;
using BlApi;
using System.Windows.Controls.Primitives;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        readonly BlApi.IBL? bl = BlApi.Factory.Get();
        readonly string person;

        public Order? OrderDetails
        {
            get { return (Order?)GetValue(OrderDP); }
            set { SetValue(OrderDP, value); }
        }

        // Using a DependencyProperty as the backing store for OrderData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderDP = DependencyProperty.Register("OrderDetails", typeof(Order), typeof(Window), new PropertyMetadata(null));

        public OrderWindow(Order o)
        {
            InitializeComponent();
            OrderDetails = o;
            person = "manager";
        }

        public OrderWindow(Order o, string customer)
        {
            InitializeComponent();
            OrderDetails = o;
            person = customer;
            UpdateStatusButton.Visibility = Visibility.Hidden;
        }

        private void UpdateStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrderDetails?.Status == OrderStatus.OrderConfirmed)
                {
                    OrderDetails = bl?.Order.ShipUpdate((int)OrderDetails?.ID!);
                }
                else if (OrderDetails?.Status == OrderStatus.Shipped)
                {
                    OrderDetails = bl?.Order.DeliveryUpdate((int)OrderDetails?.ID!);
                }
                Close();
            }
            catch (AlreadyDone ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Btn_increase_Click(object sender, RoutedEventArgs e)
        {
            if (person == "manager")
                try
                {
                    int ID = ((OrderItem)((RepeatButton)sender).DataContext).ProductID;
                    int amountInstock = bl?.Product.Get(ID).InStock ?? 0;
                    int amount = ((OrderItem)((RepeatButton)sender).DataContext).Amount;
                    if (amount < amountInstock)
                    {
                        OrderDetails = bl?.Order.ManagerUpdate(OrderDetails!.ID, ID, amount + 1)!;
                    }
                }
                catch (AlreadyDone ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Btn_decrease_Click(object sender, RoutedEventArgs e)
        {
            if (person == "manager")
                try
                {
                    int ID = ((BO.OrderItem)((RepeatButton)sender).DataContext).ProductID;
                    int amount = ((BO.OrderItem)((RepeatButton)sender).DataContext).Amount;
                    if (amount != 1)
                    {
                        OrderDetails = bl?.Order.ManagerUpdate(OrderDetails!.ID, ID, amount - 1)!;
                    }
                }
                catch (AlreadyDone ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RemoveFromOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = ((OrderItem)((Button)sender).DataContext).ProductID;
                OrderDetails = bl?.Order.ManagerUpdate(OrderDetails!.ID, ID, 0)!;
            }
            catch (AlreadyDone ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
