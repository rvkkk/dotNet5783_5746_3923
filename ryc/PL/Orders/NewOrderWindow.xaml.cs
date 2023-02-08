using PL.Products;
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
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public NewOrderWindow()
        {
            InitializeComponent();
            DataContext = bl?.Product.GetAll().Select(pI => {
                BO.Product p = bl?.Product.Get((int)pI?.ID!)!;
                if(p.InStock != 0)
                   return new BO.ProductItem() { ID = (int)p?.ID!, Name = p.Name, Category = p.Category, Price = p.Price, Amount = 0, InStock = true };
                return null;
            });
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ComboBox;
            if (ProductCategoriesSelector.SelectedItem.ToString() == "NONE")
                ProductItemsView.ItemsSource = bl?.Product.GetAll().Select(pI => {
                    BO.Product p = bl?.Product.Get((int)pI?.ID!)!;
                    return new BO.ProductItem() { ID = (int)p?.ID!, Name = p.Name, Category = p.Category, Price = p.Price, Amount = 0, InStock = p.InStock == 0 ? false : true };
                });
            else
                ProductItemsView.ItemsSource = bl?.Product.GetAll(p => p?.Category == (BO.Enums.Category)s!.SelectedIndex).Select(pI => {
                    BO.Product p = bl?.Product.Get((int)pI?.ID!)!;
                    return new BO.ProductItem() { ID = (int)p?.ID!, Name = p.Name, Category = p.Category, Price = p.Price, Amount = 0, InStock = p.InStock == 0 ? false : true };
                });
        }

        private void ProductItemsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView l = (ListView)sender;
            new ProductWindow((BO.ProductItem)l.SelectedItem).ShowDialog();
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
