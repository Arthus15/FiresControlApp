using System;
using System.Collections.Generic;
using System.Text;

namespace FiresControlApp.Game.Exceptions
{
    /// <summary>
    /// Class used for controled exceptions
    /// </summary>
    public class GameException : Exception
    {
        public GameException() : base() { }

        public GameException(string message) : base(message) { }
    }
}
