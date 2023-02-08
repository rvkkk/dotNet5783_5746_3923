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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            DataContext = bl?.Product.GetAll();
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (ProductCategoriesSelector.SelectedItem.ToString() == "NONE")
                ProductsListView.ItemsSource = bl?.Product.GetAll();
            else
                ProductsListView.ItemsSource = bl?.Product.GetAll((BO.ProductForList? p) => p?.Category == (BO.Enums.Category)s!.SelectedIndex);
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            ProductsListView.ItemsSource = bl?.Product.GetAll();
        }

        private void ViewProductWindow(object sender, MouseButtonEventArgs e)
        {
            ListView l = (ListView)sender;
            BO.ProductForList pL = (BO.ProductForList)l.SelectedItem;
            BO.Product p = bl?.Product.Get(pL.ID)!;
            new ProductWindow(p).ShowDialog();
            ProductsListView.ItemsSource = bl?.Product.GetAll();
        }
    }
}
