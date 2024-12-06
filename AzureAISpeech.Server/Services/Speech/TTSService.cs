using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using AzureAISpeech.Server.Services.Speech.Interfaces;
using System;
using System.Threading.Tasks;

namespace AzureAISpeech.Server.Services.Speech
{
    public class TTSService : ITTSService
    {
        private readonly ITTSRepository _ttsRepository;

        public TTSService(ITTSRepository ttsRepository)
        {
            _ttsRepository = ttsRepository;
        }

        public async Task SpeakAsync(string text, string languageCode)
        {
            try
            {
                await _ttsRepository.SpeakAsync(text, languageCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("[Services] Error requesting TTS: " + ex.Message);
                throw new ApplicationException("[Services] Error requesting TTS: " + ex.Message);
            }
        }
    }
}
