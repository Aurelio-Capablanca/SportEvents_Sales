using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Security;
using System.Diagnostics;

namespace SportEvents_Sales_Back_End.Controllers
{

    [ApiController]
    [Route("ticket-api")]
    public class TicketController(TicketLogic ticketLogic, IUserSessionProvider provider) : Controller
    {

        private readonly TicketLogic _ticketLogic = ticketLogic;
        private readonly GlobalSession _globalSession = provider.GetSession();

        [Authorize]
        [HttpPost("save-ticket", Name = "save-ticket")]
        public async Task<ActionResult> SaveTicketsAsync([FromBody] TicketWrapperRequest request)
        {
            var process = await this._ticketLogic.SaveTicketsAsync(request);
            if (process.Status == 200)
            {
                return Ok(process);
            }
            else
            {
                return BadRequest(process);
            }
        }


        [Authorize]
        [HttpGet("ticket-get-all", Name = "ticket-get-all")]
        public async Task<ActionResult> ReadAllTickets()
        {
            var process = await this._ticketLogic.ReadAllTickets();
            if (process.Status == 200)
            {
                return Ok(process);
            }
            else
            {
                return BadRequest(process);
            }
        }


        [Authorize]
        [HttpGet("ticket-get-one/{IdTicket}", Name = "ticket-get-one")]
        public async Task<ActionResult> ReadOneTickets(int IdTicket)
        {
            var process = await this._ticketLogic.getOneTicketAsync(IdTicket);
            if (process.Status == 200)
            {
                return Ok(process);
            }
            else
            {
                return BadRequest(process);
            }
        }


    }
}
