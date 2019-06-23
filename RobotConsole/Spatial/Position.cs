using RobotConsole.Exceptions;
using static RobotConsole.Constants;

namespace RobotConsole.Spatial
{
    /// <summary>
    /// Class for tracking the grid location of the Robot. Manages forward only movement
    /// and prevents the Robot from going out-of-bounds.
    /// </summary>
    public class Position
    {
        public Coordinate Coordinate { get; set; }

        public Position(int x, int y)
        {
            this.Coordinate = new Coordinate
            {
                X = x,
                Y = y
            };
        }

        public void MoveForward(CardinalDirection cardinalDirection)
        {
            switch(cardinalDirection)
            {
                case CardinalDirection.North:
                    if (Coordinate.Y == Constants.X_MAX)
                        throw new MoveOutOfBoundsException(
                            "Out of bounds. Unable to move Robot Y axis positive.");
                    else
                        Coordinate.Y += 1;
                    break;

                case CardinalDirection.East:
                    if (Coordinate.X == Constants.X_MAX)
                        throw new MoveOutOfBoundsException(
                            "Out of bounds. Unable to move Robot X axis positive.");
                    else
                        Coordinate.X += 1;
                    break;

                case CardinalDirection.South:
                    if (Coordinate.Y == Constants.Y_MIN)
                        throw new MoveOutOfBoundsException(
                            "Out of bounds. Unable to move Robot Y axis negative.");
                    else
                        Coordinate.Y -= 1;
                    break;

                case CardinalDirection.West:
                    if (Coordinate.X == Constants.X_MIN)
                        throw new MoveOutOfBoundsException(
                           "Out of bounds. Unable to move Robot X axis negative.");
                    else
                        Coordinate.X -= 1;
                    break;

            }
        }
    }
}
