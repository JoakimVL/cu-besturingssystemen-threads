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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThreadExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomTimer myTimer;

        public MainWindow()
        {
            InitializeComponent();

            myTimer = new CustomTimer();
            myTimer.OnTicked += MyTimer_Ticked;

        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            myTimer.Start();
        }

        private void MyTimer_Ticked(object sender, int currentTime)
        {
            if (Dispatcher.CheckAccess())
            {
                lblTime.Content = currentTime;
            }
            else
            {
                //dispatch to MAIN thread
                Dispatcher.Invoke(() => { MyTimer_Ticked(sender, currentTime); });
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            myTimer.Stop();
            Close();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            myTimer.Stop();
        }
    }
}
