using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success(
            string message)
        {
            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    message));
        }

        protected IActionResult Success<T>(
            T data,
            string message)
        {
            return Ok(
                ApiResponse<T>.SuccessResponse(
                    data,
                    message));
        }

        protected IActionResult Created<T>(
            T data,
            string message)
        {
            return StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<T>.SuccessResponse(
                    data,
                    message,
                    StatusCodes.Status201Created));
        }
    }
}
