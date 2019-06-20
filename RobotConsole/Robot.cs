using System;

namespace RobotConsole
{
    class Robot
    {
        private bool IsPlaced { get; set; }

        /// <summary>
        /// Robot created at the start of each command script.
        /// </summary>
        public Robot()
        {
            // Initialize the Robot.
            this.IsPlaced = false;
        }

        public void PlaceRobot(int x, int y, string direction)
        {
            // Instantiate a Position.
            // Instantiate a Bearing.


            IsPlaced = true;
        }
    }
}
