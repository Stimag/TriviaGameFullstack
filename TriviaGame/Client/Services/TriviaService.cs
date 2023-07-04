using System.Net.Http;
using System.Net.Http.Json;

namespace TriviaGame.Client.Services
{
    public class TriviaService : ITriviaService
    {
        private readonly HttpClient _http;

        public TriviaService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:7170/api/trivia/");
        }

        public async Task<List<TriviaQuestion>> GetRandomQuestions(string topic, int difficulty)
        {
            var url = $"randomquestions/{topic}?difficulty={difficulty}";
            return await _http.GetFromJsonAsync<List<TriviaQuestion>>(url);
        }
    }
}


