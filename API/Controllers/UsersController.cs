using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }
    
    [HttpGet("{userName}")]
    public async Task<ActionResult<MemberDto>> GetUserAsync(string userName)
    {
        var user = await userRepository.GetMemberAsync(userName);
        
        return Ok(user);
    }
}
