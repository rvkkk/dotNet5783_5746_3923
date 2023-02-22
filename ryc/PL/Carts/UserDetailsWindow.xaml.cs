using PL.Orders;
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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {

        private readonly BlApi.IBL? bl = BlApi.Factory.Get();

        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCartConfirm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty = DependencyProperty.Register("MyCartConfirm", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        public UserDetailsWindow(BO.Cart cart)
        {
            InitializeComponent();
            MyCart = cart;
        }

        private void ReturnToCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cart = new(MyCart);
            cart.Show();
            Close();
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyCart.CustomerName = tbCustomerName.Text;
                MyCart.CustomerEmail = tbCustomerEmail.Text;
                MyCart.CustomerAddress = tbCustomerAddress.Text;
                int ID = (int)bl?.Cart.MakeAnOrder(MyCart)!;
                new OrderWindow(bl?.Order.Get(ID)!, "customer").Show();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.InnerException?.ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }  
        }
    }
}

