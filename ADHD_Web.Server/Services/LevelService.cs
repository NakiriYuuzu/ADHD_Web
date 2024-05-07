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

    public async Task<Result<LevelRecord, DataError>> GetLevelById(Guid id)
    {
        try
        {
            var levelRecord = await context.LevelRecords.FindAsync(id);

            if (levelRecord == null)
            {
                return new Result<LevelRecord, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.NotFound,
                    message: $"Level with id {id} was not found."
                ));
            }

            return new Result<LevelRecord, DataError>.Success(levelRecord);
        }
        catch (DbException e)
        {
            logger.LogError(e, "Database error occurred while retrieving a player.");
            return new Result<LevelRecord, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while retrieving a player.");
            return new Result<LevelRecord, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }

    public async Task<Result<LevelRecord, DataError>> AddLevel(LevelRecord levelRecord)
    {
        try
        {
            if (levelRecord.LevelNumber > 3) 
            {
                return new Result<LevelRecord, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.UnknownError,
                    message: "Level number must start from 0 until 3."
                ));
            }
            
            // check current player level records if level number is duplicate
            var levelRecordQuery = context.LevelRecords.AsNoTracking();
            var levelRecords = await levelRecordQuery
                .Where(lr => lr.PlayerId == levelRecord.PlayerId)
                .Where(lr => lr.LevelNumber == levelRecord.LevelNumber)
                .ToListAsync();
            
            if (levelRecords.Count > 0)
            {
                return new Result<LevelRecord, DataError>.Failure(new DataError(
                    dataErrorType: DataErrorType.DuplicateData,
                    message: $"Level number {levelRecord.LevelNumber} already exists for player."
                ));
            }
            
            levelRecord.CreatedAt = DateTime.Now;
            
            await context.LevelRecords.AddAsync(levelRecord);
            await context.SaveChangesAsync();
            return new Result<LevelRecord, DataError>.Success(levelRecord);
        }
        catch (DbException e)
        {
            logger.LogError(e, "Database error occurred while adding a player.");
            return new Result<LevelRecord, DataError>.Failure(new DataError(DataErrorType.DatabaseError,
                "Database error occurred."));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unknown error occurred while adding a player.");
            return new Result<LevelRecord, DataError>.Failure(new DataError(DataErrorType.UnknownError, e.Message));
        }
    }
}