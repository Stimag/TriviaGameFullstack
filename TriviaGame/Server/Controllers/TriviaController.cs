using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TriviaGame.Shared.Models;

namespace TriviaGame.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        public TriviaController(DatabaseContext context)
        {
            dbContext = context;
        }

        [HttpGet("questions/{topic}/{difficulty}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestionsByTopicAndDifficulty(string topic, int difficulty)
        {
            var topicParameter = new SqlParameter("@topic", topic);
            var difficultyParameter = new SqlParameter("@difficulty", difficulty);

            // Execute the stored procedure and retrieve the questions
            var questions = dbContext.TriviaQuestions
                .FromSqlRaw("EXEC GetQuestionsByTopicAndDifficulty @topic, @difficulty",
                    topicParameter,
                    difficultyParameter)
                .ToList();

            if (questions == null || questions.Count == 0)
            {
                return NotFound();
            }

            RandomizeQuestionsAndChoices(questions);

            return questions;
        }

        private List<TriviaQuestion> RandomizeQuestionsAndChoices(List<TriviaQuestion> questions)
        {
            var random = new Random();

            // Randomizing order of questions
            questions = questions.OrderBy(q => random.Next()).ToList();

            // Retrieve the choices for the randomized questions
            var questionIds = questions.Select(q => q.QuestionId).ToList();
            var choices = dbContext.TriviaChoices
                .Where(c => questionIds.Contains(c.QuestionId))
                .ToList();

            // Map the choices to their respective questions
            foreach (var question in questions)
            {
                question.TriviaChoices = choices.Where(c => c.QuestionId == question.QuestionId)
                                                .OrderBy(c => random.Next())
                                                .ToList();
            }

            return questions;
        }



        /*[HttpGet("questions/{topic}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestionsByTopic(string topic)
        {
            var questions = dataContext.TriviaQuestions
                .Include(q => q.TriviaChoices)
                .Where(t => t.Topic.TopicName == topic)
                .ToList();

            if (questions == null || questions.Count == 0)
            {
                return NotFound();
            }

            questions = RandomizeQuestionsAndChoices(questions);

            return questions;
        }*/
    }
}
