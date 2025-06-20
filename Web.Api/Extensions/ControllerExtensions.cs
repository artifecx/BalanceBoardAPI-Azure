using Application.Common;
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
    }
}
