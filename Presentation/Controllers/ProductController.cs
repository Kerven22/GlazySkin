using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Presentation.Controllers;
[ApiController]
[Route("categories")]
public class ProductController(IServiceManager _serviceManager):ControllerBase
{
    [HttpGet("{categoryId:guid}/products")]
    public IActionResult GetProducts(Guid categoryId)
    {
        var products = _serviceManager.ProductService.GetProducts(categoryId, trackChanges: false);
        return Ok(products); 
    }

    [HttpGet("{categoryId:guid}/products/{productId:guid}")]
    public IActionResult GetProduct(Guid categoryId, Guid productId)
    {
        var product = _serviceManager.ProductService.GetProduct(categoryId, productId, trackChanges: false);
        return Ok(product); 
    }
}