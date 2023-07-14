using System.Text.RegularExpressions;

namespace TestChatGptApi.Helper
{
    public class PathFinder
    {
        public static string GetRootDirectory()
        {
            string dir = Directory.GetCurrentDirectory();
            Regex reg = new(".{0,}TestChatGptApi");
            return reg.Match(dir).Captures.First().Value;
        }
    }
}
