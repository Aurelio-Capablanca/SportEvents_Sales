using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Domain;
using SportEvents_Sales_Back_End.Security;

namespace SportEvents_Sales_Back_End.Controllers
{
    [ApiController]
    [Route("game-api")]
    public class GameController(GameLogic gameLogic, IUserSessionProvider provider) : Controller
    {
        private readonly GameLogic _gameLogic = gameLogic;
        private readonly GlobalSession _globalSession = provider.GetSession();


        [Authorize]
        [HttpPost("save-game", Name = "save-game")]
        public async Task<ActionResult> SaveGameAsync([FromBody] GameEntity request)
        {
            var process = await this._gameLogic.SaveGameAsync(request);
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
        [HttpGet("game-get-one/{IdGame}", Name = "game-get-one")]
        public async Task<ActionResult> ReadOnegame(int IdGame)
        {
            var process = await this._gameLogic.ShowOneEntity(IdGame);
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
        [HttpGet("game-get-all", Name = "game-get-all")]
        public async Task<ActionResult> ReadAllGames()
        {
            var process = await this._gameLogic.ShowAllEntities();
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
        [HttpGet("game-delete/{Idgame}", Name = "game-delete")]
        public async Task<ActionResult> DeleteGame(int Idgame)
        {
            var process = await this._gameLogic.DeleteGame(Idgame);
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
