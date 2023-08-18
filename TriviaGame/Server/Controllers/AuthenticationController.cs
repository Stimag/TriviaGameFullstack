using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using TriviaGame.Client.Services;
using TriviaGame.Shared.Models;
using static TriviaGame.Client.Pages.Index;
using BCrypt.Net;


namespace TriviaGame.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        public AuthenticationController(DatabaseContext context)
        {
            dbContext = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (user == null || !VerifyPassword(loginModel.Password, user.PasswordHash))
            {
                // Invalid login credentials
                return Unauthorized();
            }

            // Login successful
            return Ok();
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            bool emailExists = await dbContext.UserAccounts.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                // Email is already registered
                return Conflict();
            }

            // Email is available for registration
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAccount user)
        {
            bool emailExists = await dbContext.UserAccounts.AnyAsync(u => u.Email == user.Email);
            if (emailExists)
            {
                // Email is already registered
                return Conflict();
            }

            bool usernameExists = await dbContext.UserAccounts.AnyAsync(u => u.Username == user.Username);
            if (usernameExists)
            {
                // Username is already taken
                return Conflict();
            }

            if (string.IsNullOrEmpty(user.UserAccountId))
            {
                // Generate a UUID for UserAccountId
                user.UserAccountId = Guid.NewGuid().ToString();
            }

            // Generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash, salt);

            user.PasswordHash = passwordHash;

            dbContext.UserAccounts.Add(user);
            await dbContext.SaveChangesAsync();

            // Registration successful
            return Ok();
        }
    }
}