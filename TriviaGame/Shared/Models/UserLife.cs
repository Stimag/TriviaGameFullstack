using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class UserLife
{
    public int RemainingLives { get; set; }

    public string UserAccountId { get; set; } = null!;

    public virtual UserAccount UserAccount { get; set; } = null!;
}
