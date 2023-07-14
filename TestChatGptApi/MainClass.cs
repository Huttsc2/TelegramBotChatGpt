using TestChatGptApi.CommunicationServices;

namespace TestChatGptApi
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            TelegramBot telegramBotTest = new TelegramBot();
            telegramBotTest.Initialize();
        }
    }
}
