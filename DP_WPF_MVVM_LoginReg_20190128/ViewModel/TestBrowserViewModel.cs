using DP.MVVM.TestManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using DP_WPF_MVVM_LoginReg_20190128.LoginService.Identity;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class TestBrowserViewModel : ViewModelBase
    {
       
        #region VisibleProperty
        private System.Windows.Visibility _IsVisible = System.Windows.Visibility.Visible;
        public System.Windows.Visibility IsVisible
        {
            get
            {
                return _IsVisible;
            }
            set
            {
                _IsVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        private string _IsVisibleAdminControls = "Visible";
        public string IsVisibleAdminControls
        {
            get
            {
                return _IsVisibleAdminControls;
            }
            set
            {

                _IsVisibleAdminControls = value;
                OnPropertyChanged("IsVisibleAdminControls");
            }
        }
        private string SetVisibleAdminControls(bool IsAdmin)
        {
            if (IsAdmin)
            {
                return "Visible";
            }
            else
            {
                return "Hidden";
            }
        }
        private bool CheckAdminRoles()
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal.IsInRole("Administrators"))
            {
                return true;
            }
            return false;
        }
 
        #endregion

        private DialogService dialogService;
        private CategoryDataService categoryDataService;
        //private UserDataService userDataService;
        private TestDataService testDataService;
        private QuestionDataService questionDataService;

        #region SelectedItemProprety
        private Category selectedCategory;
        private Test _selectedTest;
        public Test SelectedTest
        {
            get { return _selectedTest; }
            set { _selectedTest = value; OnPropertyChanged("SelectedTest"); }
        }
        public Object SelectedItemTreeView
        {
            get
            {
                if (SelectedTest != null)
                {
                    return SelectedTest;
                }
                if (selectedCategory != null)
                {
                    return selectedCategory;
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value is Test)
                {
                    SelectedTest = (Test)value;
                }
                if (value is Category)
                {
                    selectedCategory = (Category)value;
                }
                OnPropertyChanged("SelectedItemTreeView");
            }
        }
        #endregion
        private Category _selectedCategory;
        public Category SelectedItemComboBox
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged("SelectedItemComboBox"); }
        }
        private ObservableCollection<Category> сategory = new ObservableCollection<Category>();
        public ObservableCollection<Category> Сategories
        {
            get
            {
                return сategory;
            }
            set
            {
                сategory = value;
                OnPropertyChanged("Сategories");
            }
        }
        private string _searchTest;

        public string SearchTest
        {
            get { return _searchTest; }
            set { _searchTest = value; OnPropertyChanged("SearchTest"); }
        }

        public TestBrowserViewModel()
        {
            Messenger.Default.Register<UpdateNewTest>(this, UpdateCategoriesNewTestAdded );
            Messenger.Default.Register<UpdateListCategoriesMessage>(this, UpdateCategories, ContextMessage.UpdateCategoryInCategoryBrwoser);
            Messenger.Default.Register<CancelCategoriesMessage>(this, CancelCreateCategories, ContextMessage.SendBackCategoryCancel);
            questionDataService = new QuestionDataService();
            categoryDataService = new CategoryDataService();
            testDataService = new TestDataService();
            dialogService = new DialogService();
            LoadDataTest();
            LoadCommands();
            //IsVisibleAdminControls = SetVisibleAdminControls(CheckAdminRoles());
        }

        private void UpdateCategoriesNewTestAdded(UpdateNewTest obj)
        {
            LoadDataTest();
            dialogService.CloseDetailDialog();
        }

        private void CancelCreateCategories(CancelCategoriesMessage obj)
        {
            dialogService.CloseDetailDialog();
        }

        private void UpdateCategories(UpdateListCategoriesMessage obj)
        {
            LoadDataTest();
            dialogService.CloseDetailDialog();
        }
        #region CommandsProperties
        public ICommand CmdSearchAnyWhereTest { get; private set; }
        public ICommand CmdSearchInCategory { get; private set; }

        public ICommand CmdOpenWindowWatchStat { get; private set; }
        public ICommand CmdOpenWindowStartTest { get; private set; }
        public ICommand CmdOpenWindowEditTest { get; private set; }
        public ICommand CmdDeleteTest { get; private set; }
        public ICommand CmdOpenWindowCreateTest { get; private set; }
        public ICommand CmdOpenWindowTest { get; private set; }
        
        public ICommand CmdAddCategory { get; private set; }
        public ICommand CmdEditCategory { get; private set; }
        public ICommand CmdDeleteCategory { get; private set; }
        #endregion
        #region CommandsFunc
        private void StartTest(object parameter)
        {
            Messenger.Default.Send<Test>(_selectedTest);
            dialogService.ShowPassingTheTest();
            
        }
        private void DeleteTest(object parameter)
        {
            if (_selectedTest!=null)
            {
                testDataService.DeleteTest(_selectedTest);
                Сategories[_selectedTest.CategoryID].Tests.Remove(_selectedTest);
                LoadDataTest();
            }
        }
        private void OpenWindowEditTest(object parameter)
        {
            Messenger.Default.Send<Test>(_selectedTest,ContextMessage.SendTestEdit);
            dialogService.ShowCreateTestView();
        }
        private void OpenWindowCreateTest(object parameter)
        {
            IsVisible = System.Windows.Visibility.Hidden;
            dialogService.ShowCreateTestView();
        }
        #endregion
        #region CommandsRuleForDO
        private bool CmdCanStartTest(object parameter)
        {
            if (_selectedTest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CmdCanEditTest(object parameter)
        {
            if (_selectedTest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CmdCanDeleteTest(object parameter)
        {
            if (_selectedTest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CmdCanCreateTest(object parameter)
        {
            //if (parameter != null)
            //{
            return true;
            //}
            //else
            //{
            //return false;
            //}
        }
        #endregion
        private void LoadCommands()
        {
            CmdSearchAnyWhereTest = new CustomCommand(SearchAnyWhereTest, CanSearchAnyWhereTest);
            CmdSearchInCategory = new CustomCommand(SearchTestInCategory, CanSearchTestInCategory);
            CmdOpenWindowWatchStat = new CustomCommand(OpenWindowWatchStat, CanOpenWindowWatchStat);
            CmdDeleteCategory = new CustomCommand(DeleteCategory, CanDeleteCategory);
            CmdEditCategory = new CustomCommand(EditCategory, CanEditCategory);
            CmdAddCategory = new CustomCommand(AddCategory, CanAddCategory);
            CmdOpenWindowStartTest = new CustomCommand(StartTest, CmdCanStartTest);
            CmdDeleteTest = new CustomCommand(DeleteTest, CmdCanDeleteTest);
            CmdOpenWindowEditTest = new CustomCommand(OpenWindowEditTest, CmdCanEditTest);
            CmdOpenWindowCreateTest = new CustomCommand(OpenWindowCreateTest, CmdCanCreateTest);
            
        }

        private bool CanSearchAnyWhereTest(object obj)
        {
            return true;
        }

        private void SearchAnyWhereTest(object obj)
        {
            var searchTest = from c in Сategories 
                             from t in c.Tests
                             where t.TestName.Contains(SearchTest)
                             select c;
            searchTest = searchTest.GroupBy(x => x.CategoryName).Select(y => y.FirstOrDefault());
            Сategories = searchTest.ToObservableCollection();
        }

        private bool CanSearchTestInCategory(object obj)
        {
            return true;
        }

        private void SearchTestInCategory(object obj)
        {
            var searchTest = from c in Сategories
                             where c.CategoryName == SelectedItemComboBox.CategoryName
                             from t in c.Tests
                             where t.TestName.Contains(SearchTest)
                             select c;
            Сategories = searchTest.ToObservableCollection();
        }

        private bool CanOpenWindowWatchStat(object obj)
        {
            if (_selectedTest != null && _selectedTest.AllResults != null)
            {
                return true;
            }
            return false;
        }

        private void OpenWindowWatchStat(object obj)
        {
            Messenger.Default.Send<List<TestResultStatistics>>(_selectedTest.AllResults, ContextMessage.SendTestStatistic);
            dialogService.ShowStatisticBrowser();
        }

        private bool CanDeleteCategory(object obj)
        {
            if (selectedCategory != null)
            { 
                return true;
            }
            return false;
        }
        
        private void DeleteCategory(object obj)
        {
            if (dialogService.ShowDialogWindow(
                TextService.StrDeleteWhat(selectedCategory.CategoryName), TextService.StrDeleteCategory())
                ==true)
            {
                categoryDataService.DeleteCategory(selectedCategory);
                LoadDataTest();
            }
            else { }
            
        }

        private bool CanEditCategory(object obj)
        {
            if (selectedCategory!=null)
            {
                return true;
            }
            return false;
        }

        private void EditCategory(object obj)
        {
            Messenger.Default.Send<Category>(selectedCategory, ContextMessage.SendCategoryEdit);
            dialogService.ShowCreateCategoryView();
        }

        private bool CanAddCategory(object obj)
        {
            return true;
        }

        private void AddCategory(object obj)
        {
            dialogService.ShowCreateCategoryView();
        }

        private void LoadDataTest()
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            var sortedСategories = from u in categoryDataService.GetAllCategories()
                                   from f in u.GroupsOfUsers
                                   where f.ID == customPrincipal.Identity.IdGroup
                                   orderby u.CategoryName
                                   select u;


            //var sortedСategories = from u in categoryDataService.GetAllCategories()
            //                         orderby u.CategoryName
            //                         select u;

            Сategories = ListExtensions.ToObservableCollection<Category>(sortedСategories);
            List<Test> tests = testDataService.GetAllTests();
            List<Question> questions = questionDataService.GetAllTests();
            for (int i = 0; i < Сategories.Count; i++)
            {
                var t = from e in tests
                        where e.CategoryID == Сategories[i].IDCategory
                        orderby e.TestName
                        select e;
                Сategories[i].Tests = t.ToList<Test>();
                for (int j = 0; j < Сategories[i].Tests.Count; j++)
                {
                    var q = from e in questions
                            where e.TestId == Сategories[i].Tests[j].Id
                            select e;
                    Сategories[i].Tests[j].Question = q.ToList<Question>();
                    Сategories[i].Tests[j].TestCategory = Сategories[i];
                    Сategories[i].Tests[j].AllResults = new List<TestResultStatistics>();
                }
            }

        }
    }
}
