using System;

namespace RobotConsole.Exceptions
{
    public class RobotPlacementException : Exception
    {
        public RobotPlacementException() { }

        public RobotPlacementException(string message)
            : base(message)
        {
        }

        public RobotPlacementException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
