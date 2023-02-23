using BlApi;
using BlImplementation;
using PL.Manager;
using PL.Orders;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

 
        private void ShowManagerButton_click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void ShowNewOrderButton_click(object sender, RoutedEventArgs e)
        {
            new NewOrderWindow().Show();
        }

        private void ShowOrdersTrackingButton_click(object sender, RoutedEventArgs e)
        {
           new ChooseOrderWindow().Show();
        }

        private void ShowSimulatorButton_click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }
    }
}
