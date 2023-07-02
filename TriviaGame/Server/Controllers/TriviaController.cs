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

        // Get all trivia topics
        [HttpGet("topics")]
        public async Task<ActionResult<List<TriviaTopic>>> GetTriviaTopics()
        {
            var topics = await _context.TriviaTopics.ToListAsync();
            return Ok(topics);
        }

        // Get all trivia questions
        [HttpGet("questions")]
        public async Task<ActionResult<List<TriviaQuestion>>> GetTriviaQuestions()
        {
            var questions = await _context.TriviaQuestions.Include(c => c.TriviaChoices).ToListAsync();
            return Ok(questions);
        }

        // Get random trivia questions
        [HttpGet("randomquestions/{topic}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestions(string topic)
        {
            var randomQuestions = _context.TriviaQuestions
                .Include(q => q.TriviaChoices)
                .Where(q => q.Topic.TopicName == topic) 
                .OrderBy(q => Guid.NewGuid())
                .ToList();

            if (randomQuestions == null || randomQuestions.Count == 0)
            {
                return NotFound();
            }

            return randomQuestions;
        }



        // Get all choices 
        [HttpGet("choices")]
        public async Task<ActionResult<List<TriviaChoice>>> GetTriviaChoices()
        {
            var choices = await _context.TriviaChoices.ToListAsync();
            return Ok(choices);
        }
    }
}
