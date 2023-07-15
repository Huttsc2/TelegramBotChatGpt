using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using TestChatGptApi.Helper;

namespace TelegramBotChatGpt.CommunicationServices
{
    public class DalleContacter
    {
        public async Task<string> GetImage(string prompt)
        {
            var apiKey = new KeysReader().GetKeys().GptKey;
            string responsContent;

            var sdk = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = apiKey
            });

            var imageResult = await sdk.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = prompt,
                N = 1,
                Size = "256x256",
                ResponseFormat = "url",
                User = "TestUser"
            });

            if (imageResult.Successful)
            {
                responsContent = string.Join("\n", imageResult.Results.Select(r => r.Url));
            }
            else //handle errors
            {
                if (imageResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                responsContent = "error";
            }
            return responsContent;

        }
    }
}
