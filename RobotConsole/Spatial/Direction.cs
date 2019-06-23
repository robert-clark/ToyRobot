using static RobotConsole.Constants;

namespace RobotConsole.Spatial
{
    /// <summary>
    /// Class to manage turning right and left so that the cardinal
    /// direction enum will loop over values.
    /// </summary>
    public class Direction
    {
        public CardinalDirection Facing;

        public Direction(CardinalDirection facing)
        {
            this.Facing = facing;
        }

        public void Turn(HandedDirection handedDirection)
        {
            switch(handedDirection)
            {
                case HandedDirection.Right:
                    RotateRight();
                    break;

                case HandedDirection.Left:
                    RotateLeft();
                    break;
            }
        }

        private void RotateRight()
        {
            if (Facing == CardinalDirection.West)
                Facing = CardinalDirection.North;
            else
                Facing += 1;
        }

        private void RotateLeft()
        {
            if (Facing == CardinalDirection.North)
                Facing = CardinalDirection.West;
            else
                Facing -= 1;
        }
    }
}
