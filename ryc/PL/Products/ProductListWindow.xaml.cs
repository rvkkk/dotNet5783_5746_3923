using BO;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        readonly BlApi.IBL? bl = BlApi.Factory.Get();

        public ObservableCollection<BO.ProductForList?> Products
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for products. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Products", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window), new PropertyMetadata(null));

        public ProductListWindow()
        {
            InitializeComponent();
            var temp = bl?.Product.GetAll();
            Products = (temp == null) ? new() : new(temp);
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (ProductCategoriesSelector.SelectedItem.ToString() == "NONE")
            {
                var temp = bl?.Product.GetAll();
                Products = (temp == null) ? new() : new(temp);
            }
            else
            {
                var temp = bl?.Product.GetAll((BO.ProductForList? p) => p?.Category == (BO.Enums.Category)s!.SelectedIndex);
                Products = (temp == null) ? new() : new(temp);
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            var temp = bl?.Product.GetAll();
            Products = (temp == null) ? new() : new(temp);
        }

        private void ViewProductWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListView l = (ListView)sender;
                BO.ProductForList pL = (BO.ProductForList)l.SelectedItem;
                BO.Product p = bl?.Product.Get(pL.ID)!;
                new ProductWindow(p).ShowDialog();
                var temp = bl?.Product.GetAll();
                Products = (temp == null) ? new() : new(temp);
            }
            catch (InvalidID ex) { MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ViewProductWindowButton(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ProductForList? pL = ((Button)sender).DataContext as BO.ProductForList;
                BO.Product p = bl?.Product.Get((int)pL?.ID!)!;
                new ProductWindow(p).ShowDialog();
                var temp = bl?.Product.GetAll();
                Products = (temp == null) ? new() : new(temp);
            }
            catch (InvalidID ex) { MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
