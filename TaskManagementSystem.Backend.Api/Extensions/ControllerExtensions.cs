using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult GetActionResult(this ControllerBase controller, Result result)
        {
            return result.OperationStatus switch
            {
                OperationStatus.SUCCESS => controller.Ok(result),
                OperationStatus.BAD_REQUEST => controller.BadRequest(result),
                OperationStatus.NOT_FOUND => controller.NotFound(result),
                _ => controller.StatusCode(500, result)
            };
        }

        public static IActionResult GetActionResultWithValue<TValue>(this ControllerBase controller, Result<TValue> result)
        {
            return result.OperationStatus switch
            {
                OperationStatus.SUCCESS => controller.Ok(result),
                OperationStatus.BAD_REQUEST => controller.BadRequest(result),
                OperationStatus.NOT_FOUND => controller.NotFound(result),
                _ => controller.StatusCode(500, result)
            };
        }
    }
}
