using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Security
{
    public class LoginSessions
    {
        private readonly AppDbContext _context;
        private readonly JWTIssuer _issuer;

        public LoginSessions(AppDbContext context, JWTIssuer issuer)
        {
            this._context = context;
            this._issuer = issuer;
        }

        public async Task<GeneralResponse<LoginResponse>> DoLogin(PasswordUser passwordUser)
        {
            // find password_hash by username
            string? pass_hash;
            if (passwordUser.IsAdmin)
            {
                pass_hash = await _context.Users
                .Where(ad => ad.UserName == passwordUser.User)
                .Select(ad => ad.PasswordHash)
                .FirstOrDefaultAsync();
            }
            else
            {
                pass_hash = await _context.Clients
                .Where(ad => ad.Email == passwordUser.User)
                .Select(ad => ad.Pass)
                .FirstOrDefaultAsync();
            }
            if (pass_hash == null) return new GeneralResponse<LoginResponse> { Error = "Error in Credentials", Status = 500, Message = "User or Passwor is incorrect" };
            // validate hash
            var hasher = new PasswordHasher<object>();
            var verifier = hasher.VerifyHashedPassword(null, pass_hash, passwordUser.Password);
            if (verifier == PasswordVerificationResult.Failed)
                return new GeneralResponse<LoginResponse>
                {
                    Error = "Error in Credentials",
                    Status = 500,
                    Message = "User or Passwor is incorrect"
                };
            // issue JWT
            var token = _issuer.GenerateToken(passwordUser);
            LoginResponse response = new() { Token = token, IsAdmin = passwordUser.IsAdmin };
            return new GeneralResponse<LoginResponse> { Status = 200, Message = "Sucess!!!", Dataset = response };
        }

    }
}
