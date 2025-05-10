using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers;
[ApiController]
[Route("categories/{categoryId:guid}")]
public class ProductController(IServiceManager _serviceManager):ControllerBase
{
    [HttpGet("products")]
    public IActionResult GetProducts(Guid categoryId)
    {
        var products = _serviceManager.ProductService.GetProducts(categoryId, trackChanges: false);
        return Ok(products); 
    }

    [HttpGet("products/{productId:guid}", Name = "GetProduct")]
    public IActionResult GetProduct(Guid categoryId, Guid productId)
    {
        var product = _serviceManager.ProductService.GetProduct(categoryId, productId, trackChanges: false);
        return Ok(product); 
    }

    [HttpPost]
    public IActionResult CreateProduct(Guid categoryId, [FromBody] ProductForCreationDto productForCreationDto)
    {
        if (productForCreationDto is null)
            return BadRequest("product object is null!"); 
        var productDto = _serviceManager.ProductService.CreateProduct(categoryId, productForCreationDto);

        return CreatedAtRoute("GetProduct", new { categoryId=categoryId, productId = productDto.Id }, productDto); 
    }
}