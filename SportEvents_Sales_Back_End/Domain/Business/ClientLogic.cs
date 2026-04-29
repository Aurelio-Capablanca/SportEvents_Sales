using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain;
using System.Data;
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
                    var CurrentClient = await _context.Clients
                        .Where(cl => cl.Email == client.Email)
                        .Select(cl => cl.Pass)
                        .FirstAsync();
                    registrator = new ClientEntity
                    {
                        Id = client.Idclient ?? 0,
                        Name = client.Name,
                        LastName = client.LastName,
                        Email = client.Email,
                        Pass = CurrentClient,
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

        public async Task<GeneralResponse<ClientRegistry>> OwnDetails(String email)
        {
            try
            {
                var client = await _context.Clients.Where(cl => cl.Email == email).FirstAsync();
                var registry = new ClientRegistry
                {
                    Idclient = client.Id,
                    Name = client.Name,
                    LastName = client.LastName,
                    Email = client.Email
                };
                return new GeneralResponse<ClientRegistry>
                {
                    Dataset = registry,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<ClientRegistry>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }


        public async Task<GeneralResponse<String>> DeleteAccount(String Email)
        {
            try
            {
                var CurrentClient = await _context
                    .Clients
                    .Where(cl => cl.Email == Email)
                    .FirstAsync();
                _context.Clients.Remove(CurrentClient);
                return new GeneralResponse<String>
                {
                    Message = "Succesfull Delete",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<String>
                {                    
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }            
        }

    }
}
