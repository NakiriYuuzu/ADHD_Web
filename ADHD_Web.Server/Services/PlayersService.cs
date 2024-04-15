using System.Data.Common;
using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class PlayersService(AdhdDbContext context, ILogger<PlayersService> logger) : IPlayersService
{
    public async Task<Result<IEnumerable<Player>, DataError>> GetAllPlayers()
    {
        try
        {
            var playerQuery = context.Players.AsNoTracking();
            var players = await playerQuery
                .Include(p => p.LevelRecords)
                .OrderBy(p => p.Id)
                // .Skip((pageNumber - 1) * pageSize)
                // .Take(pageSize)
                .ToListAsync();

            return new Result<IEnumerable<Player>, DataError>.Success(players);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while retrieving all players.");
            return new Result<IEnumerable<Player>, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving all players.");
            return new Result<IEnumerable<Player>, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                e.Message));
        }
    }

    public async Task<Result<Player, DataError>> GetPlayerById(Guid id)
    {
        try
        {
            var player = await context.Players.FindAsync(id);

            if (player == null)
            {
                return new Result<Player, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.NotFound,
                    message: $"Player with id {id} was not found."
                ));
            }

            return new Result<Player, DataError>.Success(player);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while retrieving a player.");
            return new Result<Player, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving a player.");
            return new Result<Player, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }

    public async Task<Result<Player, DataError>> AddPlayer(Player player)
    {
        try
        {
            player.CreatedAt = DateUtils.ToTaipeiTime();

            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();
            return new Result<Player, DataError>.Success(player);
        }
        catch (DbException dbEx)
        {
            logger.LogError(dbEx, "Database error occurred while adding a player.");
            return new Result<Player, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while adding a player.");
            return new Result<Player, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }
}