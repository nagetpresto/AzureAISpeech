using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using System;
using System.Threading.Tasks;

namespace AzureAISpeech.Server.Repositories.Speech
{
    public class TTSRepository : ITTSRepository
    {
        private readonly IConfiguration _configuration;

        public TTSRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SpeakAsync(string text, string languageCode)
        {
            try
            {
                var apiKey = _configuration["AzureSpeech:ApiKey"];
                var region = _configuration["AzureSpeech:Region"];

                
                var speechConfig = SpeechConfig.FromSubscription(apiKey, region);
                speechConfig.SpeechSynthesisVoiceName = languageCode;

                using (var synthesizer = new SpeechSynthesizer(speechConfig))
                {
                    var result = await synthesizer.SpeakTextAsync(text);

                    switch (result.Reason)
                    {
                        case ResultReason.SynthesizingAudioCompleted:
                            break;

                        case ResultReason.Canceled:
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            Console.WriteLine("");
                            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                Console.WriteLine("");
                                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                Console.WriteLine("CANCELED: Did you set the speech resource key and region values?");
                            }
                            break;

                        default:
                            Console.WriteLine("[Repository] Unexpected result from TTS.");
                            Console.WriteLine("");
                            throw new ApplicationException("[Repository] Unexpected result from TTS.");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Repository] Unexpected TTS Error: " + ex.Message);
                Console.WriteLine("");
                throw new ApplicationException("[Repository] Unexpected TTS Error.", ex);
            }
        }
    }
}
