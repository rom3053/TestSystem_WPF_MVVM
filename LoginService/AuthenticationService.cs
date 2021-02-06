using DP.MVVM.UserManagement.Model;
using DP_WPF_MVVM_LoginReg_20190128.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.LoginService
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        UserDataService userDataService;
        public AuthenticationService()
        {
            userDataService = new UserDataService();
            List<User> users = userDataService.GetAllUsers();
            foreach (var user in users)
            {
                var temp = new InternalUserData(user.Login, user.Name, user.Password, user.Roles, user.IdGroup);
                _users.Add(temp);
            }
        }
        
        private class InternalUserData
        {
            
            public InternalUserData(string login, string username, string hashedPassword, string[] roles, int idgroup)
            {
                Login = login;
                Username = username;
                HashedPassword = hashedPassword;
                Roles = roles;
                IdGroup = idgroup;
            }
            public string Username
            {
                get;
                private set;
            }

            public string Login
            {
                get;
                private set;
            }

            public string HashedPassword
            {
                get;
                private set;
            }

            public string[] Roles
            {
                get;
                private set;
            }

            public int IdGroup
            {
                get;
                private set;
            }
        }

        private readonly List<InternalUserData> _users = new List<InternalUserData>();

        public User AuthenticateUser(string userLogin, string clearTextPassword)
        {
            InternalUserData userData = _users.FirstOrDefault(u => u.Login.Equals(userLogin)
                && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Login)));
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new User(userData.Login, userData.Username, userData.Roles, userData.IdGroup);
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
