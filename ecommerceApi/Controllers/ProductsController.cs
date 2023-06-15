using System;
using ecommerceApi.Controllers.Requests;
using ecommerceApi.DAL.Repositories;
using ecommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ecommerceApi.Controllers;

[Route("api/products")]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _products;

    public ProductsController(IRepository<Product> products)
    {
        _products = products;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
        => Ok(await _products.GetListAsync());

    [HttpGet("{gid}")]
    public async Task<IActionResult> GetProductById(Guid gid)
        => Ok(await _products.GetByGidAsync(gid));

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {
        var product = new Product(request.Title, request.Description, request.Photo, request.Price, request.Quantity, request.Stock, request.Delivery, request.UserGid);

        var createdProduct = await _products.AddAsync(product);

        return Ok(createdProduct);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid gid)
    {
        var product = await _products.GetByGidAsync(gid);

        if (product is null) return NoContent();

        return Ok(await _products.DeleteAsync(product));
    }
}

