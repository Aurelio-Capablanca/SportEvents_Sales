using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.ModelDomain;
using SportEvents_Sales_Back_End.Security;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("auth")]
    public class LoginController : Controller
    {

        private readonly LoginSessions _loginService;

        public LoginController(LoginSessions loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<GeneralResponse<String>> Login([FromBody] PasswordUser passwordUser)
        {
            return await _loginService.DoLogin(passwordUser);
        }

    }
}