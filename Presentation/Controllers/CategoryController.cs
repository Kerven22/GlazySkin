using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ModelBinders;
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

    [HttpGet("collection/({ids})", Name="CategoryCollection")]
    public IActionResult GetCategoriesByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
       var categories =  _serviceManager.CategoryService.GetCategoriesByIds(ids, trackChanges: false);
       return Ok(categories);
    }

    [HttpPost("collection")]
    public IActionResult CreateCategoryCollection(
        [FromBody] IEnumerable<CategoryForCreationDto> categoryForCreationDtos)
    {
        var result = _serviceManager.CategoryService.CreateCategoriesCollection(categoryForCreationDtos);
        return CreatedAtRoute("CategoryCollection", new { result.ids }, result.categories); 
    }

    [HttpDelete("{categoryId:guid}")]
    public IActionResult DeleteCategory(Guid categoryId)
    {
        _serviceManager.CategoryService.DeleteCategory(categoryId, trackChanges:false);
        return NoContent(); 
    }
}