using System.Collections.Generic;

using Common;

namespace BuisnesLogic
{
    public interface IUserAccessing
    {
        IEnumerable<User> GetUsers();

        User GetUser(int userId);

        bool AddUser(User user);

        bool DelUser(int userId);

        bool UpdateUser(User user);
    }
}
