using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class TriviaChoice
{
    public int ChoiceId { get; set; }

    public string ChoiceText { get; set; } = null!;

    public string IsCorrect { get; set; } = null!;

    public int QuestionId { get; set; }

    public virtual TriviaQuestion Question { get; set; } = null!;
}
