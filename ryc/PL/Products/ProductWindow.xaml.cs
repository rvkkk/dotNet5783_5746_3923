using BlApi;
using BlImplementation;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public ProductWindow()
        {
            InitializeComponent();
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            ProductCategoriesSelector.SelectedItem = "NONE";
            ProductButton.Content = "Add Product";
        }
        public ProductWindow(BO.Product p)
        {
            InitializeComponent();
            ProductButton.Content = "Update Product";
            tbProductID.SelectedText = p.ID.ToString();
            tbProductID.IsEnabled = false;
            tbProductName.Text = p.Name;
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            ProductCategoriesSelector.SelectedItem = p.Category!;
            tbProductPrice.Text = p.Price.ToString();
            tbProductInStock.Text = p.InStock.ToString();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbProductID.Text == "" || tbProductName.Text == "" || tbProductPrice.Text == "" || tbProductInStock.Text == "" || tbProductInStock.Text == "NONE")
                MessageBox.Show("please insert all the details");
            else
                try
                {
                    BO.Product p = new BO.Product() { ID = int.Parse(tbProductID.Text), Name = tbProductName.Text, Category = (BO.Enums.Category)ProductCategoriesSelector.SelectedItem, Price = double.Parse(tbProductPrice.Text), InStock = int.Parse(tbProductInStock.Text) };
                    if (ProductButton.Content.ToString() == "Update Product")
                        bl?.Product.Update(p);
                    else
                        bl?.Product.Add(p);
                    Close();
                }
                catch { MessageBox.Show("one of the details was worng"); }
        }
    }
}
