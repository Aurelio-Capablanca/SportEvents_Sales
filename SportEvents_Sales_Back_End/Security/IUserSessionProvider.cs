using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using System.Diagnostics;
using System.Security.Claims;

namespace SportEvents_Sales_Back_End.Security
{
    public interface IUserSessionProvider
    {
        GlobalSession GetSession();
    }

    public class SessionProvider(IHttpContextAccessor accessor) : IUserSessionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = accessor;

        public GlobalSession GetSession()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                Debug.WriteLine("DEBUG: HttpContext or User is NULL");
                return new GlobalSession { Email = "anon", Role = Roles.Anon };
            }
            string role = (user?.FindFirst(ClaimTypes.Role)?.Value ?? "Anon");
            return new GlobalSession
            {
                Email = user?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                Role = Enum.Parse<Roles>(role)
            };
        }
    }
}
