using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Utils;

namespace ADHD_Web.Server.Services;

public interface IUserService
{
    Task<Result<User, DataError>> Login(User user);
    Task<Result<User, DataError>> Register(User user);
}