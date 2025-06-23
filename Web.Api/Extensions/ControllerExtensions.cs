using Application.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Data);

            if (result.Data is null)
                return controller.NotFound(result.Error);

            return controller.BadRequest(result.Error);
        }

        public static IActionResult HandleResultWithAuthFallback<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Data);

            return result.Data is null
                ? controller.Unauthorized(result.Error)
                : controller.BadRequest(result.Error);
        }

        public static IActionResult HandleDeleteResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            return controller.BadRequest(result.Error);
        }

        public static async Task<IActionResult?> ValidateRequest<T>(
            this ControllerBase controller,
            T request,
            IValidator<T> validator
        )
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Title = "Validation failed",
                    Detail = "One or more validation errors occurred.",
                    Instance = controller.HttpContext.Request.Path
                };

                return new BadRequestObjectResult(problemDetails);
            }

            return null;
        }
    }
}
