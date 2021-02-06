using DP.MVVM.TestManagement.Model;
using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.PageableDataGrid;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using GenerateXLSXwithClosedXML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class TestStatisticBrowserViewModel
    {
        private DialogService dialogService;
        private GroupsDataService groupsDataService;
        private List<TestResultStatistics> _resultStatistics;
        private SortablePageableCollection<TestResultStatistics> _testResults;
        public SortablePageableCollection<TestResultStatistics> TestResults
        {
            get
            {
                return _testResults;
            }
            set
            {
                if (_testResults != value)
                {
                    _testResults = value;
                    SendPropertyChanged(() => TestResults);
                }
            }
        }

        private ObservableCollection<UserGroup> _groupResultsListToExcel;
        public ObservableCollection<UserGroup> GroupResultsListToExcel
        {
            get { return _groupResultsListToExcel; }
            set
            {
                if (_groupResultsListToExcel != value)
                {
                    _groupResultsListToExcel = value;
                    SendPropertyChanged(() => GroupResultsListToExcel);
                }
            }
        }
        private UserGroup _selectedGroup;
        public UserGroup SelectedItemComboBox
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; SendPropertyChanged(() => SelectedItemComboBox); }
        }

        public ICommand GoToNextPageCommand { get; private set; }
        public ICommand GoToPreviousPageCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand PrintToExcelCommand { get; private set; }

        public List<int> EntriesPerPageList
        {
            get
            {
                return new List<int>() { 20, 30, 60 };
            }
        }

        public TestStatisticBrowserViewModel()
        {
            dialogService = new DialogService();
            groupsDataService = new GroupsDataService();
            Messenger.Default.Register<List<TestResultStatistics>>(this, OnReciviedResults, ContextMessage.SendTestStatistic);
            
            // Create Commands 
            GoToNextPageCommand = new RelayCommand(a => TestResults.GoToNextPage());
            GoToPreviousPageCommand = new RelayCommand(a => TestResults.GoToPreviousPage());
            RemoveItemCommand = new RelayCommand(item => TestResults.Remove(item as TestResultStatistics));
            PrintToExcelCommand = new RelayCommand(item => GenerateReportExcel());

        }

        private void OnReciviedResults(List<TestResultStatistics> obj)
        {
            _resultStatistics = obj;
            TestResults = new SortablePageableCollection<TestResultStatistics>(obj);
            GroupResultsListToExcel = groupsDataService.GetAllGroups().ToObservableCollection();
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        #endregion

        private IEnumerable<TestResultStatistics> CreateInitialContacts()
        {
            var list = new List<TestResultStatistics>();

            for (int i = 1; i < 90; i++)
            {
                var newContact = new TestResultStatistics() { UserName = "ContactName " + i.ToString("000"),
                    StatsTestName = "TestName" + i.ToString("000"),
                    GroupName ="GroupName" + i.ToString("000"),
                    StatsResultTest =(double)i+15.0,
                    StatsTestCategory="CategoryName" + i.ToString("000"),
                    StatsSelectedAnswers=new Dictionary<int, int[]>(),
                    
                };
                newContact.StatsSelectedAnswers.Add(1, new int[] { 3, 4, 5 });
                newContact.StatsSelectedAnswers.Add(2, new int[] { 5 });
                list.Add(newContact);
            }
            return list;
        }

        private void GenerateReportExcel()
        {
            var group = from g in _resultStatistics
                        where g.GroupName == SelectedItemComboBox.GroupName
                        orderby g.UserName
                        select g;

            if (group!=null)
            {
                List <TestResultStatistics> groupL = group.ToList();
                if (groupL.Capacity>0)
                {
                    GenerateXLSXClosedXML s = new GenerateXLSXClosedXML(groupL);
                }
                else
                {
                    dialogService.ShowDialogWindow("Dont find this group into");
                }

            }
            
        }
    }
}

