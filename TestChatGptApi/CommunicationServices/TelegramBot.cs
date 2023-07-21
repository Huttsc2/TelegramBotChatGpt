using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TestChatGptApi.Helper;
using TelegramBotChatGpt.CommunicationServices;

namespace TestChatGptApi.CommunicationServices
{
    public class TelegramBot
    {

        static ITelegramBotClient Bot = new TelegramBotClient(new KeysReader().GetKeys().BotKey);
        
        public static async Task GetGptResponse(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            GptContacter gpt = new GptContacter();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                
                if (message.Text == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Случилась какая то хУйня!");
                    return;
                }

                if (message.Text.ToLower() == "хуй")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Раз, два, три!");
                    return;
                }
                string request = message.Text;
                string response = gpt.GetResponse(request).GetAwaiter().GetResult();
                await botClient.SendTextMessageAsync(message.Chat, response);
                return;
            }
            return;
        }

        public static async Task GetDalleResponse(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            DalleContacter dalle = new DalleContacter();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Случилась какая то хУйня!");
                    return;
                }

                if (message.Text.ToLower() == "хуй")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Раз, два, три!");
                    return;
                }
                string request = message.Text;
                string response = dalle.GetImage(request).GetAwaiter().GetResult();

                try
                {
                    await botClient.SendPhotoAsync(message.Chat,
                    photo: InputFile.FromUri(response),
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
                }
                catch
                {
                    await botClient.SendTextMessageAsync(message.Chat, request + " - это полная хуйня, я отказываюсь \"это\" рисовать");
                }
                
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
                //GetGptResponse,
                GetDalleResponse,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
