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
        readonly BlApi.IBL? bl = BlApi.Factory.Get();

        public BO.Product ProductDetails
        {
            get { return (BO.Product)GetValue(ProductDataProperty); }
            set { SetValue(ProductDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductDataProperty = DependencyProperty.Register("ProductDetails", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

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
            ProductCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            ProductDetails = p;
            ProductButton.Content = "Update Product";
            tbProductID.IsEnabled = false;  
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbProductID.Text == "" || tbProductName.Text == "" || tbProductPrice.Text == "" || tbProductInStock.Text == "" || ProductCategoriesSelector.Text == "NONE")
                MessageBox.Show("please insert all the details");
            else
                try
                {
                    BO.Product p = new BO.Product() { ID = int.Parse(tbProductID.Text), Name = tbProductName.Text, Category = (BO.Enums.Category)ProductCategoriesSelector.SelectedItem, Price = double.Parse(tbProductPrice.Text), InStock = int.Parse(tbProductInStock.Text) };
                    if (ProductButton.Content.ToString() == "Update Product")
                        bl?.Product.Update(p);
                    else
                    { 
                        bl?.Product.Add(p);
                        Close();
                    }          
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException?.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void InsertOnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right
            || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4
            || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other 
            return;
        }

        private void ReturnToProductsList_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
