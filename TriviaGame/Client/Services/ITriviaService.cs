

namespace TriviaGame.Client.Services
{
    public interface ITriviaService
    {
        Task<List<TriviaQuestion>> GetRandomQuestions(string topic, int difficulty);

       /* Task GetUserAccounts();
        Task GetUserLife(); 
        Task GetUserScore();  */
    }
}
