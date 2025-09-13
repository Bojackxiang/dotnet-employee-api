using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Extensions
{
  /// <summary>
  /// 控制器扩展方法，用于统一 API 响应格式
  /// </summary>
  public static class ControllerExtensions
  {
    /// <summary>
    /// 返回成功响应
    /// </summary>
    public static IActionResult ApiSuccess<T>(this ControllerBase controller, T data, string message = "操作成功")
    {
      var response = ApiResponse<T>.SuccessResult(data, message);
      return controller.Ok(response);
    }

    /// <summary>
    /// 返回成功响应（无数据）
    /// </summary>
    public static IActionResult ApiSuccess(this ControllerBase controller, string message = "操作成功")
    {
      var response = ApiResponse<object?>.SuccessResult(message);
      return controller.Ok(response);
    }

    /// <summary>
    /// 返回错误响应
    /// </summary>
    public static IActionResult ApiError(this ControllerBase controller, string message, int statusCode = 400, object? errors = null)
    {
      var response = ApiResponse<object?>.ErrorResult(message, statusCode, errors);

      return statusCode switch
      {
        400 => controller.BadRequest(response),
        401 => controller.Unauthorized(response),
        403 => controller.Forbid(),
        404 => controller.NotFound(response),
        500 => controller.StatusCode(500, response),
        _ => controller.StatusCode(statusCode, response)
      };
    }

    /// <summary>
    /// 返回验证错误响应
    /// </summary>
    public static IActionResult ApiValidationError(this ControllerBase controller, object errors, string message = "验证失败")
    {
      var response = ApiResponse<object?>.ErrorResult(message, 400, errors);
      return controller.BadRequest(response);
    }
  }
}
