using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class GroupsOverviewViewModel : ViewModelBase
    {
        GroupsDataService groupsDataService;
        public GroupsOverviewViewModel()
        {
            groupsDataService = new GroupsDataService();
            LoadCommand();
            Groups = groupsDataService.GetAllGroups().ToObservableCollection();
            FilteredOptions = new CollectionViewSource();
            FilteredOptions.Source = Groups;
            FilteredOptions.Filter += ApplyFilter;
        }
        private string _newGroupName;
        public string NewGroup { get { return _newGroupName; } set { _newGroupName = value; OnPropertyChanged("NewGroup"); } }
        private string _groupName;
        public string GroupName { get { return _groupName; } set { if(value!=null)_groupName = value; OnPropertyChanged("GroupName"); } }
        private string _Filter;
        private UserGroup _selectedGroup;
        public UserGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value; OnPropertyChanged("SelectedGroup");
                if (value != null)
                {
                    GroupName = value.GroupName;
                }
            }
        }
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;
                OnFilterChanged();
                OnPropertyChanged("Filter");
            }
        }
        private void OnFilterChanged()
        {
            FilteredOptions.View.Refresh();
        }
        private ObservableCollection<UserGroup> _groups;
        public ObservableCollection<UserGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value; OnPropertyChanged("Groups");
            }
        }

        private CollectionViewSource _filteredOptions;
        public CollectionViewSource FilteredOptions
        {
            get { return _filteredOptions; }
            set
            {
                _filteredOptions = value;
                OnPropertyChanged("FilteredOptions");
            }
        }
        public ICollectionView AllStaff
        {
            get {  return FilteredOptions.View;  }
        }
        void ApplyFilter(object sender, FilterEventArgs e)
        {
            UserGroup svm = (UserGroup)e.Item;

            if (string.IsNullOrWhiteSpace(this.Filter) || this.Filter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = svm.GroupName.ToLower().Contains(Filter.ToLower());
            }
        }
        public ICommand CmdChangeGroup { get; set; }
        public ICommand CmdDeleteGroup { get; set; }
        public ICommand CmdAddNewGroup { get; set; }
        public void LoadCommand()
        {
            CmdChangeGroup = new CustomCommand(ChangeGroup, CanChangeGroup);
            CmdDeleteGroup = new CustomCommand(DeleteGroup, CanDeleteGroup);
            CmdAddNewGroup = new CustomCommand(AddNewGroup, CanAddNewGroup);
        }

        private bool CanAddNewGroup(object obj)
        {
            if (String.IsNullOrWhiteSpace(NewGroup))
            {
                 return false;
            }
            else {return true; }
           
        }

        private void AddNewGroup(object obj)
        {
            UserGroup group = new UserGroup()
            {
                ID = groupsDataService.GetAllGroups().Count + 1,
                GroupName = NewGroup,
            };
            groupsDataService.AddGroup(group);
            Groups.Add(group);
            OnFilterChanged();
        }

        private bool CanDeleteGroup(object obj)
        {
            if (SelectedGroup!=null)
            {
                return true;
            }
            return false;
        }

        private void DeleteGroup(object obj)
        {
            Groups.Remove(SelectedGroup);
            groupsDataService.DeleteGroup(SelectedGroup);
            SelectedGroup = null;
            OnFilterChanged();
        }

        private bool CanChangeGroup(object obj)
        {
            if (String.IsNullOrWhiteSpace(GroupName) && SelectedGroup!=null)
            {
                return false;
            }
            else {return true; }
            
        }

        private void ChangeGroup(object obj)
        {
            SelectedGroup.GroupName = GroupName;
            groupsDataService.UpdateGroup(SelectedGroup);
            OnFilterChanged();
        }
    }
}
