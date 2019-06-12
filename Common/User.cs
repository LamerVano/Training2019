using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string EMail { get; set; }
        [Required]
        [StringLength(40)]
        public string Password { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        private string _role;
        [Required]
        [StringLength(15)]
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                if (value == Roles.ADMIN || value == Roles.EDITOR)
                {
                    _role = value;
                }
            }
        }
    }
}
