using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.LoginService.Identity
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string login, string[] roles, int idGroup)
        {
            Name = name;
            Login = login;
            Roles = roles;
            IdGroup = idGroup;
        }

        public string Name { get; private set; }
        public string Login { get; private set; }
        public int IdGroup { get; private set; }
        public string[] Roles { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
