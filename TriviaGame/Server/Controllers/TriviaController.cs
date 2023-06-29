using Microsoft.AspNetCore.Mvc;
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

        // Get all choices 
        [HttpGet("choices")]
        public async Task<ActionResult<List<TriviaChoice>>> GetTriviaChoices()
        {
            var choices = await _context.TriviaChoices.ToListAsync();
            return Ok(choices);
        }
    }
}
