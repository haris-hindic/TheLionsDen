using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }
}
