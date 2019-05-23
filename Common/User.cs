using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        private string _role;
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                if (value == "Admin" || value == "Moderator" || value == "User")
                {
                    _role = value;
                }
            }
        }
    }
}
