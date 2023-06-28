using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class TriviaTopic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public virtual ICollection<TriviaQuestion> TriviaQuestions { get; set; } = new List<TriviaQuestion>();
}
