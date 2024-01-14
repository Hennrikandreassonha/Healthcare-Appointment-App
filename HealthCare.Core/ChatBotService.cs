using OpenAI.;

namespace HealthCare.Core;

public class ChatBotService
{
    public string ApiKey { get; set; }
    public OpenAI.Managers.OpenAIService ChatService { get; set; }
    public ChatBotService(string apiKey)
    {
        ApiKey = apiKey;

        ChatService = new OpenAI.Managers.OpenAIService(new OpenAI.OpenAiOptions()
        {
            ApiKey = ApiKey
        });
    }
    public async Task<string> SendQuestion()
    {
        var completionResult = await ChatService.ChatCompletion.CreateCompletion(
        new OpenAI.ObjectModels.RequestModels.ChatCompletionCreateRequest
        {
            Messages = new List<OpenAI.ObjectModels.RequestModels.ChatMessage>
            {
                new OpenAI.ObjectModels.RequestModels.ChatMessage("system", "You are a doctor that gives advice for patients"),
                new OpenAI.ObjectModels.RequestModels.ChatMessage("user", "how to learn c# in 24 hours"),
            },
            Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo,
            Temperature = 0.5F,
            MaxTokens = 100,
            N = 3
        });

        if (completionResult.Successful)
        {
            foreach (var choice in completionResult.Choices)
            {
                Console.WriteLine(choice.Message.Content);
            }
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
