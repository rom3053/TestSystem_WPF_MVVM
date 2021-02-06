using DP_WPF_MVVM_LoginReg_20190128.View;
using DP_WPF_MVVM_LoginReg_20190128.ViewDialogWindow;
using DP_WPF_MVVM_LoginReg_20190128.ViewModel;
using DP_WPF_MVVM_LoginReg_20190128.ViewModelDialogWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class DialogService
    {

        Window DialogView = null;

        public DialogService()
        {
        }

        public void ShowDetailDialog()
        {
            DialogView = new UserDetailView();
            DialogView.ShowDialog();
        }
        public void ShowPassingTheTest()
        {
            DialogView = new PassingTheTestView();
            DialogView.ShowDialog();
        }
        public void ShowCreateTestView()
        {
            DialogView = new CreateTestView();
            DialogView.ShowDialog();
        }
        public void ShowTwoListBoxesGroupSelectionView()
        {
            DialogView = new TwoListBoxesGroupSelectionView();
            DialogView.ShowDialog();
        }
        public void ShowCreateCategoryView()
        {
            DialogView = new CreateCategoryView();
            DialogView.ShowDialog();
        }
        public void ShowStatisticBrowser()
        {
            DialogView = new TestStatisticBrowserView();
            DialogView.Show();
        }
        public void CloseDetailDialog()
        {
            if (DialogView != null)
                DialogView.Close();
        }
 
        public bool ShowDialogWindow(string Text, string Title="MessageBox")
        {
            DialogView  = new CustomMessageBoxView();
            DialogView.DataContext = new CustomMessageBoxViewModel(Text, Title);
            if (DialogView.ShowDialog()==true)
            {
                return true;
            }
            else
            {
                //CloseDetailDialog();
                return false;
            }
            
        }
    }
}
