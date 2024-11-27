using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(IUserRepository userRepository, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterAsync([FromBody] RegisterDto register)
    {
        // if (await UserExists(register.UserName))
        //     return BadRequest("Username is taken");

        return Ok();
        // using var hmac = new HMACSHA512();
        // var user = new AppUser
        // {
        //     UserName = register.UserName.ToLower(),
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // context.Users.Add(user);
        // await context.SaveChangesAsync();

        // return new UserDto
        // {
        //     UserName = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync(LoginDto login)
    {
        var user = await userRepository.GetUserByUserNameAsync(login.UserName);
        
        var msg = "Invalid user name/password";

        if (user is null) return Unauthorized(msg);

        using var hmax = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmax.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized(msg);
        }

        return new UserDto
        {
            UserName = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    // private async Task<bool> UserExists(string userName)
    // {
    //     return await context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
    // }
}
