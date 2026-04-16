using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.ModelDomain;

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

        public async Task<GeneralResponse<String>> DoLogin(PasswordUser passwordUser)
        {
            // find password_hash by username
            string? pass_hash = await _context.Users.Where(ad => ad.UserName == passwordUser.User)
                .Select(ad => ad.PasswordHash)
                .FirstOrDefaultAsync();
            if (pass_hash == null) return new GeneralResponse<String> { Error = "Error in Credentials", Status = 500, Message = "User or Passwor is incorrect" };
            // validate hash
            var hasher = new PasswordHasher<object>();
            var verifier = hasher.VerifyHashedPassword(null, pass_hash, passwordUser.Password);
            if (verifier == PasswordVerificationResult.Failed) return new GeneralResponse<String> { Error = "Error in Credentials", Status = 500, Message = "User or Passwor is incorrect" };
            // issue JWT
            var token = _issuer.GenerateToken(passwordUser.User);
            return new GeneralResponse<String> { Status = 200, Message = "Sucess!!!", Dataset = token };
        }

    }
}
