using ADHD_Web.Server.Domains;
using ADHD_Web.Server.Models;
using ADHD_Web.Server.Services;
using ADHD_Web.Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ADHD_Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        var result = await userService.Login(user);
        return result switch
        {
            Result<User, DataError>.Success success => success.Data.Token != null ? Ok(new {token = success.Data.Token})
                : StatusCode(StatusCodes.Status401Unauthorized, new { message = "Unauthorized" }),
            Result<User, DataError>.Failure failure => failure.Error switch
            {
                {DataErrorType: DataErrorType.DuplicateData} error => StatusCode(
                    StatusCodes.Status409Conflict, new {message = error.Message}),
                {DataErrorType: DataErrorType.NotFound} error => StatusCode(
                    StatusCodes.Status404NotFound, new {message = error.Message}),
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
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var result = await userService.Register(user);
        return result switch
        {
            Result<User, DataError>.Success success => CreatedAtAction(nameof(Post), new { id = success.Data.Id }, new {message = "User created."}),
            Result<User, DataError>.Failure failure => failure.Error switch
            {
                {DataErrorType: DataErrorType.DuplicateData} error => StatusCode(
                    StatusCodes.Status409Conflict, new {message = error.Message}),
                {DataErrorType: DataErrorType.NotFound} error => StatusCode(
                    StatusCodes.Status404NotFound, new {message = error.Message}),
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
    }
}