using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("client-api")]
    public class ClientController : Controller
    {
        private readonly ClientLogic _clientLogic;
        private readonly GlobalSession _session;

        public ClientController(ClientLogic clientLogic, IUserSessionProvider provider)
        {
            this._clientLogic = clientLogic;
            this._session = provider.GetSession();
        }

        //[Authorize]
        [HttpPost("save-client", Name = "save-client")]
        public async Task<GeneralResponse<String>> CreateClient([FromBody] ClientRegistry clientEntity)
        {
            //try to do here a rate limit
            return await this._clientLogic.SaveClientAsync(clientEntity);
        }

        [Authorize]
        [HttpGet("get-own-details", Name = "get-own-details")]
        public async Task<GeneralResponse<ClientRegistry>> GetDetails()
        {
            if (!this._session.Role.Equals(Roles.Client))
            {
                return new GeneralResponse<ClientRegistry>
                {
                    Status = 401,
                    Message = "please be reasonable, you can't see it you're not a client, so you don't have a profile",
                };
            }
            return await this._clientLogic.OwnDetails(this._session.Email);
        }


    }
}