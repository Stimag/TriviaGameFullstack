using System;
using System.Collections.Generic;

namespace TriviaGame.Shared.Models;

public partial class UserAccount
{
    public string? UserAccountId { get; set; } 

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
