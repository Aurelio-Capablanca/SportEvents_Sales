using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;
using SportEvents_Sales_Back_End.Security;
using System.Diagnostics;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("auth")]
    public class LoginController : Controller
    {

        private readonly LoginSessions _loginService;
        private readonly GlobalSession _session;

        public LoginController(LoginSessions loginService, IUserSessionProvider provider)
        {
            _loginService = loginService;
            _session = provider.GetSession();
        }

        [HttpGet("friendly-hello", Name = "friendly-hello")]
        public String Friendly()
        {
            return "Friendly Hello :) you need to do Auth!";
        }

        [HttpPost("mock-hash", Name = "mock-hash")]
        public String MockSaltAndHash([FromBody] String NoHash)
        {
            var hasher = new PasswordHasher<object>();
            String hash = hasher.HashPassword(null, NoHash);
            return hash;
        }


        [HttpPost("do-login", Name = "do-login")]
        public async Task<GeneralResponse<LoginResponse>> Login([FromBody] PasswordUser passwordUser)
        {
            return await _loginService.DoLogin(passwordUser);
        }

        [Authorize]
        [HttpGet("authorized-tester")]
        public String TestSecuring()
        {            
            return $"You're Authenticated ! ! ! {_session.Email} {_session.Role}";
        }

    }
}