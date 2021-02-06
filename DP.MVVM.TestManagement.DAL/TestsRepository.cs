using DP.MVVM.TestManagement.Model;
using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.DAL
{
    public class TestsRepository
    {
        private static List<Test> tests;

        public TestsRepository()
        {
        }

        public Test GetATest()
        {
            if (tests == null)
                LoadTests();
            return tests.FirstOrDefault();
        }

        public List<Test> GetTests()
        {
            if (tests == null)
                LoadTests();
            return tests;
        }

        public Test GetTestById(int id)
        {
            if (tests == null)
                LoadTests();
            return tests.Where(c => c.Id == id).FirstOrDefault();
        }

        public void DeleteTest(Test test)
        {
            tests.Remove(test);
        }
        public void AddTest(Test test)
        {
            tests.Add(test);
        }
        public void UpdateTest(Test test)
        {
            Test testToUpdate = tests.Where(c => c.Id == test.Id).FirstOrDefault();
            testToUpdate = test;
        }
        private void LoadTests()
        {
            tests = new List<Test>()
            {
                new Test()
                {
                    Id = 0,
                    CategoryID = 0,
                    TestAuthor = "RomanTheFirst",
                    TestAuthorMail = "romanthefirst@gmail.com",
                    TestName = "MathematicsTestOne",
                    Question = new List<Question>(),
                    TestDescription = "TestDescription2"
                },
                new Test()
                {
                    Id = 1,
                    CategoryID = 0,
                    TestAuthor = "RomanTheSecond",
                    TestAuthorMail = "romanthesecond@gmail.com",
                    TestName = "MathematicsTestTwo",
                    Question = new List<Question>(),
                    TestDescription = "TestDescriptio32"
                },
                new Test()
                {
                    Id = 2,
                    CategoryID = 1,
                    TestAuthor = "RomanTheFirst",
                    TestAuthorMail = "romanthefirst@gmail.com",
                    TestName = "EnglishTestOne",
                    Question = new List<Question>(),
                    TestDescription = "TestDescription3232dfd1"
                },
                new Test()
                {
                    Id = 3,
                    CategoryID = 1,
                    TestAuthor = "RomanTheSecond",
                    TestAuthorMail = "romanthesecond@gmail.com",
                    TestName = "EnglishTestTwo",
                    Question = new List<Question>(),
                    TestDescription = "TestDescription1"
                }
            };

        }
    }
}
