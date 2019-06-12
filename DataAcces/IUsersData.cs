using Common;
using System.Collections.Generic;

namespace DataAcces
{
    public interface IUsersData
    {
        IEnumerable<User> GetUsers();

        User GetUser(int userId);

        bool AddUser(User user);

        bool DelUser(int userId);

        bool UpdateUser(User user);
    }
}
