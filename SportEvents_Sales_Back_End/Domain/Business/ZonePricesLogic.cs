using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class ZonePricesLogic(AppDbContext DbContext)
    {
        private readonly AppDbContext _context = DbContext;


        public async Task<GeneralResponse<String>> SaveZoneAsync(ZonePricesEntity entity)
        {
            try
            {
                if (entity.IdZone == 0)
                {
                    await _context.AddAsync(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Zone Created!"

                    };
                }
                else
                {
                    _context.Update(entity);
                    _context.SaveChanges();
                    return new GeneralResponse<string>
                    {
                        Status = 200,
                        Message = "Zone Updated!"

                    };
                }
            }
            catch
            {
                return new GeneralResponse<string>
                {
                    Message = "error at creating client!",
                    Status = 500
                };
            }            
        }


        public async Task<GeneralResponse<List<ZonePricesEntity>>> ShowAllEntities() {
            try
            {
                var zone = await _context.ZonePrices.ToListAsync();                
                return new GeneralResponse<List<ZonePricesEntity>>
                {
                    Dataset = zone,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<ZonePricesEntity>>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }


        public async Task<GeneralResponse<ZonePricesEntity>> ShowOneEntity(int IdEntity)
        {
            try
            {
                var zone = await _context.ZonePrices.Where(zn => zn.IdZone == IdEntity)
                    .FirstAsync();
                return new GeneralResponse<ZonePricesEntity>
                {
                    Dataset = zone,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<ZonePricesEntity>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }



        public async Task<GeneralResponse<String>> DeleteZone(int IdEntity)
        {
            try
            {
                var zone = await _context.ZonePrices.Where(zn => zn.IdZone == IdEntity)
                    .FirstAsync();
                _context.ZonePrices.Remove(zone);
                _context.SaveChanges();
                return new GeneralResponse<String>
                {
                    Dataset = "zone deleted",
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
