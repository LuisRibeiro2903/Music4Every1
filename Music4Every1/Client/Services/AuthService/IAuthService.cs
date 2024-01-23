namespace Music4Every1.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginDTO user);
        Task<string> Register(UserRegisterDTO user);

    }
}
