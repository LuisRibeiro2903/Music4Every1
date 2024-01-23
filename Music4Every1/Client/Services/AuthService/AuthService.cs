
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Music4Every1.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }
        public async Task<string> Login(UserLoginDTO user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", user);
            switch(response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    throw new UserNotFoundException("O email que inseriu não se encontra registado!");
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new WrongPasswordException("A password que inseriu é incorreta!");
                default: break;
            }
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<string> Register(UserRegisterDTO user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", user);
            switch(response.StatusCode)
            {
                case System.Net.HttpStatusCode.Conflict:
                    throw new UserAlreadyExistsException("O email que inseriu já se encontra registado!");
                default: break;
            }
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
