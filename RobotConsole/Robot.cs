using System;

namespace RobotConsole
{
    class Robot
    {
        private bool IsPlaced { get; set; }

        public Robot()
        {
            // Initialize the Robot.
            this.IsPlaced = false;
        }

        public void PlaceRobot()
        {
            IsPlaced = true;
        }
    }
}
