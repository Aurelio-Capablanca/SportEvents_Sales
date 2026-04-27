using Microsoft.AspNetCore.Identity;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain;
using System.Diagnostics;

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
            //do validation before sending it to DB
            try
            {
                var hasher = new PasswordHasher<object>();
                ClientEntity registrator;
                if (client.Idclient != null)
                {                    
                    registrator = new ClientEntity
                    {
                        Id = client.Idclient ?? 0,
                        Name = client.Name,
                        LastName = client.LastName,
                        Email = client.Email,                        
                    };
                    _context.Clients.Update(registrator);
                    await _context.SaveChangesAsync();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Client Updated!"

                    };
                }
                else 
                {
                    registrator = new ClientEntity
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
