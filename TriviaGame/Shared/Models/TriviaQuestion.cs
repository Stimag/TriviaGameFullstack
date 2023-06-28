using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class TriviaQuestion
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int QuestionDifficulty { get; set; }

    public int TopicId { get; set; }

    public virtual TriviaTopic Topic { get; set; } = null!;

    public virtual ICollection<TriviaChoice> TriviaChoices { get; set; } = new List<TriviaChoice>();
}
