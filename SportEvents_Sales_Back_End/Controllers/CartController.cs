using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{

    [ApiController]
    [Route("cart-api")]
    public class CartController : Controller
    {

        private readonly CartLogic _cartLogic;
        private readonly GlobalSession _session;

        public CartController(CartLogic cartLogic, IUserSessionProvider provider)
        {
            this._cartLogic = cartLogic;
            this._session = provider.GetSession();
        }

        [Authorize]
        [HttpPost("save-cart", Name = "save-cart")]
        public async Task<ActionResult> SaveCartAsync([FromBody] CartRequest request) {
            var process = await this._cartLogic.SaveCartAsync(request, _session);
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
        [HttpGet("get-cart", Name = "get-cart")]
        public async Task<ActionResult> ReadCartAsync() {
            string email = _session.Email;
            var process = await this._cartLogic.ReadCartAsync(email);
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
