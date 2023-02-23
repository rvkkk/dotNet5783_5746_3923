using PL.Products;
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
using PL.Carts;
using System.Collections.ObjectModel;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        readonly BlApi.IBL? bl = BlApi.Factory.Get();
      
        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartDependency); }
            set { SetValue(CartDependency, value); }
        }
        public static readonly DependencyProperty CartDependency = DependencyProperty.Register(nameof(Cart), typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        public ObservableCollection<BO.ProductItem> ProductItems
        {
            get { return (ObservableCollection<ProductItem>)GetValue(ProductItemsProperty); }
            set { SetValue(ProductItemsProperty, value); }
        }

        public static readonly DependencyProperty ProductItemsProperty = DependencyProperty.Register("ProductItems", typeof(ObservableCollection<BO.ProductItem>), typeof(Window), new PropertyMetadata(null));

        public NewOrderWindow()
        {
            InitializeComponent();
            Cart = new() { Items = new() };
            IEnumerable<BO.ProductItem?>? productItems = bl?.Product.GetAllPI();
            ProductItems = (productItems == null) ? new() : new(productItems!);
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        public NewOrderWindow(BO.Cart c)
        {
            InitializeComponent();
            Cart = c;
            IEnumerable<BO.ProductItem?>? productItems = bl?.Product.GetAllPI();
            ProductItems = (productItems == null) ? new() : new(productItems!);
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (ProductCategoriesSelector.SelectedItem.ToString() == "NONE")
            {
                var temp = bl?.Product.GetAllPI();
                ProductItems = (temp == null) ? new() : new(temp!);
            }
            else
            {
                var temp = bl?.Product.GetAllPI(p => p?.Category == (BO.Enums.Category)s!.SelectedIndex);
                ProductItems = (temp == null) ? new() : new(temp!);
            }
        }

        private void ProductItemsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView l = (ListView)sender;
            new ProductItemWindow((BO.ProductItem)l.SelectedItem, Cart).ShowDialog();
            IEnumerable<BO.ProductItem?>? productItems = bl?.Product.GetAllPI();
            ProductItems = (productItems == null) ? new() : new(productItems!);
        }
        private void ViewProductItemWindow(object sender, RoutedEventArgs e)
        {
            BO.ProductItem? p = ((Button)sender).DataContext as BO.ProductItem;
            new ProductItemWindow(p!, Cart).ShowDialog();
            IEnumerable<BO.ProductItem?>? productItems = bl?.Product.GetAllPI();
            ProductItems = (productItems == null) ? new() : new(productItems!);
            Close();
        }

        private void PopularProduct_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Content == "Popular Product")
            {
                try
                {
                    IEnumerable<BO.ProductItem?>? temp = bl?.Product.GetAllPopularProducts();
                    ProductItems = (temp == null) ? new() : new(temp!);
                    ((Button)sender).Content = "All product items";
                }
                catch (BO.InvalidID ex)
                {
                    MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                IEnumerable<BO.ProductItem?> temp = bl?.Product.GetAllPI()!;
                ProductItems = (temp == null) ? new() : new(temp!);
                ((Button)sender).Content = "Popular Product";
            }
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart).Show();
            Close();
        }
    }
}