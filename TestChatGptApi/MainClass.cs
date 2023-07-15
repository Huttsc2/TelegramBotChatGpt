using TestChatGptApi.CommunicationServices;

namespace TestChatGptApi
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            TelegramBot telegramBot = new TelegramBot();
            telegramBot.Initialize();
        }
    }
}
