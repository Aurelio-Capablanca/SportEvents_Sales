using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.DTO;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class TicketLogic
    {

        private readonly AppDbContext _context;

        public TicketLogic(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<GeneralResponse<List<TicketDTO>>> ReadAllTickets()
        {
            try
            {
                //var ticketPrices = 
                var tickets = await _context.Tickets
                    .Select(ticket => new TicketDTO
                    {
                        Id = ticket.IDTicket,
                        AvailableSeats = ticket.AvailableTotal,
                        Prices = _context.TicketPrice
                        .Where(tk => tk.Tickets.IDTicket == ticket.IDTicket)
                        .Select(prices => new PricesDTO {
                            ZonePrice = prices.ZonePrice.ZoneName,
                            Price = prices.Price
                        }).ToList(),
                        Discount = ticket.Discount,
                        Stadium = ticket.Game.Stadium.Name,
                        LocalTeam = ticket.Game.LocalTeam,
                        VisitorTeam = ticket.Game.VisitorTeam,
                        Location = ticket.Game.Stadium.Location,
                        Tournament = ticket.Game.Tournament,
                        TotalPrice = ticket.TotalPrice,
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
                        Id = ticket.IDTicket,
                        AvailableSeats = ticket.AvailableTotal,
                        Prices = _context.TicketPrice
                        .Where(tk => tk.Tickets.IDTicket == ticket.IDTicket)
                        .Select(prices => new PricesDTO
                        {
                            ZonePrice = prices.ZonePrice.ZoneName,
                            Price = prices.Price
                        }).ToList(),
                        Discount = ticket.Discount,
                        Stadium = ticket.Game.Stadium.Name,
                        LocalTeam = ticket.Game.LocalTeam,
                        VisitorTeam = ticket.Game.VisitorTeam,
                        Location = ticket.Game.Stadium.Location,
                        Tournament = ticket.Game.Tournament,
                        TotalPrice = ticket.TotalPrice,
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
