using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

public class STTRepository : ISTTRepository
{
    private readonly IConfiguration _configuration;
    private SpeechRecognizer _speechRecognizer;
    private CancellationTokenSource _cancellationTokenSource;

    public STTRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task StartRecordingAsync(Action<string> onTranscriptionReceived, string languageCode)
    {
        try
        {
            var apiKey = _configuration["AzureSpeech:ApiKey"];
            var region = _configuration["AzureSpeech:Region"];

            var speechConfig = SpeechConfig.FromSubscription(apiKey, region);
            speechConfig.SpeechRecognitionLanguage = languageCode;

            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            _speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            _speechRecognizer.Recognizing += (sender, e) =>
            {
                onTranscriptionReceived?.Invoke(e.Result.Text);
            };

            _cancellationTokenSource = new CancellationTokenSource();

            await _speechRecognizer.StartContinuousRecognitionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Repository] STT start record: " + ex.Message);
            Console.WriteLine("");
            throw new ApplicationException("[Repository] STT start record.", ex);
        }
    }

    public void CancelRecording()
    {
        try
        {
            _speechRecognizer?.StopContinuousRecognitionAsync();
            _cancellationTokenSource?.Cancel();
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Repository] STT end record: " + ex.Message);
            Console.WriteLine("");
        }
    }
}
