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
        static bool error = false;
        static List<string> names = new List<string>();
        static void Main(string[] args)
        {
            GetFiles();
            Console.WriteLine("Please input your command, use 'help' for more info");
            while (!error)
            {
                string response = Console.ReadLine().Trim().ToLower();
                string[] command = response.Split(' ');
                string input = command.First();
                switch (input)
                {
                    case "help":
                        Console.WriteLine("Commands:\nrefresh (refreshes avaliable files)\ndiff [a.txt] [b.txt] (compares two files)\nloaded (displays loaded files)\n");
                        break;
                    case "refresh":
                        GetFiles();
                        Console.WriteLine("Refreshed - Loaded files:");
                        foreach (string name in names)
                        {
                            Console.WriteLine(name);
                        }
                        Console.WriteLine();
                        break;
                    case "diff":
                        string[] files = command.Skip(1).ToArray();
                        if (names.Contains(files[0]) && names.Contains(files[1])) Compare(files[0], files[1]);
                        else Console.WriteLine("You have inputted a unrecognised file please try again\n");
                        break;
                    case "loaded":
                        Console.WriteLine("Loaded files:");
                        foreach (string name in names)
                        {
                            Console.WriteLine(name);
                        }
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Command not found, please use 'help' command\n");
                        break;
                }
            }
        }

        private static void Compare(string file1, string file2)
        {

        }

        private static void GetFiles()
        {
            names.Clear();
            try {
                string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"/Files");
                foreach (string file in files)
                {
                    names.Add(Path.GetFileName(file));
                }
            }
            catch(Exception e)
            {
                error = true;
                Console.WriteLine("An error has occured while readiing files:\n" + e);
            }
        }
    }
}
