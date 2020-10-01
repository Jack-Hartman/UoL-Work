using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GitApp.Changes;
using static GitApp.Log;

namespace GitApp
{
    class Files
    {
        public bool error = false; // Creates a public bool called error and sets it to false
        private List<FileClass> fileList = new List<FileClass>(); // Instantiates a new list called fileList using FileClass
        ChangedSentence added = null; // Sets ChangedSentence called added to null
        ChangedSentence removed = null; // Sets ChangedSentence called removed to null

        class FileClass // class called FileClass
        {
            public string name { get; set; } // Creates a public string called name
            public string[] data { get; set; } // Creates a public string array called data
        }

        public void GetFiles() // GetFiles void
        {
            fileList.Clear(); // Clears filelist
            try
            {
                string[] files = Directory.GetFiles(Environment.CurrentDirectory + "/Files"); // Gets all the files from the directory Files in the current directory
                foreach (string newFile in files) // Foreach newFile in files
                {
                    FileClass foundFile = new FileClass(); // Instantiate FileClass Object called foundFile
                    foundFile.name = Path.GetFileName(newFile).ToLower(); // Sets foundFile name to name of found file
                    foundFile.data = File.ReadAllLines(newFile); // Sets foundFile data to all the lines of found file
                    fileList.Add(foundFile); // Adds foundFile to fileList
                }
            }
            catch (Exception e) // If an error is caused
            {
                Console.WriteLine("An error has occured while readiing files:\n" + e); // Display error message
                error = true; // Set error to true
            }
        }

        public void ListFiles() // ListFiles void
        {
            Console.WriteLine("Loaded files:"); // Writes to the console
            int pos = 1; // Sets pos to 1
            foreach (FileClass file in fileList) // Foreach file in fileList
            {
                Console.WriteLine($"[{pos}] {file.name}"); // Writes name and position to console of file
                pos++; // Increments pos by one
            }
            Console.WriteLine(); // Writes a newline to console
        }

        public void Compare(string file1, string file2) // Compare void which requires two strings
        {
            FileClass fileOne = null; // Sets fileOne to null
            FileClass fileTwo = null; // Sets fileTwo to null
            int file1Int = 0; // Sets file1Int to 0
            int file2Int = 0; // Sets file2Int to 0
            int.TryParse(file1, out file1Int); // int parses file1 to file1Int
            int.TryParse(file2, out file2Int); // int parses file2 to file2Int
            if (file1Int == 0) fileOne = fileList.Find(x => x.name == file1); // If file1Int is 0 then find file in fileList using name and setr to fileOne
            else 
            {
                if (file1Int <= fileList.Count() && file1Int > 0) fileOne = fileList[file1Int-1]; // If file1 within bounds set fileone to index in fileList
            }
            if (file2Int == 0) fileTwo = fileList.Find(x => x.name == file2); // If file2Int is 0 then find file in fileList using name and setr to fileTwo
            else
            {
                if (file2Int <= fileList.Count() && file2Int > 0) fileTwo = fileList[file2Int-1]; // If file2 within bounds set fileone to index in fileList
            }
            if (fileOne == null || fileTwo == null) // If fileOne and fileTwo equal null
            {
                Console.WriteLine("You have inputted an unrecognised file, please be sure you have inputted the file type at the end and try again\n"); // Output error message
            }
            else LineChecks(fileOne, fileTwo); // Calls void LineCheck with fileOne and fileTwo
        }

        private void LineChecks(FileClass file1, FileClass file2) // void LineChecks with two FileClass requirements
        {
            bool changes = false; // sets changes to false
            WriteLog($"{file1.name}:\n{FormatArray(file1.data)}\n{file2.name}:\n{FormatArray(file2.data)}\nChanges:\n"); // Calls WriteLog with names and data of both files
            for(int line = 0; line < Math.Max(file1.data.Length, file2.data.Length); line++) // for line is less then biggest number out of both file lengths
            {
                if(line + 1 > file1.data.Length) // If line +1 is more than file1 data length
                {
                    changes = true; // set changes to true
                    Console.WriteLine($"Line: {line + 1}"); // Write line to console
                    ChangedSentence newSentence = new ChangedSentence(); // Instantiates new object of ChangedSentence called new sentence
                    newSentence.AddString(file2.data[line], true); // Calls AddString in newSentence of sentence in file and true
                    newSentence.PrintChanges(true); // Calls PrintChanges with true
                    WriteLog($"Line: {line+1}\n"); // Write line to log
                    newSentence.LogChanges(true); // Calls LogChanges with true
                }
                else if (line + 1 > file2.data.Length)
                {
                    changes = true; // set changes to true
                    Console.WriteLine($"Line: {line + 1}"); // Write line to console
                    ChangedSentence newSentence = new ChangedSentence(); // Instantiates new object of ChangedSentence called new sentence
                    newSentence.AddString(file1.data[line], true); // Calls AddString in newSentence of sentence in file and true
                    newSentence.PrintChanges(false); // Calls PrintChanges with false
                    WriteLog($"Line: {line+1}\n"); // Write line to log
                    newSentence.LogChanges(false); // Calls LogChanges with true
                }
                else
                {
                    if (!String.Equals(file1.data[line], file2.data[line], StringComparison.OrdinalIgnoreCase)) // 
                    {
                        changes = true; // set changes to true
                        Console.WriteLine($"Line: {line + 1}"); // Write line to console
                        List<string> sentence1 = file1.data[line].Split().ToList(); // Creates a new list string called sentence1 from file1 data split into a list
                        List<string> sentence2 = file2.data[line].Split().ToList();// Creates a new list string called sentence2 from file2 data split into a list
                        added = new ChangedSentence(); // sets added to new Changed Sentence
                        removed = new ChangedSentence(); // sets removed to new Changed Sentence
                        CheckWords(sentence1, sentence2); // Calls CheckWords with sentence 1 and sentence 2
                        added.PrintChanges(true); // Calls added PrintChanges with true
                        removed.PrintChanges(false); // Calls removed PrintChanges with false
                        WriteLog($"Line: {line + 1}\n"); // Write line to log
                        added.LogChanges(true); // Calls LogChanges for added with true
                        removed.LogChanges(false); // Calls LogChanges for remove with true
                    }
                }
            }
            if (!changes) // If changes equal false
            {
                Console.WriteLine($"There are no changes between {file1.name} and {file2.name}"); // Writes there are no changes to console
                WriteLog($"There are no changes between {file1.name} and {file2.name}\n\n"); // Writes there are no changes to log using WriteLog
            }
            WriteLog("----------{End Of Changes}----------\n"); // Writes end of changes to log
            Inform(); // Calls Inform
            Console.WriteLine(); // Writes a new line
        }

