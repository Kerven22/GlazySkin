using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers;
[ApiController]
[Route("glazyskin/categories")]
public class CategoryController(IServiceManager _serviceManager):ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories()
    {
            var categories = _serviceManager.CategoryService.GetAllCategories(trackChanges: false);
            return Ok(categories);

    }

    [HttpGet("{id:guid}", Name = "CategoryById")]
    [ProducesResponseType(410)]
    
    public IActionResult GetCategoryById(Guid id)
    {
        var category = _serviceManager.CategoryService.GetCategoryById(id, trackChanges: false);
        return Ok(category); 
    }

    [HttpPost]
    public IActionResult CrateCategory([FromBody]CategoryForCreationDto categoryDto)
    {
        if (categoryDto is null)
            return BadRequest("CategoryForCreationDto object is null");
        var category =  _serviceManager.CategoryService.CreateCategory(categoryDto);
        return CreatedAtRoute("CategoryById", new { id = category.Id }, category); 

    }
}