using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Services;
using ADHD_Web.Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ADHD_Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LevelsController(ILevelService levelService) : ControllerBase
{
    [HttpGet(Name = "GetLevels")]
    public Task<IActionResult> Get()
    {
        return levelService.GetAllLevels().ContinueWith<IActionResult>(task =>
        {
            var result = task.Result;
            return result switch
            {
                Result<IEnumerable<LevelRecord>, DataError>.Success success => Ok(success.Data),
                Result<IEnumerable<LevelRecord>, DataError>.Failure failure => failure.Error switch
                {
                    { DataErrorType: DataErrorType.UnknownError } error => StatusCode(
                        StatusCodes.Status500InternalServerError, new { message = error.Message }),
                    { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(
                        StatusCodes.Status500InternalServerError, new { message = error.Message }),

                    _ => StatusCode(StatusCodes.Status500InternalServerError,
                        new { message = "An unexpected error occurred." })
                },
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected result type was received." })
            };
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LevelRecord levelRecord)
    {
        var result = await levelService.AddLevel(levelRecord);
        return result switch
        {
            Result<LevelRecord, DataError>.Success success => CreatedAtAction(nameof(Get), new { id = success.Data.Id }, success.Data),
            Result<LevelRecord, DataError>.Failure failure => failure.Error switch
            {
                { DataErrorType: DataErrorType.UnknownError } error => StatusCode(
                    StatusCodes.Status500InternalServerError, new { message = error.Message }),
                { DataErrorType: DataErrorType.DuplicateData } error => StatusCode(
                    StatusCodes.Status400BadRequest, new { message = error.Message }),
                { DataErrorType: DataErrorType.DatabaseError } error => StatusCode(
                    StatusCodes.Status500InternalServerError, new { message = error.Message }),

                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred." })
            },
            _ => StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "An unexpected result type was received." })
        };
    }
}