using System.Text;
using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class OpenAIRepository : IOpenAIRepository
{
    private readonly IConfiguration _configuration;

    public OpenAIRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<(string MessageContent, int TotalTokens, int PromptTokens, int CompletionTokens)> GetResponseGPT4(List<object> texts)
    {
        try
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            var endpoint = _configuration["OpenAI:EndpointGPT4"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

                var requestBody = new
                {
                    model = "gpt-4",
                    messages = texts,
                    temperature = 0,
                    max_tokens = 50
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var response = await httpClient.PostAsync(
                    endpoint,
                    new StringContent(jsonContent, Encoding.UTF8, "application/json")
                );

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<dynamic>(result);

                string messageContent = responseObject.choices[0].message.content;
                int totalTokens = responseObject.usage.total_tokens;
                int promptTokens = responseObject.usage.prompt_tokens;
                int completionTokens = responseObject.usage.completion_tokens;

                return (messageContent, totalTokens, promptTokens, completionTokens);
            }

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("");
            Console.WriteLine($"[Repository] HTTP Request error: {ex.Message}");
            throw new ApplicationException($"[Repository] HTTP Request error: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine("");
            Console.WriteLine($"[Repository] JSON parsing error: {ex.Message}");
            throw new ApplicationException($"[Repository] JSON parsing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("");
            Console.WriteLine("[Repository] An unexpected error." + ex.Message);
            throw new ApplicationException("[Repository] An unexpected error." + ex.Message);
        }
    }
}
