using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class UserScore
{
    public int ScoreId { get; set; }

    public int UserScore1 { get; set; }

    public int UserHighscore { get; set; }

    public string UserAccountId { get; set; } = null!;

    public virtual UserAccount UserAccount { get; set; } = null!;
}
