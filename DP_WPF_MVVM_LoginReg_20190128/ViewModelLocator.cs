using DP_WPF_MVVM_LoginReg_20190128.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128
{
    public class ViewModelLocator
    {
        private static UsersOverviewViewModel usersOverviewViewModel
            = new UsersOverviewViewModel();
        private static UserDetailViewModel userDetailViewModel =
            new UserDetailViewModel();
        private static CreateTestViewModel createTestViewModel =
            new CreateTestViewModel();
        private static TestBrowserViewModel testBrowserViewModel =
            new TestBrowserViewModel();
        private static PassingTheTestViewModel passingTheTestViewModel =
            new PassingTheTestViewModel();
        private static TestStatisticBrowserViewModel testStatisticBrowser =
            new TestStatisticBrowserViewModel();
        private static TwoListBoxesGroupSelectionViewModel twoListViewModel =
            new TwoListBoxesGroupSelectionViewModel();
        private static CreateCategoryViewModel createCategory =
            new CreateCategoryViewModel();
        private static GroupsOverviewViewModel groupsOverview =
            new GroupsOverviewViewModel();
        public static CreateTestViewModel CreateTestViewModel
        {
            get
            {
                return createTestViewModel;
            }
        }
        public static UserDetailViewModel UserDetailViewModel
        {
            get
            {
                return userDetailViewModel;
            }
        }

        public static UsersOverviewViewModel UsersOverviewViewModel
        {
            get
            {
                return usersOverviewViewModel;
            }
        }
        public static TestBrowserViewModel TestBrowserViewModel
        {
            get
            {
                return testBrowserViewModel;
            }
        }
        public static PassingTheTestViewModel PassingTheTestViewModel
        {
            set
            {
                passingTheTestViewModel = value;
            }
            get
            {
                return passingTheTestViewModel;
            }
        }
        public static TestStatisticBrowserViewModel TestStatisticBrowserViewModel
        {
            set
            {
                testStatisticBrowser = value;
            }
            get
            {
                return testStatisticBrowser;
            }
        }
        public static TwoListBoxesGroupSelectionViewModel TwoListBoxesGroupSelectionViewModel
        {
            set
            {
                twoListViewModel = value;
            }
            get
            {
                return twoListViewModel;
            }
        }
        public static CreateCategoryViewModel CreateCategoryViewModel
        {
            set
            {
                createCategory = value;
            }
            get
            {
                return createCategory;
            }
        }
        public static GroupsOverviewViewModel GroupsOverviewViewModel
        {
            set
            {
                groupsOverview = value;
            }
            get
            {
                return groupsOverview;
            }
        }
    }
}
