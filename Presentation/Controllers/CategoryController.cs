using GlazySkin.ActionFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers;
[ApiController]
[Route("glazyskin/categories")]
public class CategoryController(IServiceManager _serviceManager) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync(trackChanges: false);
        return Ok(categories);

    }

    [HttpGet("{id:guid}", Name = "CategoryById")]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _serviceManager.CategoryService.GetCategoryByIdAsync(id, trackChanges: false);
        return Ok(category);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttrebute))]
    public async Task<IActionResult> CrateCategory([FromBody] CategoryForCreationDto categoryDto)
    {
        if (categoryDto is null)
            return BadRequest("CategoryForCreationDto object is null");
        var category = await _serviceManager.CategoryService.CreateCategoryAsync(categoryDto);
        return CreatedAtRoute("CategoryById", new { id = category.Id }, category);

    }

    [HttpGet("collection/({ids})", Name = "CategoryCollection")]
    public async Task<IActionResult> GetCategoriesByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var categories = await _serviceManager.CategoryService.GetCategoriesByIdsAsync(ids, trackChanges: false);
        return Ok(categories);
    }

    [HttpPost("collection")]
    [ServiceFilter(typeof(ValidationFilterAttrebute))]
    public async Task<IActionResult> CreateCategoryCollectionAsync(
        [FromBody] IEnumerable<CategoryForCreationDto> categoryForCreationDtos)
    {
        var result = await _serviceManager.CategoryService.CreateCategoriesCollectionAsync(categoryForCreationDtos);
        return CreatedAtRoute("CategoryCollection", new { result.ids }, result.categories);
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
    {
        await _serviceManager.CategoryService.DeleteCategoryAsync(categoryId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> CategoryUpdate(Guid categoryId, [FromBody] CategoryForUpdate categoryForUpdate)
    {
        if (categoryForUpdate is null)
            return BadRequest("Category for update object not be null");
        await _serviceManager.CategoryService.UpdateCategoryAsync(categoryId, categoryForUpdate, trackChanges: true);
        return NoContent();
    }
}