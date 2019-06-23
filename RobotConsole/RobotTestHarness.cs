using RobotConsole.Exceptions;
using System;
using System.IO;

namespace RobotConsole
{
    class RobotTestHarness
    {
        DirectoryInfo directoryInfo;

        public RobotTestHarness(string directoryPath)
        {
            directoryInfo = new DirectoryInfo(directoryPath);
        }

        public void ProcessCommandFiles()
        {
            foreach (FileInfo fi in directoryInfo.GetFiles("TestClass_*.txt"))
            {
                Console.WriteLine($"Running script from {fi.Name} at {DateTime.Now}...");

                // Create a new Robot for each run of the commands script.
                Console.WriteLine("Initializing new Robot...");
                Robot robot = new Robot();

                try
                {
                    using (StreamReader sr = new StreamReader(fi.FullName))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            // Split into space delimited substrings.
                            string[] commandSubstrings;
                            commandSubstrings = line.Split(' ');

                            // Parse to see if the first substring in a valid command.
                            try
                            {
                                ParseCommandLine(commandSubstrings, ref robot);
                            }
                            catch(RobotPlacementException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The file could not be read!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if(robot.GetIsPlaced() == false)
                        Console.WriteLine("ERROR: The Robot was not successfully placed...");
                }
                Console.WriteLine();
            }
        }

        private void ParseCommandLine(string[] commands, ref Robot robot)
        {
            string[] placeSubstrings;

            switch (commands[0]?.ToUpper() ?? "")
            {
                case "PLACE":
                    // Parse the x, y, direction substrings.
                    if(commands[1] != null)
                    {
                        placeSubstrings = commands[1].Split(',');
                        
                        if(placeSubstrings[0] != null & Int32.TryParse(placeSubstrings[0], out int x))
                        {
                            if(placeSubstrings[1] != null & Int32.TryParse(placeSubstrings[1], out int y))
                            {
                                if(placeSubstrings[2] != null &
                                    (placeSubstrings[2] == "NORTH" |
                                        placeSubstrings[2] == "EAST" |
                                        placeSubstrings[2] == "SOUTH" |
                                        placeSubstrings[2] == "WEST"))
                                {
                                    Console.WriteLine($"PLACE {x},{y},{placeSubstrings[2]}");

                                    try
                                    {
                                        robot.PlaceRobot(x, y, placeSubstrings[2]);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                else
                                    Console.WriteLine($"DIRECTION: {placeSubstrings[2]} was unsuccessfully parsed to LEFT/RIGHT!");
                            }
                            else
                                Console.WriteLine($"Y: {placeSubstrings[1]} was unsuccessfully parsed to an int!");
                        }
                        else
                            Console.WriteLine($"X: {placeSubstrings[0]} was unsuccessfully parsed to an int!");
                    }
                    break;

                case "MOVE":
                    try
                    {
                        Console.WriteLine("MOVE");
                        robot.MoveRobot();
                    }
                    catch(MoveOutOfBoundsException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "LEFT":
                    Console.WriteLine("LEFT");
                    robot.TurnRobot("LEFT");
                    break;

                case "RIGHT":
                    Console.WriteLine("RIGHT");
                    robot.TurnRobot("RIGHT");
                    break;

                case "REPORT":
                    Console.WriteLine("REPORT");
                    Console.WriteLine(robot.ReportRobot());
                    break;

                default:
                    Console.WriteLine($"{commands[0]} was unsuccessfully parsed into a valid robot command!");
                    break;
            }
        }
    }
}
