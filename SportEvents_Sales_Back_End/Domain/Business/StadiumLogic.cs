using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class StadiumLogic(AppDbContext DbContext)
    {
        private readonly AppDbContext _context = DbContext;


        public async Task<GeneralResponse<String>> SaveStadiumAsync(StadiumEntity entity)
        {
            try
            {
                if (entity.IdStadium == 0)
                {
                    await _context.AddAsync(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Stadium Created!"

                    };
                }
                else
                {
                    _context.Update(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Stadium Updated!"

                    };
                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>
                {
                    Message = "error at creating Stadium!",
                    Dataset = $"Error : {ex.Message}",
                    Status = 500
                };
            }
        }

        public async Task<GeneralResponse<List<StadiumEntity>>> ShowAllEntities()
        {
            try
            {
                var stadiums = await _context.Stadiums.ToListAsync();
                return new GeneralResponse<List<StadiumEntity>>
                {
                    Dataset = stadiums,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<StadiumEntity>>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }


        public async Task<GeneralResponse<StadiumEntity>> ShowOneEntity(int IdEntity)
        {
            try
            {
                var stadium = await _context.Stadiums.Where(zn => zn.IdStadium == IdEntity)
                    .FirstAsync();
                return new GeneralResponse<StadiumEntity>
                {
                    Dataset = stadium,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<StadiumEntity>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }

        public async Task<GeneralResponse<String>> DeleteStadium(int IdEntity)
        {
            try
            {
                var stadium = await _context.Stadiums.Where(zn => zn.IdStadium == IdEntity)
                    .FirstAsync();
                _context.Stadiums.Remove(stadium);
                _context.SaveChanges();
                return new GeneralResponse<String>
                {
                    Dataset = "Stadium deleted",
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
