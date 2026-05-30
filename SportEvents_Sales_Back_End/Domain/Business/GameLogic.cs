using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class GameLogic(AppDbContext DbContext)
    {
        private readonly AppDbContext _context = DbContext;

        public async Task<GeneralResponse<String>> SaveGameAsync(GameEntity entity)
        {
            try
            {
                if (entity.IdGame == 0)
                {
                    await _context.AddAsync(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Game Created!"

                    };
                }
                else
                {
                    _context.Update(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Game Updated!"

                    };
                }
            }
            catch
            {
                return new GeneralResponse<string>
                {
                    Message = "error at creating Game!",
                    Status = 500
                };
            }
        }


        public async Task<GeneralResponse<List<GameEntity>>> ShowAllEntities()
        {
            try
            {
                var game = await _context.Games.ToListAsync();
                return new GeneralResponse<List<GameEntity>>
                {
                    Dataset = game,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<GameEntity>>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }


        public async Task<GeneralResponse<GameEntity>> ShowOneEntity(int IdEntity)
        {
            try
            {
                var zone = await _context.Games.Where(zn => zn.IdGame == IdEntity)
                    .FirstAsync();
                return new GeneralResponse<GameEntity>
                {
                    Dataset = zone,
                    Message = "OK",
                    Status = 200,
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<GameEntity>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500
                };
            }
        }


        public async Task<GeneralResponse<String>> DeleteGame(int IdEntity)
        {
            try
            {
                var game = await _context.Games.Where(zn => zn.IdGame == IdEntity)
                    .FirstAsync();
                _context.Games.Remove(game);
                _context.SaveChanges();
                return new GeneralResponse<String>
                {
                    Dataset = "Game deleted",
                    Message = "Successful Deletion",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<String>
                {
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }



    }
}
