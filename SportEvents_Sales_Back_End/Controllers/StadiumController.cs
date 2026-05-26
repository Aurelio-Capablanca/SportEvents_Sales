using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("stadium-api")]
    public class StadiumController (StadiumLogic stadiumLogic, IUserSessionProvider provider) : Controller
    {
        private readonly StadiumLogic _stadiumLogic = stadiumLogic;
        private readonly GlobalSession _globalSession = provider.GetSession();


        [Authorize]
        [HttpPost("save-stadium", Name = "save-stadium")]
        public async Task<ActionResult> SaveStadiumAsync([FromBody] StadiumEntity request)
        {
            var process = await this._stadiumLogic.SaveStadiumAsync(request);
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
        [HttpGet("stadium-get-one/{IdStadium}", Name = "stadium-get-one")]
        public async Task<ActionResult> ReadOneStadium(int IdStadium)
        {
            var process = await this._stadiumLogic.ShowOneEntity(IdStadium);
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
        [HttpGet("stadium-get-all", Name = "stadium-get-all")]
        public async Task<ActionResult> ReadAllStadiums()
        {
            var process = await this._stadiumLogic.ShowAllEntities();
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
        [HttpGet("stadium-delete/{IdStadium}", Name = "stadium-delete")]
        public async Task<ActionResult> DeleteStadium(int IdStadium)
        {
            var process = await this._stadiumLogic.DeleteStadium(IdStadium);
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
