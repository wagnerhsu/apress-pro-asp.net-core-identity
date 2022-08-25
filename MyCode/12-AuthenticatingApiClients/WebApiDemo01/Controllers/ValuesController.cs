// Copyright (c) xxx, 2022. All rights reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo01.Models;

namespace WebApiDemo01.Controllers;

[ApiController]
[Route("/api/data")]
public class ValuesController : ControllerBase
{
    private ProductDbContext DbContext;

    public ValuesController(ProductDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts() => DbContext.Products;

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]
        ProductBindingTarget target)
    {
        if (ModelState.IsValid)
        {
            Product product = new Product
            {
                Name = target.Name,
                Price = target.Price,
                Category = target.Category
            };
            await DbContext.AddAsync(product);
            await DbContext.SaveChangesAsync();
            return Ok(product);
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public Task DeleteProduct(long id)
    {
        DbContext.Products.Remove(new Product { Id = id });
        return DbContext.SaveChangesAsync();
    }
}
