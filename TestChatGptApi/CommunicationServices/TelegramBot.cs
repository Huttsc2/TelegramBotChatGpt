using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TestChatGptApi.Helper;

namespace TestChatGptApi.CommunicationServices
{
    public class TelegramBot
    {

        static ITelegramBotClient Bot = new TelegramBotClient(new KeysReader().GetKeys().BotKey);
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            GptContacter test = new GptContacter();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "хуй")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Раз, два, три!");
                    return;
                }
                string request = message.Text;
                string response = test.GetResponse(request).GetAwaiter().GetResult();
                await botClient.SendTextMessageAsync(message.Chat, response);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public void Initialize()
        {
            Console.WriteLine("Bot started " + Bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            Bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
