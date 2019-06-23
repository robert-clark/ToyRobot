using System;

namespace RobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a test harness to parse each text file into robot commands
            // that will also write out the results to console.
            RobotTestHarness rth = new RobotTestHarness(@"./TestScripts");
            rth.ProcessCommandFiles();

            Console.WriteLine("Press [Enter] to continue...");
            Console.ReadLine();
        }
    }
}
