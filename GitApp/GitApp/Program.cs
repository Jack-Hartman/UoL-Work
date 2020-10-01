using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GitApp
{
    class Program
    {
        // Main static void
        static void Main(string[] args) 
        {
            Files files = new Files(); // Instantiates files class as an object
            files.GetFiles(); // Calls the GetFiles inside of files
            Console.WriteLine("Please input your command, use 'help' for more info"); // Writes help message to console
            while (!files.error) // While files.error equals false
            {
                Console.Write("> "); // Writes input prompt to console
                string response = Console.ReadLine().Trim().ToLower(); // Gets response and trims it and makes it all lower case
                string[] command = response.Split(' '); // Splits the response into an array
                string input = command.First(); // Gets the first value the array
                switch (input) // Creates a switch for input
                {
                    case "help": // If input equals help
                        Console.WriteLine("Commands:\nrefresh (refreshes avaliable files)\ndiff [a.txt] [b.txt] (compares two files)\nls (displays loaded files)\n"); // Displays help text
                        break; 
                    case "refresh": // If input equals refresh
                        files.GetFiles(); // Calls GetFiles void in files
                        files.ListFiles();    // Calls ListFiles void in files                   
                        break;
                    case "diff": // If input equals diff
                        string[] selected = command.Skip(1).ToArray(); // Crates a new array called selected from command but skipping the first value
                        if (selected.Length == 2) // Checks to see if selected length equals 2
                        {
                            files.Compare(selected[0], selected[1]); // Calls Compare in files with selected[0] and selected[1]
                        }
                        else Console.WriteLine("You haven't inputted the correct amount of arguments\n"); // Displays error message to user
                        break;
                    case "ls": // If input equals ls
                        files.ListFiles(); // Calls ListFiles void in files
                        break;
                    default: // If nothing is found
                        Console.WriteLine("Command not found, please use 'help' command\n"); // Displays error message to user
                        break;
                }
            }
            Console.WriteLine("Press any key to exit"); // Displays message to user
            Console.ReadKey(true); // Waits for any key input
        }
    }
}
