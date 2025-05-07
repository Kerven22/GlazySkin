using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers;
[ApiController]
[Route("glazyskin/categories")]
public class CategoryControllers(IServiceManager _serviceManager):ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _serviceManager.CategoryService.GetAllCategories(trackChanges: false);
            return Ok(categories);
        }
        catch
        {
            return StatusCode(500, "Internal server error!"); 
        }

    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCategoryByName(Guid id)
    {
        var category = _serviceManager.CategoryService.GetCategoryById(id, trackChanges: false);
        return Ok(category); 
    }

    [HttpPost]
    public IActionResult CrateCategory([FromBody]CategoryDto categoryDto)
    {
       var category =  _serviceManager.CategoryService.CreateCategory(categoryDto);
       return Ok(category); 
    }
}