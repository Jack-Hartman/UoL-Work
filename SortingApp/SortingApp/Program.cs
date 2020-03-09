using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp
{
    class Program
    {
        static string[] avaliableArrays = { "net_1_2048", "net_1_256", "net_2_2048", "net_2_256", "net_3_2048", "net_3_256" };
        static int[] net_1_256;
        static int[] net_2_256;
        static int[] net_3_256;
        static int[] net_1_2048;
        static int[] net_2_2048;
        static int[] net_3_2048;
        static bool complete = false;
        static void Main(string[] args) 
        {
            ReadFiles();
            while (!complete)
            {
                Console.WriteLine("Please Select An Array To Sort");
                Console.WriteLine(string.Join(", ", avaliableArrays));
                string userSelection = Console.ReadLine();
                if (avaliableArrays.Contains(userSelection.ToLower()))
                {
                    Console.WriteLine("Selected " + userSelection.ToLower());
                    switch (userSelection.ToLower())
                    {
                        case "net_1_256":
                            SortArray(net_1_256);
                            break;
                        case "net_2_256":
                            SortArray(net_2_256);
                            break;
                        case "net_3_256":
                            SortArray(net_3_256);
                            break;
                        case "net_1_2048":
                            SortArray(net_1_2048);
                            break;
                        case "net_2_2048":
                            SortArray(net_2_2048);
                            break;
                        case "net_3_2048":
                            SortArray(net_3_2048);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid file");
                }
            }
        }

        static void ReadFiles()
        {
            try
            {
                net_1_256 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_1_256.txt"), s => int.Parse(s));
                net_2_256 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_2_256.txt"), s => int.Parse(s));
                net_3_256 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_3_256.txt"), s => int.Parse(s));
                net_1_2048 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_1_2048.txt"), s => int.Parse(s));
                net_2_2048 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_2_2048.txt"), s => int.Parse(s));
                net_3_2048 = Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Net_3_2048.txt"), s => int.Parse(s));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Reading Arrays Press Any Key To Exit\n" + e);
                Console.ReadKey(true);
                complete = true;
            }
        }

        static void printArray(int[] array)
        {
            foreach(int item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static void SortArray(int[] array)
        {

            
        }
    }
}
