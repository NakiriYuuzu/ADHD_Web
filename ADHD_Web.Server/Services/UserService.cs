using System.Data.Common;
using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class UserService(AdhdDbContext context, ILogger<PlayersService> logger) : IUserService
{
    public async Task<Result<User, DataError>> Login(User user)
    {
        try
        {
            var userQuery = context.Users.AsNoTracking();
            var dbUser = await userQuery
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();

            if (dbUser == null)
            {
                return new Result<User, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.NotFound,
                    message: $"User with username {user.Username} was not found."
                ));
            }
            
            if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                return new Result<User, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.NotFound,
                    message: "Password is incorrect."
                ));
            }

            // generate token
            dbUser.Token = Guid.NewGuid().ToString();
            context.Users.Update(dbUser);
            await context.SaveChangesAsync();
            
            return new Result<User, DataError>.Success(dbUser);
        }
        catch (DbException e)
        {
            logger.LogError(e, $"DbException: {e.Message}");
            return new Result<User, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                "Server is connecting to database, please try again later."));
        }
        catch (Exception e)
        {
            logger.LogError(e, $"DbException: {e.Message}");
            return new Result<User, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                "Server is overwhelmed, please try again later."));
        }
    }

    public async Task<Result<User, DataError>> Register(User user)
    {
        try
        {
            var userQuery = context.Users.AsNoTracking();
            if (userQuery.Any()) return new Result<User, DataError>.Failure(new DataError(
                dataErrorType: DataErrorType.DuplicateData,
                message: "User already exists."
            ));
            
            var dbUser = await userQuery
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();
            
            if (dbUser != null) return new Result<User, DataError>.Failure(new DataError(
                dataErrorType: DataErrorType.DuplicateData,
                message: $"User with username {user.Username} already exists."
            ));
            
            // encode password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            user.CreatedAt = DateTime.Now;
            
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new Result<User, DataError>.Success(user);
        }
        catch (DbException e)
        {
            logger.LogError(e, $"DbException: {e.Message}");
            return new Result<User, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                "Server is connecting to database, please try again later."));
        }
        catch (Exception e)
        {
            logger.LogError(e, $"DbException: {e.Message}");
            return new Result<User, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                "Server is overwhelmed, please try again later."));
        }
    }
}