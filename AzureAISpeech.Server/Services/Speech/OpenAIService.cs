using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using AzureAISpeech.Server.Services.Speech.Interfaces;

namespace AzureAISpeech.Server.Services.Speech
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IOpenAIRepository _openAIRepository;

        public OpenAIService(IOpenAIRepository openAIRepository)
        {
            _openAIRepository = openAIRepository;
        }

        public async Task<(string MessageContent, int TotalTokens, int PromptTokens, int CompletionTokens)> GetResponseGPT4(List<object> texts)
        {
            try
            {
                var (messageContent, totalTokens, promptTokens, completionTokens) = await _openAIRepository.GetResponseGPT4(texts);
                return (messageContent, totalTokens, promptTokens, completionTokens);

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("[Services] Error requesting OpenAI: " + ex.Message);
                throw new ApplicationException("[Services] Error requesting OpenAI: " + ex.Message);
            }
        }

    }
}
