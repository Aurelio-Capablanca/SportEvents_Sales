using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Model.Entities;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{
    public class UserAdminLogic(AppDbContext DbContext)
    {
        private readonly AppDbContext _context = DbContext;

        public async Task<GeneralResponse<String>> SaveAdminAsync(UserEntity entity)
        {
            try
            {
                if (entity.Id == 0)
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

        public async Task<GeneralResponse<List<UserEntity>>> ShowAllEntities()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return new GeneralResponse<List<UserEntity>>
                {
                    Dataset = users,
                    Message = "OK",
                    Status = 200,

                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<UserEntity>>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500

                };
            }
        }


        public async Task<GeneralResponse<UserEntity>> ShowOneEntity(int IdEntity)
        {
            try
            {
                var user = await _context.Users.Where(zn => zn.Id == IdEntity)
                    .FirstAsync();
                return new GeneralResponse<UserEntity>
                {
                    Dataset = user,
                    Message = "OK",
                    Status = 200,
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<UserEntity>
                {
                    Dataset = null,
                    Message = $"Error {ex.Message}",
                    Status = 500
                };
            }
        }


        public async Task<GeneralResponse<String>> DeleteUser(int IdEntity)
        {
            try
            {
                var user = await _context.Users.Where(zn => zn.Id == IdEntity)
                    .FirstAsync();
                _context.Users.Remove(user);
                _context.SaveChanges();
                return new GeneralResponse<String>
                {
                    Dataset = "User deleted",
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
