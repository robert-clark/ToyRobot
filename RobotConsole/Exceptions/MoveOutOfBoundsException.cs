using System;

namespace RobotConsole.Exceptions
{
    public class MoveOutOfBoundsException : Exception
    {
        public MoveOutOfBoundsException() { }

        public MoveOutOfBoundsException(string message)
            : base(message)
        {
        }

        public MoveOutOfBoundsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
