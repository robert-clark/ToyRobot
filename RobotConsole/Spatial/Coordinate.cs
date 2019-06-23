namespace RobotConsole.Spatial
{
    /// <summary>
    /// Coordinates used in the Position class.
    /// </summary>
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
