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
            foreach (FileInfo fi in directoryInfo.GetFiles("TestCase_*.txt"))
            {
                Console.WriteLine($"Running script {fi.Name} at {DateTime.Now}...");

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
                            // Check line isn't just white space.
                            if(line.Trim().Length > 0)
                            {
                                // Split into space delimited substrings.
                                string[] commandSubstrings;
                                commandSubstrings = line.Split(' ');

                                // Parse to see if the first substring is a valid command.
                                try
                                {
                                    ParseCommandLine(commandSubstrings, ref robot);
                                }
                                catch (RobotPlacementException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }  
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The file could not be read!");
                    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                }
                finally
                {
                    if(robot.GetIsPlaced() == false)
                        Console.WriteLine("This test script ran without the Robot being successfully placed...");
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
                    // Write command to console.
                    Console.WriteLine($"{commands[0].ToUpper()} {commands[1]?.ToUpper()}");

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
                                    try
                                    {
                                        robot.PlaceRobot(x, y, placeSubstrings[2]);
                                    }
                                    catch(RobotPlacementException ex)
                                    {
                                        Console.WriteLine($" -> Robot not placed! ({ex.GetType().Name})");
                                        Console.WriteLine($" -> {ex.Message}");
                                    }
                                }
                                else
                                    Console.WriteLine($" -> Cardinal direction was not valid! ({placeSubstrings[2]})");
                            }
                            else
                                Console.WriteLine($" -> Y was unsuccessfully parsed to an int! ({placeSubstrings[1]})");
                        }
                        else
                            Console.WriteLine($" -> X was unsuccessfully parsed to an int! ({placeSubstrings[0]})");
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
                        Console.WriteLine($" -> Can't move off the grid! ({ex.GetType().Name})");
                    }
                    catch(NullReferenceException ex)
                    {
                        Console.WriteLine($" -> Robot not placed! ({ex.GetType().Name})");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($" -> {ex.GetType().Name}: {ex.Message}");
                    }

                    break;

                case "LEFT":
                    try
                    {
                        Console.WriteLine("LEFT");
                        robot.TurnRobot("LEFT");
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine($" -> Robot not placed! ({ex.GetType().Name})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" -> {ex.GetType().Name}: {ex.Message}");
                    }

                    break;

                case "RIGHT":
                    try
                    {
                        Console.WriteLine("RIGHT");
                        robot.TurnRobot("RIGHT");
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine($" -> Robot not placed! ({ex.GetType().Name})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" -> {ex.GetType().Name}: {ex.Message}");
                    }

                    break;

                case "REPORT":
                    try
                    {
                        Console.WriteLine("REPORT");
                        Console.WriteLine(robot.ReportRobot());
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine($" -> Robot not placed! ({ex.GetType().Name})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" -> {ex.GetType().Name}: {ex.Message}");
                    }

                    break;

                default:
                    Console.WriteLine($"{commands[0]} was unsuccessfully parsed into a valid robot command!");
                    break;
            }
        }
    }
}
