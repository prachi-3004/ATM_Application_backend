using ATM.Models;

namespace ATM.Services
{
    public interface IAuthService
    {
        public string GenerateJSONWebToken(string username, string role);

        public string AuthenticateUser(Login login);
    }
}
