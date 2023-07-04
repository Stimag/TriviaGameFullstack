using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriviaGame.Shared.Models;

namespace TriviaGame.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly DataContext _context;

        public TriviaController(DataContext context)
        {
            _context = context;
        }

        /*[HttpGet("questions/{topic}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestionsByTopic(string topic)
        {
            var questions = _context.TriviaQuestions
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

        [HttpGet("questions/{topic}/{difficulty}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestionsByTopicAndDifficulty(string topic, int difficulty)
        {
            var questions = _context.TriviaQuestions
                .Include(q => q.TriviaChoices)
                .Where(t => t.Topic.TopicName == topic)
                .Where(d => d.QuestionDifficulty == difficulty)
                .ToList();

            if (questions == null || questions.Count == 0)
            {
                return NotFound();
            }

            questions = RandomizeQuestionsAndChoices(questions);

            return questions;
        }


        private List<TriviaQuestion> RandomizeQuestionsAndChoices(List<TriviaQuestion> questions)
        {
            var random = new Random();

            // Randomizing order of questions
            questions = questions.OrderBy(q => random.Next()).ToList();

            // Randomizing order of choices 
            foreach (var question in questions)
            {
                question.TriviaChoices = question.TriviaChoices.OrderBy(c => random.Next()).ToList();
            }

            return questions;
        }
    }
}
