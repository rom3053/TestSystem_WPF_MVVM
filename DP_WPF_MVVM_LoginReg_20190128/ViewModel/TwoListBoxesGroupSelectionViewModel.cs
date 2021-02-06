using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class TwoListBoxesGroupSelectionViewModel:ViewModelBase
    {
        public TwoListBoxesGroupSelectionViewModel()
        {
            groupsDataService = new GroupsDataService();
            Messenger.Default.Register<ObservableCollection<UserGroup>>(this, GetSelectedGroups);
            LoadCommands();
            AllGroupsToTheRight = groupsDataService.GetAllGroups().ToObservableCollection();
            
        }

        private void GetSelectedGroups(ObservableCollection<UserGroup> obj)
        {
            listGroupsLeft = obj;
        }

        ObservableCollection<UserGroup> groupForUsers = new ObservableCollection<UserGroup>();
        ObservableCollection<UserGroup> listGroupsLeft = new ObservableCollection<UserGroup>();
        ObservableCollection<UserGroup> listGroupsRight = new ObservableCollection<UserGroup>();

        public ObservableCollection<UserGroup> AllGroupsToTheRight { get { return listGroupsRight; } set { listGroupsRight = value; OnPropertyChanged("AllGroupsToTheRight"); } }
        public ObservableCollection<UserGroup> AddGroupsToTheLeft { get { return listGroupsLeft; } set { listGroupsLeft = value; OnPropertyChanged("AddGroupsToTheLeft"); } }

        //public ObservableCollection<UserGroup> List2 { get { return listGroupsRight; } set { listGroupsRight = value; OnPropertyChanged("List2"); } }
        private UserGroup selectedRight;
        private GroupsDataService groupsDataService;
        public UserGroup SelectedRight
        {
            get { return selectedRight; }
            set { selectedRight = value; OnPropertyChanged("SelectedRight"); }
        }
        private UserGroup selectedLeft;

        public UserGroup SelectedLeft
        {
            get { return selectedLeft; }
            set { selectedLeft = value; OnPropertyChanged("SelectedLeft"); }
        }
        private void LoadCommands()
        {
            Cancel = new RelayCommand(CancelChoice, CanCancelChoice);
            Accept = new RelayCommand(AcceptChoice, CanAcceptChoice);
            MoveRight = new RelayCommand(MoveRigthElement, CanMoveRigth);
            MoveLeft = new RelayCommand(MoveLeftelemetn, CanMoveLeft);
        }

        private bool CanCancelChoice(object obj)
        {
            return true;
        }

        private void CancelChoice(object obj)
        {
            Messenger.Default.Send<CancelTwoListBoxesGroup>(new CancelTwoListBoxesGroup());
        }

        private bool CanAcceptChoice(object obj)
        {
            return true;
        }

        private void AcceptChoice(object obj)
        {
            
            Messenger.Default.Send<ObservableCollection<UserGroup>>(listGroupsLeft, ContextMessage.SendBackGroupsToCreateTestViewModel);
        }

        private void MoveRigthElement(object obj)
        {
            if (SelectedLeft!=null)
            {
                UserGroup group = new UserGroup();
                group = SelectedLeft;
                listGroupsRight.Add(group);
                listGroupsLeft.Remove(SelectedLeft);
            }


        }

        private void MoveLeftelemetn(object obj)
        {
            if (SelectedRight!=null)
            {
                UserGroup group = new UserGroup();
                group = SelectedRight;
                listGroupsLeft.Add(group);
                listGroupsRight.Remove(SelectedRight);
            }

        }

        private bool CanMoveLeft(object arg)
        {
            //if (SelectedRight != null)
            //{
                return true;
            //}
            //return false;
        }

        private bool CanMoveRigth(object arg)
        {
            //if (SelectedLeft != null)
            //{
                return true;
            //}
            //return false;
        }

        public ICommand MoveRight { get; set; }
        public ICommand MoveLeft { get; set; }
        public ICommand Accept { get; set; }
        public ICommand Cancel { get; set; }

    }
}
