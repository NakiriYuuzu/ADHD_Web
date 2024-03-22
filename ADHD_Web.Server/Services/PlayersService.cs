using System.Data.Common;
using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class PlayersService(AdhdDbContext context, ILogger<PlayersService> logger) : IPlayersService
{
    public async Task<Result<IEnumerable<Players>, DataError>> GetAllPlayers(int pageNumber, int pageSize)
    {
        try
        {
            var players = await context.Players
                .OrderBy(p => p.PlayerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Result<IEnumerable<Players>, DataError>.Success(players);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while retrieving all players.");
            return new Result<IEnumerable<Players>, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving all players.");
            return new Result<IEnumerable<Players>, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                e.Message));
        }
    }

    public async Task<Result<Players, DataError>> GetPlayerById(int id)
    {
        try
        {
            var player = await context.Players.FindAsync(id);

            if (player == null)
            {
                return new Result<Players, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.NotFound,
                    message: $"Player with id {id} was not found."
                ));
            }

            return new Result<Players, DataError>.Success(player);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while retrieving a player.");
            return new Result<Players, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving a player.");
            return new Result<Players, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }

    public async Task<Result<Players, DataError>> AddPlayer(Players player)
    {
        try
        {
            if (await context.Players.AnyAsync(p => p.Name == player.Name))
            {
                return new Result<Players, DataError>.Failure(new DataError(
                    DataErrorType.DuplicateData,
                    $"Player with name {player.Name} already exists."));
            }

            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();
            return new Result<Players, DataError>.Success(player);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while adding a player.");
            return new Result<Players, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while adding a player.");
            return new Result<Players, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }
}