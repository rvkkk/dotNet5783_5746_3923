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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        public OrdersListWindow()
        {
            InitializeComponent();
            OrdersListView.ItemsSource = bl?.Order.GetAll();
        }
    }
}
