using AzureAISpeech.Server.Models.Master;

namespace AzureAISpeech.Server.Repositories.Master.Interfaces
{
    public interface IMasterLanguagesRepository
    {
        Task<List<MasterLanguages>> GetAllAsync();
        
    }
}
