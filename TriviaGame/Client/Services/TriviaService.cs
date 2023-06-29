/*using System.Net.Http.Json;

namespace TriviaGame.Client.Services
{
    public class TriviaService : ITriviaService
    {
        private readonly HttpClient _http;

        public TriviaService(HttpClient http)
        {
            _http = http;
        }

        public List<TriviaTopic> TriviaTopics { get; set; } = new List<TriviaTopic>();
        public List<TriviaQuestion> TriviaQuestions { get; set;} = new List<TriviaQuestion>();
        public List<TriviaChoice> TriviaChoices { get; set;} = new List<TriviaChoice>();

        public async Task GetTriviaTopics()
        {
            var result = await _http.GetFromJsonAsync<List<TriviaTopic>>("api/trivia/topics");
            if (result == null)
                TriviaTopics = result;
        }

        public async Task GetTriviaQuestions()
        {
            var result = await _http.GetFromJsonAsync<List<TriviaQuestion>>("api/trivia/questions");
            if (result == null)
                TriviaQuestions = result;
        }

        public async Task GetTriviaChoices()
        {
            var result = await _http.GetFromJsonAsync<List<TriviaChoice>>("api/trivia/choices");
            if (result == null)
                TriviaChoices = result;
        }
    }
}*/


