namespace RobotConsole
{
    /// <summary>
    /// Global constants.
    /// </summary>
    public static class Constants
    {
        // Represents the max values for the grid upon which the
        // Robot can move. Zero based index.
        public const int X_MAX = 4;
        public const int Y_MAX = 4;
        public const int X_MIN = 0;
        public const int Y_MIN = 0;

        /// <summary>
        /// Global enum representation for N, S, E and W.
        /// </summary>
        public enum CardinalDirection
        {
            North,
            East,
            South,
            West
        }

        /// <summary>
        /// Global enum representation for LEFT and RIGHT.
        /// </summary>
        public enum HandedDirection
        {
            Right,
            Left
        }
    }
}
