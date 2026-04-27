using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("client-api")]
    public class ClientController : Controller
    {
        private readonly ClientLogic _clientLogic;

        public ClientController(ClientLogic clientLogic) 
        {
            this._clientLogic = clientLogic;
        }

        //[Authorize]
        [HttpPost("save-client", Name = "save-client")]
        public async Task<GeneralResponse<String>> CreateClient([FromBody] ClientRegistry clientEntity) 
        {
            return await this._clientLogic.SaveClientAsync(clientEntity);
        }



    }
}