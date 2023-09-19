using ATM.Data;
using ATM.Models;
using ATM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IConfiguration config, IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            IActionResult response = Unauthorized();
            var user = await _authService.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = _authService.GenerateJSONWebToken(user, login.Role);

                response = Ok(new LoginResponse { token = tokenString, User_Id = user, Role = 0 });
            }

            return response;
        }

    }
}
