

namespace AzureAISpeech.Server.Repositories.Speech.Interfaces
{
    public interface ISTTRepository
    {
        Task StartRecordingAsync(Action<string> onTranscriptionReceived, string languageCode);
        void CancelRecording();
    }

}
