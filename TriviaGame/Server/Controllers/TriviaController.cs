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

        // Get a single trivia topic by id
        [HttpGet("topic/{id}")]
        public async Task<ActionResult<List<TriviaTopic>>> GetSingleTopic(int id)
        {
            var topic = await _context.TriviaTopics
                .Include(h => h.TriviaQuestions)
                .FirstOrDefaultAsync(h => h.TopicId == id);
            if(topic == null)
            {
                return NotFound("Topic not found");
            }
            return Ok(topic);
        }

        // Get all trivia questions
        [HttpGet("questions")]
        public async Task<ActionResult<List<TriviaQuestion>>> GetTriviaQuestions()
        {
            var questions = await _context.TriviaQuestions.ToListAsync();
            return Ok(questions);
        }

        // Get a single trivia question by id
        [HttpGet("question/{id}")]
        public async Task<ActionResult<List<TriviaQuestion>>> GetSingleQuestion(int id)
        {
            var question = await _context.TriviaQuestions
                .Include(h => h.TriviaChoices)
                .FirstOrDefaultAsync(h => h.QuestionId == id);
            if (question == null)
            {
                return NotFound("Question not found");
            }
            return Ok(question);
        }

        // Get all choices 
        [HttpGet("choices")]
        public async Task<ActionResult<List<TriviaChoice>>> GetTriviaChoices()
        {
            var choices = await _context.TriviaChoices.ToListAsync();
            return Ok(choices);
        }

        // Get a single trivia choice by id
        [HttpGet("choices/{id}")]
        public async Task<ActionResult<List<TriviaChoice>>> GetSingleChoice(int id)
        {
            var choice = await _context.TriviaChoices
                .FirstOrDefaultAsync(h => h.ChoiceId == id);
            if (choice == null)
            {
                return NotFound("Choice not found");
            }
            return Ok(choice);
        }


    }
}
