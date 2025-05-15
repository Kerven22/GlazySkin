using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GlazySkin.ActionFilter;

public class ValidationFilterAttrebute:IActionFilter
{
    private readonly ILogger<ValidationFilterAttrebute> _logger;
    public ValidationFilterAttrebute(ILogger<ValidationFilterAttrebute> logger)
    {
        _logger = logger;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogError("Validation filter before execution!");
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, Action: {action}");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState); 
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Validation filter after execution!");
    }
}