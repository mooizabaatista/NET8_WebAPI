namespace MbStore.Application.DTOs;

public class ResponseDto
{
    public object? ResultObject { get; set; }
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}
