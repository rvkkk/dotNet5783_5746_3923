using BO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using PL;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        readonly BlApi.IBL? bl = BlApi.Factory.Get();

        Cart? myCart;
        public ProductItem ProductItemDetails
        {
            get { return (ProductItem)GetValue(ProductDataProperty); }
            set { SetValue(ProductDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductDataProperty =
        DependencyProperty.Register("ProductItemDetails", typeof(ProductItem), typeof(Window), new PropertyMetadata(null));

        public ProductItemWindow(ProductItem p, Cart c)
        {
            myCart = c;
            InitializeComponent();
            try
            {
                ProductItemDetails = bl?.Product.Get(p.ID, c)!;
            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Cart.AddToCart(myCart!, ProductItemDetails.ID);
                if (ProductItemDetails.Amount != 1)
                {
                    myCart = bl?.Cart.UpdateCart(myCart!, ProductItemDetails.ID, ProductItemDetails.Amount);
                }
            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (AlreadyExists ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (LessAmount ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Btn_increase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amountInstock = bl?.Product.Get(ProductItemDetails.ID).InStock ?? 0;
                if (ProductItemDetails.Amount < amountInstock)
                {
                    ProductItemDetails.Amount++;
                    tbProductAmonut.Text = ProductItemDetails.Amount.ToString();
                }

            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Btn_decrease_Click(object sender, RoutedEventArgs e)
        {
            if (ProductItemDetails.Amount > 0)
            {
                ProductItemDetails.Amount--;
                tbProductAmonut.Text = ProductItemDetails.Amount.ToString();
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductItemDetails.Amount != 0)
                {
                    myCart = bl?.Cart.UpdateCart(myCart!, ProductItemDetails.ID, 0);
                    NewOrderWindow catalog = new(myCart!);
                    catalog.Show();
                    Close();
                }
            }
            catch (InvalidID ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (LessAmount ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (DalException ex) { MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ReturnToCatalog_Click(object sender, RoutedEventArgs e)
        {
            NewOrderWindow catalog = new(myCart!);
            catalog.Show();
            Close();
        }
    }
}
