using DP.MVVM.TestManagement.DAL;
using DP.MVVM.TestManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class CategoryDataService
    {
        CategoriesRepository repository = new CategoriesRepository();
        public CategoryDataService()
        {
            this.repository = repository;
        }

        public Category GetCategoryDetail(int categoryId)
        {
            return repository.GetCategoryById(categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return repository.GetCategories();
        }

        public void UpdateCategory(Category category)
        {
            repository.UpdateUser(category);
        }

        public void DeleteCategory(Category category)
        {
            repository.DeleteUser(category);
        }
        public void AddCategory(Category category)
        {
            repository.AddCategory(category);
        }
    }
}
