using TestChatGptApi.Data;
using Newtonsoft.Json;

namespace TestChatGptApi.Helper
{
    public class KeysReader
    {
        public Keys GetKeys()
        {
            string path = PathFinder.GetRootDirectory();
            using StreamReader r = new(path + "/Data/Data.json");
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Keys>(json);
        }
    }
}
