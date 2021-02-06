using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using System.Windows;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class UsersOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //private UserGroupsDataService userGroupsDataService;
        private UserDataService userDataService;
        private DialogService dialogService;
        private ObservableCollection<User> usersSource;
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }
        private string _searchGroup="";
        public string SearchGroup
        {
            get { return _searchGroup; }
            set { _searchGroup = value; RaisePropertyChanged("SearchGroup"); }
        }

        private string _searchID="";
        public string SearchID
        {
            get { return _searchID; }
            set { _searchID = value; RaisePropertyChanged("SearchID"); }
        }

        private string _searchLastName="";
        public string SearchLastName
        {
            get { return _searchLastName; }
            set { _searchLastName = value; RaisePropertyChanged("SearchLastName"); }
        }

        private string _searchLogin="";
        public string SearchLogin
        {
            get { return _searchLogin; }
            set { _searchLogin = value; RaisePropertyChanged("SearchLogin"); }
        }

        private string _strLastName;
        public string NewUserLastName
        {
            get { return _strLastName; }
            set { _strLastName = value; RaisePropertyChanged("NewUserLastName"); }
        }

        private string _strLogin;
        public string NewUserLogin
        {
            get { return _strLogin; }
            set { _strLogin = value; RaisePropertyChanged("NewUserLogin"); }
        }

        private string _strPassword;
        public string NewUserPassword
        {
            get { return _strPassword; }
            set { _strPassword = value; RaisePropertyChanged("NewUserPassword"); }
        }

        private string _strName;
        public string NewUserName
        {
            get { return _strName; }
            set { _strName = value; RaisePropertyChanged("NewUserName"); }
        }

        private UserGroup _selectedUserGroup;
        public UserGroup SelectedItemComboBox
        {
            get { return _selectedUserGroup; }
            set { _selectedUserGroup = value; RaisePropertyChanged("SelectedItemComboBox"); }
        }

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

        public ICommand DeleteUserCommand { get; set; }
        public ICommand ResetSearchCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddNewUserCommand { get; set; }
        public ICommand SearchUserCommand { get; set; } 

        private void LoadCommands()
        {
            DeleteUserCommand = new CustomCommand(DeleteUser, CanDeleteUser);
            ResetSearchCommand = new CustomCommand(ResetSearch, CanResetSearch);
            SearchUserCommand = new CustomCommand(SearchUser, CanSearchUser);
            EditCommand = new CustomCommand(EditUser, CanEditUser);
            AddNewUserCommand = new CustomCommand(AddNewUser, CanAddNewUser);
        }

        private bool CanDeleteUser(object obj)
        {
            if (SelectedUser!=null)
            {
                return true;
            }
            return false;
        }

        private void DeleteUser(object obj)
        {
            userDataService.DeleteUser(SelectedUser);
            LoadData();
        }

        private bool CanResetSearch(object obj)
        {
            return true;
        }

        private void ResetSearch(object obj)
        {
            Users = usersSource;
        }

        private bool CanSearchUser(object obj)
        {
            //if (String.IsNullOrWhiteSpace(SearchLastName) || String.IsNullOrWhiteSpace(SearchLogin)
            //    || String.IsNullOrWhiteSpace(SearchID))
            //{
                return true;
            //}
            //return false;
        }

        private void SearchUser(object obj)
        {
            
            if (SearchLastName!=""  && SearchLogin!="")
            {
                var sUser = from s in usersSource
                            where  s.Name.ToLower().Contains(SearchLastName.ToLower()) ||
                                   s.Login.ToLower().Contains(SearchLogin.ToLower()) 
                            select s;
                Users = sUser.ToObservableCollection<User>();
            }
            if (SearchLastName == "" && SearchLogin != "")
            {
                var sUser = from s in usersSource
                            where s.Login.ToLower().Contains(SearchLogin.ToLower()) 
                            select s;
                Users = sUser.ToObservableCollection<User>();
            }
            if (SearchLogin == "" && SearchLastName != "")
            {
                var sUser = from s in usersSource
                            where s.Name.ToLower().Contains(SearchLastName.ToLower()) 
                            select s;
                Users = sUser.ToObservableCollection<User>();
            }
            if (SearchID != "")
            {
                var sUser = from s in usersSource
                            where s.UserId.ToString() == SearchID
                            select s;
                Users = sUser.ToObservableCollection<User>();
            }
  
        }

        private bool CanAddNewUser(object obj)
        {
            if (  String.IsNullOrWhiteSpace(NewUserLastName) && 
                 String.IsNullOrWhiteSpace(NewUserLogin) && 
                 String.IsNullOrWhiteSpace(NewUserPassword) && 
                 String.IsNullOrWhiteSpace(NewUserName) && 
                 SelectedItemComboBox != null)
            {
                return true;
            }
            else { return false; }
        }

        private void AddNewUser(object obj)
        {
            User user = new User()
            {
                Group = SelectedItemComboBox,
                Login = NewUserLogin,
                Name = NewUserName,
                Password = NewUserPassword,
                UserId = userDataService.GetAllUsers().Count,
                Roles = new string[] { },
                
            };
            userDataService.AddUser(user);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        //КОНСТРУКТОР
        public UsersOverviewViewModel()
        {
            userDataService = new UserDataService();
            dialogService = new DialogService();
            LoadData();
            
            LoadCommands();

            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }


        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
            dialogService.CloseDetailDialog();
        }

        

        private void EditUser(object obj)
        {
            
            Messenger.Default.Send<User>(selectedUser);

            dialogService.ShowDetailDialog();
        }

        private bool CanEditUser(object obj)
        {
            if (SelectedUser != null)
                return true;
            return false;
        }

        private void LoadData()
        {
            Users = userDataService.GetAllUsers().ToObservableCollection();
            usersSource = Users;
        }
    }
}
