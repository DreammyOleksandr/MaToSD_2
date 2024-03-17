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
                await HTMLConverter.CreateHtmlFile(text, filePath);
        }
        else
        {
            text = ANSIConverter.Convert(text);
            Console.WriteLine(text);
            text = await File.ReadAllTextAsync(filePath);
            text = HTMLConverter.Convert(text);
            await HTMLConverter.CreateHtmlFile(text, filePath);
        }
    }
}