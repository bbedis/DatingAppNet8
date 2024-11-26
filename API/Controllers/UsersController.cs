using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        
        if (user is null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }
}
