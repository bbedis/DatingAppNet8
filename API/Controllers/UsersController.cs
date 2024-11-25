using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

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
