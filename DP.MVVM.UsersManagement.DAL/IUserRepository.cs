using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.UsersManagement.DAL
{
    public interface IUserRepository
    {
        void DeleteUser(User user);
        User GetAUser();
        User GetUserById(int id);
        List<User> GetUsers();
        void UpdateUser(User user);
        void AddUser(User user);
    }
}
