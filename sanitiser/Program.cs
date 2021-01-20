using System;
using System.Collections.Generic;
using System.IO;

namespace sanitiser
{
    class Program
    {
        static int replacementCount = 0;
        static void Main(string[] args)
        {
            // Display if no file argument passed
            if (args.Length == 0)
            {
                Console.WriteLine("* Sanitiser Error *");
                Console.WriteLine("Please enter a file name for processing.");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Looks for any variables with __SECRET and then sanitises anything after an '=' sign.");
                Environment.Exit(-1);
            }

            // Read, process and write the sanitised file out
            string fileName = args[0];
            string[] readFile = LoadFile(fileName);
            string processedText = Process(readFile);
            WriteSanitised(fileName, processedText);
            Console.WriteLine(string.Format("Processed :{0} secrets", replacementCount));
            Console.WriteLine(string.Format("Sanitised file writen as: {0}", fileName+"(sanitised)"));
        }

        // write out text to new sanitised file
        static private void WriteSanitised(string fileName, string output)
        {
            try
            {
                // write sanitised file
                File.WriteAllText(fileName + "(sanitised)", output);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be writen:");
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        // read file into string array
        static private string[] LoadFile(string fileName)
        {
            string[] lines = new string[] { };
            try
            {
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(fileName))
                {
                    using (var streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8, true, BufferSize))
                    {
                        lines = File.ReadAllLines(fileName);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }

            return lines;
        }

        // processes string array to remove secrets
        static private string Process(string[] input)
        {
            List<string> sanitised = new List<string>();

            foreach (string line in input)
            {
                string sLine = line;
                if (line.ToUpper().IndexOf("__SECRET") != -1)
                {
                    int index = line.IndexOf("=");
                    if (index > 0)
                        sLine = line.Substring(0, index + 1) + "<enter your value>";
                    sanitised.Add(sLine);
                    replacementCount++;
                }
                else
                {
                    sanitised.Add(sLine);
                }
            }

            return string.Join(Environment.NewLine, sanitised.ToArray());
        }

    }
}

