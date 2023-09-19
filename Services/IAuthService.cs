using ATM.Models;

namespace ATM.Services
{
    public interface IAuthService
    {
        public string GenerateJSONWebToken(string username, string role);

        public Task<string> AuthenticateUser(Login login);
    }
}
