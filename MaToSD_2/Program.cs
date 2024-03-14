﻿using MaToSD_2;

class Program
{
    static void Main(string[] args)
    {
        string filePath = args[^1];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        string text = File.ReadAllText(filePath);


        if (args.Contains("--format=ansi"))
        {
            text = ANSIConverter.Convert(text);
            Console.WriteLine(text);
        }
        else if (args.Contains("--format=html"))
        {
            text = HTMLConverter.Convert(text);
            Console.WriteLine(text);
            if (args.Contains("--out"))
            {
                string outputFile = Path.ChangeExtension(filePath, ".html");
                File.WriteAllText(outputFile, text);
            }
        }
        else
        {
            text = ANSIConverter.Convert(text);
            Console.WriteLine(text);
            text = File.ReadAllText(filePath);
            text = HTMLConverter.Convert(text);
            string outputFile = Path.ChangeExtension(filePath, ".html");
            File.WriteAllText(outputFile, text);
        }
    }
}