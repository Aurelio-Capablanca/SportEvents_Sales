using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;
using System.Diagnostics;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class CartLogic
    {

        private readonly AppDbContext _context;

        public CartLogic(AppDbContext dbContext)
        {
            this._context = dbContext;
        }


        //Task<ActionResult<GeneralResponse<CartResponse>>>
        public async Task<GeneralResponse<CartResponse>> SaveCartAsync(CartRequest request, GlobalSession session)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                OrderEntity order = null;
                if (request.IdOrder == null)
                {
                    var CurrentClient = await _context.Clients
                           .Where(cl => cl.Email == session.Email)
                           .Select(cl => cl.Id)
                           .FirstAsync();
                    var totalForOrder = request.Tickets.Sum(tickets => tickets.TotalPrice);
                    order = new OrderEntity
                    {
                        Status = true,
                        DateStart = DateTime.Now,
                        IdClient = CurrentClient,
                        SubTotal = 0,
                        TotalPrice = totalForOrder,
                    };
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();
                }
                // INSERT TO Ticket_Orders (add always, since this concats with Order)
                foreach (var ticket in request.Tickets)
                {                    
                    var ticketOrder = new TicketOrderEntity()
                    {
                        IdOrder = order?.Id ?? request.IdOrder.Value,
                        IdTicket = ticket.Id,
                    };
                    await _context.TicketOrder.AddAsync(ticketOrder);
                    await _context.SaveChangesAsync();
                }
                //UPDATE TICKET (total_tickets =-1)
                foreach (var ticket in request.Tickets)
                {
                    await _context.Tickets.Where(ticket => ticket.IDTicket == ticket.IDTicket)
                        .ExecuteUpdateAsync(set => set.SetProperty(up => up.AvailableTotal, (ticket.AvailableSeats - 1)));
                    await _context.SaveChangesAsync();
                }                
                await transaction.CommitAsync();
                return new GeneralResponse<CartResponse>
                {
                    Status = 200,
                    Message = "Added To Cart!"

                };
                //return Ok(response);               
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GeneralResponse<CartResponse>
                {
                    Message = "error at Adding to Cart",
                    Error = ex.Message,
                    Status = 500
                };
                //return BadRequest(err);

            }


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
