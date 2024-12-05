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
                                    CodeTTS = reader.GetString(1),
                                    CodeSTT = reader.GetString(2),
                                    Description = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("[Repository] Db error: " + sqlEx.Message);
                Console.WriteLine("");
                throw new ApplicationException("[Repository] Db error.", sqlEx);
            }
            catch (TimeoutException timeoutEx)
            {
                Console.WriteLine("[Repository] Db Timeout: " + timeoutEx.Message);
                Console.WriteLine("");
                throw new ApplicationException("[Repository] Db Timeout", timeoutEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Repository] An unexpected error." + ex.Message);
                Console.WriteLine("");
                throw new ApplicationException("[Repository] An unexpected error.", ex);
            }

            return masterLanguages;
        }
    }
}
