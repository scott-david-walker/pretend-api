namespace Pretender;

public class RequestInput
{
    public string Path { get; set; }
    public string? ContentType { get; set; }
    public string Method { get; set; }
    public Dictionary<string, string> Headers { get; set; } = [];
    public Dictionary<string, string> QueryParams { get; set; } = [];
    public Stream Body { get; set; }
}