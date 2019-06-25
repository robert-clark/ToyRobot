using System;
using RobotConsole.Exceptions;
using RobotConsole.Spatial;
using static RobotConsole.Constants;

namespace RobotConsole
{
    public class Robot
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        private bool IsPlaced { get; set; }

        /// <summary>
        /// A Robot to be created at the start of each test iteration. The Robot will
        /// not be on the grid until successfully placed.
        /// </summary>
        public Robot()
        {
            this.IsPlaced = false;
        }

        public void PlaceRobot(int x, int y, string direction)
        {
            // Instantiate a Position, checking that x and y are within the bounds
            // of the grid's max/min values.
            if (x > X_MAX | x < X_MIN)
                throw new RobotPlacementException("The x value is beyond the grid boundary.");
            if(y > Y_MAX | y < Y_MIN)
                throw new RobotPlacementException("The y value is beyond the grid boundary.");
            this.Position = new Position(x, y);

            // Instantiate Bearing, including parsing the direction faced.
            switch(direction?.ToUpper() ?? "")
            {
                case "NORTH":
                    this.Direction = new Direction(CardinalDirection.North);
                    IsPlaced = true;
                    break;

                case "EAST":
                    this.Direction = new Direction(CardinalDirection.East);
                    IsPlaced = true;
                    break;

                case "SOUTH":
                    this.Direction = new Direction(CardinalDirection.South);
                    IsPlaced = true;
                    break;

                case "WEST":
                    this.Direction = new Direction(CardinalDirection.West);
                    IsPlaced = true;
                    break;

                default:
                    throw new RobotPlacementException("Unable to parse the direction facing for Robot.");
            }   
        }

        /// <summary>
        /// Get IsPlaced.
        /// </summary>
        /// <returns></returns>
        public bool GetIsPlaced()
        {
            return this.IsPlaced;
        }

        /// <summary>
        /// Turning
        /// </summary>
        /// <param name="direction"></param>
        public void TurnRobot(string direction)
        {
            switch (direction?.ToUpper() ?? "")
            {
                case "RIGHT":
                    Direction.Turn(HandedDirection.Right);
                    break;

                case "LEFT":
                    Direction.Turn(HandedDirection.Left);
                    break;

                default:
                    throw new FormatException("Failed to convert to LEFT or RIGHT.");
            }
        }

        /// <summary>
        /// Forward only movement.
        /// </summary>
        public void MoveRobot()
        {
            this.Position.MoveForward(Direction.Facing);
        }

        public string ReportRobot()
        {
            return $"Output: {Position.Coordinate.ToString()},{Direction.Facing.ToString().ToUpper()}";
        }
    }
}
