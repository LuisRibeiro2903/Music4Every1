using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
        {
        }

        public WrongPasswordException(string message)
            : base(message)
        {
        }

        public WrongPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
