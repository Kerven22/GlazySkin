using System.Text.Json;
using GlazySkin.ActionFilter;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceContracts;
using Shared;
using Shared.RequestFeatures;

namespace Presentation.Controllers;
[ApiController]
[Route("categories/{categoryId:guid}")]
public class ProductController(IServiceManager _serviceManager):ControllerBase
{
    [HttpGet("products")]
    public async Task<IActionResult> GetProductsAsync(Guid categoryId, [FromQuery] ProductParameters productParameters)
    {
        var pagedList = await _serviceManager.ProductService.GetProductsAsync(categoryId, productParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedList.metaData));
        Log.Information("GetProductAsync"); 
        return Ok(pagedList.productDtos); 
    }

    [HttpGet("products/{productId:guid}", Name = "GetProduct")]
    public async Task<IActionResult> GetProductAsync(Guid categoryId, Guid productId)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(categoryId, productId, trackChanges: false);
        return Ok(product); 
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttrebute))]
    public async Task<IActionResult> CreateProductAsync(Guid categoryId, [FromBody] ProductForCreationDto productForCreationDto)
    {
        if (productForCreationDto is null)
            return BadRequest("product object is null!"); 
        var productDto = await _serviceManager.ProductService.CreateProductAsync(categoryId, productForCreationDto);

        return CreatedAtRoute("GetProduct", new { categoryId=categoryId, productId = productDto.Id }, productDto); 
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(Guid categoryId, Guid productId)
    {
        await _serviceManager.ProductService.DeleteProductAsync(categoryId, productId, trackChanges:false);
        return NoContent(); 
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttrebute))]
    public async Task<IActionResult> UpdateProductAsync(Guid categoryId, Guid productId,
        [FromBody] ProductForUpdateDto productForUpdateDto)
    {

        if (productForUpdateDto is null)
            return BadRequest("productUpdateObject is null!"); 
        await _serviceManager.ProductService.ProductUpdateAsync(categoryId, productId, productForUpdateDto,catTrackChanges:false, productTrackChanges:true);

        return NoContent();
    }

    [HttpPatch("{productId:guid}")]
    public async Task<IActionResult> PartiallyUpdatePoductAsync(Guid categoryId, Guid productId,
        [FromBody] JsonPatchDocument<ProductForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument Object is null");
        var result = await _serviceManager.ProductService.GetProductForPatchAsync(categoryId, productId,
            categoryTrachChanges: false, productTrackChanges: true);
        patchDocument.ApplyTo(result.productForUpdateDto);
        await _serviceManager.ProductService.SaveChangesForPatchAsync(result.productForUpdateDto, result.product);
        return NoContent(); 
    }
}