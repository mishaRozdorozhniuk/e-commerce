using System;
using System.Security.Cryptography;
using ecommerceApi.Controllers.Requests;
using ecommerceApi.DAL;
using ecommerceApi.DAL.Repositories;
using ecommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ecommerceApi.Controllers;

[Route("api/products")]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _products;

    private readonly EcommerceContext _dbContext;

    public ProductsController(IRepository<Product> products, EcommerceContext dbContext)
    {
        _products = products;
        _dbContext = dbContext;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    public async Task<IActionResult> GetProducts()
        => Ok(await _products.GetListAsync());

    [HttpGet("{gid}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductById(Guid gid)
        => Ok(await _products.GetByGidAsync(gid));

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Product))]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {
        var product = new Product(request.Title, request.Description, request.Photo, request.Price, request.Quantity, request.Stock, request.Delivery, request.UserGid);

        var createdProduct = await _products.AddAsync(product);

        return Ok(createdProduct);
    }

    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid gid)
    {
        var product = await _products.GetByGidAsync(gid);

        if (product is null) return NoContent( );

        return Ok(await _products.DeleteAsync(product));
    }

    [HttpPatch("{gid}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500, Type = typeof(string))]
    public async Task<IActionResult> UpdateProduct(Guid gid, [FromBody] JsonPatchDocument<Product> patchDTO)
    {
        try
        {
            var product = await _products.GetByGidAsync(gid);

            if (product == null)
            {
                return NotFound();
            }

            patchDTO.ApplyTo(product, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _products.UpdateAsync(product);

            return Ok(product); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}"); 
        }
    }
}

