using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.DTO;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class TicketLogic(AppDbContext dbContext)
    {

        private readonly AppDbContext _context = dbContext;

        public async Task<GeneralResponse<String>> SaveTicketsAsync(TicketWrapperRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (request.IdTicket == 0)
                {
                    //INSERT
                    var ticket = new TicketEntity
                    {
                        HasDiscount = false,
                        Discount = 0,
                        IdGame = request.IdGame
                    };
                    await _context.AddAsync(ticket);
                    await _context.SaveChangesAsync();
                    foreach (TicketPriceRequest price in request.TicketPrices)
                    {
                        var prices = new TicketPriceEntity
                        {
                            IdTicket = ticket.IDTicket,
                            AvailableSeats = price.AvailableSeats,
                            IdPriceZone = price.IdPriceZone,
                            Price = price.Price,
                        };
                        await _context.AddAsync(prices);
                        await _context.SaveChangesAsync();
                    }
                    await transaction.CommitAsync();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Ticket Saved",
                        Dataset = ""
                    };
                }
                else
                {
                    //UPDATE
                    var ticket = new TicketEntity
                    {
                        IDTicket = request.IdTicket,
                        HasDiscount = false,
                        Discount = 0,
                        IdGame = request.IdGame
                    };
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    foreach (TicketPriceRequest price in request.TicketPrices)
                    {
                        var prices = new TicketPriceEntity
                        {
                            IdTicket = ticket.IDTicket,
                            IdTicketPrice = price.IdTicketPrice,
                            AvailableSeats = price.AvailableSeats,
                            IdPriceZone = price.IdPriceZone,
                            Price = price.Price,
                        };
                        _context.Update(prices);
                        await _context.SaveChangesAsync();
                    }
                    await transaction.CommitAsync();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Ticket Updated",
                        Dataset = ""
                    };
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GeneralResponse<String>
                {
                    Message = "error at Saving to Ticket",
                    Error = ex.Message,
                    Status = 500
                };
            }            
        }

        public async Task<GeneralResponse<List<TicketDTO>>> ReadAllTickets()
        {
            try
            {
                //var ticketPrices = 
                var tickets = await _context.Tickets
                    .Select(ticket => new TicketDTO
                    {
                        IdTicket = ticket.IDTicket,                        
                        Prices = _context.TicketPrice
                        .Where(tk => tk.Tickets.IDTicket == ticket.IDTicket)
                        .Select(prices => new PricesDTO
                        {
                            ZonePrice = prices.ZonePrice.ZoneName,
                            Price = prices.Price,
                            AvailableSeats = prices.AvailableSeats,
                            Id = prices.IdPriceZone
                        }).ToList(),
                        Discount = ticket.Discount,
                        Stadium = ticket.Game.Stadium.Name,
                        LocalTeam = ticket.Game.LocalTeam,
                        VisitorTeam = ticket.Game.VisitorTeam,
                        Location = ticket.Game.Stadium.Location,
                        Tournament = ticket.Game.Tournament,                       
                        Date = ticket.Game.TimeGame.ToString("dd/MM/yyyy"),
                        Time = ticket.Game.TimeGame.ToString("h:mm tt")
                    }).ToListAsync();

                return new GeneralResponse<List<TicketDTO>>
                {
                    Message = "Found Data!",
                    Dataset = tickets,
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<TicketDTO>>
                {
                    Message = "Error in Getting data!",
                    Error = ex.Message,
                    Status = 500
                };
            }
        }


        public async Task<GeneralResponse<TicketDTO>> getOneTicketAsync(int IdTicket)
        {
            try
            {
                var tickets = await _context.Tickets
                    .Where(ticket => ticket.IDTicket == IdTicket)
                    .Select(ticket => new TicketDTO
                    {
                        IdTicket = ticket.IDTicket,                       
                        Prices = _context.TicketPrice
                        .Where(tk => tk.Tickets.IDTicket == ticket.IDTicket)
                        .Select(prices => new PricesDTO
                        {
                            ZonePrice = prices.ZonePrice.ZoneName,
                            Price = prices.Price,
                            AvailableSeats = prices.AvailableSeats,
                            Id = prices.IdTicketPrice
                        }).ToList(),
                        Discount = ticket.Discount,
                        Stadium = ticket.Game.Stadium.Name,
                        LocalTeam = ticket.Game.LocalTeam,
                        VisitorTeam = ticket.Game.VisitorTeam,
                        Location = ticket.Game.Stadium.Location,
                        Tournament = ticket.Game.Tournament,
                        Date = ticket.Game.TimeGame.ToString("dd/MM/yyyy"),
                        Time = ticket.Game.TimeGame.ToString("h:mm tt")
                    }).FirstAsync();
                return new GeneralResponse<TicketDTO>
                {
                    Message = "Found Data!",
                    Dataset = tickets,
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<TicketDTO>
                {
                    Message = "Error in Getting data!",
                    Error = ex.Message,
                    Status = 500
                };
            }
        }



    }
}
