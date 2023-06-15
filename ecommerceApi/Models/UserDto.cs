using System;
namespace ecommerceApi.Models;

public class UserDto
{
    public required string FirstName { get; set; } = string.Empty;

    public required string LastName { get; set; } = string.Empty;

    public required string Password { get; set; } = string.Empty;
}

