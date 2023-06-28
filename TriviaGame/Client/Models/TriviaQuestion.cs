namespace TriviaGame.Models
{
    public class TriviaQuestion
    {
        public string? Question { get; set; }
        public string[]? Choices { get; set; }
        public int CorrectChoice { get; set; }
    }
}
