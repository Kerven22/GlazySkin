using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [ProducesResponseType(410)]
    public IActionResult GetCategoryById(Guid id)
    {
        var category = _serviceManager.CategoryService.GetCategoryById(id, trackChanges: false);
        return Ok(category); 
    }

    [HttpPost]
    public IActionResult CrateCategory([FromBody]CategoryDto categoryDto)
    {
        try
        {
            var category =  _serviceManager.CategoryService.CreateCategory(categoryDto);
            return Ok(category); 
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}