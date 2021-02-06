using DP.MVVM.TestManagement.DAL;
using DP.MVVM.TestManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class QuestionDataService
    {
        QuestionRepository repository = new QuestionRepository();
        public QuestionDataService()
        {
            this.repository = repository;
        }

        public Question GetTestDetail(int questionId)
        {
            return repository.GetQuestionById(questionId);
        }

        public List<Question> GetAllTests()
        {
            return repository.GetQuestions();
        }

        public void UpdateTest(Question question)
        {
            repository.UpdateQuestion(question);
        }

        public void DeleteTest(Question question)
        {
            repository.DeleteQuestion(question);
        }
        public void AddListQuestions(List<Question> questions,int idTest)
        {
            repository.AddListQuestions(questions, idTest);
        }
    }
}
