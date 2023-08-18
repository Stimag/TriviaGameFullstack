using System.Net.Http;
using System.Net.Http.Json;

namespace TriviaGame.Client.Services
{
    public class TriviaService
    {
        private readonly HttpClient httpClient;

        public TriviaService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7170/api/trivia/")
            };

        }

        /*public async Task<List<TriviaQuestion>> GetRandomQuestionsByTopic(string topic)
        {
            var url = $"questions/{topic}";
            return await _http.GetFromJsonAsync<List<TriviaQuestion>>(url);
        }*/

        public async Task<List<TriviaQuestion>> GetRandomQuestionsByTopicAndDifficulty(string topic, int difficulty)
        {
            var url = $"questions/{topic}/{difficulty}";
            return await httpClient.GetFromJsonAsync<List<TriviaQuestion>>(url);
        }
    }
}


