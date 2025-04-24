using Domain.UseCases.CategoryUseCases.GetCategoryUseCase;
using Microsoft.AspNetCore.Mvc;

namespace GlazySkin.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController(IGetCategoryUseCase _getCategory):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _getCategory.GetCategories(cancellationToken);

            return Ok(categories); 
        }
    } 
}
