using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DP.MVVM.UsersManagement.DAL
{
    public class UserRepository : IUserRepository
    {
        private static List<User> users;

        public UserRepository()
        {
        }

        public User GetAUser()
        {
            if (users == null)
                LoadUsers();
            return users.FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            if (users == null)
                LoadUsers();
            return users;
        }

        public User GetUserById(int id)
        {
            if (users == null)
                LoadUsers();
            return users.Where(c => c.UserId == id).FirstOrDefault();
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            User userToUpdate = users.Where(c => c.UserId == user.UserId).FirstOrDefault();
            userToUpdate = user;
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }
        private void LoadUsers()
        {
            users = new List<User>()
            {
                new User ()
                {
                    UserId = 0,
                    Name = "Admin",
                    Login = "Admin",
                    Password = "u7exn+HtLzKIPTIjQkbYhGLfUo8aBdNYlXru2AWkR+A=",
                    Roles = new string[] { "Administrators" },
                    IdGroup = 0,
                },
                new User ()
                {
                    UserId = 1,
                    Name = "Mark R.R.",
                    Login ="Mark",
                    Password = "MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE=",
                    Roles = new string[] { },
                    IdGroup = 1,
                },
                new User ()
                {
                    UserId = 2,
                    Name = "John J.J.",
                    Login = "John",
                    Password = "hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=",
                    Roles = new string[] { },
                    IdGroup = 2,
                }
            };
        }

    }
}
