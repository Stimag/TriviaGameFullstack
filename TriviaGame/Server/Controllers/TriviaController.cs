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

        [HttpGet("randomquestions/{topic}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestions(string topic, int difficulty)
        {
            var randomQuestions = _context.TriviaQuestions
                .Include(q => q.TriviaChoices)
                .Where(t => t.Topic.TopicName == topic)
                .Where(d => d.QuestionDifficulty == difficulty)
                .ToList();

            if (randomQuestions == null || randomQuestions.Count == 0)
            {
                return NotFound();
            }

            // Randomizing order of questions
            var random = new Random();
            randomQuestions = randomQuestions.OrderBy(q => random.Next()).ToList();

            // Randomizing order of choices 
            foreach (var question in randomQuestions)
            {
                question.TriviaChoices = question.TriviaChoices.OrderBy(c => random.Next()).ToList();
            }

            return randomQuestions;
        }
    }
}
