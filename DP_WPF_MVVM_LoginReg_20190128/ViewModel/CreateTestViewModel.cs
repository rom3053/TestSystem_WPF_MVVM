using DP.MVVM.TestManagement.Model;
using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Extensions;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class CreateTestViewModel : ViewModelBase
    {
        private void LoadDataString()
        {
            _testName = "TestName";
            _testDescription = "TestDescription ";
            _testTheme = "TestTheme";
            _testAuthor = "TestAuthor";
            _testAuthorMail = "TestAuthorMail";
        }
        
        public CreateTestViewModel()
        {
            dialogService = new DialogService();
            categoryDataService = new CategoryDataService();
            groupsDataService = new GroupsDataService();
            testDataService = new TestDataService();
            questionDataService = new QuestionDataService();
            Categories = categoryDataService.GetAllCategories().ToObservableCollection();
            //GroupsForUsers = groupsDataService.GetAllGroups().ToObservableCollection();
            LoadCommands();
            Messenger.Default.Register<Test>(this, GetTestForEdit,ContextMessage.SendTestEdit);
            Messenger.Default.Register<ObservableCollection<UserGroup>>(this, GetGroup, ContextMessage.SendBackGroupsToCreateTestViewModel);
            Messenger.Default.Register<CancelTwoListBoxesGroup>(this, CancelAddGroupsIntoTest);
            Messenger.Default.Register<UpdateListCategoriesMessage>(this, UpdateCategories, ContextMessage.UpdateCategoryInCreateTest);
            LoadDataString();
        }

        private void UpdateCategories(UpdateListCategoriesMessage obj)
        {
            Categories = categoryDataService.GetAllCategories().ToObservableCollection();
        }

        private void GetTestForEdit(Test obj)
        {
            Id = obj.Id;
            Questions = obj.Question.ToObservableCollection();
            TestName = obj.TestName;
            TestDescription = obj.TestDescription;
            TestAuthor = obj.TestAuthor;
            TestAuthorMail = obj.TestAuthorMail;
            if (obj.GroupsForUsers != null)
            {
                GroupsForUsers = obj.GroupsForUsers.ToObservableCollection();
                foreach (var item in obj.GroupsForUsers)
                {
                    SelectedGroups += item.GroupName + ", ";
                }
            }

            SelectedItemComboBox = obj.TestCategory;
        }

        #region Test`sTextProperty 
        private string _testName;
        private string _testDescription;
        private string _testTheme;
        private string _testAuthor;
        private string _testAuthorMail;

        public int Id { get; set; }
        public string TestName {
            get
            {
                return _testName;
            }
            set
            {
                _testName = value;
                OnPropertyChanged("TestName");
            }
        }
        public string TestDescription
        {
            get
            {
                return _testDescription;
            }
            set
            {
                _testDescription = value;
                OnPropertyChanged("TestDescription");
            }
        }
        public string TestTheme
        {
            get
            {
                return _testTheme;
            }
            set
            {
                _testTheme = value;
                OnPropertyChanged("TestTheme");
            }
        }
        public string TestAuthor
        {
            get
            {
                return _testAuthor;
            }
            set
            {
                _testAuthor = value;
                OnPropertyChanged("TestAuthor");
            }
        }
        public string TestAuthorMail
        {
            get
            {
                return _testAuthorMail;
            }
            set
            {
                _testAuthorMail = value;
                OnPropertyChanged("TestAuthorMail");
            }
        }

        private string _questionAnswerOne;
        public string QuestionAnswerOne
        {
            get { return _questionAnswerOne; }
            set
            {
                if (value != null)
                {
                    _questionAnswerOne = value;
                }
                else { _questionAnswerOne = " "; }
                OnPropertyChanged("QuestionAnswerOne"); }
        }
        private string _questionAnswerTwo;
        public string QuestionAnswerTwo
        {
            get { return _questionAnswerTwo; }
            set
            {
                if (value != null)
                {
                    _questionAnswerTwo = value;
                }
                else { _questionAnswerTwo = " "; }
                OnPropertyChanged("QuestionAnswerTwo"); }
        }
        private string _questionAnswerThree;
        public string QuestionAnswerThree
        {
            get { return _questionAnswerThree; }
            set
            {
                if (value != null)
                {
                    _questionAnswerThree = value;
                }
                else { _questionAnswerThree = " "; }
                OnPropertyChanged("QuestionAnswerThree"); }
        }
        private string _questionAnswerFour;
        public string QuestionAnswerFour
        {
            get { return _questionAnswerFour; }
            set
            {
                if (value != null)
                {
                    _questionAnswerFour = value;
                }
                else { _questionAnswerFour = " "; }

                OnPropertyChanged("QuestionAnswerFour");
            }
        }
        #endregion
        #region DataService
        private DialogService dialogService;
        private CategoryDataService categoryDataService;
        private GroupsDataService groupsDataService;
        //private UserDataService userDataService;
        private TestDataService testDataService;
        private QuestionDataService questionDataService;
        #endregion
        private ObservableCollection<UserGroup> groupForUsers = new ObservableCollection<UserGroup>();

        private string _myProp;
        public string SelectedGroups
        {
            get { return _myProp; }
            set { _myProp = value; OnPropertyChanged("SelectedGroups"); }
        }

        public ObservableCollection<UserGroup> GroupsForUsers
        {
            get { return groupForUsers; }
            set { groupForUsers = value; OnPropertyChanged("GroupsForUsers"); }
        }
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        #region Combobox Property
        private Category _selectedCategory;
        public Category SelectedItemComboBox
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged("SelectedItemComboBox"); }
        }
        #endregion



        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _questions;
            }
            set
            {
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }
        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get
            {
                if (_selectedQuestion!=null)
                {

                    if (_selectedQuestion.Answer!=null)
                    {
                        for (int i = 0; i < _selectedQuestion.Answer.Count; i++)
                        {
                            QuestionAnswersOnView(i);
                        }
                        CorrectAnswer = _selectedQuestion.IDTrueAnswer;
                    }
                    else
                    {
                        QuestionAnswerOne = "";
                        QuestionAnswerTwo = "";
                        QuestionAnswerThree = "";
                        QuestionAnswerFour = "";
                        CorrectAnswer = "";
                    }
                }
                return _selectedQuestion;
                
            }
            set
            {
                _selectedQuestion = value;

                
                OnPropertyChanged("SelectedQuestion");

                if (_selectedQuestion != null)
                {
                    if (_selectedQuestion.Answer != null)
                    {
                       for (int i = 0; i < _selectedQuestion.Answer.Count; i++)
                        {
                            QuestionAnswersOnView(i);
                        }
                    }
                }
            }
        }
        private void QuestionAnswersOnView(int indexAnswer)
        {
            switch (indexAnswer)
            {
                case 0 :
                    QuestionAnswerOne = _selectedQuestion.Answer[indexAnswer].AnswerText;
                    break;
                case 1:
                    QuestionAnswerTwo = _selectedQuestion.Answer[indexAnswer].AnswerText;
                    break;
                case 2:
                    QuestionAnswerThree = _selectedQuestion.Answer[indexAnswer].AnswerText;
                    break;
                case 3:
                    QuestionAnswerFour = _selectedQuestion.Answer[indexAnswer].AnswerText;
                    break;
                default:
                    break;
            }
        }
        private TabItem _tabControl;
        public TabItem TabControl
        {
            get
            {
                return _tabControl;
            }
            set
            {
                _tabControl = value;
                OnPropertyChanged("TabControl");
            }
        }
        private string _questionText;
        public string QuestionText
        {
            get { return _questionText; }
            set { _questionText = value; OnPropertyChanged("QuestionText"); }
        }
        private int _selectedQuestionIndex;
        public int SelectedQuestionIndex
        {
            get
            {
                return _selectedQuestionIndex;
            }
            set
            {
                _selectedQuestionIndex = value;
                OnPropertyChanged("SelectedQuestionIndex");
            }
        }
        private string _correctAnswer = "Variant 1";
        

        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set
            {
                _correctAnswer = value;
                OnPropertyChanged("CorrectAnswer");
            }
        }

        public ICommand SelectionGroupsForTestCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddTrueAnswerRadioButtonCommand { get; set; }
        public ICommand MoveUPCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CreateQuestionCommand { get; set; }
        public ICommand CreateTestCommand { get; set; }

        private void LoadCommands()
        {
            SelectionGroupsForTestCommand = new CustomCommand(SelectionGroupsForTest, CanSelectionGroupsForTest);
            EditQuestionCommand = new CustomCommand(EditQuestion, CanEditQuestion);
            CreateQuestionCommand = new CustomCommand(CreateQuestion,CanCreateQuestion);
            MoveDownCommand = new CustomCommand(MoveDownQuestion, CanMoveDownQuestion);
            MoveUPCommand = new CustomCommand(MoveUPQuestion, CanMoveUPQuestion);
            DeleteCommand = new CustomCommand(DeleteQuestion, CanDeleteQuestion);
            AddTrueAnswerRadioButtonCommand = new CustomCommand(AddTrueAnswerRadioButton, CanAddTrueAnswerRadioButton);

            CreateTestCommand = new CustomCommand(CreateTest, CanCreateTest);
        }

        private bool CanCreateTest(object obj)
        {
            return true;
        }

        private void CreateTest(object obj)
        {
            questionDataService.AddListQuestions(Questions.ToList(), testDataService.GetAllTests().Count);
            Test test = new Test()
            {
                Id = testDataService.GetAllTests().Count,
                CategoryID = SelectedItemComboBox.IDCategory,
                TestCategory = SelectedItemComboBox,
                GroupsForUsers = this.GroupsForUsers.ToList(),
                
                Question = this.Questions.ToList(),

                TestAuthor = this.TestAuthor,
                TestAuthorMail = this.TestAuthorMail,
                
                TestDescription = this.TestDescription,
                TestName = this.TestName,
                TestTheme = this.TestTheme,

                AllResults = new List<TestResultStatistics>(),
            };
            testDataService.AddTest(test);
            Messenger.Default.Send<UpdateNewTest>(new UpdateNewTest());
        }

        private bool CanSelectionGroupsForTest(object obj)
        {
            return true;
        }

        private void SelectionGroupsForTest(object obj)
        {
            Messenger.Default.Send(groupForUsers);
            dialogService.ShowTwoListBoxesGroupSelectionView();
        }

        private void AddTrueAnswerRadioButton(object parametr)
        {
            CorrectAnswer = (String)parametr;
        }
        private void EditQuestion(object obj)
        {
            Question temp = new Question();
            temp.Id = _selectedQuestionIndex;
            temp.QuestionText = _selectedQuestion.QuestionText;
            temp.Answer = new List<Answer>();
            Grid grid = (Grid)_tabControl.Content;
            for (int j = 4; j < grid.Children.Count; j++)
            {
                Answer f = new Answer
                {
                    AnswerText = (grid.Children[j] as TextBox).Text,
                };
                temp.Answer.Add(f);
            }
            temp.TrueAnswer = temp.Answer[Get_CorrectAnswer(_correctAnswer)].AnswerText;
            temp.IDTrueAnswer = _correctAnswer;
            _questions[_selectedQuestionIndex] = temp;
        }
        private int Get_CorrectAnswer(string correctAnswer)
        {
            int indexCorrectAnswer = 0;
            switch (correctAnswer)
            {
                case "Variant 1":
                    indexCorrectAnswer = 0;
                    break;
                case "Variant 2":
                    indexCorrectAnswer = 1;
                    break;
                case "Variant 3":
                    indexCorrectAnswer = 2;
                    break;
                case "Variant 4":
                    indexCorrectAnswer = 3;
                    break;
                default:
                    break;
            }
            return indexCorrectAnswer;
        }
        private bool CanEditQuestion(object obj)
        {
            if (_selectedQuestion != null)
                return true;
            return false;
        }

        private void CreateQuestion(object obj)
        {
            int qtnCount = _questions.Count + 1;
            _questions.Add(new Question { Id = qtnCount, QuestionText = "Question" + qtnCount.ToString() });
        }
        #region MoveQuestionMethods
        private void DeleteQuestion(object obj)
        {
            _questions.RemoveAt(_selectedQuestionIndex);
        }
        private void MoveDownQuestion(object obj)
        {
            _questions.Move(_selectedQuestionIndex, _selectedQuestionIndex + 1);
            
        }
        private void MoveUPQuestion(object obj)
        {
            _questions.Move(_selectedQuestionIndex, _selectedQuestionIndex - 1);
        }
        private bool CanMoveUPQuestion(object obj)
        {
            if (_selectedQuestion != null && _selectedQuestionIndex != 0)
                return true;
            return false;
        }
        private bool CanMoveDownQuestion(object obj)
        {
            if (_selectedQuestion != null && _selectedQuestionIndex != _questions.Count-1)
                return true;
            return false;
        }
        private bool CanDeleteQuestion(object obj)
        {
            if (_selectedQuestionIndex != (-1))
                return true;
            return false;
        }
        private bool CanCreateQuestion(object obj)
        {
            if (_questions != null)
                return true;
            return false;
        }
        #endregion

        private bool CanAddTrueAnswerRadioButton(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #region MessageMethods
        private void CancelAddGroupsIntoTest(CancelTwoListBoxesGroup obj)
        {
            dialogService.CloseDetailDialog();
        }

        private void GetGroup(ObservableCollection<UserGroup> obj)
        {
            groupForUsers = obj;
            foreach (var item in obj)
            {
                SelectedGroups = SelectedGroups + item.GroupName+", ";
            }
        }
        #endregion
    }
}
