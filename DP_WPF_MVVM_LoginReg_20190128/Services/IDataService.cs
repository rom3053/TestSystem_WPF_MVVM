
using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    interface IDataService
    {
        void DeleteUser(User user);
        List<User> GetAllUsers();
        User GetUserDetail(int userId);
        void UpdateUser(User user);
    }
}
