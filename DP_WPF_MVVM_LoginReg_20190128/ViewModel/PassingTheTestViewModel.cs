using DP.MVVM.TestManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.LoginService.Identity;
using DP_WPF_MVVM_LoginReg_20190128.Models;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class PassingTheTestViewModel : ViewModelBase
    {
        public Test PassingTest { get; set; } = new Test();
        private GroupsDataService groupsData;
        private AnswerModel testModel;
        List<Question> _questions = new List<Question>();
        private bool _isNable = true;
        public bool IsNable { get { return _isNable; } set { _isNable = value; OnPropertyChanged("IsNable"); } } 
        public ICommand AddCommand { get; private set; }
        public ICommand CmdEnd { get; private set; }
        public PassingTheTestViewModel()
        {
            Messenger.Default.Register<Test>(this, OnTestReceived);
        }
        public PassingTheTestViewModel(StackPanel stkpnlDynamicControls, Test test)
        {
            Messenger.Default.Register<Test>(this, OnTestReceived);
            testModel = new AnswerModel();
            StackPanelForAnswers.stkPanel = stkpnlDynamicControls;
            groupsData = new GroupsDataService();
            PassingTest = test;
            _questions = test.Question;
            AddCommand = new CustomCommand(CreateViewTestAnswer, CanAddMethod);
            CmdEnd = new CustomCommand(EndTest, CanEndTest);
        }

        private bool CanEndTest(object obj)
        {
            return true;
        }

        private void EndTest(object obj)
        {
            List<bool> testAnsRight = new List<bool>();
            List<bool> testAnsWrong = new List<bool>();
            Dictionary<int, int[]> questionsSelectedAnswers = new Dictionary<int, int[]>();
            for (int i = 0; i < StackPanelForAnswers.stkPanel.Children.Count; i++)
            {
                if (StackPanelForAnswers.stkPanel.Children[i] is StackPanel)
                {
                    StackPanel temp = (StackPanel)StackPanelForAnswers.stkPanel.Children[i];
                    int[] SelectedAnswers = new int[temp.Children.Count];
                    for (int j = 0; j < temp.Children.Count; j++)
                    {

                        if (temp.Children[j] is RadioButton)
                        {
                            RadioButton tempRB = (RadioButton)temp.Children[j];
                            
                            if (tempRB.IsChecked == true)
                            {
                                SelectedAnswers[j] = j;
                                TextBlock textBlock = (TextBlock)tempRB.Content;

                                bool answer = textBlock.Text.Equals(_questions[i].TrueAnswer);
                                var selAnswr = from s in SelectedAnswers
                                               where s != 0
                                               select s;

                                questionsSelectedAnswers.Add(i + 1, selAnswr.ToArray());
                                if (answer)
                                {
                                    textBlock.Foreground = Brushes.Green;
                                    testAnsRight.Add(true);
                                }
                                else
                                {
                                    textBlock.Foreground = Brushes.Red;
                                    testAnsWrong.Add(false);
                                }
                            }
                        }
                    }
                }
                IsNable = false;
            }

            double result = CalculateResult(testAnsWrong);

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            TestResultStatistics testResultStatistics = new TestResultStatistics()
            {

                GroupName = groupsData.GetGroupDetail(customPrincipal.Identity.IdGroup).GroupName,
                StatsID = PassingTest.AllResults.Count+1,
                StatsQuestion = _questions,
                StatsResultTest = result,
                StatsSelectedAnswers = questionsSelectedAnswers,
                StatsTestCategory = PassingTest.TestCategory.CategoryName,
                StatsTestCompletionDate = DateTime.Now.ToString(),
                StatsTestName = PassingTest.TestName,
                TestID = PassingTest.Id,
                UserID = 1,
                UserLogin = customPrincipal.Identity.Login,
                UserName = customPrincipal.Identity.Name
            };

            PassingTest.AllResults.Add(testResultStatistics);
            MessageBox.Show(TextService.StrResult(result.ToString(), testAnsRight.Count.ToString(), testAnsWrong.Count.ToString()), "The result of the passed test");
        }

        private double CalculateResult(List<bool> testAnsWrong)
        {
            double result = ((double)testAnsWrong.Count / (double)_questions.Count) * 100.0;
            double result2 = 100.0 - result;
            return result2;
        }

        public AnswerModel StackPanelForAnswers
        {
            get { return testModel; }
            set { SetProperty(ref testModel, value); }
        }
        private void OnTestReceived(Test obj)
        {
            if (obj != null)
            {
                PassingTest = obj;

            }
        }
        private bool CanAddMethod(object obj)
        {
            return true;
        }
        private void CreateViewTestAnswer(object oj)
        {
            AddStackPanelsToView(GetStackPanelsAnswers(_questions));
        }

        private StackPanel[] GetStackPanelsAnswers(List<Question> questions)
        {
            StackPanel[] stackPanels = new StackPanel[questions.Count];
            for (int i = 0; i < stackPanels.Length; i++)
            {
                stackPanels[i] = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(20d),
                };

                TextBox textBoxQuestionNumber = new TextBox
                {
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 20,
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Question "+(i+1)+".",
                    IsReadOnly = true,
                    BorderThickness = new Thickness(0),
                };
                TextBox textBox = new TextBox
                {
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    TextWrapping = TextWrapping.Wrap,
                    Text = questions[i].QuestionText,
                    IsReadOnly = true,
                    BorderThickness = new Thickness(2d),
                };
                stackPanels[i].Children.Add(textBoxQuestionNumber);
                stackPanels[i].Children.Add(textBox);
                for (int j = 0; j < questions[i].Answer.Count; j++)
                {
                    RadioButton buttons2 = new RadioButton// создаются с массива ответов в Вопросе
                    {

                        Margin = new Thickness(8d),
                        GroupName = i.ToString(),

                        Content = new TextBlock
                        {
                            FontSize = 16,
                            TextWrapping = TextWrapping.Wrap,
                            Text = questions[i].Answer[j].AnswerText
                        }

                    };
                    stackPanels[i].Children.Add(buttons2);
                }
            }
            return stackPanels;
        }
        private void AddStackPanelsToView(StackPanel[] stkPanelWithQuestions)
        {
            for (int i = 0; i < stkPanelWithQuestions.Length; i++)
            {
                StackPanelForAnswers.stkPanel.Children.Add(stkPanelWithQuestions[i]);
            }

        }
    }
}
