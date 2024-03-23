using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Services;
using ADHD_Web.Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ADHD_Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController(IPlayersService playersService) : ControllerBase
{
    [HttpGet(Name = "GetPlayers")]
    public Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        return playersService.GetAllPlayers(pageNumber, pageSize).ContinueWith<IActionResult>(task =>
        {
            var result = task.Result;
            return result switch
            {
                Result<IEnumerable<Players>, DataError>.Success success => Ok(success.Data),
                Result<IEnumerable<Players>, DataError>.Failure failure => failure.Error switch
                {
                    { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                    { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),

                    _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
                },
                _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected result type was received.")
            };
        });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await playersService.GetPlayerById(id);
        return result switch
        {
            Result<Players, DataError>.Success success => Ok(success.Data),
            Result<Players, DataError>.Failure failure => failure.Error switch
            {
                { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            },
            _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected result type was received.")
        };
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Players player)
    {
        var result = await playersService.AddPlayer(player);
        return result switch
        {
            Result<Players, DataError>.Success success => CreatedAtAction(nameof(Get), new { id = success.Data.PlayerId }, success.Data),
            Result<Players, DataError>.Failure failure => failure.Error switch
            {
                { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            },
            _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected result type was received.")
        };
    }
}