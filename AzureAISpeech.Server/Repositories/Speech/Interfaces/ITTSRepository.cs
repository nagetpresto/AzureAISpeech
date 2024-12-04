

namespace AzureAISpeech.Server.Repositories.Speech.Interfaces
{
    public interface ITTSRepository
    {
        Task SpeakAsync(string text, string languageCode);
    }
}
