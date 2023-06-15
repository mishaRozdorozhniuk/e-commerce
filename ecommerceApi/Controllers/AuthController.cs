using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using ecommerceApi.DAL;
using ecommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace ecommerceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	public readonly IConfiguration _configuration;

    private readonly EcommerceContext _dbContext;

    public AuthController(IConfiguration configuration, EcommerceContext dbContext)
	{
		_configuration = configuration;
		_dbContext = dbContext;
    }

	[HttpPost("register")]
	public ActionResult<User> Register(UserDto request)
	{
        bool userExists = _dbContext.User.Any(u => u.FirstName == request.FirstName);

        if (userExists)
        {
            return BadRequest("User already exist");
        }

        string passwordHash
			= BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = passwordHash
        };

        _dbContext.User.Add(user);
        _dbContext.SaveChanges();

        return Ok(user);
	}

    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request)
    {
        User user = _dbContext.User.FirstOrDefault(u => u.FirstName == request.FirstName);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
		{
			return BadRequest("Wrong Password");
		}

		string token = CreateToken(user);

		return Ok(token);
    }

	private string CreateToken(User user)
	{
		List<Claim> claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value!
			));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
			);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;

	}
}

