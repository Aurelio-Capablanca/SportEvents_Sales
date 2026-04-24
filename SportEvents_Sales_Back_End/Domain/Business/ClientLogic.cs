using Microsoft.AspNetCore.Identity;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class ClientLogic
    {
        private readonly AppDbContext _context;

        public ClientLogic(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse<string>> SaveClientAsync(ClientRegistry client)
        {
            try
            {
                var hasher = new PasswordHasher<object>();
                var registrator = new ClientEntity
                {
                    Name = client.Name,
                    LastName = client.LastName,
                    Email = client.Email,
                    Pass = hasher.HashPassword(null, client.Pass)
                };
                await _context.AddAsync(registrator);
                _context.SaveChanges();
                return new GeneralResponse<string>
                {
                    Status = 200,
                    Message = "Client Created!"

                };
            }
            catch
            {
                return new GeneralResponse<string>
                {
                    Message = "error at creating client!",
                    Status = 500
                };
            }

        }

    }
}
