using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;

namespace ADHD_Web.Server.Services;

public interface IPlayersService
{
    Task<Result<IEnumerable<Player>, DataError>> GetAllPlayers();
    Task<Result<Player, DataError>> GetPlayerById(Guid id);
    Task<Result<Player, DataError>> AddPlayer(Player player);
}