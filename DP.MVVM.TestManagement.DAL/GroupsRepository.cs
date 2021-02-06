using DP.MVVM.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.DAL
{
    public class GroupsRepository
    {
        private static List<UserGroup> groups;

        public GroupsRepository()
        {
        }

        public UserGroup GetAGroup()
        {
            if (groups == null)
                LoadGroups();
            return groups.FirstOrDefault();
        }

        public List<UserGroup> GetGroups()
        {
            if (groups == null)
                LoadGroups();
            return groups;
        }

        public UserGroup GetGroupById(int id)
        {
            if (groups == null)
                LoadGroups();
            return groups.Where(c => c.ID == id).FirstOrDefault();
        }

        public void DeleteGroups(UserGroup group)
        {
            groups.Remove(group);
        }
        public void AddGroups(UserGroup group)
        {
            groups.Add(group);
        }
        public void UpdateGroup(UserGroup group)
        {
            UserGroup userToUpdate = groups.Where(c => c.ID == group.ID).FirstOrDefault();
            userToUpdate = group;
        }
        private void LoadGroups()
        {
            groups = new List<UserGroup>()
            {
                new UserGroup()
                {
                    ID = 0,
                    GroupName = "Admin",
                },
                new UserGroup()
                {
                    ID = 1,
                    GroupName = "4OT1",
                },
                new UserGroup()
                {
                    ID = 2,
                    GroupName = "121-16sk-1",
                },
            };

        }
    }
}
