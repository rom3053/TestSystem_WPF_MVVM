using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
   public class UserDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserDataService userDataService;
       
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand { get; set; }

        private User selectedUser;
        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }
        public UserDetailViewModel()
        {
            Messenger.Default.Register<User>(this, OnUserReceived);

            SaveCommand = new CustomCommand(SaveUser, CanSaveUser);
 
            userDataService = new UserDataService();

            
        }

        private void OnUserReceived(User user)
        {
            SelectedUser = user;
        }

        private bool CanSaveUser(object obj)
        {
            return true;
        }
        private void SaveUser(object obj)
        {
            userDataService.UpdateUser(selectedUser);
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }
    }
}
