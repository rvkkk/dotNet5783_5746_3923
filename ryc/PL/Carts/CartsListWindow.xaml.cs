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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartsListWindow.xaml
    /// </summary>
    public partial class CartsListWindow : Window
    {
        private IBL bl = new Bl();
        public CartsListWindow()
        {
            InitializeComponent();
            //CartsListView.ItemsSource = 
        }
    }
}
