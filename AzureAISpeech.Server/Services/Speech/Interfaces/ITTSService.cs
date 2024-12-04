namespace AzureAISpeech.Server.Services.Speech.Interfaces
{
    public interface ITTSService
    {
        Task SpeakAsync(string text, string languageCode);
    }
}
