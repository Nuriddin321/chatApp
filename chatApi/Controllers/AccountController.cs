using System.Security.Claims;
using chatApi.Entities;
using chatApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly MessageService _messageService;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, MessageService messageService = null)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _messageService = messageService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(string username, string password) // fromform or frombody
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is not null)
            return BadRequest("user with this name already exist");

        user = new AppUser()
        {
            UserName = username
        };

        await _userManager.CreateAsync(user, password);

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok(username);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
            return BadRequest("user with this name not found");

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, false);

        if (!result.Succeeded)
            return BadRequest("wrong password");

        return Ok(username);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }


    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile() =>
        Ok($"{User.FindFirst(ClaimTypes.Name)?.Value}");


    [HttpGet("groups")]
    public async Task<IActionResult>? GetGroups()
    {
        var group = await _messageService.GetGroups();
        return Ok(group);
    }

    [HttpPost("key")]
    public async Task<IActionResult> GetKey([FromBody]string key)
    {
        var result = await _messageService.IsContainsKey(key);
 
        return Ok(result);
    }
    

    // [HttpGet("groups/{group}")]
    // public IActionResult GetGroupMessage(string group, [FromServices] MessageService messageService)
    // {
    //     return Ok(messageService.Messages[group].Select(t => $"{t.Item1}: {t.Item2}").ToList());
    // }

}
