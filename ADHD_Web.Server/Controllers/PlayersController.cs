using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Domains.validators;
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
                Result<IEnumerable<Player>, DataError>.Success success => Ok(success.Data),
                Result<IEnumerable<Player>, DataError>.Failure failure => failure.Error switch
                {
                    { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, new {message = error.Message}),
                    { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, new {message = error.Message}),

                    _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected error occurred."})
                },
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected result type was received."})
            };
        });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await playersService.GetPlayerById(id);
        return result switch
        {
            Result<Player, DataError>.Success success => Ok(success.Data),
            Result<Player, DataError>.Failure failure => failure.Error switch
            {
                { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected error occurred."})
            },
            _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected result type was received."})
        };
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Player player)
    {
        var ageValidator = PlayerValidator.VerifyAge(player.Age);
        var genderValidator = PlayerValidator.VerifyGender(player.Gender);
        if (!ageValidator) return BadRequest(new {message = "Age is invalid."});
        if (!genderValidator) return BadRequest(new { message = "Please check your gender in " + string.Join(", ", PlayerValidator.GenderList) });

        var newPlayer = new Player
        {
            Name = player.Name,
            Age = player.Age,
            Gender = player.Gender
        };
        
        var result = await playersService.AddPlayer(newPlayer);
        return result switch
        {
            Result<Player, DataError>.Success success => CreatedAtAction(nameof(Get), new { id = success.Data.Id }, success.Data),
            Result<Player, DataError>.Failure failure => failure.Error switch
            {
                { DataErrorType: DataErrorType.UnknownError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected error occurred."})
            },
            _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = "An unexpected result type was received."})
        };
    }
}