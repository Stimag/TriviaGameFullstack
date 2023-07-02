

namespace TriviaGame.Client.Services
{
    public interface ITriviaService
    {
        List<TriviaTopic> TriviaTopics { get; set; }
        List<TriviaQuestion> TriviaQuestions { get; set; }
        List<TriviaChoice> TriviaChoices { get; set; }
        /*List<UserAccount> UserAccounts { get; set; }
        List<UserLife> userLives { get; set; }
        List<UserScore> userScores { get; set; }*/

        Task GetTriviaTopics();
        //Task<List<TriviaQuestion>> GetTriviaQuestions();

        Task<List<TriviaQuestion>> GetRandomQuestions(string topic);
        Task GetTriviaChoices();
       /* Task GetUserAccounts();
        Task GetUserLife(); 
        Task GetUserScore();  */
    }
}
