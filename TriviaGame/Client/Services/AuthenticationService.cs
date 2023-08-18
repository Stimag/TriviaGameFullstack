using System.Text;
using System.Text.Json;
using static TriviaGame.Client.Pages.Index;


namespace TriviaGame.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient httpClient;

        public AuthenticationService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7170/api/authentication/")
            };
        }

        public HttpClient? Http { get; }

        public async Task<bool> Login(LoginModel loginModel)
        {
            using var client = new HttpClient();

            string json = JsonSerializer.Serialize(loginModel);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                // Login successful
                return true;
            }

            // Login failed
            return false;
        }

        public async Task<bool> Register(UserAccount user)
        {
            string json = JsonSerializer.Serialize(user);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("register", content);

            if (response.IsSuccessStatusCode)
            {
                // Registration successful
                return true;
            }

            // Registration failed
            return false;
        }
    }
}