public class ApiFailResponse
{
    public bool Success { get; set; }
    public string Error { get; set; } = "";

    public ApiFailResponse(string error)
    {
        Success = false;
        Error = error;
    }
}