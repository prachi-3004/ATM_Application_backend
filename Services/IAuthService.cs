using ATM.Models;

namespace ATM.Services
{
    public interface IAuthService
    {
        public string GenerateJSONWebToken(int id, int role);

        public Task<int> AuthenticateUser(Login login);
    }
}
