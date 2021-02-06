using DP.MVVM.TestManagement.Model;
using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.DAL
{
    public class CategoriesRepository
    {
        private static List<Category> categories;

        public CategoriesRepository()
        {
        }

        public Category GetAUser()
        {
            if (categories == null)
                LoadCategories();
            return categories.FirstOrDefault();
        }

        public List<Category> GetCategories()
        {
            if (categories == null)
                LoadCategories();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            if (categories == null)
                LoadCategories();
            return categories.Where(c => c.IDCategory == id).FirstOrDefault();
        }

        public void DeleteUser(Category category)
        {
            categories.Remove(category);
        }
        public void AddCategory(Category category)
        {
            categories.Add(category);
        }
        public void UpdateUser(Category category)
        {
            Category userToUpdate = categories.Where(c => c.IDCategory == category.IDCategory).FirstOrDefault();
            userToUpdate = category;
        }
        private void LoadCategories()
        {
            categories = new List<Category>()
            {
                new Category()
                {
                    IDCategory = 0,
                    CategoryName = "Mathematics",
                    GroupsOfUsers = new List<UserGroup>(){ new UserGroup{ID = 1} , new UserGroup{ID = 0}},
                    Tests = new List<Test>()
                },
                new Category()
                {
                    IDCategory = 1,
                    CategoryName = "English",
                    GroupsOfUsers = new List<UserGroup>(){ new UserGroup{ID = 2}, new UserGroup{ID = 0}},
                    Tests = new List<Test>()
                },
            };
            
        }
    }
}
