using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static TriviaGame.Client.Pages.Index;


namespace TriviaGame.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient httpClient;
        private string errorMessage;

        public AuthenticationService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7170/api/authentication/")
            };
        }

        public HttpClient? Http { get; }

        // Error message methods
        public string GetErrorMessage()
        {
            return errorMessage;
        }

        private class ErrorResponse
        {
            public string Message { get; set; }
        }

        public void ClearErrorMessage()
        {
            errorMessage = null; // Reset error message
        }

        // Login method
        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                string json = JsonSerializer.Serialize(loginModel);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync("login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Login successful
                    return true;
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(responseContent))
                    {
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                        if (errorResponse != null && !string.IsNullOrWhiteSpace(errorResponse.Message))
                        {
                            errorMessage = errorResponse.Message;
                        }
                        else
                        {
                            errorMessage = "An error occurred during login.";
                        }
                    }
                    else
                    {
                        errorMessage = "An error occurred during login.";
                    }

                    return false; // Login failed
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Network or HTTP error: " + ex.Message);
                errorMessage = "Network or server error occurred.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Other error: " + ex.Message);
                errorMessage = "An unexpected error occurred.";
            }

            return false; // Error 
        }

        // Register method
        public async Task<bool> Register(UserAccount user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync("register", content);

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful
                    return true;
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(responseContent))
                    {
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                        if (errorResponse != null && !string.IsNullOrWhiteSpace(errorResponse.Message))
                        {
                            errorMessage = errorResponse.Message;
                        }
                        else
                        {
                            errorMessage = "An error occurred during registration.";
                        }
                    }
                    else
                    {
                        errorMessage = "An error occurred during registration.";
                    }

                    return false; // Registration failed
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Network or HTTP error: " + ex.Message);
                errorMessage = "Network or server error occurred during registration.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Other error: " + ex.Message);
                errorMessage = "An unexpected error occurred during registration.";
            }

            return false; // Error 
        }

        // Input validation
        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,254}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool IsValidUsername(string username)
        {
            string usernamePattern = @"^[a-zA-Z0-9]{3,30}$";
            return Regex.IsMatch(username, usernamePattern);
        }

        public bool IsValidPassword(string password)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&^]).{8,254}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        public bool IsValidUserAccount(UserAccount userAccount)
        {
            return IsValidEmail(userAccount.Email) && IsValidUsername(userAccount.Username);
        }
    }
}
