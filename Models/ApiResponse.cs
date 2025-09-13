using System;

namespace EmployeeManagementApi.Models
{
  /// <summary>
  /// 统一的 API 响应格式
  /// </summary>
  /// <typeparam name="T">数据类型</typeparam>
  public class ApiResponse<T>
  {
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; }
    public object? Errors { get; set; }

    public ApiResponse()
    {
      Timestamp = DateTime.UtcNow;
    }

    // 静态方法：成功响应
    public static ApiResponse<T> SuccessResult(T data, string message = "Success")
    {
      return new ApiResponse<T>
      {
        Success = true,
        Message = message,
        Data = data,
        StatusCode = 200
      };
    }

    // 静态方法：失败响应
    public static ApiResponse<T> ErrorResult(string message, int statusCode = 400, object? errors = null)
    {
      return new ApiResponse<T>
      {
        Success = false,
        Message = message,
        Data = default(T),
        StatusCode = statusCode,
        Errors = errors
      };
    }

    // 静态方法：不带数据的成功响应
    public static ApiResponse<object?> SuccessResult(string message = "Success")
    {
      return new ApiResponse<object?>
      {
        Success = true,
        Message = message,
        Data = null,
        StatusCode = 200
      };
    }
  }
}
