using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{

    [ApiController]
    [Route("zone-prices-api")]
    public class ZonePricesController(ZonePricesLogic zoneLogic, IUserSessionProvider provider) : Controller
    {
        private readonly ZonePricesLogic _zoneLogic = zoneLogic;
        private readonly GlobalSession _globalSession = provider.GetSession();

        [Authorize]
        [HttpPost("save-zone", Name = "save-zone")]
        public async Task<ActionResult> SaveCartAsync([FromBody] ZonePricesEntity request)
        {
            var process = await this._zoneLogic.SaveZoneAsync(request);
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
        [HttpGet("zone-get-one/{IdZone}", Name = "zone-get-one")]
        public async Task<ActionResult> ReadOneTickets(int IdZone)
        {
            var process = await this._zoneLogic.ShowOneEntity(IdZone);
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
        [HttpGet("zone-get-all", Name = "zone-get-all")]
        public async Task<ActionResult> ReadAllTickets()
        {
            var process = await this._zoneLogic.ShowAllEntities();
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
        [HttpGet("zone-delete/{IdZone}", Name = "zone-delete")]
        public async Task<ActionResult> DeleteTicket(int IdZone)
        {
            var process = await this._zoneLogic.DeleteZone(IdZone);
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
