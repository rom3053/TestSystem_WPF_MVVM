using DP.MVVM.TestManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.DAL
{
    public class QuestionRepository
    {
        private static List<Question> questions;

        public QuestionRepository()
        {
        }

        public Question GetAQuestion()
        {
            if (questions == null)
                LoadCategories();
            return questions.FirstOrDefault();
        }

        public List<Question> GetQuestions()
        {
            if (questions == null)
                LoadCategories();
            return questions;
        }

        public Question GetQuestionById(int id)
        {
            if (questions == null)
                LoadCategories();
            return questions.Where(c => c.Id == id).FirstOrDefault();
        }

        public void DeleteQuestion(Question question)
        {
            questions.Remove(question);
        }

        public void UpdateQuestion(Question question)
        {
            Question questionToUpdate = questions.Where(c => c.Id == question.Id).FirstOrDefault();
            questionToUpdate = question;
        }
        public void AddListQuestions(List<Question> qstn,int idTest)
        {
            foreach (var q in qstn)
            {
                q.TestId = idTest;
                q.Id = questions.Count;
                questions.Add(q);
            }
        }
        private void LoadCategories()
        {
            questions = new List<Question>()
            {
                new Question()
                {
                    Id = 0,
                    TestId = 0, 
                    QuestionText = "2+2",
                    TrueAnswer = "4",
                    Answer = new List<Answer>()
                    {
                        new Answer(){ Id = 0, QuestionId = 0, AnswerText = "5"},
                        new Answer(){ Id = 1, QuestionId = 0, AnswerText = "3"},
                        new Answer(){ Id = 2, QuestionId = 0, AnswerText = "4"},
                        new Answer(){ Id = 3, QuestionId = 0, AnswerText = "8"}
                    }
                },
                new Question()
                {
                    Id = 1,
                    TestId = 0,
                    QuestionText = "2+2*2",
                    TrueAnswer = "6",
                    Answer = new List<Answer>()
                    {
                        new Answer(){ Id = 4, QuestionId = 1, AnswerText = "5"},
                        new Answer(){ Id = 5, QuestionId = 1, AnswerText = "3"},
                        new Answer(){ Id = 6, QuestionId = 1, AnswerText = "6"},
                        new Answer(){ Id = 7, QuestionId = 1, AnswerText = "8"}
                    }
                },
                //TQ2
                new Question()
                {
                    Id = 2,
                    TestId = 1,
                    QuestionText = "80+90",
                    TrueAnswer = "170",
                    Answer = new List<Answer>()
                    {
                        new Answer(){ Id = 8, QuestionId = 2, AnswerText = "190"},
                        new Answer(){ Id = 9, QuestionId = 2, AnswerText = "180"},
                        new Answer(){ Id = 10, QuestionId = 2, AnswerText = "170"},
                        new Answer(){ Id = 11, QuestionId = 2, AnswerText = "160"}
                    }
                },
                new Question()
                {
                    Id = 3,
                    TestId = 1,
                    QuestionText = "50/5",
                    TrueAnswer = "10",
                    Answer = new List<Answer>()
                    {
                        new Answer(){ Id = 12, QuestionId = 2, AnswerText = "10"},
                        new Answer(){ Id = 13, QuestionId = 2, AnswerText = "15"},
                        new Answer(){ Id = 14, QuestionId = 2, AnswerText = "12"},
                        new Answer(){ Id = 15, QuestionId = 2, AnswerText = "5"}
                    }
                }
            };

        }
    }
}
