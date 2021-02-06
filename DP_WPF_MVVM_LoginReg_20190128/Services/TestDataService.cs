using DP.MVVM.TestManagement.DAL;
using DP.MVVM.TestManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class TestDataService
    {
        TestsRepository repository = new TestsRepository();
        public TestDataService()
        {
            this.repository = repository;
        }

        public Test GetTestDetail(int testId)
        {
            return repository.GetTestById(testId);
        }

        public List<Test> GetAllTests()
        {
            return repository.GetTests();
        }

        public void UpdateTest(Test test)
        {
            repository.UpdateTest(test);
        }

        public void DeleteTest(Test test)
        {
            repository.DeleteTest(test);
        }
        public void AddTest(Test test)
        {
            repository.AddTest(test);
        }
    }
}
