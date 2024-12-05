namespace AzureAISpeech.Server.Services.Speech.Interfaces
{
    public interface ISTTService
    {
        Task StartRecordingAsync(Action<string> onTranscriptionReceived, string languageCode);
        void CancelRecording();
    }
}
