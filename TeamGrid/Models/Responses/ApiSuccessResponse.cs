public class ApiSuccessResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }

    public ApiSuccessResponse(T data, string message)
    {
        Success = true;
        Message = message;
        Data = data;
    }
}