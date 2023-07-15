using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using TestChatGptApi.Helper;

namespace TestChatGptApi.CommunicationServices
{
    public class GptContacter
    {
        public async Task<string> GetResponse(string request)
        {
            var apiKey = new KeysReader().GetKeys().GptKey;
            string responsContent;

            var gpt3 = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = apiKey
            });

            var completionResult = await gpt3.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
            {
                Messages = new List<ChatMessage>(new ChatMessage[] {
                new ChatMessage("user", request)
            }),
                Model = Models.ChatGpt3_5Turbo
            });

            if (completionResult.Successful)
            {
                responsContent = completionResult.Choices[0].Message.Content;
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                return "Error";
            }
            return responsContent;
        }
    }
}
