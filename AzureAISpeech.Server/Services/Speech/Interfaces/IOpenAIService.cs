namespace AzureAISpeech.Server.Services.Speech.Interfaces
{
    public interface IOpenAIService
    {
        Task<(string MessageContent, int TotalTokens, int PromptTokens, int CompletionTokens)> GetResponseGPT4(List<object> texts);    
    }
}
