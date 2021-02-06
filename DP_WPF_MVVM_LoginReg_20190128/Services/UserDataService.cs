using DP.MVVM.UserManagement.Model;
using DP.MVVM.UsersManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class UserDataService: IDataService
    {
        IUserRepository repository = new UserRepository();
        public UserDataService()
        {
            this.repository = repository;
        }

        public User GetUserDetail(int userId)
        {
            return repository.GetUserById(userId);
        }

        public List<User> GetAllUsers()
        {
            return repository.GetUsers();
        }

        public void UpdateUser(User user)
        {
            repository.UpdateUser(user);
        }

        public void DeleteUser(User user)
        {
            repository.DeleteUser(user);
        }
        public void AddUser(User user)
        {
            repository.AddUser(user);
        }
    }
}
