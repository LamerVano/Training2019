using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class UserAccessing : IUserAccessing
    {
        IUsersData _userData;

        public UserAccessing(IUsersData usersData)
        {
            _userData = usersData;
        }

        public bool AddUser(User user)
        {
            return _userData.AddUser(user);
        }

        public bool UpdateUser(User user)
        {
            return _userData.UpdateUser(user);
        }

        public bool DelUser(int userId)
        {
            return _userData.DelUser(userId);
        }

        public User GetUser(int userId)
        {
            return _userData.GetUser(userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userData.GetUsers();
        }
    }
}
