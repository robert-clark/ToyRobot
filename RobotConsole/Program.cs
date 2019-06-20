using System;

namespace RobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandParser commandParser = new CommandParser(@"./RobotCommands");
            commandParser.ProcessCommandFiles();

            Console.WriteLine("Press [Enter] to continue...");
            Console.ReadLine();
        }
    }
}
