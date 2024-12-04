using AzureAISpeech.Server.Models.Master;

namespace AzureAISpeech.Server.Services.Master.Interfaces
{
    public interface IMasterLanguagesService
    {
        Task<List<MasterLanguages>> GetAllLanguagesAsync();
    }
}