        void CheckWords(List<string> sentence1, List<string> sentence2) // CheckWords void using requiring two string lists
        {
            for (int word = 0; word < Math.Max(sentence1.Count(), sentence2.Count()); word++) // for word is less then biggest number out of both sentence lengths
            {
                if (word + 1 > sentence1.Count()) // If word + 1 is more than sentence length
                {
                    added.AddString(sentence2[word], true); // Calls AddString in added with sentence2 word pos and true
                }
                else if (word + 1 > sentence2.Count()) // If word + 1 is more than sentence length
                {
                    removed.AddString(sentence1[word], true); // Calls AddString in added with sentence1 word pos and true
                }
                else if (!String.Equals(sentence1[word], sentence2[word], StringComparison.OrdinalIgnoreCase)) // If one word doesn't equal the other from the two sentences
                {
                    if (word + 1 < sentence2.Count()) // If word + 1 is less then sentence2 count
                    {
                        if (!String.Equals(sentence1[word], sentence2[word + 1], StringComparison.OrdinalIgnoreCase))
                        {
                            char[] word1 = sentence1[word].ToCharArray(); // creates word 1 char array from sentence 1 word
                            char[] word2 = sentence2[word].ToCharArray(); // creates word 2 char array from sentence 2 word
                            CheckCharacters(word1, word2); // Calls CheckCharacters with word1 and 2 char array
                        }
                        else
                        {
                            added.AddString(sentence2[word], true); // Calls AddString for added with sentence 2 word and true
                            added.AddString(sentence2[word + 1], false); // Calls AddString for added with sentence 2 word +1 and false
                            removed.AddString(sentence1[word], false); // Calls AddString for removed with sentence 1 word and false
                            sentence2.RemoveAt(0);
                        }
                    }
                    else
                    {
                        added.AddString(sentence2[word], false); // Calls AddString for added with sentence 2 word and false
                        removed.AddString(sentence1[word], false); // Calls AddString for removed with sentence 1 word and false
                    }
                }
                else
                {
                    added.AddString(sentence1[word], false); // Calls AddString for added with sentence 1 word and false
                    removed.AddString(sentence1[word], false); // Calls AddString for removed with sentence 1 word and false
                }
            }
        }

        void CheckCharacters(char[] word1, char[] word2) // CheckCharacters void with two char arrays
        {
            for (int character = 0; character < Math.Max(word1.Length, word2.Length); character++) // for character is less then biggest number out of both word lengths
            {
                if (character + 1 > word1.Length) // If Character +1 is more than word1 length
                {
                    added.AddChar(word2[character], true); // Calls addchar for added with word 2 character with true
                }
                else if (character + 1 > word2.Length) // If Character +1 is more than word2 length
                {
                    removed.AddChar(word1[character], true); // Calls addchar for removed with word 1 character with true
                }
                else if (!String.Equals(word1[character].ToString(), word2[character].ToString(), StringComparison.OrdinalIgnoreCase)) // If word1 character doesn't equal word2 character
                {
                    added.AddChar(word2[character], true); // Calls addchar for added with word 2 character with true
                    removed.AddChar(word1[character], true); // Calls addchar for removed with word 1 character with false
                }
                else
                {
                    added.AddChar(word1[character], false); // Calls addchar for added with word 1 character with false
                    removed.AddChar(word1[character], false); // Calls addchar for removed with word 1 character with false
                }
            }
            added.AddSpace(false); // Calls addspace for added with false
            removed.AddSpace(false); // Calls addspace for removed with false
        }
    }
}
