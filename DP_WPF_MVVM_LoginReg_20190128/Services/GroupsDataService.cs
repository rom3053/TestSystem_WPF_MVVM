using DP.MVVM.TestManagement.DAL;
using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class GroupsDataService
    {
        GroupsRepository repository = new GroupsRepository();
        public GroupsDataService()
        {
            this.repository = repository;
        }

        public UserGroup GetGroupDetail(int groupId)
        {
            return repository.GetGroupById(groupId);
        }

        public List<UserGroup> GetAllGroups()
        {
            return repository.GetGroups();
        }

        public void UpdateGroup(UserGroup group)
        {
            repository.UpdateGroup(group);
        }

        public void DeleteGroup(UserGroup group)
        {
            repository.DeleteGroups(group);
        }
        public void AddGroup(UserGroup group)
        {
            repository.AddGroups(group);
        }
    }
}

