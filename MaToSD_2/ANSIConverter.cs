using System.Text;
using System.Text.RegularExpressions;

namespace MaToSD_2;

public class ANSIConverter
{
    public static string Convert(string text)
    {
        text = PrePartsConverter(text);
        IsOpenedClosedCorrectly(text);
        StringBuilder content = new();
        string[] lines = text.Split("\n\r".ToCharArray());
        for (int i = 0; i < lines.Length; i++)
        {
            if (Regex.IsMatch(lines[i], @"(\u001b[7m)(.*?)(\u001b[0m])"))
            {
                content.AppendLine(lines[i]);
                i++;
            }

            if (!IsMarkUp(lines[i]))
            {
                while (i < lines.Length && !IsMarkUp(lines[i]))
                {
                    content.Append(lines[i]);
                    i++;
                }
                content.AppendLine(" ");
            }

            if (i < lines.Length)
            {
                string convertedLine = ConvertSimpleMarkUp(lines[i]);
                content.AppendLine(convertedLine);
            }
        }

        return content.ToString();
    }

    private static void IsOpenedClosedCorrectly(string text)
    {
        MatchCollection boldMatches = Regex.Matches(text, @"\*\*");
        if (boldMatches.Count % 2 != 0) throw new Exception("b markup is not opened/closed correctly");

        if ((Regex.IsMatch(text, @"`([^`]+)") || Regex.IsMatch(text, @"([^`]+)`")) &&
            !Regex.IsMatch(text, @"`([^`]+)`"))
            throw new Exception("tt markup is not opened/closed correctly");
    }

    private static string ConvertSimpleMarkUp(string line)
    {
        if (line.Equals("")) return null;

        //this is a Bold text 
        if (Regex.IsMatch(line, @"\*\*(.*?)\*\*"))
        {
            line = Regex.Replace(line, @"\*\*(.*?)\*\*", "\u001b[1m$1\u001b[0m");
            if (Regex.IsMatch(line, @"_(.*?)_") || Regex.IsMatch(line, @"`([^`]+)`"))
                throw new Exception("Nested markup inside <b></b> is detected");
        }

        //this is an Italic text 
        if (Regex.IsMatch(line, @"_(.*?)_"))
        {
            line = Regex.Replace(line, @"_(.*?)_", "\u001b[3m$1\u001b[0m");
            if (Regex.IsMatch(line, @"\*\*(.*?)\*\*") || Regex.IsMatch(line, @"`([^`]+)`"))
                throw new Exception("Nested markup inside <i></i> is detected");
        }

        //this is a Monospaced text 
        if (Regex.IsMatch(line, @"`([^`]+)`"))
        {
            line = Regex.Replace(line, @"`([^`]+)`", "\u001b[7m$1\u001b[0m");
            if (Regex.IsMatch(line, @"\*\*(.*?)\*\*") || Regex.IsMatch(line, @"_(.*?)_"))
                throw new Exception("Nested markup inside <tt></tt> is detected");
        }

        return line;
    }

    private static string PrePartsConverter(string text)
    {
        if (Regex.IsMatch(text, @"\*\*(.*?)```\s*([\s\S]*?)\s*```(.*?)\*\*") ||
            Regex.IsMatch(text, @"_(.*?)```\s*([\s\S]*?)\s*```(.*?)_") ||
            Regex.IsMatch(text, @"`(.*?)```\s*([\s\S]*?)\s*```(.*?)`"))
            throw new Exception("Nested markup is detected");

        if ((Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*") || Regex.IsMatch(text, @"\s*([\s\S]*?)\s*```")) &&
            !Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
            throw new Exception("pre markup is not opened/closed correctly");

        if (Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
            text = Regex.Replace(text, @"```\s*([\s\S]*?)\s*```", "\u001b[7m$1\u001b[0m");

        return text;
    }

    private static bool IsMarkUp(string line) =>
        Regex.IsMatch(line, @"```\s*([\s\S]*?)\s*```") ||
        Regex.IsMatch(line, @"\*\*(.*?)\*\*") ||
        Regex.IsMatch(line, @"`([^`]+)`") ||
        Regex.IsMatch(line, @"_(.*?)_") ||
        line.Equals("");
}