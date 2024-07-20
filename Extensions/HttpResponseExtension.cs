using System.Net;
using api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Extensions;

public static class HttpResponseExtension
{
    public static ResponseDto<T> SendSuccess<T>(this ControllerBase controller, T data,
        string message = "Success", HttpStatusCode code = HttpStatusCode.OK)
    {
        return new ResponseDto<T>
        {
            Success = true,
            Code = (int)code,
            Message = message,
            Data = data
        };
    }
    public static ResponseDto<T?> SendFailure<T>(this ControllerBase controller, T data,
        string message = "Failure", HttpStatusCode code = HttpStatusCode.BadRequest)
    {
        return new ResponseDto<T?>
        {
            Success = false,
            Code = (int)code,
            Message = message,
        };
    }
}