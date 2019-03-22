using System;
using System.Collections.Generic;
using System.Text;

namespace FiresControlApp.Game.Exceptions
{
    public class FormatException : Exception
    {
        public FormatException() : base() { }

        public FormatException(string message) : base(message) { }
    }
}
