using BO;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        public Order Order
        {
            get => (Order)GetValue(oSDependency);
            private set => SetValue(oSDependency, value);
        }
        public static readonly DependencyProperty oSDependency = DependencyProperty.Register("Order", typeof(Order), typeof(Window), new PropertyMetadata(null));


        BackgroundWorker bw;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public SimulatorWindow()
        {
            InitializeComponent();
            tbTimer.Text = DateTime.Now.ToLongTimeString();
            Loaded += SimulatorWindow_Loaded;
            bw = new();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.RegisterStartUpdate((sender, e) => { bw.ReportProgress(1, e); });
            Simulator.Simulator.RegisterEndUpdate((sender, e) => { bw.ReportProgress(2, null); });
            Simulator.Simulator.RegisterEndSimulator((sender, e) => { bw.CancelAsync(); });
            Simulator.Simulator.Active();

            while (!bw.CancellationPending)
            {
                Thread.Sleep(1000);
                bw.ReportProgress(3, null);
            }

        }
        private void Bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int x = e.ProgressPercentage;

            switch (x)
            {
                case 1:
                    Order = ((UpdateStartEventArgs)e.UserState!).CurrentOrder;
                    tbstartTime.Text = DateTime.Now.ToLongTimeString();
                    tbEndTime.Text = ((UpdateStartEventArgs)e.UserState!).EndTime.ToLongTimeString();
                    break;
                case 2:
                    MessageBox.Show($"Finish updating order {Order.ID}");
                    break;
                case 3:
                    tbTimer.Text = DateTime.Now.ToLongTimeString();
                    break;
            }
        }
        private void Bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void SimulatorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Stop(); 
        }
    }
}
