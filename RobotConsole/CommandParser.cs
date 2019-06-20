using System;
using System.IO;

namespace RobotConsole
{
    class CommandParser
    {
        DirectoryInfo directoryInfo;

        public CommandParser(string directoryPath)
        {
            directoryInfo = new DirectoryInfo(directoryPath);
        }

        public void ProcessCommandFiles()
        {

            foreach (FileInfo fi in directoryInfo.GetFiles())
            {
                

                Console.WriteLine($"Running script from {fi.Name} at {DateTime.Now}...");

                // TODO: Create a new Robot for each run of the commands script.
                Console.WriteLine("Initializing new Robot...");
                Console.WriteLine();

                try
                {
                    using (StreamReader sr = new StreamReader(fi.FullName))
                    {
                        int lineNum = 1;
                        string line;  

                        while ((line = sr.ReadLine()) != null)
                        {

                            // Split into space delimited substrings.
                            string[] commandSubstrings;
                            commandSubstrings = line.Split(' ');

                            // Parse to see if the first substring in a valid command.
                            ParseCommandLine(commandSubstrings);

                            lineNum++;

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
                    Console.WriteLine("Output:...");
                    Console.WriteLine();
                }
                
            }
        }

        private void ParseCommandLine(string[] commands)
        {
            string[] placeSubstrings;

            switch (commands[0]?.ToUpper() ?? "")
            {

                case "PLACE":
                    // Parse the X,Y,DIRECTION substrings.
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

                                    Console.WriteLine($"PlaceRobot({x}, {y}, {placeSubstrings[2]});");
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
                    Console.WriteLine("MoveRobot();"); 
                    break;
                case "LEFT":
                    Console.WriteLine("TurnRobotLeft();"); 
                    break;
                case "RIGHT":
                    Console.WriteLine("TurnRobotRight();");
                    break;
                case "REPORT":
                    Console.WriteLine("Report()");
                    break;
                default:
                    Console.WriteLine($"{commands[0]} was unsuccessfully parsed into a valid robot command!");
                    break;
            }
        }
    }
}
