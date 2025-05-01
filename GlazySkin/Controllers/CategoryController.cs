using Domain.UseCases.CategoryUseCases.GetCategoryUseCase;
using GlazySkin.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace GlazySkin.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController(IGetCategoryUseCase _getCategory) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _getCategory.GetCategories(cancellationToken);

            return Ok(categories);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest category, CancellationToken cancellationToken)
        {

        }
    }
}
