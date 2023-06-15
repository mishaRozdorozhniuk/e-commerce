using System;
namespace ecommerceApi.Models;

public class User : BaseEntity
{
	public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public List<Product>? Products { get; set; }
}

