﻿
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Music4Every1.Client.Services.AuthService
{
    public class AuthService : AuthenticationStateProvider, IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
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

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");

            var indentity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                indentity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(indentity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;

        }

    }
}
