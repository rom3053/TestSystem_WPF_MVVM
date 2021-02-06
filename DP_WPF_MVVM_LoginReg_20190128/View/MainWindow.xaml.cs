using DP_WPF_MVVM_LoginReg_20190128.View;
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

namespace DP_WPF_MVVM_LoginReg_20190128
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window coffeeDetailView = new UsersOverviewView();
            coffeeDetailView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window createTest = new CreateTestView();
            createTest.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window browserTest = new TestBrowserView();
            browserTest.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window statisticBrowser = new TestStatisticBrowserView();
            statisticBrowser.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window statisticBrowser = new GroupsOverviewView();
            statisticBrowser.Show();
        }
    }
}
