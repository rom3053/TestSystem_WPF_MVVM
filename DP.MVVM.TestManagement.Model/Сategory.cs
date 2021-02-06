using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DP.MVVM.TestManagement.Model
{
    public class Category: INotifyPropertyChanged
    {
        public Category()
        {

        }
        public Category(string CategoryName)
        {
            this.CategoryName = CategoryName;
        }
        public int IDCategory
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
                RaisePropertyChanged("IDCategory");
            }
        }
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                RaisePropertyChanged("CategoryName");
            }
        }
        public List<Test> Tests
        {
            get
            {
                return _tests;
            }
            set
            {
                _tests = value;
                RaisePropertyChanged("Tests");
            }
        }
        public List<UserGroup> GroupsOfUsers { get; set; }

        int categoryID;
        string categoryName;
        List<Test> _tests { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
