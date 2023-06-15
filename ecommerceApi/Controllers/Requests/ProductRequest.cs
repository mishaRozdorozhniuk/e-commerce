using System;
namespace ecommerceApi.Controllers.Requests;

public class ProductRequest
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Photo { get; set; } = string.Empty;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int Stock { get; set; }

    public bool Delivery { get; set; }

    public Guid UserGid { get; set; }
}