using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitDemo
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

        private int clicks = 0;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            label.Content = "Click: " + ++clicks;
        }

        private void noAsyncBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(3000);
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(3000).ConfigureAwait(continueOnCapturedContext: true);
            doneLabel.Content = "DONE!";
        }
    }
}
