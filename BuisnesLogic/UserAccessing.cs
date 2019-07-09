using System.Collections.Generic;
using Common;
using DataAcces;

namespace BuisnesLogic
{
    public class UserAccessing : IUserAccessing
    {
        IUserData _userData;

        public UserAccessing(IUserData usersData)
        {
            _userData = usersData;
        }

        public void Add(User entity)
        {
            _userData.Add(entity);
        }

        public void Delete(int id)
        {
            _userData.Delete(id);
        }

        public void Edit(User entity)
        {
            _userData.Edit(entity);
        }

        public User GetById(int id)
        {
            return _userData.GetById(id);
        }

        public IEnumerable<User> List()
        {
            return _userData.List();
        }
    }
}
