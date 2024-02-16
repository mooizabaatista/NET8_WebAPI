namespace MbStore.Application.DTOs;

public class ResponseAuthDto
{
    public object? ResultObject { get; set; }
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}
