using MahApps.Metro.Controls;
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

namespace DP_WPF_MVVM_LoginReg_20190128.ViewDialogWindow
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBoxView.xaml
    /// </summary>
    public partial class CustomMessageBoxView : MetroWindow
    {
        public CustomMessageBoxView()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
