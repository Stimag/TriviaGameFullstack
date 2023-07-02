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

        public List<TriviaTopic> TriviaTopics { get; set; } = new List<TriviaTopic>();
        public List<TriviaQuestion> TriviaQuestions { get; set;} = new List<TriviaQuestion>();
        public List<TriviaChoice> TriviaChoices { get; set;} = new List<TriviaChoice>();

        public async Task GetTriviaTopics()
        {
            var topics = await _http.GetFromJsonAsync<List<TriviaTopic>>("topics");
            if (topics == null)
                TriviaTopics = topics;
        }

        public async Task<List<TriviaQuestion>> GetRandomQuestions(string topic)
        {
            var url = $"randomquestions/{topic}";
            return await _http.GetFromJsonAsync<List<TriviaQuestion>>(url);
        }


        public async Task GetTriviaChoices()
        {
            var choices = await _http.GetFromJsonAsync<List<TriviaChoice>>("choices");
            if (choices == null)
                TriviaChoices = choices;
        }
    }
}


