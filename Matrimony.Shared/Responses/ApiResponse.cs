using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Shared.Responses
{
    

    
        public class ApiResponse<T>
        {
            public bool Success { get; set; }

            public int StatusCode { get; set; }

            public string Message { get; set; } = string.Empty;

            public T? Data { get; set; }

            public static ApiResponse<T> SuccessResponse(
                T? data,
                string message = "Success",
                int statusCode = StatusCodes.Status200OK)
            {
                return new ApiResponse<T>
                {
                    Success = true,
                    StatusCode = statusCode,
                    Message = message,
                    Data = data
                };
            }

            public static ApiResponse<T> FailureResponse(
                string message,
                int statusCode)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    StatusCode = statusCode,
                    Message = message
                };
            }
        }
}
