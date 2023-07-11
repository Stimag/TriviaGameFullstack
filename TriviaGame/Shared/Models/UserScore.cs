using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class UserScore
{
    public int Score { get; set; }

    public int Highscore { get; set; }

    public string UserAccountId { get; set; } = null!;

    public virtual UserAccount UserAccount { get; set; } = null!;
}
