using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{

    [ApiController]
    [Route("user-api")]
    public class UserAdminController(UserAdminLogic userLogic, IUserSessionProvider provider) : Controller
    {
        private readonly UserAdminLogic _userLogic = userLogic;
        private readonly GlobalSession _globalSession = provider.GetSession();

        [Authorize]
        [HttpPost("save-user", Name = "save-user")]
        public async Task<ActionResult> SaveuserAsync([FromBody] UserEntity request)
        {
            var process = await this._userLogic.SaveAdminAsync(request);
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
        [HttpGet("user-get-one/{IdUser}", Name = "user-get-one")]
        public async Task<ActionResult> ReadOneUser(int IdUser)
        {
            var process = await this._userLogic.ShowOneEntity(IdUser);
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
        [HttpGet("user-get-all", Name = "user-get-all")]
        public async Task<ActionResult> ReadAllAdmin()
        {
            var process = await this._userLogic.ShowAllEntities();
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
        [HttpGet("user-delete/{IdUser}", Name = "user-delete")]
        public async Task<ActionResult> DeleteUser(int IdUser)
        {
            var process = await this._userLogic.DeleteUser(IdUser);
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
