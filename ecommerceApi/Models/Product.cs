using System;
namespace ecommerceApi.Models;

public class Product : BaseEntity
{
	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

    public string Photo { get; set; } = string.Empty;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int Stock { get; set; }

	public bool Delivery { get; set; }

    public Guid UserGid { get; set; }

    public User? User { get; set; }

    public Product(string title, string description, string photo, int price, int quantity, int stock, bool delivery, Guid userGid)
        : base()
    {
        Title = title;
        Description = description;
        Photo = photo;
        Price = price;
        Quantity = quantity;
        Stock = stock;
        Delivery = delivery;
        UserGid = userGid;
    }

    public Product()
    {
    }
}
	

