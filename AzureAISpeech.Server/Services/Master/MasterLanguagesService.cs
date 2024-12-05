using AzureAISpeech.Server.Repositories.Master.Interfaces;
using AzureAISpeech.Server.Models.Master;
using AzureAISpeech.Server.Services.Master.Interfaces;

namespace AzureAISpeech.Server.Services.Master
{
    public class MasterLanguagesService : IMasterLanguagesService
    {
        private readonly IMasterLanguagesRepository _masterLanguagesRepository;

        public MasterLanguagesService(IMasterLanguagesRepository masterLanguagesRepository)
        {
            _masterLanguagesRepository = masterLanguagesRepository;
        }

        public async Task<List<MasterLanguages>> GetAllLanguagesAsync()
        {
            try
            {
                var languages = await _masterLanguagesRepository.GetAllAsync();
                return languages;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
                Console.WriteLine("");

                throw new ApplicationException("[Services] Error retrieving data.", ex);
            }
        }
    }
}
