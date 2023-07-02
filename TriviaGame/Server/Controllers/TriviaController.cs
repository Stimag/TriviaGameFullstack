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

        // Get random trivia questions
        [HttpGet("randomquestions/{topic}")]
        public ActionResult<List<TriviaQuestion>> GetRandomQuestions(string topic)
        {
            var randomQuestions = _context.TriviaQuestions
                .Include(q => q.TriviaChoices)
                .Where(t => t.Topic.TopicName == topic) 
                .OrderBy(q => Guid.NewGuid())
                .ToList();

            if (randomQuestions == null || randomQuestions.Count == 0)
            {
                return NotFound();
            }

            return randomQuestions;
        }
    }
}
