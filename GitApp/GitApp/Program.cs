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
        static List<string> storedFiles = new List<string>();
        static void Main(string[] args)
        {
            GetFiles();
            Console.WriteLine("Please input your command, use 'help' for more info");
            while (!error)
            {
                Console.Write("> ");
                string response = Console.ReadLine().Trim().ToLower();
                string[] command = response.Split(' ');
                string input = command.First();
                switch (input)
                {
                    case "help":
                        Console.WriteLine("Commands:\nrefresh (refreshes avaliable files)\ndiff [a.txt] [b.txt] (compares two files)\nls (displays loaded files)\n");
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
                        string[] selected = command.Skip(1).ToArray();
                        if (selected.Length == 2)
                        {
                            if (names.Contains(selected[0]) && names.Contains(selected[1]))
                            {
                                bool comparison = Compare(selected[0], selected[1]);
                                if (comparison) Console.WriteLine($"{selected[0]} and {selected[1]} are not different", ConsoleColor.Green);
                                else Console.WriteLine($"", ConsoleColor.Red);
                            }
                            else Console.WriteLine("You have inputted an unrecognised file please try again\n");
                        }
                        else Console.WriteLine("You haven't inputted the correct amount of arguments\n");
                        break;
                    case "ls":
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
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }

        private static bool Compare(string file1, string file2)
        {
            if (String.Equals(file1, file2)) return true;
            else return false;
        }

        private static void GetFiles()
        {
            names.Clear();
            storedFiles.Clear();
            try {
                string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"/Files");
                foreach (string file in files)
                {
                    names.Add(Path.GetFileName(file));
                    storedFiles.Add(File.ReadAllText(file));
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
