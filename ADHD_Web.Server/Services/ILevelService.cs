using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;

namespace ADHD_Web.Server.Services;

public interface ILevelService
{
    Task<Result<IEnumerable<LevelRecord>, DataError>> GetAllLevels();
    Task<Result<LevelRecord, DataError>> GetLevelById(Guid id);
    Task<Result<LevelRecord, DataError>> AddLevel(LevelRecord levelRecord);
}