using System.Data.Common;
using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class LevelService(AdhdDbContext context, ILogger<PlayersService> logger) : ILevelService
{
    public async Task<Result<IEnumerable<LevelRecord>, DataError>> GetAllLevels()
    {
        try
        {
            var levelRecordQuery = context.LevelRecords.AsNoTracking();
            var levelRecords = await levelRecordQuery
                .Include(lr => lr.Player)
                .OrderBy(lr => lr.Id)
                .ToListAsync();

            return new Result<IEnumerable<LevelRecord>, DataError>.Success(levelRecords);
        }
        catch (DbException e)
        {
            logger.LogError(e, "Database error occurred while retrieving all players.");
            return new Result<IEnumerable<LevelRecord>, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving all players.");
            return new Result<IEnumerable<LevelRecord>, DataError>.Failure(new DataError(DataErrorType.UnknownError,
                e.Message));
        }
    }

    public Task<Result<LevelRecord, DataError>> GetLevelById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<LevelRecord, DataError>> AddLevel(LevelRecord levelRecord)
    {
        throw new NotImplementedException();
    }
}