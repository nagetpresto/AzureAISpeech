
namespace AzureAISpeech.Server.Repositories.Speech.Interfaces
{
    public interface IOpenAIRepository
    {
        Task<(string MessageContent, int TotalTokens, int PromptTokens, int CompletionTokens)> GetResponseGPT4(List<object> texts);
    }
}
