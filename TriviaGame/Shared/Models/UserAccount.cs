using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class UserAccount
{
    public string UserAccountId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UserScore> UserScores { get; set; } = new List<UserScore>();
}
