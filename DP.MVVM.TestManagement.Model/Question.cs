using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.Model
{
    public class Question : INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        public string TrueAnswer { get; set; }
        public string IDTrueAnswer { get; set; }
        public int TestId { get; set; }
        public List<Answer> Answer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                questionText = value;
                //RaisePropertyChanged("QuestionText");
            }
        }
    }
}
