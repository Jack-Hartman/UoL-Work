using System;
using System.IO;

namespace GitApp
{
    class Log
    {
        private static string activeLogFile = null; // Creates a static string called activeLogFile with the value of null

        // WriteLog void
        public static void WriteLog(string data) // Request string input
        {
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + @"\Logs"); // Creates directory called Logs in current directory
            if(activeLogFile == null) // If activeLogFile equals null
            {
                activeLogFile = Environment.CurrentDirectory + $@"\Logs\Log_{DateTime.Now.ToString("yyyy-MM-dd_hhmmss")}.txt"; // Sets actievLogFile to Filename and directory
                File.Create(activeLogFile).Close(); // Creates log file and closes it
            }
            using (var file = new StreamWriter(activeLogFile, true)) // While streamWriter is active
            {
                file.Write(data); // Write to file with data
            }
        }

        // FormatArray string void
        public static string FormatArray(string[] array)  // Requires string array input
        {
            string newString = null; // Creates newString and sets it to null
            foreach(string line in array) // foreach line in array
            {
                newString += $"{line}\n"; // Adds to new string with line
            }
            return newString; // returns newString
        }

        // Inform void
        public static void Inform()
        {
            Console.WriteLine($"\nChanges reported to log which can be found here:\n{activeLogFile}\n"); // Prints info of log file to user
        }
    }
}
