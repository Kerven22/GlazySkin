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

    [HttpPost]
    public IActionResult CrateCategory(string name)
    {
       var category =  _serviceManager.CategoryService.CreateCategory(name);
       return Ok(category); 
    }
}