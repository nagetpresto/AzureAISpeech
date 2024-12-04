using AzureAISpeech.Server.Models.Master;
using Microsoft.Data.SqlClient;
using AzureAISpeech.Server.Repositories.Master.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AzureAISpeech.Server.Repositories.Master
{
    public class MasterLanguagesRepository : IMasterLanguagesRepository
    {
        private readonly string _connectionString;

        public MasterLanguagesRepository()
        {
            var configService = new ConfigurationService();
            _connectionString = configService.Configuration.GetConnectionString("DefaultConnection") ;
        }

        public async Task<List<MasterLanguages>> GetAllAsync()
        {
            var masterLanguages = new List<MasterLanguages>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var commandText = "select * from OpenAI.azs.MasterLanguages order by Description asc";


                    using (var command = new SqlCommand(commandText, connection))
                    {
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                masterLanguages.Add(new MasterLanguages
                                {
                                    ID = reader.GetInt32(0),
                                    Code = reader.GetString(1),
                                    Description = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("[Repository] Db error.", sqlEx);
            }
            catch (TimeoutException timeoutEx)
            {
                throw new ApplicationException("[Repository] Db Timeout", timeoutEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("[Repository] An unexpected error.", ex);
            }

            return masterLanguages;
        }
    }
}
