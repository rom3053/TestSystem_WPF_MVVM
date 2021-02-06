using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.Model
{
    public class Test : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Test()
        {

        }
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public List<UserGroup> GroupsForUsers { get; set; }
        public Category TestCategory{ get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public string TestTheme { get; set; }
        public string TestAuthor { get; set; }
        public string TestAuthorMail { get; set; }
        public List<Question> Question { get; set; }
        public List<TestResultStatistics> AllResults { get; set; }
    }
}
