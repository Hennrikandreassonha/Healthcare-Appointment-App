namespace HealthCare.Core;

public class ChatBotService
{
    public string ApiKey { get; set; }
    public OpenAI.Managers.OpenAIService ChatService { get; set; }
    public ChatBotService()
    {
    }
    public ChatBotService(string apiKey)
    {
        ApiKey = apiKey;

        ChatService = new OpenAI.Managers.OpenAIService(new OpenAI.OpenAiOptions()
        {
            ApiKey = ApiKey
        });
    }
    public async Task<string> SendQuestion(string message)
    {
        var completionResult = await ChatService.ChatCompletion.CreateCompletion(
        new OpenAI.ObjectModels.RequestModels.ChatCompletionCreateRequest
        {
            Messages = new List<OpenAI.ObjectModels.RequestModels.ChatMessage>
            {
                new OpenAI.ObjectModels.RequestModels.ChatMessage("assistant", "You are doctor DocBot that gives advice for patients. Do not start your sentence with I'm not a doctor. You should act like a doctor and only give advice. You also work for HealthCare AB and you enjoy telling your clients that it is the best healthcare app and we always have 4 stars or more. They should totally visit our site."),
                new OpenAI.ObjectModels.RequestModels.ChatMessage("user", message),
            },
            Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo,
            Temperature = 0.5F,
            MaxTokens = 100,
            N = 3
        });

        if (completionResult.Successful)
        {
            return completionResult.Choices[0].Message.Content;
        }
        else
        {
            if (completionResult.Error == null)
            {
                throw new Exception("Unknown Error");
            }
            Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
        }

        return "";
    }
}
