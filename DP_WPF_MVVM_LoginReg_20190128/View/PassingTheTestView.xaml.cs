using DP_WPF_MVVM_LoginReg_20190128.Utility;
using DP_WPF_MVVM_LoginReg_20190128.ViewModel;
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

namespace DP_WPF_MVVM_LoginReg_20190128.View
{
    /// <summary>
    /// Логика взаимодействия для PassingTheTestView.xaml
    /// </summary>
    public partial class PassingTheTestView : MetroWindow
    {
        public PassingTheTestView()
        {
            InitializeComponent();
            PassingTheTestViewModel PartTry = ViewModelLocator.PassingTheTestViewModel;
            ViewModelLocator.PassingTheTestViewModel.StackPanelForAnswers = new Models.AnswerModel();
            ViewModelLocator.PassingTheTestViewModel.StackPanelForAnswers.stkPanel = stkpnlDynamicControls;
            PassingTheTestViewModel temp = new PassingTheTestViewModel(ViewModelLocator.PassingTheTestViewModel.StackPanelForAnswers.stkPanel, ViewModelLocator.PassingTheTestViewModel.PassingTest);
            
            this.DataContext = temp;
            
        }
    }
}
