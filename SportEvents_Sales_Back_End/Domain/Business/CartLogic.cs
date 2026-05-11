using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.DTO;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                    await _context.Tickets.Where(tck => tck.IDTicket == ticket.Id)
                        .ExecuteUpdateAsync(set => set.SetProperty(up => up.AvailableTotal, (ticket.AvailableSeats - 1)));
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return new GeneralResponse<CartResponse>
                {
                    Status = 200,
                    Message = "Added To Cart!"

                };
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
            }
        }

        public async Task<GeneralResponse<CartResponse>> ReadCartAsync(String EmailClient)
        {
            try
            {

                var tickets = await _context.TicketOrder
                    .Where(ot => ot.Order.Client.Email == EmailClient)
                    .Select(ot => new TicketDTO
                    {
                        Id = ot.Tickets.IDTicket,
                        AvailableSeats = ot.Tickets.AvailableTotal,
                        Discount = ot.Tickets.Discount,
                        ZonePrice = ot.Tickets.ZonePrice.ZoneName,
                        Price = ot.Tickets.ZonePrice.Price,
                        Stadium = ot.Tickets.Game.Stadium.Name,
                        LocalTeam = ot.Tickets.Game.LocalTeam,
                        VisitorTeam = ot.Tickets.Game.VisitorTeam,
                        Location = ot.Tickets.Game.Stadium.Location,
                        SolePrice = ot.Tickets.SolePrice,
                        TotalPrice = ot.Tickets.TotalPrice,
                    })
                    .ToListAsync();
                var order = await _context.Orders.Where(or => or.Client.Email == EmailClient && or.Status).FirstAsync();
                CartResponse cartResponse = new()
                {
                    Status = order.Status,
                    IdClient = order.IdClient,
                    TotalPrice = order.TotalPrice,
                    ListTickets = tickets
                };
                return new GeneralResponse<CartResponse>
                {
                    Status = 200,
                    Dataset = cartResponse
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CartResponse>
                {
                    Message = "Error in Getting data!",
                    Error = ex.Message,
                    Status = 500
                };
            }
        }

        // Do a Read All tickets! 


        //+ ReadClientByArgument(T id) -> Task<GeneralResponse<DomainModel>>

        public async Task<GeneralResponse<String>> DeleteFromCartAsync(int IdTicket, int IdOrder)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                var ticketRemove = await _context.Tickets
                    .Where(ot => ot.IDTicket == IdTicket)
                    .Select(ot => ot.AvailableTotal)
                    .FirstAsync();
                await _context.Tickets.Where(tck => tck.IDTicket == IdTicket)
                   .ExecuteUpdateAsync(set => set.SetProperty(up => up.AvailableTotal, (ticketRemove + 1)));
                await _context.SaveChangesAsync();
                //count all remnat tickets
                var tickets = await _context.TicketOrder
                    .Where(ot => ot.Order.Id == IdOrder && ot.Tickets.IDTicket != IdTicket)
                    .Select(ot => new TicketDTO
                    {
                        Id = ot.Tickets.IDTicket,
                        AvailableSeats = ot.Tickets.AvailableTotal,
                        Discount = ot.Tickets.Discount,
                        ZonePrice = ot.Tickets.ZonePrice.ZoneName,
                        Price = ot.Tickets.ZonePrice.Price,
                        Stadium = ot.Tickets.Game.Stadium.Name,
                        LocalTeam = ot.Tickets.Game.LocalTeam,
                        VisitorTeam = ot.Tickets.Game.VisitorTeam,
                        Location = ot.Tickets.Game.Stadium.Location,
                        SolePrice = ot.Tickets.SolePrice,
                        TotalPrice = ot.Tickets.TotalPrice,
                    })
                    .ToListAsync();
                //Update Order(sum all remnant Tickets) where idOrder = idCart
                var totalForOrder = tickets.Sum(tickets => tickets.TotalPrice);
                await _context.Orders.Where(ord => ord.Id == IdOrder)
                    .ExecuteUpdateAsync(set => set.SetProperty(ord => ord.TotalPrice, totalForOrder));
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse<String>
                {
                    Status = 200,
                    Message = "Deleted From Cart!"

                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GeneralResponse<String>
                {
                    Message = "error at Delete Element from Cart",
                    Error = ex.Message,
                    Dataset = "",
                    Status = 500
                };
            }
        }

        public async Task<GeneralResponse<String>> DeleteAllCartAsync(CartRequest request)
        {
            /*
                DB -> {
                Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                And id_ticket = @n ...
                }
            */
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var tickets in request.Tickets)
                {
                    await _context.Tickets.Where(tck => tck.IDTicket == tickets.Id)
                    .ExecuteUpdateAsync(set => set.SetProperty(up => up.AvailableTotal, (tickets.AvailableSeats + 1)));
                    await _context.SaveChangesAsync();
                }
                await _context.Orders.Where(ord => ord.Id == request.IdOrder)
                   .ExecuteUpdateAsync(set => set
                   .SetProperty(ord => ord.Status, false)
                   .SetProperty(ord => ord.DateEnd, DateTime.Now)
                   );
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse<String>
                {
                    Status = 200,
                    Message = "Deleted From Cart!"

                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GeneralResponse<String>
                {
                    Message = "error at Delete Element from Cart",
                    Error = ex.Message,
                    Dataset = "",
                    Status = 500
                };
            }
        }


        public async Task<GeneralResponse<String>> CheckOutCartAsync(CartRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                await _context.Orders.Where(ord => ord.Id == request.IdOrder)
                   .ExecuteUpdateAsync(set => set
                   .SetProperty(ord => ord.Status, false)
                   .SetProperty(ord => ord.DateEnd, DateTime.Now)
                   );
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return new GeneralResponse<String>
                {
                    Status = 200,
                    Message = "Successful Check Out!"

                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GeneralResponse<String>
                {
                    Message = "error at Check Out from Cart",
                    Error = ex.Message,
                    Dataset = "",
                    Status = 500
                };
            }
        }

    }
}
