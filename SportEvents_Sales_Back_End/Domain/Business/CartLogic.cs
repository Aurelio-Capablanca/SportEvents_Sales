using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class CartLogic
    {

        private readonly AppDbContext _context;

        public CartLogic(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<GeneralResponse<CartResponse>> SaveCartAsync(CartRequest request, GlobalSession session)
        {

            /*
                DB -> {
                INSERT TO Order, (if ID doesn't exists)
                INSERT TO Ticket_Orders (add always, since this concats with Order)
                UPDATE TICKET (total_tickets =-1) 
                }
            */
            if (request.IdOrder != null) 
            {
                var CurrentClient = await _context.Clients
                       .Where(cl => cl.Email == session.Email)
                       .Select(cl => cl.Id)
                       .FirstAsync();
                var order = new OrderEntity
                {
                    Status = true,
                    DateStart = DateTime.Now,
                    IdClient = CurrentClient,
                };
            }
            return null;
        }

        public async Task<GeneralResponse<List<CartResponse>>> ReadCartAsync(String EmailClient)
        {
            return null;
        }


        //+ ReadClientByArgument(T id) -> Task<GeneralResponse<DomainModel>>

        public async Task<GeneralResponse<String>> DeleteFromCartAsync(CartRequest request)
        {
            /*
                DB -> {
                Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                Update Order (sum all remnan Tickets) where idOrder = idCart
                }
            */
            return null;
        }

        public async Task<GeneralResponse<String>> DeleteAllCartAsync(int IdCart)
        {
            /*
                DB -> {
                Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                And id_ticket = @n ...
                }
            */
            return null;
        }



    }
}
