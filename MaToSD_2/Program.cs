using MaToSD_2;

class Program
{
    static async Task Main(string[] args)
    {
        string filePath = args[^1];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        string text = await File.ReadAllTextAsync(filePath);
        
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
                await File.WriteAllTextAsync(outputFile, text);
            }
        }
        else
        {
            text = ANSIConverter.Convert(text);
            Console.WriteLine(text);
            text = await File.ReadAllTextAsync(filePath);
            text = HTMLConverter.Convert(text);
            string outputFile = Path.ChangeExtension(filePath, ".html");
            await File.WriteAllTextAsync(outputFile, text);
        }
    }
}