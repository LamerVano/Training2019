using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotValidException: Exception
    {
        public NotValidException(string message) : base(message)
        {
        }
    }
}
