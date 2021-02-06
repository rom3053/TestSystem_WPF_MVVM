using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModelDialogWindow
{
    public class CustomMessageBoxViewModel:ViewModelBase
    {
        public CustomMessageBoxViewModel()
        {

        }
        public CustomMessageBoxViewModel(string Text, string Title)
        {
            this.Title = Title;
            TextMessage = Text;
        }
        private string _title;
        private string _textMessage;

        public string TextMessage
        {
            get { return _textMessage; }
            set { _textMessage = value; OnPropertyChanged("TextMessage"); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }

    }
}
