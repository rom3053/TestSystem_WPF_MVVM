using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DP_WPF_MVVM_LoginReg_20190128.Models
{
    public class AnswerModel : ViewModelBase
    {
        private StackPanel _stkPanel;
        public StackPanel stkPanel
        {
            get { return _stkPanel; }
            set { SetProperty(ref _stkPanel, value); }
        }
    }
}
