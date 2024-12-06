using AzureAISpeech.Server.Repositories.Speech;
using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using AzureAISpeech.Server.Services.Speech.Interfaces;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AzureAISpeech.Server.Services.Speech
{
    public class STTService : ISTTService
    {
        private readonly ISTTRepository _sttRepository;

        public STTService(ISTTRepository sttRepository)
        {
            _sttRepository = sttRepository;
        }

        public async Task StartRecordingAsync(Action<string> onTranscriptionReceived, string languageCode)
        {
            try
            {
                await _sttRepository.StartRecordingAsync(onTranscriptionReceived, languageCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("[Services] Error requesting STT: " + ex.Message);
                throw new ApplicationException("[Services] Error requesting STT: " + ex.Message);
            }
        }

        public void CancelRecording()
        {
            try
            {
                _sttRepository.CancelRecording();

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("[Services] Error requesting STT: " + ex.Message);
                throw new ApplicationException("[Services] Error requesting STT: " + ex.Message);
            }

            
        }
    }
}
