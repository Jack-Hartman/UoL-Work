using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp
{
    class Changes
    {
        public class Letter // Public class called Letter
        {
            public bool changed { get; set; } // Creates a public bool called changed
            public char character { get; set; } // Creates a public char called character
        }   

        public class ChangedSentence // Public class called ChangedSentence
        {
            public List<Letter> changed = new List<Letter>(); // Creates a public object list using Letter called changed

            public void AddChar(char character, bool boolChanged) // AddChar void requires a char and a bool input
            {
                Letter newChar = new Letter(); // Instantiates Letter names newChar
                newChar.changed = boolChanged; // Sets changed of newChar to boolChanged
                newChar.character = character; // Sets character of newChar to character
                changed.Add(newChar); // Adds newChar to changed list
            }

            public void AddString(string add, bool boolChanged) // AddString void requires a string and a bool input
            {
                char[] newAdd = add.ToCharArray(); // Creates a char array called newAdd and sets the value to add string converted to a charArray
                if(newAdd.Length == 0) AddSpace(true); // if newAdd length equals then call AddSpace with true
                else
                {
                    foreach (char character in newAdd) // Foreach character in newAdd
                    {
                        Letter newChar = new Letter(); // Instantiates Letter names newChar 
                        newChar.changed = boolChanged; // Sets changed of newChar to boolChanged
                        newChar.character = character; // Sets character of newChar to character
                        changed.Add(newChar); // Adds newChar to changed list
                    }
                    AddSpace(false); // Call AddSpace with false
                }
            }

            public void AddSpace(bool newLine) // AddSpace void  requires a bool called newLine
            {
                Letter newChar = new Letter(); // Instantiates Letter names newChar 
                newChar.changed = newLine; // Sets changed of newChar to newLine
                newChar.character = (char)32; // Sets character of newChar to char 32
                changed.Add(newChar); // Adds newChar to changed list
            }

            public void LogChanges(bool added) // LogChanges void requres a bool added
            {
                if (changed.FindIndex(x => x.changed == true) >= 0) // Checks to see if anything has been changed in the list
                {
                    int pos = 0; // sets pos to 0 
                    if (added) Log.WriteLog("Added:\n"); // Writes Added to log file
                    else Log.WriteLog("Removed:\n"); // Writes Removed to log file
                    foreach (Letter let in changed) // Foreach letter in changed
                    {
                        if (let.changed) // If let changed equals true
                        {
                            if (pos != 0 && changed[changed.FindIndex(find => find == let) - 1].changed == false) // If pos doesn't equal 0 and changed found value equals false
                            {
                                Log.WriteLog("\t"); // Adds tab
                            }
                            Log.WriteLog(let.character.ToString()); // Calls log WriteFile with let chracter converted to string
                            pos++; // Increments pos by one
                        }
                    }
                    Log.WriteLog("\n\n"); // Calls WriteLog with two new lines
                }
            }

            public void PrintChanges(bool added) // PrintChanges void requres a bool added
            {
                if (changed.FindIndex(x => x.changed == true) >= 0) // Checks to see if anything has been changed in the list
                {
                    if (added) Console.Write("+ "); // If added output a plus
                    else Console.Write("- "); // Output a minus
                    foreach (Letter let in changed) // Foreach letter in changed
                    {
                        if (let.changed) // If let changed
                        {
                            if (added) Console.ForegroundColor = ConsoleColor.Green; // If added set console colour to green 
                            else Console.ForegroundColor = ConsoleColor.Red; // Else set console colour to red
                            Console.Write(let.character); // Writes let character
                            Console.ResetColor(); // Resets the console colour
                        }
                        else
                        {
                            Console.Write(let.character); // Writes let character to console
                        }
                    }
                    Console.WriteLine(); // Writes a new line to console
                }
            }
        }
    }
}
