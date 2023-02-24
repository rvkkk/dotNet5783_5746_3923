using BO;
using PL.Carts;
using PL.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        Cart? myCart;
        private readonly BlApi.IBL? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderItem?> CartItems
        {
            get { return (ObservableCollection<OrderItem?>)GetValue(CartItemsProperty); }
            set { SetValue(CartItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductsItem. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartItemsProperty = DependencyProperty.Register("CartItems", typeof(ObservableCollection<OrderItem?>), typeof(Window), new PropertyMetadata(null));

        public CartWindow(Cart cart)
        {
            InitializeComponent();
            myCart = cart;
            IEnumerable<OrderItem?>? temp = myCart?.Items;
            CartItems = (temp == null) ? new() : new(temp!);
            tBtotalPrice.Text = myCart?.TotalPrice.ToString();
        }

        private void ConfirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new UserDetailsWindow(myCart!).Show();
            Close();
        }

        private void Btn_increase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = ((OrderItem)((RepeatButton)sender).DataContext).ProductID;
                int amountInstock = bl?.Product.Get(ID).InStock ?? 0;
                int amount = ((OrderItem)((RepeatButton)sender).DataContext).Amount;
                if (amount < amountInstock)
                {
                    myCart = bl?.Cart.UpdateCart(myCart!, ID, amount + 1)!;
                    IEnumerable<OrderItem?>? temp = myCart.Items;
                    CartItems = (temp == null) ? new() : new(temp!);
                    tBtotalPrice.Text = myCart.TotalPrice.ToString();
                }
            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (LessAmount ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Btn_decrease_Click(object sender, RoutedEventArgs e)
        {
            int ID = ((BO.OrderItem)((RepeatButton)sender).DataContext).ProductID;
            int amount = ((BO.OrderItem)((RepeatButton)sender).DataContext).Amount;
            try
            {
                if (amount != 1)
                {
                    myCart = bl?.Cart.UpdateCart(myCart!, ID, amount - 1)!;
                    IEnumerable<BO.OrderItem?>? temp = myCart.Items;
                    CartItems = (temp == null) ? new() : new(temp!);
                    tBtotalPrice.Text = myCart.TotalPrice.ToString();
                }
            }
            catch (BO.InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (LessAmount ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = ((OrderItem)((Button)sender).DataContext).ProductID;
                myCart = bl?.Cart.UpdateCart(myCart!, ID, 0);
                IEnumerable<OrderItem?>? temp = myCart?.Items;
                CartItems = (temp == null) ? new() : new(temp!);
                tBtotalPrice.Text = myCart?.TotalPrice.ToString();
            }
            catch (BO.InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (LessAmount ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EmptyCartBtn_Click(object sender, RoutedEventArgs e)
        {
            myCart = null;
            CartItems = new();
        }

        private void ReturnToCatalog_Click(object sender, RoutedEventArgs e)
        {
            if (myCart != null)
            {
                NewOrderWindow catalog = new(myCart);
                catalog.Show();
            }
            else
            {
                NewOrderWindow catalog = new();
                catalog.Show();
            }
            Close();
        }
    }
}