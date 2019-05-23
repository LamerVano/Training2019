using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IUser
    {
        int UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EMail { get; set; }        
        string Password { get; set; }        
        string Phone { get; set; }
        string Role { get; set; }
    }
}
