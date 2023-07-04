

namespace TriviaGame.Client.Services
{
    public interface ITriviaService
    {
        /*Task<List<TriviaQuestion>> GetRandomQuestionsByTopic(string topic);*/
        Task<List<TriviaQuestion>> GetRandomQuestionsByTopicAndDifficulty(string topic, int difficulty);

       /* Task GetUserAccounts();
        Task GetUserLife(); 
        Task GetUserScore();  */
    }
}
