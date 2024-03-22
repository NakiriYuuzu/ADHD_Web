using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;

namespace ADHD_Web.Server.Services;

public interface IPlayersService
{
    Task<Result<IEnumerable<Players>, DataError>> GetAllPlayers(int pageNumber, int pageSize);
    Task<Result<Players, DataError>> GetPlayerById(int id);
    Task<Result<Players, DataError>> AddPlayer(Players player);
}