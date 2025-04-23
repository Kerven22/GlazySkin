using Domain.UseCases.CategoryUseCases.GetCategoryUseCase;
using Microsoft.AspNetCore.Mvc;

namespace GlazySkin.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController(IGetCategoryUseCase getCategory):ControllerBase
    {
    } 
}
