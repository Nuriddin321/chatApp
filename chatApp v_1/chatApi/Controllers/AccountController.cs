using System.Security.Claims;
using chatApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username)
    {
        var user = new IdentityUser(username);

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok(username);
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile() => 
        Ok($"username is {User.FindFirst(ClaimTypes.Name)?.Value}");


    [HttpGet("groups")]
    public IActionResult GetGroups() => Ok(new List<string>(){
        "Dotnet",
        "Java",
        "Python"
    });

    [HttpGet("groups/{group}")]
    public IActionResult GetGroupMessage(string group, [FromServices]MessageService messageService)
    {
        return Ok(messageService.Messages[group].Select(t => $"{t.Item1}: {t.Item2}").ToList());
    }

}
