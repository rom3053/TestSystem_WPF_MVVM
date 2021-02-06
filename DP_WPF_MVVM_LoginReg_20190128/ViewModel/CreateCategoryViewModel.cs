using DP.MVVM.TestManagement.Model;
using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Messages;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using DP_WPF_MVVM_LoginReg_20190128.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_WPF_MVVM_LoginReg_20190128.ViewModel
{
    public class CreateCategoryViewModel: ViewModelBase
    {

        private CategoryDataService categoryDataService;
        private Category _categoryReceived;
        private List<Category> _categories = new List<Category>();
        public CreateCategoryViewModel()
        {
            Messenger.Default.Register<Category>(this, OnCategoryReceived, ContextMessage.SendCategoryEdit);
            categoryDataService = new CategoryDataService();
            _categories = categoryDataService.GetAllCategories();
            LoadCommand();
        }

        private void OnCategoryReceived(Category obj)
        {
            _categoryReceived = obj;
            NameCategory = obj.CategoryName;
            ContentButtonAccept = "Edit";
        }

        private string _nameCategory;

        public string NameCategory
        {
            get { return _nameCategory; }
            set { _nameCategory = value; OnPropertyChanged("NameCategory"); }
        }
        private string _contentButton = "Create new";

        public string ContentButtonAccept
        {
            get { return _contentButton; }
            set { _contentButton = value; OnPropertyChanged("ContentButton"); }
        }

        public ICommand CmdCreateNewCategory { get; set; }
        public ICommand CmdCancel { get; set; }
        private void LoadCommand()
        {
            CmdCreateNewCategory = new CustomCommand(CreateNewCategory, CanCreateCategory);
            CmdCancel = new CustomCommand(CancelCreate, CanCancelCreate);
        }

        private bool CanCancelCreate(object obj)
        {
            return true;
        }

        private void CancelCreate(object obj)
        {
            Messenger.Default.Send<CancelCategoriesMessage>(new CancelCategoriesMessage(), ContextMessage.SendBackCategoryCancel);
        }

        private bool CanCreateCategory(object obj)
        {
            
            if (NameCategory!=null && NameCategory!=String.Empty)
            {
                return true;
                
            }
            
            return false;
        }

        private void CreateNewCategory(object obj)
        {
            if (_categoryReceived!=null)
            {
                _categoryReceived.CategoryName = NameCategory;
                categoryDataService.UpdateCategory(_categoryReceived);
                _categoryReceived = null;
                _contentButton = "Create new";
            }
            else
            {
                categoryDataService.AddCategory
                (new Category()
                            {
                                IDCategory = categoryDataService.GetAllCategories().Count + 1,
                                CategoryName = NameCategory,
                                GroupsOfUsers = new List<UserGroup>() { new UserGroup { ID = 0 } },
                                Tests = new List<Test>(),
                            }
                );
                
            }
            Messenger.Default.Send<UpdateListCategoriesMessage>(new UpdateListCategoriesMessage(), ContextMessage.UpdateCategoryInCategoryBrwoser);
            Messenger.Default.Send<UpdateListCategoriesMessage>(new UpdateListCategoriesMessage(), ContextMessage.UpdateCategoryInCreateTest);
        }
    }
}
